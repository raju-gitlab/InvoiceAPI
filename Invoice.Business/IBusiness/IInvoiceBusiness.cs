using Invoice.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Business.IBusiness
{
    public interface IInvoiceBusiness
    {
        #region Get
        BaseModel InvoiceItems(BaseModel baseModel);
        #endregion

        bool Add(InvoiceModel invoiceModel, BaseModel baseModel);
        BaseModel AddNewInvoiceAndItems(InvoiceModel invoiceModel, BaseModel baseModel);
        List<InvoiceItemsModel> invoiceItems(string InvoiceId);
    }
}
