using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabbit.Models.Entities;
using Rabbit.Services.Interfaces;

namespace Rabbit.Publisher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMessagesController : ControllerBase
    {
        private readonly IRabbitMessageService _service;

        public RabbitMessagesController(IRabbitMessageService service)
        {
            _service = service;
        }
        [HttpPost]
        public void AddMensagem(RabbitMessage message)
        {
            _service.SendMessage(message);
        }
    }
}
