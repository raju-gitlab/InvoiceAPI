using Invoice.Business.IBusiness;
using Invoice.Model.Master;
using Invoice.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Business.Business
{
    public class InvoiceBusiness : IInvoiceBusiness
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceBusiness(IInvoiceRepository invoiceRepository)
        {
            this._invoiceRepository = invoiceRepository;
        }


        #region Get
        public BaseModel InvoiceItems(BaseModel baseModel)
        {
            return this._invoiceRepository.InvoiceItems(baseModel);
        }
        #endregion
        public bool Add(InvoiceModel invoiceModel,BaseModel baseModel)
        {
            return this._invoiceRepository.Add(invoiceModel, baseModel); 
        }
        public BaseModel AddNewInvoiceAndItems(InvoiceModel invoiceModel, BaseModel baseModel)
        {
            if (this._invoiceRepository.Add(invoiceModel, baseModel))
            {
                return this._invoiceRepository.InvoiceItems(baseModel);
            }
            else
            {
                return null;
            }
        }

        public List<InvoiceItemsModel> invoiceItems(string InvoiceId)
        {
            return this._invoiceRepository.invoiceItems(InvoiceId);
        }
    }
}
