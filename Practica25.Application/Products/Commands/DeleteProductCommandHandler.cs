using MediatR;
using Practica25.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practica25.Application.Products.Commands
{
     public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
     {
          private readonly ApplicationDbContext _context;

          public DeleteProductCommandHandler(ApplicationDbContext context)
          {
               _context = context;
          }

          public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
          {
               var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);


               if (product is not null)
               {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync(cancellationToken);
               }
          }

     }
}
