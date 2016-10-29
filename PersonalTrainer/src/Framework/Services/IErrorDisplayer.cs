using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Services
{
    public interface IErrorDisplayer
    {
        void AddError(String error);

        void AddError(IEnumerable<String> errors);

        void Clear();

        void Display();
    }
}
