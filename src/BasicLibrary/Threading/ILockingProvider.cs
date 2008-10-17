using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Threading
{
    public interface ILockingProvider<TokenType> where TokenType : ILockToken
    {
        TokenType Lock(string type, int id, int secondsToWait);
        void Release(TokenType token);
    }
}
