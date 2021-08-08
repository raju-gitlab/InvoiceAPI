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
    public class HomeBusiness : IHomeBusiness
    {
        private readonly IHomeRepository _homeRepository;

        public HomeBusiness(IHomeRepository homeRepository)
        {
            this._homeRepository = homeRepository;
        }
        public List<HomeModel> Products()
        {
            return this._homeRepository.Products();
        }

        public bool CheckCartItemAvailability(string ProductId)
        {
            return this._homeRepository.CheckCartItemAvailability(ProductId);
        }

        public HomeModel getProductById(string ProductId)
        {
            return this._homeRepository.getProductById(ProductId);
        }
        public bool CreateCart(CartModel cart)
        {
            return this._homeRepository.CreateCart(cart);
        }
        public bool AddUser(UserModel user)
        {
            return this._homeRepository.AddUser(user);
        }
        public bool AddItemsInCart(BaseModel cart)
        {
            return this._homeRepository.AddItemsInCart(cart);
        }
        public UserModel Login(UserModel user)
        {
            return this._homeRepository.Login(user);
        }
        public List<CartProductsModel> GetCartItems(BaseModel Id)
        {
            return this._homeRepository.GetCartItems(Id);
        }
        public bool IncreaseCartItems(BaseModel cartItems)
        {
            return this._homeRepository.IncreaseCartItems(cartItems);
        }
        public bool DecreaseCartItems(BaseModel cartItems)
        {
            return this._homeRepository.DecreaseCartItems(cartItems);
        }
        public bool DeleteFromCart(BaseModel baseModel)
        {
            return this._homeRepository.DeleteFromCart(baseModel);
        }
    }
}
