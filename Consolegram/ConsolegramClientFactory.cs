using Db.Logging;

namespace Consolegram
{
    public static class ConsolegramClientFactory
    {
        public static ConsolegramClient New()
        {
            var log = new ConsoleLog();
            
            return new ConsolegramClient(log);
        }
    }
}