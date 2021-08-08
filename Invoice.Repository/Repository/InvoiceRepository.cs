using Invoice.Model.Master;
using Invoice.Repository.IRepository;
using Invoice.Utilities.DbContextModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Repository.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext appDbContext;
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public InvoiceRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public long LongBetween(long maxValue, long minValue)
        {
            return (long)Math.Round(random.NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        public BaseModel InvoiceItems(BaseModel baseModel)
        {
            List<InvoiceItemModel> invoiceItems = new List<InvoiceItemModel>();
            var UserId = this.appDbContext.UserTbl.Where(e => e.UserId.ToString().ToUpper() == baseModel.Value.ToString().ToUpper()).Select(e => e.Id).ToList();
            /*var ProductId = this.appDbContext.Product.Where(e => e.ProductGuid.ToString().ToUpper() == cart.Code.ToString().ToUpper()).Select(e => e.Id);*/
            var CartId = this.appDbContext.Cart.Where(e => e.UserId == UserId.FirstOrDefault()).Select(e => e.Id).ToList();
            var InvoiceId = this.appDbContext.Invoice.ToList().Where(e => e.InvoiceNumber == Convert.ToInt32(baseModel.Code)).Select(e => e.Id).ToList();
            var CartItems = (from a in this.appDbContext.CartItems
                             join
                             b in this.appDbContext.Product on a.ProductId equals b.Id
                             select new InvoiceItemModel { InvoiceId = InvoiceId.FirstOrDefault(), ItemDescription = "abc", Quantity = a.ProductQuantity, Rate = float.Parse(b.ProductPrice), Amount = (a.ProductQuantity * Convert.ToInt32(b.ProductPrice)) }).ToList();


            foreach (var item in CartItems)
            {
                this.appDbContext.InvoiceItem.Add(item);
                this.appDbContext.SaveChanges();
            }
            return baseModel;
        }

        public bool Add(InvoiceModel invoiceModel, BaseModel baseModel)
        {
            var InvoiceNumber = LongBetween(10000000,1);
            invoiceModel.InvoiceNumber = Convert.ToInt32(InvoiceNumber);
            try
            {
                var abc = this.appDbContext.Invoice.Add(invoiceModel);
                var res = this.appDbContext.SaveChanges();
                baseModel.Code = Convert.ToString(InvoiceNumber);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<InvoiceItemsModel> invoiceItems(string InvoiceId)
        {
            try
            {
                var CartId = this.appDbContext.Invoice.Where(e => e.InvoiceNumber == Convert.ToInt32(InvoiceId)).Select(e => e.Id).ToList();
                var query = from a in this.appDbContext.Invoice
                               join
                               b in this.appDbContext.InvoiceItem on a.Id equals b.InvoiceId
                               where b.InvoiceId == CartId.FirstOrDefault()
                               select new InvoiceItemsModel { CartItems = new HomeModel { CurrentQuantity = b.Quantity , ProductName = b.ItemDescription, ProductPrice = b.Rate.ToString()}, InvoiceDetails = new InvoiceModel { 
                                   InvoiceNumber = a.InvoiceNumber, IssuDate = a.IssuDate, ShipAddress = a.ShipAddress, Tax = a.Tax, SubTotal = a.SubTotal, Total = a.Total, ZipCode = a.ZipCode, SupplierName = a.SupplierName, BillAddress = a.BillAddress
                               
                               } };
                return query.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
