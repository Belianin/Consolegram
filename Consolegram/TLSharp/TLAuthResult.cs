using System;
using Consolegram.Abstractions;
using TLSharp.Core;

namespace Consolegram.TLSharp
{
    internal class TLAuthResult : IAuthResult
    {
        private readonly TelegramClient client;

        private readonly string hash;

        private readonly string session;
        public bool IsAuthorized => client == null;

        public void EnterCode(string code)
        {
            if (IsAuthorized)
                throw new Exception();
            
            client.MakeAuthAsync(session, hash, code).Wait();
        }

        internal TLAuthResult() {}

        internal TLAuthResult(TelegramClient client, string hash, string session)
        {
            this.client = client;
            this.hash = hash;
            this.session = session;
        }
    }
}