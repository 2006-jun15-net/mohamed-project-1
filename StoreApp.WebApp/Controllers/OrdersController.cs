using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Models;
using StoreApp.WebApp.ViewModels;

namespace StoreApp.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly OrderHandler _orderHandler;

        public OrdersController(IOrderRepository orderRepo, ILocationRepository locationRepo, ICustomerRepository customerRepo)
        {
            _orderRepo = orderRepo;
            _locationRepo = locationRepo;
            _customerRepo = customerRepo;
            _orderHandler = new OrderHandler(_locationRepo, _orderRepo);
        }

        public IActionResult GetProducts(int id)
        {
            try
            {
                var model = _locationRepo.GetById(id);
                model.Inventory = _locationRepo.GetAllProducts(model.Id);
                return View(model);
            }
            catch (Exception e)
            {
                TempData["ErrorMsg"] = e.Message;
                return RedirectToAction(nameof(GetLocation));
            }

        }
        [HttpPost]
        public IActionResult GetProducts(int id, [Bind("ProductId", "Amount")] OrderLineViewModel viewModel)
        {
            var model = _locationRepo.GetById(id);
            model.Inventory = _locationRepo.GetAllProducts(model.Id);
            if (ModelState.IsValid)
            {
                double Total = 0;
                foreach (var product in model.Inventory.Keys)
                {
                    if (TempData[product.Name] == null)
                    {
                        TempData[product.Name] = 0;
                    }

                    int count = (int)TempData[product.Name];
                    if (product.ProductId == viewModel.ProductId)
                    {
                        count += viewModel.Amount;
                        if (count > model.Inventory[product])
                        {
                            ModelState.AddModelError("", "Not enough stock available for request");
                        }
                        else
                        {
                            TempData[product.Name] = count;
                        }

                    }
                    TempData.Keep(product.Name);

                    Total += product.Price * (int)TempData[product.Name];
                }
                ViewData["Total"] = Total;

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Invalid quantity.");
                return View(model);
            }


        }
        public IActionResult GetLocation()
        {
            var locations = _locationRepo.GetAll().ToList();

            ViewBag.locations = locations;

            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(int LocationId)
        {
            var location = _locationRepo.GetById(LocationId);
            location.Inventory = _locationRepo.GetAllProducts(location.Id);
            ShoppingCart cart = new ShoppingCart(location);
            int customerID = (int)TempData["CustomerID"];
            Customer customer;

            //Check that user is signed in 
            try
            {
                customer = _customerRepo.GetById(customerID);
            }
            catch (Exception)
            {
                TempData["errorMsg"] = "No customer for the order. Register or Sign in and try again.";
                return RedirectToAction(nameof(GetProducts), new { location.Id });
            }

            //Fill cart, if cart is empty return with error
            foreach (var product in location.Inventory.Keys)
            {
                if (TempData[product.Name] == null)
                {
                    TempData["errorMsg"] = "No products in order.";
                    return RedirectToAction(nameof(GetProducts), new { location.Id });
                }
                int qty = (int)TempData[product.Name];
                if (qty != 0)
                {
                    try
                    {
                        cart.AddToCart(product, qty);
                    }
                    catch (Exception ex)
                    {
                        TempData["errorMsg"] = ex.Message;
                        return RedirectToAction(nameof(GetProducts), new { location.Id });
                    }

                }
            }
            if (cart.Items.Count == 0)
            {
                TempData["errorMsg"] = "No products in order.";
                return RedirectToAction(nameof(GetProducts), new { location.Id });
            }

            //Create the order with order service then display customer order history
            try
            {

                _orderHandler.CreateOrder(cart, customer);

                return RedirectToAction(nameof(OrdersHistory), new { customerID = customer.customerID });
            }
            catch (Exception)
            {
                TempData["errorMsg"] = "Error in placing an order.";
                return RedirectToAction(nameof(GetProducts), new { location.Id });
            }

        }

        public IActionResult OrdersHistory(int customerID)
        {
            try
            {
                var orderHistory = _orderRepo.GetOrderHistoryofCustomer(_customerRepo.GetById(customerID));
                List<OrdersViewModel> viewModels = new List<OrdersViewModel>();
                foreach (var order in orderHistory)
                {
                    viewModels.Add(new OrdersViewModel
                    {
                        OrderDate = (DateTime)order.OrderTime,
                        OrderId = order.OrderId,
                        OrderLine = order.OrderLine,
                        Location = order.StoreLocation.Name,
                        OrderTotal = order.totalCost
                    });
                }
                return View(viewModels);
            }
            catch (Exception)
            {
                ViewData["ErrorMsg"] = "Invalid customer or customer not detected. Please sign in and try again.";
                return View();
            }


        }

        public IActionResult LocationOrderHistory()
        {
            ViewBag.locations = _locationRepo.GetAll();

            return View(new LocationViewModel());
        }

        [HttpPost]
        public IActionResult LocationOrderHistory(int id)
        {
            ViewBag.locations = _locationRepo.GetAll();
            var location = _locationRepo.GetById(id);
            LocationViewModel viewModel = new LocationViewModel
            {
                LocationId = location.Id,
                Name = location.Name,
            };
            var orderHistory = _orderRepo.GetOrderHistoryofLocation(location);
            viewModel.OrderHistory = orderHistory.Select(o => new OrdersViewModel
            {
                OrderId = o.OrderId,
                OrderDate = (DateTime)o.OrderTime,
                Location = o.StoreLocation.Name,
                OrderTotal = o.totalCost

            }).ToList();
            return View(viewModel);

        }

        public IActionResult ViewDetails(int OrderId)
        {
            try
            {
                Orders order = _orderRepo.GetById(OrderId);
                OrdersViewModel orderDetails = new OrdersViewModel
                {
                    OrderId = order.OrderId,
                    OrderDate = (DateTime)order.OrderTime,
                    OrderLine = order.OrderLine,
                    Location = order.StoreLocation.Name,
                    OrderTotal = order.totalCost
                };
                return View(orderDetails);
            }
            catch (Exception)
            {
                ViewData["ErrorMsg"] = "Order not found.";
                return View(new OrdersViewModel());
            }


        }
    }
}
