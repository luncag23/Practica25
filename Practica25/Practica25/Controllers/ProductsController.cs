using Microsoft.AspNetCore.Mvc;
using Practica25.Shared.DTOs;
using Practica25.Application.Products.Commands;
using Practica25.Application.Products.Queries;
using MediatR;

namespace Practica25.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class ProductsController : ControllerBase
     {
          private readonly IMediator _mediator;

          public ProductsController(IMediator mediator)
          {
               _mediator = mediator;
          }

          // GET: api/products
          [HttpGet]
          public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
          {
               var products = await _mediator.Send(new GetAllProductsQuery());
               return Ok(products);
          }

          // GET: api/products/5
          [HttpGet("{id}")]
          public async Task<ActionResult<ProductDTO>> GetProductById(int id)
          {
               var product = await _mediator.Send(new GetProductByIdQuery(id));
               return product is not null ? Ok(product) : NotFound();
          }

          // POST: api/products
          [HttpPost]
          public async Task<ActionResult<int>> CreateProduct([FromBody] ProductDTO product)
          {
               if (!ModelState.IsValid)
                    return BadRequest(ModelState);

               var newProductId = await _mediator.Send(new AddProductCommand(product));
               return CreatedAtAction(nameof(GetProductById), new { id = newProductId }, newProductId);
          }

          // PUT: api/products/5
          [HttpPut("{id}")]
          public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO product)
          {
               if (!ModelState.IsValid)
                    return BadRequest(ModelState);

               try
               {
                    await _mediator.Send(new UpdateProductCommand(id, product));
                    return NoContent();
               }
               catch (KeyNotFoundException)
               {
                    return NotFound();
               }
          }

          // DELETE: api/products/5
          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteProduct(int id)
          {
               try
               {
                    await _mediator.Send(new DeleteProductCommand(id));
                    return NoContent();
               }
               catch (KeyNotFoundException)
               {
                    return NotFound();
               }
          }
     }
}
