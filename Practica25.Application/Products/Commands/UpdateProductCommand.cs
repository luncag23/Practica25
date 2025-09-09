using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Practica25.Shared.DTOs;

namespace Practica25.Application.Products.Commands
{
     public record UpdateProductCommand(int Id, ProductDTO Product) : IRequest;
     
}
