using Rabbit.Commands;

namespace User.Microservice.Domain.Commands
{
    public class CreatePetCommand : BaseCommand
    {
        public string Name { get; set; }
    }
}
