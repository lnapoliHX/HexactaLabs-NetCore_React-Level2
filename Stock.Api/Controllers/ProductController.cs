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
        private readonly ProductService service;

        private readonly ProductTypeService productTypeService;

        private readonly ProviderService providerService;

        private readonly IMapper mapper;

        public ProductController(ProductService service, IMapper mapper, ProductTypeService productTypeService, ProviderService providerService)
        {
            this.service = service;
            this.mapper = mapper;
            this.productTypeService = productTypeService;
            this.providerService = providerService;
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una instancia</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            return this.mapper.Map<ProductDTO>(this.service.Get(id));
        }

        /// <summary>
        /// Permite crear una instancia mediante un identificador
        /// </summary>
        /// <returns>Una instancia</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var product = this.mapper.Map<Product>(value);
                product.ProductType = this.productTypeService.Get(value.ProductTypeId.ToString());
                product.Provider = this.providerService.Get(value.ProviderId.ToString());
                product.SumarStock(value.Stock);
                this.service.Create(product);
                value.Id = product.Id;
                return Ok(new { Success = true, Message = " el producto se cargò con exito ", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        /// <summary>
        /// Actualiza una instancia
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="value">Identificador</param>
        /// <returns>Una instancia</returns>
        [HttpPut("Update")]

        public ActionResult<ProductDTO> Update(string id, [FromBody] ProductDTO value)
        {
            var product = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, product);
            this.service.Update(product);

            return this.mapper.Map<ProductDTO>(this.service.Get(id));
        }

        /// <summary>
        /// Permite aumentar el stock
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="value">Valor a incrementar</param>
        /// <returns>Una instancia</returns>
        [HttpPut("IncreaseStock")]

        public ActionResult<ProductDTO> IncreseStock(string id, string value)
        {
            var product = this.service.Get(id);
            if (product != null)
            {
                product.SumarStock(Int32.Parse(value));
                this.service.Update(product);
                return Ok(new { Succes = true, Message = "Stock Actualizado", Product = this.mapper.Map<ProductDTO>(this.service.Get(id)) });
            }
            else
            {
                return Ok(new { Succes = false, Message = "error: el producto no existe" });
            }
        }

        /// <summary>
        /// Permite reducir el stock
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="value">Valor a reducir</param>
        /// <returns>Una instancia</returns>
        [HttpPut("DecreaseStock")]

        public ActionResult<ProductDTO> DecreaseStock(string id, string value)
        {
            var product = this.service.Get(id);
            if (product != null)
            {
                product.DescontarStock(Int32.Parse(value));
                this.service.Update(product);
                return Ok(new { Succes = true, Message = "Stock Actualizado", Product = this.mapper.Map<ProductDTO>(this.service.Get(id)) });
            }
            else
            {
                return Ok(new { Succes = false, Message = "error: el producto no existe" });
            }
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var product = this.service.Get(id);
                this.service.Delete(product);
                return Ok(new { Success = true, Message = "Eliminado", data = id });
            }
            catch
            {
                return Ok(new { Success = false, Message = "Id no encontrado", data = id });
            }
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        [HttpDelete]
        public ActionResult DeleteAll()
        {
            try
            {
                var products = this.service.GetAll();
                foreach (Product i in products)
                {
                    this.service.Delete(i);
                }
                return Ok(new { Success = true, Message = "Eliminado" });
            }
            catch
            {
                return Ok(new { Success = false, Message = "Id no encontrado" });
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

            if (!string.IsNullOrWhiteSpace(model.ProviderName))
            {
                filter = filter.AndOrCustom(
                    x => x.Provider.Name.ToUpper().Contains(model.ProviderName.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var products = this.service.Search(filter);
            return Ok(this.mapper.Map<IEnumerable<ProductDTO>>(products));
        }
    }
}