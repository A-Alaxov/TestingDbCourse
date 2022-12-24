//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture(Author = "alax", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class IntTestNotAuthController
    {
        [Test]
        public void TestGetUserByLogin()
        {
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository UserRep = new UserRepository(context);

            var rep = new NotAuthController(UserRep);

            User res = rep.GetUserByLogin("Inlucker");

            Assert.That(res.Login, Is.EqualTo("Inlucker"), "GetUserByLogin Login");
            Assert.That(res.Name_, Is.EqualTo("Arseny"), "GetUserByLogin Name");
        }

        [Test]
        public void TestGetUserByLoginNull()
        {
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository UserRep = new UserRepository(context);

            var rep = new NotAuthController(UserRep);

            User res = rep.GetUserByLogin("login");

            Assert.That(res, Is.EqualTo(null), "GetUserByLoginNull");
        }

        [Test]
        public void TestAddUser()
        {
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository UserRep = new UserRepository(context);

            var rep = new NotAuthController(UserRep);

            rep.AddUser("alax", "", "Andrwey", "");

            User res = rep.GetUserByLogin("alax");

            Assert.That(res.Login, Is.EqualTo("alax"), "AddUserLogin");
            Assert.That(res.Name_, Is.EqualTo("Andrwey"), "AddUserName");

            UserRep.Delete(res);
        }
    }
}

