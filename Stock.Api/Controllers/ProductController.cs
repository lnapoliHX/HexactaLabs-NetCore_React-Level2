using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService productService;
        private ProviderService providerService;
        private ProductTypeService productTypeService;
        private readonly IMapper mapper;

        public ProductController(ProductService productService, ProviderService providerService, ProductTypeService productTypeService, IMapper mapper)
        {
            this.productService = productService;
            this.providerService = providerService;
            this.productTypeService = productTypeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var product = this.mapper.Map<Product>(value);
                var provider = this.providerService.Get(value.ProviderId);
                product.Provider = provider;
                var productType = this.productTypeService.Get(value.ProductTypeId);
                product.ProductType = productType;
                this.productService.Create(product);
                value = this.mapper.Map<ProductDTO>(product);

                return Ok(new { Success = true, Message = "", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var result = this.productService.GetAll();
                return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var result = this.productService.Get(id);
                return this.mapper.Map<ProductDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductDTO value)
        {
            var product = this.productService.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, product);
            var provider = this.providerService.Get(value.ProviderId);
            product.Provider = provider;
            var productType = this.productTypeService.Get(value.ProductTypeId);
            product.ProductType = productType;
            this.productService.Update(product);
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try {
                var product = this.productService.Get(id);

                this.productService.Delete(product);
                return Ok(new { Success = true, Message = "", data = id });
            } catch {
                return Ok(new { Success = false, Message = "", data = id });
            }
        }

        /// <summary>
        /// Permite buscar instancias
        /// </summary>
        /// <param name="model">DTO de busqueda de productos</param>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProductSearchDTO model)
        {
            Expression<Func<Product, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);
            
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.ProviderName))
            {
                filter = filter.AndOrCustom(
                    x => x.Provider.Name.ToUpper().Contains(model.ProviderName.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.ProductTypeDesc))
            {
                filter = filter.AndOrCustom(
                    x => x.ProductType.Description.ToUpper().Contains(model.ProductTypeDesc.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var products = this.productService.Search(filter);
            var result = this.mapper.Map<IEnumerable<ProductDTO>>(products).ToList();
            return Ok(result);
        }
    }
}