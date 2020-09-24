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
        private ProductService service;
        private readonly IMapper mapper;
        private ProductTypeService productTypeService;
        private ProviderService providerService;

        public ProductController(ProductService service, IMapper mapper, ProductTypeService productTypeService, ProviderService providerService)
        {
            this.service = service;
            this.mapper = mapper;
            this.productTypeService = productTypeService;
            this.providerService = providerService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var Product = this.mapper.Map<Product>(value);
                Product.ProductType = productTypeService.Get(value.ProductTypeId);
                if(Product.ProductType == null)
                    return Ok(new { Success = false, Message = "ProductType not found"});
                if(Product.Provider == null)
                    return Ok(new { Success = false, Message = "Provider not found"});
                this.service.Create(Product);
                value.Id = Product.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                var aux = this.mapper.Map<ProductDTO>(result);
                aux.ProductTypeId = result.ProductType.Id;
                aux.ProductTypeDesc = result.ProductType.Description;
                aux.ProviderName = result.Provider.Name;
                return aux;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductDTO value)
        {
            var Product = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, Product);
            Product.ProductType = productTypeService.Get(value.ProductTypeId);
            if(Product.ProductType != null)
            {
                this.service.Update(Product);
            }
                

        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try {
                var Product = this.service.Get(id);

                Expression<Func<Product, bool>> filter = x => x.Id.Equals(id);

                this.service.Delete(Product);
                return Ok(new { Success = true, Message = "", data = id });
            } catch {
                return Ok(new { Success = false, Message = "", data = id });
            }
        }

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

            var Products = this.service.Search(filter);
            return Ok(Products);
        }

        [HttpPut("addStock")]
        public ActionResult addStock(string id, int value)
        {
            var product = this.service.Get(id);
            if(product != null){
                product.SumarStock(value);   
                this.service.Update(product);                
                return Ok(new { Success = true, Message = "", data = value });
            } else {
                return StatusCode(500);
            }
        }

        [HttpPut("subtractStock")]
        public ActionResult subtractStock(string id, int value)
        {
            var product = this.service.Get(id);
            if(product != null && value != 0){
                try{
                    product.DescontarStock(value);  
                    this.service.Update(product);
                } catch {
                     return StatusCode(500);
                }
                return Ok(new { Success = true, Message = "", data = value });
            } else {
                return StatusCode(500);
            }         
        }
    }
}