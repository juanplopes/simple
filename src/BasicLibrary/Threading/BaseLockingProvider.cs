using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Threading
{
    public abstract class BaseLockingProvider<TokenType> : ILockingProvider<TokenType> where TokenType : BaseLockToken
    {
        protected ThreadData<BaseLockingProvider<TokenType>> Data { get; set; }

        public BaseLockingProvider()
        {
            Data = new ThreadData<BaseLockingProvider<TokenType>>();
        }

        protected TokenType GetToken(string type, int id, int secondsToWait)
        {
            TokenType token = Data[type, id] as TokenType;
            if (token == null)
            {
                token = CreateLockToken(type, id);
                Data[type, id] = token;
            }
            return token;
        }

        public virtual void Release(TokenType token)
        {
            token.ConnectedClients--;
            if (Data[token.Type, token.Id] != null && token.ConnectedClients==0)
            {
                Data[token.Type, token.Id] = null;
            }
        }

        public TokenType Lock(string type, int id, int secondsToWait)
        {
            TokenType token = GetToken(type, id, secondsToWait);
            EnsureLock(token, secondsToWait);
            token.ConnectedClients++;
            return token;
        }

        protected abstract void EnsureLock(TokenType token, int secondsToWait);
        protected abstract TokenType CreateLockToken(string type, int id);
    }
}
