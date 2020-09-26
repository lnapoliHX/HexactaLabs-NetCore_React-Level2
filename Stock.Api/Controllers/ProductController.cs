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
        public ActionResult <IEnumerable<ProductDTO>> Get()
        {
            var result = this.service.GetAll();
            return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {

            var product = this.mapper.Map<Product>(value);
            this.service.Create(product);
            value.Id = product.Id;
            return Ok(new { Success = true, Message = "", data = value });

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
