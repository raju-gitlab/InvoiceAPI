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
    public class HomeController : ControllerBase
    {
        private readonly IHomeBusiness homeBusiness;

        public HomeController(IHomeBusiness homeBusiness)
        {
            this.homeBusiness = homeBusiness;
        }

        [HttpGet]
        public ActionResult products()
        {
            var result = this.homeBusiness.Products();
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult CheckCartItemAvailability([FromQuery]string ProductId)
        {
            var result = this.homeBusiness.CheckCartItemAvailability(ProductId);
            if(result == true)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        public ActionResult getProductById([FromQuery]string ProductId)
        {
            var eaders = HttpContext.Request.Headers["userid"];
            var result = this.homeBusiness.getProductById(ProductId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult AddUser([FromBody]UserModel userModel)
        {
            var result = this.homeBusiness.AddUser(userModel);
            if(result == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public ActionResult AddItemsInCart([FromBody]BaseModel cart)
        {
            cart.Value = HttpContext.Request.Headers["UserId"];
            var result = this.homeBusiness.AddItemsInCart(cart);
            return Ok();
        }

        [HttpPost]
        public ActionResult Login([FromBody]UserModel user)
        {
            var result = this.homeBusiness.Login(user);
            if(result != null)
            {
                if(result.password == user.password)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult GetCartItems()
        {
            BaseModel model = new BaseModel { Code = HttpContext.Request.Headers["UserId"] };
            var result = this.homeBusiness.GetCartItems(model);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public ActionResult IncreaseCartItems([FromBody]BaseModel CartItems)
        {
            CartItems.Code = HttpContext.Request.Headers["UserId"];
            var result = this.homeBusiness.IncreaseCartItems(CartItems);
            if(result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public ActionResult DecreaseCartItems([FromBody] BaseModel CartItems)
        {
            CartItems.Code = HttpContext.Request.Headers["UserId"];
            var result = this.homeBusiness.DecreaseCartItems(CartItems);
            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public ActionResult DeleteFromCart([FromQuery] string ProductId)
        {
            BaseModel model = new BaseModel()
            {
                Value = ProductId,
                Code = HttpContext.Request.Headers["UserId"]
            };
            var result = this.homeBusiness.DeleteFromCart(model);
            return Ok();

        }
    }
}
