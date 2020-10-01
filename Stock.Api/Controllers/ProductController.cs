using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        private ProductTypeService productTypeService;
        private readonly IMapper mapper;

        public ProductController(ProductService service, ProductTypeService productTypeService, IMapper mapper)
        {
            this.service = service;
            this.productTypeService = productTypeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite agregar un nuevo Producto al repositorio
        /// </summary>
        /// <param name="value">Propiedades del Producto</param>
        /// <returns>Exito o Error y el Producto que se intentó crear</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            Console.WriteLine(value);
            TryValidateModel(value);

            try
            {
                var product = this.mapper.Map<Product>(value);
                product.ProductType = this.productTypeService.Get(value.ProductTypeId.ToString());
                product.AddStock(value.Stock);
                this.service.Create(product);
                value.Id = product.Id;
                
                return Ok(new { Success = true, Message = "Product Created!", Product = value });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The name is already in use!!!", Product = value });
            }
        }

        /// <summary>
        /// Permite recuperar todos los Productos existentes en el repositorio
        /// </summary>
        /// <returns>Una colección de Productos o un código en caso de error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return Ok(new {Success = true, Message = "List of all Products", 
                                Products = this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList()} );
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite recuperar las propiedades de un Producto mediante su Id
        /// </summary>
        /// <param name="id">Identificador del Producto a recuperar</param>
        /// <returns>Una instancia de Producto o un código en caso de error</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return Ok(new { Success = true, Message = "Product Obtained!", 
                        Product = this.mapper.Map<ProductDTO>(result) });
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite actualizar las propiedades de un Producto
        /// </summary>
        /// <param name="id">Identificador del Producto a actualizar</param>
        /// <param name="value">Propiedades del Producto</param>
        /// <returns>Exito o Error y el Producto que se intentó actualizar</returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            var product = this.service.Get(id);
            try
            {
                //var dtoStock = value.Stock;
                //var originalProductStock = product.Stock;
                this.mapper.Map<ProductDTO, Product>(value, product);
                //var afterMapperProductStock = product.Stock;
                product.AddStock(value.Stock - product.Stock);
                //var afterAddStockProductStock = product.Stock;
                this.service.Update(product);
                return Ok(new { Success = true, Message = "Product Updated!", 
                                //ProductStock_Original = originalProductStock, 
                                //ProductStock_AfterMapper = afterMapperProductStock,
                                //ProductStock_AfterAddStock = afterAddStockProductStock,
                                //DtoStock = dtoStock, 
                                Product = value });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The name is already in use!!!", Product = value });
            }
        }

        /// <summary>
        /// Permite borrar un Producto
        /// </summary>
        /// <param name="id">Identificador del Producto a borrar</param>
        /// <returns>Exito y el Id del Producto eliminada o un codigo en caso de error</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var product = this.service.Get(id);

            try
            {          
                this.service.Delete(product);
                return Ok(new { Success = true, Message = "Product Deleted!", data = id });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite realizar la búsqueda de Productos
        /// </summary>
        /// <param name="model">Objeto que contiene los parametros de Busquesda</param>
        /// <returns>Lista de los Productos filtradas</returns>
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
            return Ok(new {Success = true, Message = "List of all Products", 
                            Products = this.mapper.Map<IEnumerable<ProductDTO>>(products).ToList()} );
        }
    }
}