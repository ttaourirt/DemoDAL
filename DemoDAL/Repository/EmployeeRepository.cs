using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Repository
{
    public class EmployeeRepository
    {
        ChinookEntities _ChinookEntities;

        public EmployeeRepository(ChinookEntities chinookEntities)
        {
            _ChinookEntities = chinookEntities;
        }

        public void Add(Employee entity)
        {            
            if (entity.EmployeeId == 0)
            {
                //Si il n'y a rien dans la table, la méthode Max plante
                if (_ChinookEntities.Employee.Count() == 0)
                    entity.EmployeeId = 1;
                else
                    entity.EmployeeId = _ChinookEntities.Employee.Max(x => x.EmployeeId) + 1;
            }

            _ChinookEntities.Employee.Add(entity);
        }

        public void Update(Employee entity)
        {
            _ChinookEntities.Employee.Attach(entity);
            _ChinookEntities.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Employee entity)
        {            
            _ChinookEntities.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            _ChinookEntities.Employee.Remove(entity);            
        }

        public Employee GetEmployee(int id)
        {
            return _ChinookEntities.Employee.FirstOrDefault(x => x.EmployeeId == id);
        }
    }
}
