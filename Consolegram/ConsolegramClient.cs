using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Consolegram.Abstractions;
using Db.Logging.Abstractions;

namespace Consolegram
{
    public class ConsolegramClient
    {
        private readonly ITelegramApi api;

        private readonly ILog log;

        private readonly CancellationTokenSource mainCts;

        public ConsolegramClient(ITelegramApi api, ILog log)
        {
            this.api = api;
            this.log = log;
            mainCts = new CancellationTokenSource();
        }
        
        public void Run()
        {
            Greet();
            LogIn();
            
            foreach (var input in ReadInput(mainCts.Token))
                ParseInput(input);

            Bye();
        }

        private void Stop()
        {
            mainCts.Cancel();
            api.Dispose();
        }

        private void ParseInput(string input)
        {
            Console.WriteLine(input);
        }

        private void LogIn()
        {
            Console.WriteLine("Enter phone:");
            var phone = GetValue(@"^ *\+?\d{11} *$", "Enter valid phone number").Trim();
            var result = api.Auth(phone);
            
            if (result.IsAuthorized)
                return;

            Console.WriteLine("Enter code:");
            var code = GetValue(@".*", "Enter code:");
            result.EnterCode(code);
        }

        private static IEnumerable<string> ReadInput(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
                yield return Console.ReadLine();
        }

        private string GetValue(string regex, string message)
        {
            var cts = new CancellationTokenSource();
            foreach (var input in ReadInput(cts.Token))
            {
                if (Regex.IsMatch(input, regex))
                {
                    cts.Cancel();
                    return input;
                }
                
                Console.WriteLine(message);
            }

            return null;
        }

        private static void Greet()
        {
            var width = Console.WindowWidth;
            var greeting = "Welcome to Consolegram";
            Console.Write(new string(' ', width / 2 - greeting.Length / 2));
            Console.WriteLine(greeting);
        }

        private static void Bye()
        {
            Console.WriteLine("Bye!");
        }
    }
}