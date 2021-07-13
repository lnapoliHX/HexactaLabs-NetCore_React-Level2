using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        private readonly IMapper mapper;

        public ProductController(ProductService service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            var product = this.mapper.Map<Product>(value);
            ProductType pType = new ProductType();
            product.ProductType = pType;
            product.ProductType.Id = value.ProductTypeId;
            product.SumarStock(value.Stock);
            product = this.service.Create(product);
            value = this.mapper.Map<ProductDTO>(product);
            return Ok(new { Success = true, Message = "", data = value });
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value)
        {
            if (TryValidateModel(value))
            {
                var product = this.mapper.Map<Product>(value);
                product.Id = value.Id;
                var updatedProduct =this.service.UpdateProduct(product, value.ProductTypeId,value.Stock);
                value = this.mapper.Map<ProductDTO>(updatedProduct);
                return Ok(new { Success = true, Message = "", data = value });
            }
            else
            {
                return BadRequest("Missing Validations");
            }
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var product = this.service.Get(id);
            this.service.Delete(product);
            return Ok();
        }
    }
}
