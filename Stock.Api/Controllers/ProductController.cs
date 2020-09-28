using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using Stock.Model.Exceptions;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private ProductService service;
        private ProductTypeService productTypeService;
        private readonly IMapper mapper;

        public ProductController(ProductService service, ProductTypeService productTypeService, IMapper mapper)
        {
            this.service = service;
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
                if (!string.IsNullOrWhiteSpace(value.ProductTypeId))
                {
                    var productType = this.productTypeService.Get(value.ProductTypeId);
                    if (productType == null)
                        return Ok(new { Success = false, Message = "Product type doesn't exist" });
                    product.ProductType = productType;
                    value.ProductTypeDesc = productType.Description;
                }
                this.service.Create(product);
                value.Id = product.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (ModelException ex)
            {
                return Ok(new { Success = false, Message = "Error Validation: " + ex.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message });
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
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                var result = this.service.Get(id);
                return this.mapper.Map<ProductDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value)
        {
            TryValidateModel(value);
            try{
                var product = this.service.Get(id);
                this.mapper.Map<ProductDTO, Product>(value, product);
                if (!string.IsNullOrWhiteSpace(value.ProductTypeId))
                {
                    if (value.ProductTypeId != product.ProductType.Id)
                    {
                        var productType = this.productTypeService.Get(value.ProductTypeId);
                        if (productType == null)
                            return Ok(new { Success = false, Message = "Product type doesn't exist" });
                        product.ProductType = productType;
                    }
                }
                else
                {
                    product.ProductType = null;
                }
                product = this.service.Update(product);
                value = this.mapper.Map<ProductDTO>(product);
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (ModelException ex)
            {
                return Ok(new { Success = false, Message = "Error Validation: " + ex.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message });
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
                var product = this.service.Get(id);

                this.service.Delete(product);
                return Ok(new { Success = true, Message = "", data = id });
            } catch {
                return Ok(new { Success = false, Message = "", data = id });
            }
        }

        /// <summary>
        /// Permite filtrar por un criterio de búsqueda
        /// </summary>
        /// <param name="model">Instancia con los datos a filtrar</param>
        [HttpPost("search")]
        public ActionResult<IEnumerable<ProductDTO>> Search([FromBody] ProductSearchDTO model)
        {
            Expression<Func<Product, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.ProductTypeDesc))
            {
                filter = filter.AndOrCustom(
                    x => x.ProductType.Description.ToUpper().Contains(model.ProductTypeDesc.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var products = this.service.Search(filter);
            return this.mapper.Map<IEnumerable<ProductDTO>>(products).ToList();
        }

        /// <summary>
        /// Permite incrementar el stock de un producto
        /// </summary>
        /// <param name="model">Instancia con los datos del product al que se le quiere modificar el stock</param>
        [HttpPost("stock")]
        public ActionResult<ProductDTO> ModifyStock([FromBody]ModifyStockDataDTO model)
        {
            try
            {
                var product = this.service.Get(model.Id);
                if (product != null)
                {
                    if (model.Increase)
                        product.SumarStock(model.Value);
                    else
                        product.DescontarStock(model.Value);
                    var result = this.service.Update(product);
                    return this.mapper.Map<ProductDTO>(result);
                }
                else
                    return Ok(new { Success = false, Message = "El producto no existe" });
            }
            catch (ModelException ex)
            {
                return Ok(new { Success = false, Message = "Error Validation: " + ex.Message });
            }
        }
    }
}