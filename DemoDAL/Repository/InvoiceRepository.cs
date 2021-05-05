using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Repository
{
    public class InvoiceRepository
    {
        ChinookEntities _ChinookEntities;

        public InvoiceRepository(ChinookEntities chinookEntities)
        {
            _ChinookEntities = chinookEntities;
        }

        public List<Invoice> GetListInvoicesByIdCustomer(int idCustomer)
        {
            return _ChinookEntities.Invoice.Where(x => x.CustomerId == idCustomer).ToList();
        }

        public void RemoveListInvoicesLine(List<InvoiceLine> listInvoiceLines)
        {
            _ChinookEntities.InvoiceLine.RemoveRange(listInvoiceLines);
        }

        public void AddListInvoicesLine(List<InvoiceLine> listInvoiceLines)
        {
            _ChinookEntities.InvoiceLine.AddRange(listInvoiceLines);
        }

        public void AddInvoice(Invoice invoice)
        {
            if (invoice.InvoiceId == 0)
            {
                //Si il n'y a rien dans la table, la méthode Max plante
                if (_ChinookEntities.Invoice.Count() == 0)
                    invoice.InvoiceId = 1;
                else
                    invoice.InvoiceId = _ChinookEntities.Invoice.Max(x => x.InvoiceId) + 1;
            }

            _ChinookEntities.Invoice.Add(invoice);
        }
    }
}
