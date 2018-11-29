using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Northwind.Web.Helpers
{
    public static class ModelStateHelper
    {
        public static string ToJson(ModelStateDictionary modelState) {
            return JsonConvert.SerializeObject(modelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            ));
        }
    }
}