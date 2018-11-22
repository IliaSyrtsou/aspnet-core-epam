using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.Models.Api
{
    public class QueryObject
    {
        [Required]
        public int? PageSize {get;set;}
        [Required]
        public int? PageNumber {get;set;}
        public string OrderBy {get;set;}
    }
}