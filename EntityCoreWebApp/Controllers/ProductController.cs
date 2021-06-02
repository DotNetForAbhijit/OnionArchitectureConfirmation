using DAL;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace EntityCoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _uow;

        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            //Either of the syntax can be used

            //var data = _db.Products.ToList();

            //var data = _db.Products.Select(s => s).ToList();

            //var data = (from prd in _db.Products
            //            select prd).ToList();


            var data = _uow.ProductRepo.GetProduct();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            //We are explicitely removing error from here because we dont have productId field in create however we have in edit.
            // this needs to be done because we are using same view for create and edit
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                _uow.ProductRepo.Add(model);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll();
            return View();
        }

        public IActionResult Edit(int id)
        {
            //Here we are getting data by using linq
            //Product model = _db.Products.Find(id);

            //same data by using stored procs
            //Product model = _db.usp_getproduct(id);

            Product model = _uow.ProductRepo.Find(id);
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            //Here we are getting data by using linq
            //Product model = _db.Products.Find(id);

            //same data ny using stored procs
            //Product model = _db.usp_getproduct(id);

            if (ModelState.IsValid)
            {
                _uow.ProductRepo.Update(model);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll();
            return View();
        }

        public IActionResult Delete(int id)
        {
            Product model = _uow.ProductRepo.Find(id);
            if (model != null)
            {
                _uow.ProductRepo.DeleteById(id);
                _uow.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
    }
}
