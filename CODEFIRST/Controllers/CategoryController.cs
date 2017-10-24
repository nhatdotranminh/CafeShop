using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CODEFIRST.Models;
using CODEFIRST.ViewModels;


namespace CODEFIRST.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        //private ApplicationDbContext _dbContext;
        private ApplicationDbContext _dbContext;
        public CategoryController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {

            var category = _dbContext.Categories.ToList();
           
            
            return View(category);
        }
        public ActionResult Menu(int id)
        {
            var Product = _dbContext.Product.Where(m => m.CategoryId == id).ToList();
            //var Product = _dbContext.Product.ToList();
            return View(Product);
        }
       
    }
}