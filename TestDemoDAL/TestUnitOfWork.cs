using DemoDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDemoDAL
{
    [TestClass]
    public class TestUnitOfWork
    {
        UnitOfWork _UnitOfWork = new UnitOfWork();

        [TestMethod]
        public void TestAddInvoicesLineAndCommitJeanBon()
        {
            using (var trans = _UnitOfWork.BeginTransaction())
            {
                var customer = _UnitOfWork.CustomerRepo.GetCustomerByFirstAndLastName("Kai", "Lenny");

                var invoice = _UnitOfWork.InvoiceRepo.GetListInvoicesByIdCustomer(customer.CustomerId).FirstOrDefault();

                var listInvoicelines = new List<InvoiceLine>();

                var listTracks = _UnitOfWork.TrackRepo.GetTrackByComposer("Da Gama/Toni Garrido");

                foreach (var track in listTracks)
                {
                    listInvoicelines.Add(new InvoiceLine() { InvoiceId = invoice.InvoiceId, TrackId = track.TrackId, Quantity = 1, UnitPrice = 0.99m });
                }

                _UnitOfWork.InvoiceRepo.AddListInvoicesLine(listInvoicelines);

                invoice.Total = invoice.InvoiceLine.Sum(x => x.Quantity * x.UnitPrice);

                _UnitOfWork.Save();

                trans.Commit();

            }
        }

        /// <summary>
        /// On créé un client, puis on crée une facture et une playlist avec des titres AC/DC, le tout dans une transaction
        /// </summary>
        [TestMethod]
        public void TestCreerCustomerAndInvoiceAndPlaylist()
        {
            using (var trans = _UnitOfWork.BeginTransaction())
            {
                Customer customer = new Customer()
                {
                    Address = "Villejean",
                    Country = "France",
                    Email = "titi@gmail.com",
                    Fax = "blabla",
                    PostalCode = "35000",
                    Phone = "xxx",
                    City = "Rennes",
                    FirstName = "John John",
                    LastName = "Florence 2"
                };

                _UnitOfWork.CustomerRepo.Add(customer);

                Invoice invoice = new Invoice() { BillingState = "Hawaii", CustomerId = customer.CustomerId, InvoiceDate = DateTime.Now, BillingPostalCode = "XX", BillingCountry = "US" };

                List<Track> listTracks = _UnitOfWork.TrackRepo.GetTrackByComposer("AC/DC");

                invoice.InvoiceLine = new List<InvoiceLine>();
                foreach (var track in listTracks)
                {
                    invoice.InvoiceLine.Add(new InvoiceLine() { InvoiceId = invoice.InvoiceId, TrackId = track.TrackId, Quantity = 1, UnitPrice = 0.99m });
                }
                Playlist playlist = new Playlist() { Name = "Par défaut", Track = listTracks };

                invoice.Total = invoice.InvoiceLine.Sum(x => x.Quantity * x.UnitPrice);

                _UnitOfWork.InvoiceRepo.AddInvoice(invoice);

                _UnitOfWork.PlaylistRepo.AddPlaylist(playlist);

                _UnitOfWork.Save();

                trans.Commit();

            }

        }

        /// <summary>
        /// (avec repository) Ajout d'employé en BDD, puis lecture pour vérifier qu'il est bien présent en BDD puis suppression pour ne pas laisser de trace de test en BDD
        /// </summary>
        [TestMethod]
        public void TestAjoutSupprEmployeeAvecRepository()
        {
            _UnitOfWork = new UnitOfWork();
            Employee empl = new Employee()
            {
                FirstName = "Miriam",
                LastName = "LEFEVRE",
                Country = "France",
                Title = "Gestionnaire"
            };

            _UnitOfWork.EmployeeRepo.Add(empl);
            _UnitOfWork.Save();

            //On mémorise l'ID
            int id = empl.EmployeeId;

            // Pour s'assurer que l'employé a bien été ajouté en BDD, on réinstancie le contexte pour faire une lecture en BDD
            _UnitOfWork = new UnitOfWork();

            Employee employee = _UnitOfWork.EmployeeRepo.GetEmployee(id);

            //On test pour savoir si on récupère bien un employé et si son nom est bien LEFEVRE            
            // On utilise la méthode Assert, le TU n'est pas validé si la condition n'est pas remplie
            Assert.IsTrue(employee?.LastName == "LEFEVRE");

            _UnitOfWork = new UnitOfWork();

            //On supprime l'entité en BDD pour revenir dans l'état initial
            _UnitOfWork.EmployeeRepo.Delete(empl);

            _UnitOfWork.Save();

            _UnitOfWork = new UnitOfWork();

            Employee employeeDeleted = _UnitOfWork.EmployeeRepo.GetEmployee(id);

            Assert.IsTrue(employeeDeleted == null);
        }
    }
}
