using System;
using Db.Logging.Abstractions;
using TeleSharp.TL;
using TLSharp.Core;

namespace Consolegram
{
    public class TLSharpTelegramApi : ITelegramApi
    {
        private TelegramClient client;

        private readonly ISessionStore sessionStore;

        private readonly int apiId;

        private readonly string apiHash;

        private readonly ILog log;

        private TLUser user;

        public TLSharpTelegramApi(int apiId, string apiHash, ILog log)
        {
            this.apiId = apiId;
            this.apiHash = apiHash;
            this.log = log;
            sessionStore = new FileSessionStore();
        }

        public void Auth(string session)
        {
            log.Info("Establishing TCP connection...");
            client = new TelegramClient(apiId, apiHash, sessionStore, session, null);
            log.Info("Authenticating Consolegram...");
            client.ConnectAsync().Wait();
            if (!client.IsUserAuthorized())
            {
                log.Info("Sending code...");
                var hash = client.SendCodeRequestAsync(session).Result;
                var code = GetCode();
                log.Info($"Received code {code}.");
                log.Info("Authenticating user...");
                user = client.MakeAuthAsync(session, hash, code).Result;
            }
            else
            {
                log.Info("User already authorized.");
                user = sessionStore.Load(session).TLUser;
            }
            
            log.Info("Done.");
        }

        private string GetCode()
        {
            Console.WriteLine("Enter code:");
            return Console.ReadLine();
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}