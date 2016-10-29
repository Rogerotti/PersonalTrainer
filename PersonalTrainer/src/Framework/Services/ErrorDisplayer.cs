using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace Framework.Services
{
    public class ErrorDisplayer : IErrorDisplayer
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITempDataDictionaryFactory tempDataDictionaryFactory;
        private const String errorDisplayeId = nameof(errorDisplayeId);

        private List<String> errors;

        public ErrorDisplayer(IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            this.tempDataDictionaryFactory = tempDataDictionaryFactory;
            this.httpContextAccessor = httpContextAccessor;
            errors = new List<String>();
        }

        public void AddError(IEnumerable<String> errors)
        {
            this.errors.AddRange(errors);
        }

        public void AddError(String error)
        {
            errors.Add(error);
        }

        public void Clear()
        {
            var tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
            errors.Clear();
            if (tempDataDictionary.ContainsKey(errorDisplayeId))
                tempDataDictionary.Remove(errorDisplayeId);
        }

        public void Display()
        {
            var tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
            var er = new List<String>();
            er.AddRange(errors);
            tempDataDictionary[errorDisplayeId] = er;
            errors.Clear();
        }
    }
}
