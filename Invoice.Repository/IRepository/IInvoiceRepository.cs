using Invoice.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Repository.IRepository
{
    public interface IInvoiceRepository
    {

        #region Get
        BaseModel InvoiceItems(BaseModel baseModel);

        List<InvoiceItemsModel> invoiceItems(string InvoiceId);
        #endregion

        #region Post
        bool Add(InvoiceModel invoiceModel, BaseModel baseModel);
        #endregion

    }
}
