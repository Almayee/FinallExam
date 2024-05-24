using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Core.Models
{
	public class Product:BaseEntity
	{
		
		public string? ImageUrl { get; set; }
		[NotMapped]
		public IFormFile? ImageFile { get; set; }
	}
}
