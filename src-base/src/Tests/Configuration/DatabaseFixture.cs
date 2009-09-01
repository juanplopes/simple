using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Conspirarte.Domain;
using Simple;
using Simple.Entities;

namespace Conspirarte.Tests.Configuration
{
    public class UserService : EntityService<User> { }

    public class DatabaseFixture : TransactionedFixture
    {
        [Test]
        public void CanSaveUser()
        {
            var u = new User();
            u.Login = "whatever";
            u.Password = Encoding.UTF8.GetBytes("teste");
            u = u.Save();

            var u2 = User.Do.Load(u.Id);
            Assert.AreEqual(u.Login, u2.Login);
            Assert.AreEqual(u.Password, u2.Password);
        }
    }
}
