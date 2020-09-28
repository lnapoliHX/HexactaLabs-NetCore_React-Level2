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
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private ProviderService providerService;
        private ProductService productService;
        private readonly IMapper mapper;

        public ProviderController(ProviderService providerService, ProductService productService, IMapper mapper)
        {
            this.providerService = providerService;
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var provider = this.mapper.Map<Provider>(value);
                this.providerService.Create(provider);
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.providerService.GetAll();
                return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = this.providerService.Get(id);
                return this.mapper.Map<ProviderDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = this.providerService.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProviderDTO, Provider>(value, provider);
            this.providerService.Update(provider);

            Func<Product, bool> filter = x => x.Provider.Id.Equals(value.Id);
            var products = this.productService.GetAll();
            foreach (Product product in products){
                if(filter(product)){
                    product.Provider = provider;
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
                var provider = this.providerService.Get(id);
                var isUsed = false;

                Func<Product, bool> filter = x => x.Provider.Id.Equals(id);
                var products = this.productService.GetAll();
                foreach (Product product in products){
                    if(filter(product)){
                        isUsed = true;
                        break;
                    }
                }

                if(!isUsed){
                    this.providerService.Delete(provider);
                    return Ok(new { Success = true, Message = "", data = id });
                }

                return Ok(new { Success = false, Message = "No se puede eliminar un proveedor que esté asociado a un producto", data = id });
            } catch {
                return Ok(new { Success = false, Message = "", data = id });
            }
        }

        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var providers = this.providerService.Search(filter);
            return Ok(providers);
        }
    }
}