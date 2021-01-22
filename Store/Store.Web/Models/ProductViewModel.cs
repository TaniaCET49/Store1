using Microsoft.AspNetCore.Http;
using Store.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
	public class ProductViewModel : Product
	{
		[Display(Name = "Image")]
		public IFormFile ImageFile { get; set; }
	}
}
