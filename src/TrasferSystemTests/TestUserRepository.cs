//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TrasferSystemTests
{
    [TestFixture(Author = "alax", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestUserRepository
    {
        [Test]
        public void TestAdd()
        {
            var User = new User(_login: "Alaxov", _password_: "qwerty", _name_: "Arseny", _surname: "Pronin");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository rep = new UserRepository(context);
            
            rep.Add(User);

            User checkUser1 = rep.GetUserByLogin("Alaxov");

            Assert.IsNotNull(checkUser1, "Users was not added");
            Assert.AreEqual("Alaxov", checkUser1.Login, "Not equal Added User");
            Assert.AreEqual("qwerty", checkUser1.Password_, "Not equal Added User");
            Assert.AreEqual("Arseny", checkUser1.Name_, "Not equal Added User");
            Assert.AreEqual("Pronin", checkUser1.Surname, "Not equal Added User");

            rep.Delete(checkUser1);
        }

        [Test]
        public void TestGetAll()
        {
            var User = new User(_login: "Alaxov", _password_: "qwerty", _name_: "Arseny", _surname: "Pronin");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);
            
            List<User> Users = rep.GetAll();

            Assert.IsNotNull(Users, "Can't find Users");

            User checkUser2 = rep.GetUserByLogin("Alaxov");
            Assert.AreEqual("Alaxov", checkUser2.Login, "Not equal Added User");
            Assert.AreEqual("qwerty", checkUser2.Password_, "Not equal Added User");
            Assert.AreEqual("Arseny", checkUser2.Name_, "Not equal Added User");
            Assert.AreEqual("Pronin", checkUser2.Surname, "Not equal Added User");

            rep.Delete(checkUser2);
        }

        [Test]
        public void TestUpdate()
        {
            var User = new User(_login: "Alaxov", _password_: "qwerty", _name_: "Arseny", _surname: "Pronin");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);

            User newUser = new User(_login: "Alaxov", _password_: "qweasd", _name_: "Arseny", _surname: "Pronin");

            rep.Update(newUser);

            User checkUser2 = rep.GetUserByLogin(newUser.Login);

            Assert.IsNotNull(checkUser2, "cannot find User by id");
            Assert.AreEqual("Alaxov", newUser.Login, "Not equal added User");
            Assert.AreEqual("qweasd", checkUser2.Password_, "Not equal Added User");
            Assert.AreEqual("Arseny", checkUser2.Name_, "Not equal Added User");
            Assert.AreEqual("Pronin", checkUser2.Surname, "Not equal Added User");

            rep.Delete(checkUser2);
        }

        [Test]
        public void TestDelete()
        {
            var User = new User(_login: "Alaxov", _password_: "qwerty", _name_: "Arseny", _surname: "Pronin");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);
            User addedUser = rep.GetAll().Last();

            rep.Delete(addedUser);

            Assert.IsNull(rep.GetUserByLogin(addedUser.Login), "User was not deleted");
        }

        [Test]
        public void TestGetUserByLogin()
        {
            var User = new User(_login: "Alaxov", _password_: "qwerty", _name_: "Arseny", _surname: "Pronin");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);
            User addedUser = rep.GetAll().Last();

            User checkUser1 = rep.GetUserByLogin(addedUser.Login);

            Assert.IsNotNull(checkUser1, "Users1 was not found");
            Assert.AreEqual("Alaxov", checkUser1.Login, "Not equal found User");
            Assert.AreEqual("qwerty", checkUser1.Password_, "Not equal found User");
            Assert.AreEqual("Arseny", checkUser1.Name_, "Not equal found User");
            Assert.AreEqual("Pronin", checkUser1.Surname, "Not equal found User");

            rep.Delete(addedUser);
        }
    }
}
