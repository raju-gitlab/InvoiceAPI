using Invoice.Business.Business;
using Invoice.Business.IBusiness;
using Invoice.Model.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Controllers.Master
{
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceBusiness _invoiceBusiness;

        public InvoiceController(IInvoiceBusiness invoiceBusiness)
        {
            this._invoiceBusiness = invoiceBusiness;
        }
        [HttpPost]
        public IActionResult res([FromBody]InvoiceModel invoiceModel)
        {
            BaseModel baseModel = new BaseModel()
            {
                Value = HttpContext.Request.Headers["UserId"]
            };
            var result = this._invoiceBusiness.AddNewInvoiceAndItems(invoiceModel,baseModel);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetInvoice([FromQuery]string InvoiceId)
        {
            var result = this._invoiceBusiness.invoiceItems(InvoiceId);
            return Ok(result);
        }
    }
}
