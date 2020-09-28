using Img.ELicensing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCoreAPI.Dtos;

namespace TestCoreAPI.IServices
{
    public interface IPaycorService
    {
        Task<ResponseWrapper<ProductsDto>> GetProducts(string name, string sellStartDate, string keywords, int pageIndex, int pageSize);

        Task<ResponseWrapper<OrdersDto>> GetOrderDetails(string dateFrom, string dateTo, int pageIndex, int pageSize);
    }
}
