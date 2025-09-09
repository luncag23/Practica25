using MediatR;
using Practica25.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica25.Application.Products.Queries
{
     public record GetAllProductsQuery : IRequest<List<ProductDTO>>;

}
