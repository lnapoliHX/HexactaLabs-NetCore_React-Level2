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
    public class ProductController : ControllerBase
    {
        private ProductService serviceProduct;
        private ProductTypeService serviceProductType;
        private readonly IMapper mapper;

        public ProductController(ProductService serviceProduct, ProductTypeService serviceProductType, IMapper mapper){
            this.serviceProduct = serviceProduct;
            this.serviceProductType = serviceProductType;
            this.mapper = mapper;
        }

        /// <summary>      
        /// Permite crear una nueva instacia con stock en 0
        /// </summary>
        /// <param name = "value">Una instancia</param>   
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value ){
            TryValidateModel(value);
            try{
                var product = this.mapper.Map<Product> (value);
                var productType = this.serviceProductType.Get(value.ProductTypeId);
                if(productType == null){
                    return Ok(new {success = false, message = "ProductType not found"});
                }
                product.ProductType = productType;
                //product.SumarStock(value.stock); // Validar que sea Mayor a Cero
                this.serviceProduct.Create(product);
                value.Id = product.Id;
                return Ok(new {success = true, message="", data = value});
            }
            catch (ModelException e){
                return Ok(new {success = false, message = e.Message});
            }
            catch (ArgumentException e){
                return Ok(new {success = false, message = e.Message });
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns> Una colección de instancias </returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get(){
            try{
                var result = this.serviceProduct.GetAll();
                return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>      
        /// Permite permite recuperar una nueva instacia
        /// </summary>
        /// <param name = "id"> Identificador de la instancia a recuperar </param> 
        /// <returns> Una instancia </returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id){
            try{
                var result = this.serviceProduct.Get(id);
                return this.mapper.Map<ProductDTO>(result);
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value){
            try{
                var product = this.serviceProduct.Get(id);
                TryValidateModel(value);
                this.mapper.Map<ProductDTO,Product>(value,product);
                var productType = this.serviceProductType.Get(value.ProductTypeId);
                if(productType == null){
                    return Ok(new {success = false, message = "ProductType not found"});
                }
                this.serviceProduct.Update(product);
                return Ok(new {success = true, message = "", data = value});
            }
            catch (ArgumentException e){
                return Ok(new {success = false, message = e.Message});
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite incrementar el stock de una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia para incrementar stock</param>
        /// <param name="value">Stock a incrementar</param>
        [HttpPut("{id}/increase-stock")]
        public ActionResult IncreaseStock(string id, int value){
            try{
                var product = this.serviceProduct.Get(id);
                product.SumarStock(value);
                this.serviceProduct.Update(product);
                return Ok(new {success = true, message = ""});
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                var product = this.serviceProduct.Get(id);
                product.DescontarStock(value);
                this.serviceProduct.Update(product);
                return Ok(new {success = true, message = ""});
            }
            catch (ModelException e){
                return Ok(new {success = false, message = e.Message});
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id){
            try{
                var product = this.serviceProduct.Get(id);
                this.serviceProduct.Delete(product);
                return Ok(new {success = true, message = "", data = id });
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite recuperar todas las instancias que cumplan con los criterios de busqueda
        /// </summary>
        /// <param  name="model"> Criterio de Busqueda </param>
        [HttpPost("search")]
        public ActionResult<ProductDTO> Search([FromBody] ProductSearchDTO model)
        {
            Expression<Func<Product, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }
            try{
                var products = this.serviceProduct.Search(filter);
                return Ok(this.mapper.Map<IEnumerable<ProductDTO>>(products).ToList());
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);    
            }
        }
    }
}