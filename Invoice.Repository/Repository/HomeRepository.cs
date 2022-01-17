using Invoice.Model.Master;
using Invoice.Repository.IRepository;
using Invoice.Utilities.DbContextModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Repository.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDbContext appDbContext;
        public HomeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public List<HomeModel> Products()
        {
            try
            {
                string query = "select * from Product";
                List<HomeModel> products = new List<HomeModel>();
                products = this.appDbContext.Product.FromSqlRaw(query).ToList();
                return products;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public HomeModel getProductById(string pId)
        {
            try
            {
                var result = this.appDbContext.Product.ToList();
                var sec = from p in result where p.ProductGuid.ToString().ToUpper() == pId.ToString().ToUpper() select p;
                return sec.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool CheckCartItemAvailability(string ProductId)
        {
            try
            {
                var productId = this.appDbContext.Product.ToList().Where(e => e.ProductGuid.ToString().ToUpper() == ProductId.ToUpper()).Select(e=> e.Id);
                var result = this.appDbContext.CartItems.ToList();
                var findResult = from p in result where p.ProductId.ToString() == productId.FirstOrDefault().ToString() select p;
                if(findResult.FirstOrDefault() != null)
                {
                    return true; 
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CreateCart(CartModel cart)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool AddItemsInCart(BaseModel cart)
        {
            try
            {
                var UserId = this.appDbContext.UserTbl.Where(e => e.UserId.ToString().ToUpper() == cart.Value.ToString().ToUpper()).Select(e => e.Id);
                var ProductId = this.appDbContext.Product.Where(e => e.ProductGuid.ToString().ToUpper() == cart.Code.ToString().ToUpper()).Select(e => e.Id);
                var CartId = this.appDbContext.Cart.Where(e => e.UserId == UserId.FirstOrDefault()).Select(e => e.Id);
                CartItemModel itemModel = new CartItemModel()
                {
                    CartId = CartId.FirstOrDefault(),
                    ProductId = ProductId.FirstOrDefault(),
                    ProductQuantity = 1
                };
                this.appDbContext.CartItems.Add(itemModel);
                var result = this.appDbContext.SaveChanges();
                
                if(result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool AddUser(UserModel user)
        {
            try
            {
                this.appDbContext.UserTbl.Add(user);
                var Result  = this.appDbContext.SaveChanges();
                if (Result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public UserModel Login(UserModel user)
        {
            try
            {
                var Loginresult = this.appDbContext.UserTbl.ToList().Where(e => e.Email.ToString() == user.Email.ToString());
                if(Loginresult.FirstOrDefault() != null)
                {
                    return Loginresult.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<CartProductsModel> GetCartItems(BaseModel Id)
        {
            try
            {
                var UserId = this.appDbContext.UserTbl.Where(e => e.UserId.ToString().ToUpper() == Id.Code.ToString().ToUpper()).Select(e => e.Id);
                var CartId = this.appDbContext.Cart.Where(e => e.UserId == UserId.FirstOrDefault()).Select(e => e.Id);

                var query = (from a in this.appDbContext.Product
                             join b in this.appDbContext.CartItems on a.Id equals b.ProductId where b.CartId.ToString() == CartId.FirstOrDefault().ToString()

                             select new CartProductsModel { ProductName = a.ProductName, ProductPrice = a.ProductPrice, ProductGuid = a.ProductGuid, ProductQuantity = b.ProductQuantity, ProductImagePath = a.ProductImagePath }).ToList();
                if (query.FirstOrDefault() != null)
                {
                    return query.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool IncreaseCartItems(BaseModel cartItems)
        {
            try
            {
                cartItems.Data = "1";
                var UserId = this.appDbContext.UserTbl.Where(e => e.UserId.ToString().ToUpper() == cartItems.Code.ToString().ToUpper()).Select(e => e.Id);
                var ProductId = this.appDbContext.Product.Where(e => e.ProductGuid.ToString().ToUpper() == cartItems.Value.ToString().ToUpper()).Select(e => e.Id);
                var CartId = this.appDbContext.Cart.Where(e => e.UserId == UserId.FirstOrDefault()).Select(e => e.Id);

                CartItemModel CartItems = new CartItemModel
                {
                    CartId = Convert.ToInt32(CartId.FirstOrDefault().ToString()),
                    ProductId = Convert.ToInt32(ProductId.FirstOrDefault().ToString()),
                    ProductQuantity = Convert.ToInt32(cartItems.Data)
                };
                var query = $"UPDATE CartItems SET ProductQuantity = (ProductQuantity+'{cartItems.Data}') WHERE CartId = (@Id) and ProductId = (@PID)";
                var param1 = new SqlParameter("@Id", CartId.FirstOrDefault());
                var param2 = new SqlParameter("@PID", ProductId.FirstOrDefault());
                object[] abc = { param1, param2 };
                /* this.appDbContext.Entry(CartItems).State = EntityState.Modified;*/
                this.appDbContext.Database.ExecuteSqlRaw(query, abc);
                var result = this.appDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool DecreaseCartItems(BaseModel cartItems)
        {
            try
            {
                cartItems.Data = "1";
                var UserId = this.appDbContext.UserTbl.Where(e => e.UserId.ToString().ToUpper() == cartItems.Code.ToString().ToUpper()).Select(e => e.Id);
                var ProductId = this.appDbContext.Product.Where(e => e.ProductGuid.ToString().ToUpper() == cartItems.Value.ToString().ToUpper()).Select(e => e.Id);
                var CartId = this.appDbContext.Cart.Where(e => e.UserId == UserId.FirstOrDefault()).Select(e => e.Id);

                CartItemModel CartItems = new CartItemModel
                {
                    CartId = Convert.ToInt32(CartId.FirstOrDefault().ToString()),
                    ProductId = Convert.ToInt32(ProductId.FirstOrDefault().ToString()),
                    ProductQuantity = Convert.ToInt32(cartItems.Data)
                };
                var query = $"UPDATE CartItems SET ProductQuantity = (ProductQuantity-'{cartItems.Data}') WHERE CartId = (@Id) and ProductId = (@PID)";
                var param1 = new SqlParameter("@Id", CartId.FirstOrDefault());
                var param2 = new SqlParameter("@PID", ProductId.FirstOrDefault());
                object[] abc = { param1, param2 };
                /* this.appDbContext.Entry(CartItems).State = EntityState.Modified;*/
                this.appDbContext.Database.ExecuteSqlRaw(query, abc);
                var result = this.appDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteFromCart(BaseModel baseModel)
        {
            try
            {
                var UserId = this.appDbContext.UserTbl.Where(e => e.UserId.ToString().ToUpper() == baseModel.Code.ToString().ToUpper()).Select(e => e.Id);
                var ProductId = this.appDbContext.Product.Where(e => e.ProductGuid.ToString().ToUpper() == baseModel.Value.ToString().ToUpper()).Select(e => e.Id);
                var CartId = this.appDbContext.Cart.Where(e => e.UserId == UserId.FirstOrDefault()).Select(e => e.Id);
                var CartItemsId = (from a in this.appDbContext.Product
                                   join b in this.appDbContext.CartItems on a.Id equals b.ProductId
                                   join c in this.appDbContext.Cart on b.CartId equals c.Id
                                   where b.CartId == CartId.FirstOrDefault() 

                                   select new CartItemModel { Id = b.Id,ProductId = a.Id }).ToList();

                var GetId = CartItemsId.Find(e => e.ProductId == ProductId.FirstOrDefault());

            /*    CartItemModel CartItemModel = new CartItemModel()
                {
                    CartId = CartId.FirstOrDefault(),
                    ProductId = ProductId.FirstOrDefault()
                };*/
                this.appDbContext.CartItems.RemoveRange(this.appDbContext.CartItems.Find(GetId.Id));
                this.appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}