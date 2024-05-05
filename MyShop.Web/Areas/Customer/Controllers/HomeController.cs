using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MyShop.DataAccess.Implementation;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
using MyShop.Utilities;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public HomeController( IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {



            var products = _unitofwork.Product.GetAll();
            return View(products);
        }
        [HttpGet]


        public IActionResult Details(int ProductId)
        {


            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = _unitofwork.Product.GetFirstorDefault(v => v.Id == ProductId, Includeword: "Category"),
                Count = 1,

            };
            return View(shoppingCart);
        }
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]



        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;
            ShoppingCart obj = _unitofwork.ShoppingCart.GetFirstorDefault(
                u=>u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId
                );
            if (obj == null)
            {
                _unitofwork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _unitofwork.ShoppingCart.IncreaseCount(obj, shoppingCart.Count);
            }
            
                _unitofwork.Complete();
                

            return RedirectToAction("Index");
        }


    }
}








