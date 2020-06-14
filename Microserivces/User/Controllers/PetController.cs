using System.Threading.Tasks;
using Cqrs;
using Microsoft.AspNetCore.Mvc;

using User.Microservice.Application.Commands.CreatePet;


namespace User.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IBaseCqrs _cqrs;

        public PetController(IBaseCqrs crqs)
        {
            this._cqrs = crqs;
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var command = new CreatedPetCommand
            {
                Name = "Meo Con"
            };
            await this._cqrs.SendCommand(command);

            return Ok();
        }
        
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await this._petRepository.GetAll()) ;
        //}
    }
}