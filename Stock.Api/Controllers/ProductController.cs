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
using Stock.Model.Exceptions;

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

        /// <summary>
        /// Permite cargar una nueva instancia de producto. El stock se inicializa siempre en cero.
        /// </summary>
        /// <param name="value">Instancia nueva a cargar</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var product = this.mapper.Map<Product>(value);
                product.ProductType = productTypeService.Get(value.ProductTypeId);
                if(product.ProductType == null)
                    return Ok(new { Success = false, Message = "ProductType not found"});
                product.Provider = providerService.Get(value.ProviderId);
                if(product.Provider == null)
                    return Ok(new { Success = false, Message = "Provider not found"});
                this.service.Create(product);
                value.Id = product.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }
        
        /// <summary>
        /// Permite obtener todas las instancias.
        /// </summary>
        /// <return>Todos las instancias de productos.</return>
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

        /// <summary>
        /// Permite obtener una instancia.
        /// </summary>
        /// <param name="id">Id de la instancia a recuperar</param>
        /// <return>Una instancia cuyo Id se recibe por parámetro</return>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                var toReturn = this.mapper.Map<ProductDTO>(result);
                toReturn.ProductTypeId = result.ProductType.Id;
                toReturn.ProductTypeDesc = result.ProductType.Description;
                toReturn.ProviderName = result.Provider.Name;
                return toReturn;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Permite actualizar los valores de una instancia previamente cargada.
        /// </summary>
        /// <param name="id">Id de la instancia a actualizar</param>
        /// <param name="value">Una instancia con los datos para filtrar la búsqueda.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductDTO value)
        {
            var product = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, product);
            product.ProductType = productTypeService.Get(value.ProductTypeId);
            if(product.ProductType != null)
                this.service.Update(product);
            
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
        /// Permite buscar un producto.
        /// </summary>
        /// <param name="model">Una instancia con los datos para filtrar la búsqueda.</param>
        /// <return>Todos las instancias que coinciden con los parametros de búsqueda.</return>
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
        
            var providers = this.service.Search(filter);

            return Ok(this.mapper.Map<IEnumerable<ProductDTO>>(providers).ToList());
        }

        /// <summary>
        /// Permite aumentar el stock de un producto
        /// </summary>
        /// <param name="id">Identificador de la instancia que aumentará su stock</param>
        /// <param name="value">Cantidad a aumentar</param>
        [HttpPut("increaseStock")]
        public ActionResult<Product> IncreaseStock(string id, int value)
        {
            var product = this.service.Get(id);
            if(product != null){
                product.SumarStock(value);   
                this.service.Update(product);                
                return Ok(new { Success = true, Message = "", data = value });
            } else {
                return Ok(new { Success = true, Message = "Error: producto no encontrado", data = value });
            }
        }

        /// <summary>
        /// Permite reducir el stock de un producto
        /// </summary>
        /// <param name="id">Identificador de la instancia que reducirá su stock</param>
        /// <param name="value">Cantidad a reducir</param>
        [HttpPut("reduceStock")]
        public ActionResult ReduceStock(string id, int value)
        {
            var product = this.service.Get(id);
            if(product != null && value != 0){
                try{
                    product.DescontarStock(value);  
                    this.service.Update(product);
                } catch (ModelException e){
                     return Ok(new { Success = true, Message = e.Message, data = value });
                }
                return Ok(new { Success = true, Message = "", data = value });
            } else {
                return Ok(new { Success = true, Message = "Error: producto no encontrado", data = value });
            }         
        }
    }
}