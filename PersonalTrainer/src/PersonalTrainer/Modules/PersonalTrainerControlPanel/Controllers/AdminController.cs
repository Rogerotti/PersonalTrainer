﻿using Framework.Models.Dto;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PersonalTrainerControlPanel.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserManagement userManagement;
        private readonly IProductManagement productManagement;

        public AdminController(
            IUserManagement userManagement,
            IProductManagement productManagement)
        {
            this.userManagement = userManagement;
            this.productManagement = productManagement;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            try
            {
                var users = userManagement.GetAllUsers();
                return View(users);
            }
            catch (Exception exc)
            {
                ModelState.TryAddModelError("AdditionalValidation", exc.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult AcceptSubscription(String productId)
        {
            try
            {
                productManagement.AcceptSubscription(new Guid(productId));
            }
            catch (Exception exc)
            {
                ModelState.TryAddModelError("AdditionalValidation", exc.Message);
            }
            return RedirectToAction("Products", "Admin");
        }

        [HttpGet]
        public IActionResult DeclineSubscription(String productId)
        {
            try
            {
                productManagement.DeclineSubscription(new Guid(productId));
            }
            catch (Exception exc)
            {
                ModelState.TryAddModelError("AdditionalValidation", exc.Message);
            }
            return RedirectToAction("Products", "Admin");
        }

        [HttpGet]
        public IActionResult SubscribeProduct(String productId)
        {
            try
            {
                productManagement.SubscribeProduct(new Guid(productId));
            }
            catch (Exception exc)
            {
                ModelState.TryAddModelError("AdditionalValidation", exc.Message);
            }
            return RedirectToAction("Products", "Admin");
        }

        [HttpGet]
        public IActionResult Products()
        {
            try
            {
                var pendingProducts = productManagement.GetProducts();
                return View(pendingProducts);
            }
            catch (Exception exc)
            {
                ModelState.TryAddModelError("AdditionalValidation", exc.Message);
                return View(new List<ProductDto>());
            }
        }

        [HttpPost]
        public JsonResult GetUserDetails([FromBody]JToken jsonBody)
        {
            var id = jsonBody.Value<String>("Id");
            var userDetails = userManagement.GetUser(new Guid(id));
            return new JsonResult(userDetails);
        }
    }
}
