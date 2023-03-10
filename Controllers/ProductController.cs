using Mc_Task.DTO;
using Mc_Task.Models;
using Mc_Task.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TechnicalTassk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService productservice;
        ApplicationDbContext context;
        public ProductController(IProductService pservice, ApplicationDbContext _context)
        {
            productservice = pservice;
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> AllProducts([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await context.products
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await context.products.CountAsync();

            return Ok(new PagedResponse<List<Product>>(pagedData, validFilter.PageNumber, validFilter.PageSize));


        }


        [HttpGet("{id:int}", Name = "getOneRoute")]

        public IActionResult GetDetails(int id)
        {
            Product product = productservice.GetById(id);
            if (product == null)
            {
                return BadRequest("There Are No Product Same That You Want");
            }
            return Ok(product);
        }
        //search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search([FromQuery]SearchRequest request)
        {
            try
            {
                var result = await productservice.Search( request);

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
     
        [HttpPost]
        public IActionResult AddNewProduct([FromBody] ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productservice.Insert(model);
                    string url = Url.Link("getOneRoute", new { id = model.ProductId});
                    return Created(url, model);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    productservice.Update(id, product);

                    return StatusCode(204, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    productservice.Delete(id);

                    return StatusCode(204, "Product Deleted Sucessfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
    }
}
