using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stock.Api.Controllers
{    

    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {

        private readonly ProductService service;
        private readonly IMapper mapper;


        public ProductController(ProductService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }


        /// <summary>
        /// Get all product.
        /// </summary>
        /// <returns>List of <see cref="ProductDTO"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {

            try
            {
                var product = service.GetAll();
                if (product == null)
                {
                    return NotFound();
                }
                return mapper.Map<IEnumerable<ProductDTO>>(product).ToList();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets a product by id.
        /// </summary>
        /// <param name="id">Product id to return.</param>
        /// <returns>A <see cref="ProductDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var product = mapper.Map<ProductDTO>(service.Get(id));
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                return BadRequest();
            }

        }

        /// <summary>
        /// Add a product
        /// </summary>
        /// <param name="value">Product information.</param>
        [HttpPost]
        public Product Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);
            var product = service.Create(mapper.Map<Product>(value));
            return mapper.Map<Product>(product);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id">Product id to edit.</param>
        /// <param name="value">Product information.</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value)
        {
            TryValidateModel(value);
            try
            {
                var product = service.Get(id);
                if (product == null)
                {
                    return NotFound();
                }
                mapper.Map<ProductDTO, Product>(value, product);
                service.Update(product);
                return Ok(new { statusCode = "200", result = "Producto modificado exitosamente" });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">Product id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var product = service.Get(id);
                if (product is null)
                    return NotFound();

                service.Delete(product);
                return Ok(new { statusCode = "200", result = "Producto eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                return BadRequest();
            }
        }

        /// <summary>
        /// Search by Name  
        /// </summary>
        /// <param name="model">Product information.</param>        
        /// <returns>A <see cref="ProductSearchDTO"/></returns>
        [HttpGet]
        [Route("search")]
        public ActionResult Search([FromQuery] ProductSearchDTO model)
        {
            Expression<Func<Product, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            // Search by Name
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }


            var product = service.Search(filter);
            if (product.Count() == 0)
                return NotFound();

            return Ok(product);
        }
    }
}
