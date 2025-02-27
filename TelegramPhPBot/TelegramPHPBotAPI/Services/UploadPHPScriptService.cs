using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Tsp;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Text;
using TelegramPHPBotAPI.Models;

namespace TelegramPHPBotAPI.Services
{
    public class UploadPHPScriptService
    {
        public string GeneratePhpScript(CurrentPhpScript currentScript)
        {
            return $@"<?php
                            $appName = '{currentScript.AppName}';
                            $appBundle = '{currentScript.AppBundle}';
                            $secretKey = '{currentScript.Secret}';

                            if($secretKey == $_GET['{currentScript.Secret_Key_Param}']){{
                                echo 'Привет, я приложение ' . $appName . ' моя ссылка на Google Play: https://play.google.com/store/apps/details?id=' . $appBundle;
                            }}
                            ?>";
        }


        public string GetRemoteFilePath(string appName, string remoteDirectory)
        {
            string remoteFileName = $"{appName}_script.php";
            return remoteDirectory.TrimEnd('/') + "/" + remoteFileName;
        }

        public async Task<Response> UploadPhpScript(long telegramId, string remoteDirectory)
        {
            var user = _context.users.FirstOrDefault(u => u.telegram_id == telegramId);
            if (user == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Користувач не знайдений."
                };
            }

            var currentScript = _context.current_php_scripts.FirstOrDefault(c => c.user_id == user.user_id);
            if (currentScript == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Скрипт для цього користувача не знайдений."
                };
            }

            var currentServerCredits = _context.server_credits.FirstOrDefault(c => c.user_id == user.user_id);
            if (currentServerCredits == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Дані сервера для цього користувача не знайдені."
                };
            }

            // Перевірка заповненості полів
            var missingFields = new List<string>();

            if (string.IsNullOrEmpty(currentScript.AppName))
            {
                missingFields.Add("AppName");
            }
            if (string.IsNullOrEmpty(currentScript.AppBundle))
            {
                missingFields.Add("AppBundle");
            }
            if (string.IsNullOrEmpty(currentScript.Secret))
            {
                missingFields.Add("Secret");
            }
            if (string.IsNullOrEmpty(currentScript.Secret_Key_Param))
            {
                missingFields.Add("SecretKeyParam");
            }
            if (string.IsNullOrEmpty(currentServerCredits.host))
            {
                missingFields.Add("Host");
            }
            if (string.IsNullOrEmpty(currentServerCredits.login))
            {
                missingFields.Add("Login");
            }
            if (string.IsNullOrEmpty(currentServerCredits.password))
            {
                missingFields.Add("Password");
            }

            if (missingFields.Any())
            {
                return new Response
                {
                    Success = false,
                    Message = $"Не всі необхідні поля заповнені: {string.Join(", ", missingFields)}"
                };
            }

            string phpScript = GeneratePhpScript(currentScript);
            string remotePath = GetRemoteFilePath(currentScript.AppName, remoteDirectory);
            var uploadPhPScript = new UploadPhPScript
            {
                user_id = user.user_id,
                app_name = currentScript.AppName,
                app_bundle = currentScript.AppBundle,
                secret = currentScript.Secret,
                secret_key_param = currentScript.Secret_Key_Param,
                host = currentServerCredits.host,
                login = currentServerCredits.login,
                password = currentServerCredits.password
            };
            _context.php_scripts.Add(uploadPhPScript);
            _context.SaveChanges();
           return await UploadFileToSftp(phpScript, currentServerCredits, remoteDirectory, remotePath);
            
        }


        public async Task<Response> UploadFileToSftp(string fileContent, ServerCredit server, string remoteDirectory, string fileName)
        {
            try
            {
                // Створення підключення до SFTP сервера
                using (var client = new SftpClient(server.host, server.login, server.password))
                {
                    // Встановлюємо тайм-аут підключення (наприклад, 30 секунд)
                    client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);

                    // Спробуємо підключитися з тайм-аутом
                    var connectTask = Task.Run(() => client.Connect());
                    if (await Task.WhenAny(connectTask, Task.Delay(10000)) == connectTask)
                    {
                        if (client.IsConnected)
                        {
                            Console.WriteLine("Підключено до SFTP сервера.");

                            using (var memoryStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)))
                            {
                                // Завантаження файлу на сервер
                                client.UploadFile(memoryStream, $"{remoteDirectory}/{fileName}");
                                return new Response
                                {
                                    Success = true,
                                    Message = "Файл успішно завантажено"
                                };
                            }
                        }
                        else
                        {
                            return new Response
                            {
                                Success = false,
                                Message = "Не вдалося підключитися до SFTP сервера в межах часу"
                            };
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            Success = false,
                            Message = "Час очікування підключення вичерпано"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Помилка: {ex.Message}"
                };
            }
        }


        private readonly TelegramPHPBotContext _context;

        public UploadPHPScriptService(TelegramPHPBotContext context)
        {
            _context = context;
        }
    }
}
