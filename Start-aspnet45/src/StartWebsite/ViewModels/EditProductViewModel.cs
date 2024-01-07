using System.Collections.Generic;
using System.Web.Mvc;
using Start.Models;

namespace Start.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}