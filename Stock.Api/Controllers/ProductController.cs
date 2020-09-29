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

        public ProductController(ProductService service, IMapper mapper, ProductTypeService productTypeService)
        {
            this.service = service;
            this.mapper = mapper;
            this.productTypeService = productTypeService;
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
                var product= this.mapper.Map<Product>(value);
                product.ProductType = this.productTypeService.Get(value.ProductTypeId.ToString());
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
                var result = this.service.Get(id);
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
            var product = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, product);
            product.ProductType = productTypeService.Get(value.ProductTypeId.ToString());
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
        /// Permite filtrar por un criterio de búsqueda
        /// </summary>
        /// <param name="model">Instancia con los datos para filtrar la busqueda</param>
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
            
            var products = this.service.Search(filter);
            return Ok(products);
        }

        /// <summary>
        /// Permite incrementar el stock de una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia para incrementar stock</param>
        /// <param name="value">Stock a incrementar</param>
        [HttpPut("{id}/increase-stock")]
        public ActionResult IncreaseStock(string id, int value){
            try{
                var product = this.service.Get(id);
                product.SumarStock(value);
                this.service.Update(product);
                return Ok(new {success = true, message = ""});
            }
            catch{
                    return StatusCode(500);
            }
        }

        /// <summary>
        /// Permite decrementar el stock de una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia para decrementar stock</param>
        /// <param name="value">Stock a decrementar</param>
        [HttpPut("{id}/decrease-stock")]
        public ActionResult DecreaseStock(string id, int value){
            try{
                var product = this.service.Get(id);
                product.DescontarStock(value);
                this.service.Update(product);
                return Ok(new {success = true, message = ""});
            }
            catch {
                return StatusCode(500);
            }
        }
    }
}