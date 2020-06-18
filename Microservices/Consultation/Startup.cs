using Consultation.Application.Messages.CreatedConsultation;
using Consultation.Infrastructure;
using Core.Domain.Repositories;
using Cqrs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rabbit.Bus;
using Rabbit.Infrastructure;

namespace Consultation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            services.AddDbContext<ConsultationDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ConsultationConnectionString")));
            services.AddScoped<DbContext, ConsultationDbContext>();
            services.AddScoped<IBaseCqrs, BaseCqrs>();
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //
            services.AddTransient<CreatedConsultationMessageHandler>();
            // rabbit mq
            services.Configure<RabbitMQConfiguration>(Configuration.GetSection("RabbitMQConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<CreatedConsultationMessage, CreatedConsultationMessageHandler>();
        }
    }
}
