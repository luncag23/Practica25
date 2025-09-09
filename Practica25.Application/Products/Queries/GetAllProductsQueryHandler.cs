using MediatR;
using Microsoft.EntityFrameworkCore;
using Practica25.Infrastructure.Data;
using Practica25.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica25.Application.Products.Queries
{
     public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
     {
          private readonly ApplicationDbContext _context;

          public GetAllProductsQueryHandler(ApplicationDbContext context)
          {
               _context = context;
          }

          public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
          {
               var products = await _context.Products
                    .Select(p => new ProductDTO
                    {
                         Id = p.Id,
                         Name = p.Name,
                         Description = p.Description,
                         Price = p.Price
                    })
                    .ToListAsync(cancellationToken);

               return products;
          }
     }
}
