using Rabbit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Microservice.Application.Commands.CreatePet
{
    public class CreatedPetCommand : BaseCommand
    {
        public string Name { get; set; }
    }
}
