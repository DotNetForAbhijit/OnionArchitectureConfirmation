using DAL;
using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        IUnitOfWork _uow;
        public CatalogController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            return _uow.ProductRepo.GetProduct();
        }

        ////By default rest api returns status code
        //[HttpGet("{id}")]
        //public Product GetProducts(int id)
        //{
        //    return _uow.ProductRepo.Find(id); //By default is OK:200
        //}

        //By default rest api returns status code
        //Above code also works its just that this is generic return type
        [HttpGet("{id}")]
        public ActionResult<Product> GetProducts(int id)
        {
            return _uow.ProductRepo.Find(id); //By default is OK:200
        }

        [HttpPost]
        public IActionResult AddProducts(Product model)
        {
            try 
            {
                _uow.ProductRepo.Add(model);
                _uow.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProducts(int id, Product model)
        {
            try
            {
                if (id != model.ProductId)
                    return BadRequest();

                _uow.ProductRepo.Add(model);
                _uow.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducts(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                _uow.ProductRepo.DeleteById(id);
                _uow.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
