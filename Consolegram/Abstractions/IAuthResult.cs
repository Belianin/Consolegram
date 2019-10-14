namespace Consolegram.Abstractions
{
    public interface IAuthResult
    {
        bool IsAuthorized { get; }

        void EnterCode(string code);
    }
}