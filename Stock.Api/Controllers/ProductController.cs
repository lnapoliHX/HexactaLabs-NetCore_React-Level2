using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.Extensions;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        private readonly IMapper mapper;

        private readonly ProductTypeService productTypeService;

        public ProductController(ProductService service, IMapper mapper, ProductTypeService ptservice)
        {
            this.service = service;
            this.mapper = mapper;
            this.productTypeService = ptservice;
        }

        /// <summary>
        /// Permite leer todas las instancias
        /// </summary>
        /// <returns>Un listado de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite leer una instancia específica mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a leer</param>
        /// <returns>Una instancia determinada</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            return this.mapper.Map<ProductDTO>(this.service.Get(id));
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
                var product = this.mapper.Map<Product>(value);
                var producttype = this.productTypeService.Get(value.ProductTypeId);
                if(producttype!=null){
                    product.ProductType = producttype;
                    this.service.Create(product);
                    value.Id = product.Id;
                    value.ProductTypeDesc = producttype.Description;
                    return Ok(new { Success = true, Message = "", data = value });
                }else {
                    return Ok(new { Success = false, Message = "Una categoría existente es requerída." });
                }
        }

        /// <summary>
        /// Permite modificar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a modificar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value)
        {
            var product = this.service.Get(id);
            this.mapper.Map<ProductDTO, Product>(value, product);
            var producttype = this.productTypeService.Get(value.ProductTypeId);
            if(producttype!=null){
                product.ProductType = producttype;
                this.service.Update(product);
                value.Id = product.Id;
                    value.ProductTypeDesc = producttype.Description;
                return Ok(new { Success = true, Message = "",data = value });
            }else {
                return Ok(new { Success = false, Message = "Se requiere una categoría que exista." });
            }


        }

        /// <summary>
        /// Permite eliminar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a eliminar</param>
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
        /// Permite encontrar una instancia
        /// </summary>
        /// <param name="model">Datos que utilizamos para la búsqueda: Nombre y/o Descripción de Categoría</param>
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

            if (!string.IsNullOrWhiteSpace(model.ProductTypeDesc))
            {
                filter = filter.AndOrCustom(
                    x => x.ProductType!=null,
                    model.Condition.Equals(ActionDto.AND));

                filter = filter.AndOrCustom(
                    x => x.ProductType.Description.ToUpper().Contains(model.ProductTypeDesc.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }
            var products = this.service.Search(filter);
            var mappedProducts = this.mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(mappedProducts);
        }
    }
}