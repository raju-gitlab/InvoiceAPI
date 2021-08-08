using Invoice.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Repository.IRepository
{
    public interface IHomeRepository
    {
        List<HomeModel> Products();

        bool CheckCartItemAvailability(string ProductId);

        HomeModel getProductById(string ProductId);
        bool CreateCart(CartModel cart);
        bool AddUser(UserModel user);
        bool AddItemsInCart(BaseModel cart);
        UserModel Login(UserModel user);
        List<CartProductsModel> GetCartItems(BaseModel Id);
        bool IncreaseCartItems(BaseModel cartItems);
        bool DecreaseCartItems(BaseModel cartItems);
        bool DeleteFromCart(BaseModel baseModel);
    }
}
