using System;

namespace Consolegram
{
    public interface ITelegramApi : IDisposable
    {
        void Auth(string phone);
    }
}