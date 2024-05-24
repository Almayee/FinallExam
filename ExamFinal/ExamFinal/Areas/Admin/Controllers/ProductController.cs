using FinalExam.Business.Exceptions;
using FinalExam.Business.Services.Abstracts;
using FinalExam.Core.Models;
using FinalExam.Core.RepositoryAabstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ExamFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class ProductController : Controller
    {
        private readonly IProductService _prductService;
        public ProductController(IProductService prductService)
        {
            _prductService=prductService;
        }

        public IActionResult Index()
        {
            var products = _prductService.GetAllProducts();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
               await _prductService.AddProduct(product);
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existBlog = _prductService.GetProduct(x => x.Id == id);
            if (existBlog == null) return NotFound();
            return View(existBlog);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _prductService.UpdateProduct(product, product.Id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (ImageContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FinalExam.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existBlog = _prductService.GetProduct(x => x.Id == id);
            if (existBlog == null) return NotFound();
            return View(existBlog);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {

            try
            {
                _prductService.DeleteProduct(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FinalExam.Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
       
    }
}
