using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica25.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Practica25.Application.Products.Commands
{
     public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
     {
          private readonly ApplicationDbContext _context;

          public UpdateProductCommandHandler(ApplicationDbContext context)
          {
               _context = context;
          }
          public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
          {
               var product = await _context.Products.FirstOrDefaultAsync(h => h.Id == command.Id, cancellationToken);

               if (product is null)
               {
                    // Or handle this case as you see fit, maybe return a specific result
                    throw new KeyNotFoundException($"Product with Id {command.Id} not found.");
               }

               product.Name = command.Product.Name;
               product.Description = command.Product.Description;
               product.Price = command.Product.Price;
               

               await _context.SaveChangesAsync(cancellationToken);


          }
     }
}
