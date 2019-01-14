using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Specifications;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository; // @issue@I02

        public OrderController(IOrderRepository orderRepository) { // @issue@I02
            _orderRepository = orderRepository; // @issue@I02
        }
        
        public async Task<IActionResult> Index() // @issue@I02
        {
            var orders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name)); // @issue@I02

            var viewModel = orders // @issue@I02
                .Select(o => new OrderViewModel()
                {
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems?.Select(oi => new OrderItemViewModel()
                    {
                        Discount = 0,
                        PictureUrl = oi.ItemOrdered.PictureUri,
                        ProductId = oi.ItemOrdered.CatalogItemId,
                        ProductName = oi.ItemOrdered.ProductName,
                        UnitPrice = oi.UnitPrice,
                        Units = oi.Units
                    }).ToList(),
                    OrderNumber = o.Id,
                    ShippingAddress = o.ShipToAddress,
                    Status = "Pending",
                    Total = o.Total()

                });
            return View(viewModel); // @issue@I02
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Detail(int orderId) // @issue@I02
        {
            var customerOrders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name)); // @issue@I02
            var order = customerOrders.FirstOrDefault(o => o.Id == orderId); // @issue@I02
            if (order == null) // @issue@I02
            {
                return BadRequest("No such order found for this user."); // @issue@I02
            }
            var viewModel = new OrderViewModel() // @issue@I02
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    Discount = 0,
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipToAddress,
                Status = "Pending",
                Total = order.Total()
            };
            return View(viewModel); // @issue@I02
        }
    }
}
