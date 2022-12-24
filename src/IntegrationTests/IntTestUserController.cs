//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using ComponentAccessToDB;

namespace IntegrationTests
{
    [TestFixture(Author = "alax", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class IntTestUserController
    {
        [Test]
        public void TestAddCompany()
        {
            var user = new User("Inlucker", "qwerty", "Arseny", "Pronin");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            rep.AddCompany("qoollo", 1994);

            var res1 = CompanyRep.GetAll().Last();
            Assert.That(res1.Title, Is.EqualTo("qoollo"), "AddCompany Title");
            Assert.That(res1.Foundationyear, Is.EqualTo(1994), "AddCompany Foundationyear");

            var tmp = EmployeeRep.GetAll();
            tmp.Sort((x, y) => x.Employeeid.CompareTo(y.Employeeid));
            var res2 = tmp.Last();
            Assert.That(res2.User_, Is.EqualTo("Inlucker"), "AddCompany User_");
            Assert.That(res2.Company, Is.EqualTo(res1.Companyid), "AddCompany Company");
            Assert.That(res2.Department, Is.EqualTo(null), "AddCompany Department");
            Assert.That(res2.Permission_, Is.EqualTo((int)Permissions.Founder), "AddCompany Permission_");

            CompanyRep.Delete(res1);
        }

        [Test]
        public void TestGetWorkplaces()
        {
            var user = new User("Inlucker", "qwerty", "Arseny", "Pronin");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            List<WorkplaceView> res = rep.GetWorkplaces();

            Assert.That(res.Count, Is.EqualTo(1), "GetWorkplaces Count");
            Assert.That(res[0].EmployeeID, Is.EqualTo(1), "GetWorkplaces Employee");
            Assert.That(res[0].Company.Companyid, Is.EqualTo(1), "GetWorkplaces Company");
            Assert.That(res[0].Department, Is.EqualTo(null), "GetWorkplaces Department");
            Assert.That(res[0].Permission_, Is.EqualTo(2), "GetWorkplaces Permission_");
        }

        [Test]
        public void TestGetEmployeeByWorkplace()
        {
            var user = new User("Inlucker", "qwerty", "Arseny", "Pronin");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            Employee res = rep.GetEmployeeByWorkplace(1);

            Assert.That(res.Employeeid, Is.EqualTo(1), "GetEmployeeByWorkplace Employee");
            Assert.That(res.User_, Is.EqualTo("Inlucker"), "GetEmployeeByWorkplace User_");
        }

        [Test]
        public void TestUpdateUser()
        {
            var user = new User("Inlucker", "qwerty", "Arseny", "Pronin");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            rep.UpdateUser("qwe", "Arseny", "Pronin");

            User res = UserRep.GetUserByLogin("Inlucker");

            Assert.That(res.Login, Is.EqualTo("Inlucker"), "UpdateUser Login");
            Assert.That(res.Password_, Is.EqualTo("qwe"), "UpdateUser Password_");
            Assert.That(res.Name_, Is.EqualTo("Arseny"), "UpdateUser Name_");
            Assert.That(res.Surname, Is.EqualTo("Pronin"), "UpdateUser Surname");

            rep.UpdateUser("qwerty", "Arseny", "Pronin");
        }

        [Test]
        public void TestDeleteUser()
        {
            var user = new User("alax", "", "Andrwey", "");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            UserRep.Add(user);

            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            rep.DeleteUser();

            User res = UserRep.GetUserByLogin("alax");

            Assert.That(res, Is.EqualTo(null), "GetUserByLoginNull");
        }
    }
}

