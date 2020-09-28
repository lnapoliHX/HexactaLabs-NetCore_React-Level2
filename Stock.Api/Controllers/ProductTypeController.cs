using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/producttype")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService productTypeService;
        private readonly ProductService productService;
        private readonly IMapper mapper;
        
        public ProductTypeController(ProductTypeService productTypeService, ProductService productService, IMapper mapper)
        {
            this.productTypeService = productTypeService;
            this.productService = productService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.productTypeService.GetAll()).ToList();
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(string id)
        {
            return this.mapper.Map<ProductTypeDTO>(this.productTypeService.Get(id));
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);
            
            try
            {
                var productType = this.mapper.Map<ProductType>(value);
                this.productTypeService.Create(productType);
                value.Id = productType.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductTypeDTO value)
        {
            var productType = this.productTypeService.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductTypeDTO, ProductType>(value, productType);
            this.productTypeService.Update(productType);

            Func<Product, bool> filter = x => x.ProductType.Id.Equals(value.Id);
            var products = this.productService.GetAll();
            foreach (Product product in products){
                if(filter(product)){
                    product.ProductType = productType;
                    this.productService.Update(product);
                }
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
                var productType = this.productTypeService.Get(id);
                var isUsed = false;

                Func<Product, bool> filter = x => x.ProductType.Id.Equals(id);
                var products = this.productService.GetAll();
                foreach (Product product in products){
                    if(filter(product)){
                        isUsed = true;
                        break;
                    }
                }
                
                if(!isUsed){
                    this.productTypeService.Delete(productType);
                    return Ok(new { Success = true, Message = "", data = id });
                }

                return Ok(new { Success = false, Message = "No se puede eliminar una categoría que esté asociada a un producto", data = id });
            } catch {
                return Ok(new { Success = false, Message = "", data = id });
            }
        }
    }
}
