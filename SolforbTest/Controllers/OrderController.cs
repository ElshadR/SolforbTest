using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Infrastructure.Db;
using SolforbTest.Infrastructure.Db.Models;
using SolforbTest.Infrastructure.Services.Contracts;
using SolforbTest.Models;
using System.Diagnostics;

namespace SolforbTest.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private IOrderService _orderService { get; set; }
        private IProviderService _providerService { get; set; }

        public OrderController(
            ILogger<OrderController> logger,
            IProviderService providerService,
            IOrderService orderService)
        {
            _logger = logger;
            _providerService = providerService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetList(DateTime.Now, DateTime.Now.AddMonths(1));

            ViewData["Providers"] = await _providerService.GetList();

            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Providers"] = await _providerService.GetList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderModel model)
        {
            var item = await _orderService.Create(model);
            if (item != null)
                return RedirectToAction("Index", "Order");
            else
                return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.Get(id);

            var model = new EditOrderModel
            {
                Id = id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId,
                OrderItems = order.OrderItems
                .Select(x => new OrderItemModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                }).ToList(),
            };

            ViewData["Providers"] = await _providerService.GetList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditOrderModel model)
        {
            var item = await _orderService.Edit(model);
            if (item !=null)
                return RedirectToAction("Index", "Order");
            else
                return RedirectToAction("Edit", "Order", new { Id = model.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Search(SearchOrderModel model)
        {
            var orders = await _orderService.Search(model);

            return PartialView("~/Views/Shared/Partials/_TablePartial.cshtml", orders);
        }


        public async Task<IActionResult> Show(int id)
        {
            var order = await _orderService.Get(id);

            ViewData["Providers"] = await _providerService.GetList();

            return View(order);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _orderService.Remove(id);
            if (item != null)
                return RedirectToAction("Index", "Order");
            else
                return RedirectToAction("Show", "Order", new { Id = id });
        }

        [HttpPost]
        public IActionResult AddOrderItemPartial(int index)
        {
            return PartialView("~/Views/Shared/Partials/_OrderItemPartial.cshtml", new OrderItemModel { Index = index });
        }
    }
}