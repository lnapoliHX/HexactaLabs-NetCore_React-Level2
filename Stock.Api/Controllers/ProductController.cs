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

namespace Stock.Api.Controllers
{
    /// <summary>
    /// Product endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;

        private readonly IMapper mapper;

        private readonly ProductTypeService productTypeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="service">Product service.</param>
        /// <param name="mapper">Mapper configurator.</param> 

        public ProductController(ProductService service, ProductTypeService productTypeService, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.productTypeService = productTypeService ?? throw new ArgumentException(nameof(productTypeService));
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of <see cref="ProductDTO"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductService>> Get()
        {
            return Ok(mapper.Map<IEnumerable<ProductDTO>>(service.GetAll()).ToList());
        }


        /// <summary>
        /// Gets a product by id.
        /// </summary>
        /// <param name="id">Product id to return.</param>
        /// <returns>A <see cref="ProductDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            return Ok(mapper.Map<ProductDTO>(service.Get(id)));
        }


        /// <summary>
        /// Add a product.
        /// </summary>
        /// <param name="value">Product information.</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var product = this.mapper.Map<Product>(value);
                var productType = this.productTypeService.Get(value.ProductTypeId);
                if (productType == null)
                {
                    return BadRequest(new { Success = false, Message = "The productType does not exists" });
                }

                product.ProductType = productType;

                var newProduct = this.service.Create(product);

                return Ok(new { Success = true, Message = "", data = newProduct });
            }

            catch
            {
                return BadRequest(new { Success = false, Message = "The name is already in use" });
            }
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id">Product id to edit.</param>
        /// <param name="value">Product information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductDTO value)
        {
            var product = this.service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProductDTO, Product>(value, product);

            var productType = this.productTypeService.Get(value.ProductTypeId);
            if (productType != null)
            {
                product.ProductType = productType;
            }

            service.Update(product);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">Product id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var product = service.Get(id);

                Expression<Func<Product, bool>> filter = x => x.Id.Equals(id);

                service.Delete(product);
                return Ok(new { Success = true, Message = "", data = id });
            }
            catch
            {
                return Ok(new { Success = false, Message = "", data = id });
            }
        }

        /// <summary>
        /// Search a product by Id.
        /// </summary>
        /// <param name="id">Product id to search.</param>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProductSearchDTO model)
        {
            Expression<Func<Product, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrEmpty(model.Name))
            {
                filter = filter.AndOrCustom(
                   x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                   model.Condition.Equals(ActionDto.OR));
            }

            var products = service.Search(filter);
            return Ok(products);
        }

    }
}
