using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Threading
{
    public abstract class BaseLockingProvider<TokenType> : ILockingProvider<TokenType> where TokenType : BaseLockToken
    {
        protected ThreadData Data { get; set; }

        public BaseLockingProvider()
        {
            Data = new ThreadData();
        }

        protected string GetKeyFromToken(string type, int id)
        {
            return type + "&&&" + id;
        }

        protected TokenType GetToken(string type, int id, int secondsToWait)
        {
            TokenType token = Data.Get<TokenType>(GetKeyFromToken(type, id)) as TokenType;
            if (token == null)
            {
                token = CreateLockToken(type, id);
                Data.Set(GetKeyFromToken(type, id), token);
            }
            return token;
        }

        public virtual void Release(TokenType token)
        {
            token.ConnectedClients--;
            if (Data.Get<TokenType>(GetKeyFromToken(token.Type, token.Id)) != null && token.ConnectedClients == 0)
            {
                Data.Remove(GetKeyFromToken(token.Type, token.Id));
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
