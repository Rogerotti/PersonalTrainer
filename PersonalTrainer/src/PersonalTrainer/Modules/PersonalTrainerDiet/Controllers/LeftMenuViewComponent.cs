﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalTrainerDiet.Controllers
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public LeftMenuViewComponent()
        {
          
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
    }
}