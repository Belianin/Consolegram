using Db.Logging;

namespace Consolegram
{
    public static class ConsolegramClientFactory
    {
        public static ConsolegramClient New(int apiId, string apiHash)
        {
            var log = new ConsoleLog();
            var api = new TLSharpTelegramApi(apiId, apiHash, log);
        
            return new ConsolegramClient(api, log);
        }
    }
}