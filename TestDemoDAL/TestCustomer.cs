using DemoDAL;
using DemoDAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDemoDAL
{
    [TestClass]
    public class TestCustomer
    {

        ChinookEntities _ChinookEntities;

        CustomerRepository _CustomerRepo;
        TrackRepository _TrackRepo;
        PlaylistRepository _PlayListRepo;

        public TestCustomer()
        {
            _ChinookEntities = new ChinookEntities();

            _CustomerRepo = new CustomerRepository(_ChinookEntities);
            _TrackRepo = new TrackRepository(_ChinookEntities);
            _PlayListRepo = new PlaylistRepository(_ChinookEntities);
        }
        
        [TestMethod]
        public void TestUpdateCustomer()
        {
            Customer customer = _CustomerRepo.GetCustomerByFirstAndLastName("Jean", "Bon");
            customer.Address = "Beaulieu";
            _ChinookEntities.SaveChanges();

            
        }
        
        /// <summary>
        /// Test d'ajout de client puis annulation de la transaction
        /// </summary>
        [TestMethod]
        public void TestAddCustomer()
        {
            using(var trans = _ChinookEntities.Database.BeginTransaction())
            {
                Customer customer = new Customer()
                {
                    FirstName = "Jean",
                    LastName = "Bon",
                    Company = "Unknow",
                    Address = "5 bis",
                    City = "Gotham",                     
                    Email = "toto@gmail.com",
                    Country = "EU"
                };
                _CustomerRepo.Add(customer);

                _ChinookEntities.SaveChanges();

                trans.Rollback();
            }
        }

        /// <summary>
        /// Test d
        /// </summary>
        [TestMethod]
        public void TestGetWithCriteria()
        {
            List<Customer> customers = _CustomerRepo.GetCustomersWithCriteria(new CustomerSearchCriteria() { Adress = "5 bis" });

            using (var trans = _ChinookEntities.Database.BeginTransaction())
            {
                Customer customer = customers.FirstOrDefault();
                customer.PostalCode = "50461";
                _ChinookEntities.SaveChanges();

                trans.Commit();
            }     
            

        }


        /// <summary>
        /// Test d'appel à une procédure stockée
        /// </summary>
        [TestMethod]
        public void TestGetCustomerFromProcedure()
        {
            var result = _CustomerRepo.GetCustomerFromProcedure(0);           
        }


        /// <summary>
        /// On créé un client, puis on crée une playlist avec des titres AC/DC, le tout dans une transaction
        /// </summary>
        [TestMethod]
        public void TestCreerCustomerAndPlaylist()
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
                FirstName = "Kai",
                LastName = "Lenny"
            };

            using (var trans = _ChinookEntities.Database.BeginTransaction())
            {
                _CustomerRepo.Add(customer);

                List<Track> listTracks = _TrackRepo.GetTrackByComposer("AC/DC");

                Playlist playlist = new Playlist() { Name = "Par défaut", Track = listTracks };

                _PlayListRepo.AddPlaylist(playlist);

                _ChinookEntities.SaveChanges();

                trans.Commit();

            }

        }
        


    }
}
