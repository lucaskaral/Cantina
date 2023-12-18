using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Rabbit.Models.Entities;
namespace Rabbit.Services.Interfaces
{
    public interface IRabbitMessageService
    {
        void SendMessage(RabbitMessage message);
    }
}
