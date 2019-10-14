using System;

namespace Consolegram
{
    public interface ITelegramApi : IDisposable
    {
        IAuthResult Auth(string phone);
    }
}