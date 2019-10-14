using TLSharp.Core;

namespace Consolegram
{
    public class TelegramApi : ITelegramApi
    {
        private readonly TelegramClient client;

        private readonly ISessionStore sessionStore;

        public TelegramApi(int apiId, string apiHash, string session)
        {
            sessionStore = new FileSessionStore();
            client = new TelegramClient(apiId, apiHash, sessionStore, session, null);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}