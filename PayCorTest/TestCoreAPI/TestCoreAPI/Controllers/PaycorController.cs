using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Img.ELicensing.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCoreAPI.Dtos;
using TestCoreAPI.IServices;

namespace TestCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaycorController : ControllerBase
    {
        private IPaycorService _paycorService;
        public PaycorController(IPaycorService paycorService)
        {
            _paycorService = paycorService;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts([FromQuery]string name, [FromQuery]string sellStartDate, [FromQuery]string description, [FromQuery]int pageIndex, [FromQuery]int pageSize)
        {
            var products = await _paycorService.GetProducts(name, sellStartDate, description, pageIndex, pageSize);
            return Ok(products.Data);
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetOrderDetails([FromQuery]string dateFrom, [FromQuery]string dateTo, [FromQuery]int pageIndex, [FromQuery]int pageSize)
        {
            var products = await _paycorService.GetOrderDetails(dateFrom, dateTo, pageIndex, pageSize);
            return Ok(products.Data);
        }
    }
}