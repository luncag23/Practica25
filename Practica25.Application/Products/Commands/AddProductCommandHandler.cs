using MediatR;
using Practica25.Application.Products.Commands;
using Practica25.Domain.Entities;
using Practica25.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Practica25.Application.Products.Commands
{
     public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
     {
          private readonly ApplicationDbContext _context;

          public AddProductCommandHandler(ApplicationDbContext context)
          {
               _context = context;
          }

          public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
          {
               var product = new Product
               {
                    Name = request.Product.Name,
                    Description = request.Product.Description,
                    Price = request.Product.Price
               };
               _context.Products.Add(product);
               await _context.SaveChangesAsync(cancellationToken);

               return product.Id;

          }
     }
}
