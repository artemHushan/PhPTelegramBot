using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramPhPBot.Core.Interfaces
{
    public interface IBaseMessageHandler
    {
        bool CanHandle(string message);
    }
}
