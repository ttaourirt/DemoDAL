using DemoDAL;
using DemoDAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestDemoDAL
{
    [TestClass]
    public class TestEmployee
    {
        ChinookEntities _ChinookEntities;

        EmployeeRepository _EmployeeRepo;

        public TestEmployee()
        {
            _ChinookEntities = new ChinookEntities();
            _EmployeeRepo = new EmployeeRepository(_ChinookEntities);
        }


        /// <summary>
        /// (avec repository) Ajout d'employé en BDD, puis lecture pour vérifier qu'il est bien présent en BDD puis suppression pour ne pas laisser de trace de test en BDD
        /// </summary>
        [TestMethod]
        public void TestAjoutSupprEmployeeAvecRepository()
        {
            Employee empl = new Employee()
            {
                FirstName = "Miriam",
                LastName = "LEFEVRE",
                Country = "France",
                Title = "Gestionnaire"
            };

            _EmployeeRepo.Add(empl);
            _ChinookEntities.SaveChanges();

            //On mémorise l'ID
            int id = empl.EmployeeId;

            // Pour s'assurer que l'employé a bien été ajouté en BDD, on réinstancie le contexte pour faire une lecture en BDD
            _ChinookEntities = new ChinookEntities();
            _EmployeeRepo = new EmployeeRepository(_ChinookEntities);

            Employee employee = _EmployeeRepo.GetEmployee(id);

            //On test pour savoir si on récupère bien un employé et si son nom est bien LEFEVRE            
            // On utilise la méthode Assert, le TU n'est pas validé si la condition n'est pas remplie
            Assert.IsTrue(employee?.LastName == "LEFEVRE");

            _ChinookEntities = new ChinookEntities();
            _EmployeeRepo = new EmployeeRepository(_ChinookEntities);

            //On supprime l'entité en BDD pour revenir dans l'état initial
            _EmployeeRepo.Delete(empl);

            _ChinookEntities.SaveChanges();

            _ChinookEntities = new ChinookEntities();
            _EmployeeRepo = new EmployeeRepository(_ChinookEntities);

            Employee employeeDeleted = _EmployeeRepo.GetEmployee(id);

            Assert.IsTrue(employeeDeleted == null);
        }


        /// <summary>
        /// (sans repository) Ajout d'employé en BDD, puis lecture pour vérifier qu'il est bien présent en BDD puis suppression pour ne pas laisser de trace de test en BDD
        /// </summary>
        [TestMethod]
        public void TestAjoutSupprEmployeeSansRepository()
        {
            Employee empl = new Employee()
            {
                FirstName = "Miriam",
                LastName = "LEFEVRE",
                Country = "France",
                Title = "Gestionnaire"
            };

            _ChinookEntities.Employee.Add(empl);
            _ChinookEntities.SaveChanges();

            //On mémorise l'ID
            int id = empl.EmployeeId;

            // Pour s'assurer que l'employé a bien été ajouté en BDD, on réinstancie le contexte pour faire une lecture en BDD
            _ChinookEntities = new ChinookEntities();

            Employee employee = _ChinookEntities.Employee.FirstOrDefault(x => x.EmployeeId == id);

            //On test pour savoir si on récupère bien un employé et si son nom est bien LEFEVRE            
            // On utilise la méthode Assert, le TU n'est pas validé si la condition n'est pas remplie
            Assert.IsTrue(employee?.LastName == "LEFEVRE");

            _ChinookEntities = new ChinookEntities();

            //On supprime l'entité en BDD pour revenir dans l'état initial
            _ChinookEntities.Employee.Remove(empl);

            _ChinookEntities.SaveChanges();

            _ChinookEntities = new ChinookEntities();

            Employee employeeDeleted = _ChinookEntities.Employee.FirstOrDefault(x => x.EmployeeId == id);

            Assert.IsTrue(employeeDeleted == null);
        }
    }
}
