using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService service;
        private readonly IMapper mapper;
        public ProductController(ProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            var result = this.service.GetAll();
            return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            return this.mapper.Map<ProductDTO>(this.service.Get(id));
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {

            var product = this.mapper.Map<Product>(value);
            product.SumarStock(value.Stock);
            this.service.Create(product);
            value.Id = product.Id;
            value.ProductType = product.ProductType;
            return Ok(new { Success = true, Message = "", data = value });

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductDTO value)
        {

            var product = this.service.Get(id);
            
            TryValidateModel(value);
            this.mapper.Map<ProductDTO, Product>(value, product);
            
            this.service.UptdateP(product, value.Stock);
            
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var product = this.service.Get(id);

                this.service.Delete(product);
                return Ok(new { Success = true, Message = "", data = id });
            }
            catch
            {
                return Ok(new { Success = false, Message = "", data = id });
            }


        }
    }
}
