using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramPhPBot.Core.Handlers;
using TelegramPhPBot.Core.Interfaces;
using TelegramPhPBot.Infrastructure.Managers;
namespace TelegramPhPBot.Core.UseCase
{
    public class HandleMessageUseCase
    {
        public async Task<string> HandleMessage(string message, long telegramId)
        {
            var handler = _handlers.First(h => h.CanHandle(message));

            return handler switch
            {
                INonPersonalizedMessageHandler handlerWithoutId => handlerWithoutId.Handle(message),
                IMessageHandler handlerWithId => await handlerWithId.Handle(message, telegramId),
                _ => "error"
            };
        }

        private readonly List<IBaseMessageHandler> _handlers;

        private readonly UserSessionManager _sessionManager;
        private readonly PHPScriptManager _phpScriptManager;
        private readonly AdminFuncManager _adminFuncManager;
        public HandleMessageUseCase(UserSessionManager sessionManager, PHPScriptManager phpScriptManager, AdminFuncManager adminFuncManager)
        {
            _sessionManager = sessionManager;
            _phpScriptManager = phpScriptManager;
            _adminFuncManager = adminFuncManager;

            _handlers = new List<IBaseMessageHandler>
              {
                new GreetingMessageHandler(),
                new StartCommandHandler(),
                new AuthMessageHandler(_sessionManager),
                new LogoutMessageHandle(_sessionManager),
                new RoleMessageHandler(_sessionManager),
                new AddAppNameHandler(_phpScriptManager,_sessionManager),
                new AddAppBundleHandler(_phpScriptManager,_sessionManager),
                new GenerateSecretHandler(_phpScriptManager,_sessionManager),
                new ServerCreditsHandler(_phpScriptManager, _sessionManager),
                new UploadScriptHandler(_phpScriptManager,_sessionManager),
                new GetScriptsHandler(_adminFuncManager,_sessionManager),
                new GetUserHandler(_adminFuncManager,_sessionManager),
                new ChangeRoleHandler(_adminFuncManager,_sessionManager),
                new DefaultMessageHandler() // Завжди останній у списку
              };
        }
    }
}
