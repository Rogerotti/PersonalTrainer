using Framework.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalTrainerCore.Controllers
{
    public class ErrorDisplayerViewComponent : ViewComponent
    {
        private readonly ITempDataDictionaryFactory tempDataDictionaryFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IErrorDisplayer errorDisplayer;
        private const String errorDisplayeId = nameof(errorDisplayeId);

        public ErrorDisplayerViewComponent(
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory,
            IErrorDisplayer errorDisplayer)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tempDataDictionaryFactory = tempDataDictionaryFactory;
            this.errorDisplayer = errorDisplayer;
        }

        public async Task<IViewComponentResult> InvokeAsync(HttpContext context)
        {
            object ErrorList;
            var tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
            tempDataDictionary.TryGetValue(errorDisplayeId, out ErrorList);

            var list =   ErrorList as List<String>;
            return await Task.Run(() =>
            {
                return View(ErrorList);
            });
        }
    }
}

