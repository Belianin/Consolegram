using System;
using System.IO;
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

        public TLSharpTelegramApi(int apiId, string apiHash, ILog log)
        {
            this.apiId = apiId;
            this.apiHash = apiHash;
            this.log = log;
            
            sessionStore = new FileSessionStore(new DirectoryInfo("session/"));
        }

        public IAuthResult Auth(string session)
        {
            log.Info("Establishing TCP connection...");
            client = new TelegramClient(apiId, apiHash, sessionStore, session, null);
            log.Info("Authenticating Consolegram...");
            client.ConnectAsync().Wait();

            if (client.IsUserAuthorized()) 
                return new TLAuthResult();
            
            log.Info("Sending code...");
            var hash = client.SendCodeRequestAsync(session).Result;
            return new TLAuthResult(client, hash, session);
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