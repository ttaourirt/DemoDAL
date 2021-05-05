using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace DemoDAL.Repository
{
    public class CustomerSearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Company { get; set; }

    }
    public class CustomerRepository
    {
        ChinookEntities _ChinookEntities;

        public CustomerRepository(ChinookEntities chinookEntities)
        {
            _ChinookEntities = chinookEntities;
        }

        public List<Customer> GetCustomersWithCriteria(CustomerSearchCriteria searchCriteria)
        {
            return _ChinookEntities.Customer.Where(x =>
            (searchCriteria.FirstName == null || searchCriteria.FirstName == x.FirstName)
            && (searchCriteria.LastName == null || searchCriteria.LastName == x.LastName)
            && (searchCriteria.Adress == null || searchCriteria.Adress == x.Address)
            && (searchCriteria.Company == null || searchCriteria.Company == x.Company)).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _ChinookEntities.Customer.FirstOrDefault(x => x.CustomerId == id);
        }

        public void Add(Customer entity)
        {
            if (entity.CustomerId == 0)
                entity.CustomerId = _ChinookEntities.Customer.Max(x => x.CustomerId) + 1;

            _ChinookEntities.Customer.Add(entity);
        }

        public void Update(Customer entity)
        {
            _ChinookEntities.Customer.Attach(entity);
            _ChinookEntities.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Customer entity)
        {
            _ChinookEntities.Customer.Remove(entity);
        }

        public ObjectResult<GetCustomer_Result> GetCustomerFromProcedure(int id)
        {
            return _ChinookEntities.GetCustomer(id);           
        }

        public Customer GetCustomerByFirstAndLastName(string firstName, string lastName)
        {
            return _ChinookEntities.Customer.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }

    }
}
