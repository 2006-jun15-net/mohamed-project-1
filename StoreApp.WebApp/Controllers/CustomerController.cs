using System;
using System.Linq;
using System.Security.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Models;
using StoreApp.WebApp.ViewModels;

namespace StoreApp.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerepo)
        {
            _customerRepo = customerepo;
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer( CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                Library.Models.Customer customer = new Library.Models.Customer(viewModel.FirstName, viewModel.LastName);
                _customerRepo.Add(customer);
                customer = _customerRepo.SearchCustomerByName(viewModel.FirstName);
                TempData["CustomerID"] = customer.customerID;
                return RedirectToAction(nameof(ViewDetails), new { id = customer.customerID });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "there was some error, try again.");
                return View();
            }
        }

        public IActionResult ViewDetails(int id)
        {
            try
            {
                Customer customer = _customerRepo.GetById(id);
                var viewModel = new CustomerViewModel
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    //CustomerId = customer.customerID
                };
                TempData["CustomerID"] = customer.customerID;
                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMsg"] = "Customer not found.";
                return RedirectToAction(nameof(SearchCustomer));
            }

        }

        public IActionResult SearchCustomer(string query)
        {
            if (query != null)
            {
                string x = query.ToLower();
                if (_customerRepo.GetAll().Any(c => c.FirstName.ToLower().Equals(x)))
                {
                    int cid = _customerRepo.SearchCustomerByName(query).customerID;
                    return RedirectToAction(nameof(ViewDetails), new { id = cid });
                }
                return View();
            }
            return View();
            
        }


        public IActionResult Edit(int customerID)
        {
            try
            {
                Customer customer = _customerRepo.GetById(customerID);
                var viewModel = new CustomerViewModel
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    //CustomerId = customer.customerID
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMsg"] = "Customer not found.";
                return RedirectToAction(nameof(SearchCustomer));
            }
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        public IActionResult Edit([Bind("FirstName, LastName")] CustomerViewModel viewModel, [FromRoute] int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = _customerRepo.GetById(id);
                    customer.FirstName = viewModel.FirstName;
                    customer.LastName = viewModel.LastName;
                    customer.customerID = viewModel.CustomerId;
                    _customerRepo.Update(customer);

                    return RedirectToAction(nameof(ViewDetails), new { customerID = customer.customerID });
                }
                return View(viewModel);
            }
            catch (Exception)
            {
                return View(viewModel);
            }

        }

    }
}
