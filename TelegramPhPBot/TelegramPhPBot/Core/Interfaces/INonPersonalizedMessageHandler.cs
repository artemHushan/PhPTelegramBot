using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramPhPBot.Core.Interfaces
{
    public interface INonPersonalizedMessageHandler : IBaseMessageHandler
    {
        string Handle(string message);
    }
}
