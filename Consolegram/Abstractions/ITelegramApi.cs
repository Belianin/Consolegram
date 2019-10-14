using System;

namespace Consolegram.Abstractions
{
    public interface ITelegramApi : IDisposable
    {
        IAuthResult Auth(string phone);
    }
}