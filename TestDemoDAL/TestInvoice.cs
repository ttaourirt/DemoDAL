using DemoDAL;
using DemoDAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
namespace TestDemoDAL
{
    [TestClass]
    public class TestInvoice
    {
        ChinookEntities _ChinookEntities;
        InvoiceRepository _InvoiceRepo;
        CustomerRepository _CustomerRepo;
        TrackRepository _TrackRepo;
        public TestInvoice()
        {
            _ChinookEntities = new ChinookEntities();
            _InvoiceRepo = new InvoiceRepository(_ChinookEntities);
            _CustomerRepo = new CustomerRepository(_ChinookEntities);
            _TrackRepo = new TrackRepository(_ChinookEntities);
        }

        [TestMethod]
        public void TestDeleteInvoicesLineAndRollBackJeanBon()
        {
            using (var trans = _ChinookEntities.Database.BeginTransaction())
            {
                var customer = _CustomerRepo.GetCustomerByFirstAndLastName("Jean", "Bon");

                var invoice = _InvoiceRepo.GetListInvoicesByIdCustomer(customer.CustomerId).FirstOrDefault();
                
                var listInvoicelines = invoice.InvoiceLine.Where(x => x.Track.Composer == "AC/DC").ToList();

                _InvoiceRepo.RemoveListInvoicesLine(listInvoicelines);

                invoice.Total = invoice.InvoiceLine.Sum(x => x.Quantity * x.UnitPrice);
                
                _ChinookEntities.SaveChanges();

                trans.Rollback();
                
            }
        }

        [TestMethod]
        public void TestAddInvoicesLineAndCommitJeanBon()
        {
            using (var trans = _ChinookEntities.Database.BeginTransaction())
            {
                var customer = _CustomerRepo.GetCustomerByFirstAndLastName("Jean", "Bon");

                var invoice = _InvoiceRepo.GetListInvoicesByIdCustomer(customer.CustomerId).FirstOrDefault();

                var listInvoicelines = new List<InvoiceLine>();

                var listTracks = _TrackRepo.GetTrackByComposer("Da Gama/Toni Garrido");

                foreach (var track in listTracks)
                {
                    listInvoicelines.Add(new InvoiceLine() { InvoiceId = invoice.InvoiceId, TrackId = track.TrackId, Quantity = 1, UnitPrice = 0.99m });
                }

                _InvoiceRepo.AddListInvoicesLine(listInvoicelines);
                
                invoice.Total = invoice.InvoiceLine.Sum(x => x.Quantity * x.UnitPrice);

                _ChinookEntities.SaveChanges();

                trans.Commit();

            }
        }
    }
}
