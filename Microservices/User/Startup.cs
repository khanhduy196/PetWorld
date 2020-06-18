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
using User.Microservice.Infrastructure.Extensions;

namespace User
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
            //services.AddDbContext<UserDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("UserConnectionString")));
            //services.AddScoped<DbContext, UserDbContext>();
            services.AddScoped<IBaseCqrs, BaseCqrs>();
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Swagger
            services.SwaggerConfigureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //swagger
            app.SwaggerConfigure();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
