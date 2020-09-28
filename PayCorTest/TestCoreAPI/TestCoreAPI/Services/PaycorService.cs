using AutoMapper;
using Img.ELicensing.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCoreAPI.ApplicationConstants;
using TestCoreAPI.Dtos;
using TestCoreAPI.IServices;
using TestCoreAPI.Models;

namespace TestCoreAPI.Services
{
    public class PaycorService : IPaycorService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public PaycorService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseWrapper<ProductsDto>> GetProducts(string name, string sellStartDate, string keywords, int pageIndex, int pageSize)
        {
            try
            {
                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                string[] keywordsList = null;
                DateTime? sellStartDateCast = null;
                if (!string.IsNullOrWhiteSpace(keywords))
                {
                    keywordsList = keywords.Split(delimiterChars);
                }
                if (!string.IsNullOrWhiteSpace(sellStartDate))
                {
                    sellStartDateCast = DateTime.Parse(sellStartDate);
                }

                var source = _context.ViewProducts
                    .Where(x => (!string.IsNullOrWhiteSpace(name) ? x.Name.Contains(name) : true)
                                && (sellStartDateCast != null ? x.SellStartDate.Date == sellStartDateCast.Value : true))
                    .AsEnumerable()
                    .Where(f => keywordsList != null ? keywordsList.Any(w => f.Description.Contains(w)) : true);

                int totalCount = source.Count();
                var totalPages = totalCount / pageSize;
                if (totalCount % pageSize > 0)
                {
                    totalPages++;
                }

                List<vProductAndDescription> products = source
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .ToList();

                List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);

                var returnProducts = new ProductsDto
                {
                    TotalPages = totalPages,
                    Products = productDtos
                };

                return ResponseWrapper<ProductsDto>.Success(returnProducts);
            }
            catch (Exception)
            {
                return ResponseWrapper<ProductsDto>.Error(AppConstants.GetFailed);
            }
        }

        public async Task<ResponseWrapper<OrdersDto>> GetOrderDetails(string dateFrom, string dateTo, int pageIndex, int pageSize)
        {
            try
            {
                DateTime? dateFromCast = DateTime.Parse(dateFrom);
                DateTime? dateToCast = DateTime.Parse(dateTo);
                var source = _context.PurchaseOrderDetails
                    .Where(x => x.DueDate >= dateFromCast.Value && x.DueDate <= dateToCast.Value)
                    .GroupBy(x => x.DueDate, (x, y) => new PurchaseOrderDetailDto
                    {
                        Day = x,
                        LineTotal = y.Sum(z => z.LineTotal),
                        OrderQty = y.Sum(z => z.OrderQty),
                        TotalSum = y.Sum(z => z.OrderQty) + y.Sum(z => z.LineTotal)
                    });

                int totalCount = source.Count();
                var totalPages = totalCount / pageSize;
                if (totalCount % pageSize > 0)
                {
                    totalPages++;
                }

                List<PurchaseOrderDetailDto> orders = await source
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .ToListAsync();

                var returnOrders = new OrdersDto
                {
                    Orders = orders,
                    TotalPages = totalPages
                };

                return ResponseWrapper<OrdersDto>.Success(returnOrders);
            }
            catch (Exception)
            {
                return ResponseWrapper<OrdersDto>.Error(AppConstants.GetFailed);
            }
        }
    }
}
