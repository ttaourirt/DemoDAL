using DemoDAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL
{
    public class UnitOfWork
    {
        ChinookEntities _ChinookEntities = new ChinookEntities();

        public ArtisteRepository ArticleRepo;
        public CustomerRepository CustomerRepo;
        public EmployeeRepository EmployeeRepo;        
        public InvoiceRepository InvoiceRepo;
        public PlaylistRepository PlaylistRepo;
        public TrackRepository TrackRepo;

        public UnitOfWork()
        {
            ArticleRepo = new ArtisteRepository(_ChinookEntities);
            CustomerRepo = new CustomerRepository(_ChinookEntities);
            EmployeeRepo = new EmployeeRepository(_ChinookEntities);
            InvoiceRepo = new InvoiceRepository(_ChinookEntities);
            PlaylistRepo = new PlaylistRepository(_ChinookEntities);
            TrackRepo = new TrackRepository(_ChinookEntities);
        }

        public DbContextTransaction BeginTransaction()
        {
            return _ChinookEntities.Database.BeginTransaction();
        }

        public void Save()
        {
            _ChinookEntities.SaveChanges();
        }

    }
}
