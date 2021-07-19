using System.Collections.Generic;
using System.Threading;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet.Messages;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Thread.Sleep(1000);
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Data) ;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetBytId(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
           var result = _productService.Add(product);
            if (result.Success)
            {
                
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("ramazan")]
        public IActionResult Ramazan()
        {
            string ramo = "Ramazan halid 35 12312423 r4523 3rgferg ergerg";
            return Ok(ramo);
        }
    }
}