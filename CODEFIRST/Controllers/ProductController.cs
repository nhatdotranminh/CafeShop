using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CODEFIRST.Models;
using CODEFIRST.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.IO;

namespace CODEFIRST.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext;
        public ProductController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Index()
        {
            if (HttpContext.User.IsInRole("ADMIN"))
            {
                var show = _dbContext.Product
                            .Include(c => c.Category)
                            .Include(c => c.Employee);
                return View(show);
            }
            return RedirectToAction("Index","Home");
        }
        // GET: Product
        [Authorize]
        public ActionResult Create()
        {
            if (HttpContext.User.IsInRole("ADMIN"))
            {
                var viewModel = new ProductViewModel
                {
                    Categories = _dbContext.Categories.ToList(),
                    Heading = "Create"
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                Image.SaveAs(path);
                var sp = new Product
                {
                    EmployeeId = User.Identity.GetUserId(),
                    CategoryId = viewModel.Category,
                    Name = viewModel.Name,
                    StartDate = DateTime.Now,
                    Price = viewModel.Price,
                    Quantity = viewModel.Quantity,
                    Image = fileName
                };
                _dbContext.Product.Add(sp);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else {
                var sp = new Product
                {
                    EmployeeId = User.Identity.GetUserId(),
                    CategoryId = viewModel.Category,
                    Name = viewModel.Name,
                    StartDate = DateTime.Now,
                    Price = viewModel.Price,
                    Quantity = viewModel.Quantity,
                    Image = "image-default.png"
                };
                _dbContext.Product.Add(sp);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                var sp = _dbContext.Product.Single(m => m.Id == id && m.EmployeeId == userId);
                var viewModel = new ProductViewModel
                {
                    Categories = _dbContext.Categories.ToList(),
                    Name = sp.Name,
                    Heading = "Edit",
                    Price = sp.Price,
                    Quantity = sp.Quantity,
                    Category = sp.CategoryId,
                    Image= sp.Image,
                    Id = sp.Id
                };
                return View("Create", viewModel);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductViewModel viewModel, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }

            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                Image.SaveAs(path);
                var userId = User.Identity.GetUserId();
                var sp = _dbContext.Product.Single(m => m.Id == viewModel.Id && m.EmployeeId == userId);
                sp.Name = viewModel.Name;
                sp.Price = viewModel.Price;
                sp.Quantity = viewModel.Quantity;
                sp.CategoryId = viewModel.Category;
                sp.Image = fileName;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var sp = _dbContext.Product.Single(m => m.Id == viewModel.Id && m.EmployeeId == userId);
                sp.Name = viewModel.Name;
                sp.Price = viewModel.Price;
                sp.Quantity = viewModel.Quantity;
                sp.CategoryId = viewModel.Category;
                sp.Image = sp.Image;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var sp = _dbContext.Product.Include(c => c.Category).Include(c => c.Employee).Single(m => m.Id == id && m.EmployeeId == userId);
            return View(sp);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var sp = _dbContext.Product.Include(c => c.Category).Include(c => c.Employee).Single(m => m.Id == id && m.EmployeeId == userId);
            return View(sp);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, Product sp)
        {
            var userId = User.Identity.GetUserId();
             sp = _dbContext.Product.Include(c => c.Employee).Include(c => c.Category).Single(s => s.Id == id && s.EmployeeId == userId);
            _dbContext.Product.Remove(sp);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        public JsonResult AddToCart(int id)
        {
            List<CartModel> listCartItem;
            if(Session["CartItem"] == null)
            {
                listCartItem = new List<CartModel>();
                listCartItem.Add(new CartModel { Quantity = 1, Product = _dbContext.Product.Find(id) });
                Session["CartItem"] = listCartItem;
            }
            else
            {
                bool flag = false;
                listCartItem = (List < CartModel >) Session["CartItem"];
                foreach(CartModel item in listCartItem)
                {
                    if(item.Product.Id == id)
                    {
                        item.Quantity++;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    listCartItem.Add(new CartModel { Quantity = 1, Product = _dbContext.Product.Find(id) });
                }
                Session["CartItem"] = listCartItem;
            }
            int cartCount = 0;
            List<CartModel> ls = (List<CartModel>)Session["CartItem"];
            foreach(CartModel item in ls)
            {
                cartCount += item.Quantity;
            }
            return Json(new { ItemAmount = cartCount });
            
        }
        public ActionResult GetCartAmount()
            
        {
            int cartcount = 0;
            if (Session["CartItem"] != null) {
                List<CartModel> ls = (List<CartModel>)Session["CartItem"];
                foreach (CartModel item in ls)
                {
                    cartcount += item.Quantity;
                }
            }

            return PartialView("CartCount", cartcount);
        }
        public ActionResult ListCartItem()
        {
            return View();
        }
    }
}