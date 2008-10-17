using System;
using System.Collections.Generic;
using System.Text;
using BasicLibrary.Common;

namespace BasicLibrary.Threading
{
    public interface IDataStoreLockingProvider<TokenType> : ILockingProvider<TokenType>
        where TokenType : IDataStoreLockToken
    {
        T GetData<T>(TokenType token) where T : IInitializable, new();
        void SetData(TokenType token, object obj);
        void RemoveData(TokenType token);
    }
}
