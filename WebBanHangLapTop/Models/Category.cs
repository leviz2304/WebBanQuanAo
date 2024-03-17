﻿using System.ComponentModel.DataAnnotations;

namespace WebBanHangLapTop.Models
{
    //lop dac biet chi khai bao model
    public class Category
    {
		
		public int Id { get; set; }
		[Required, StringLength(50)]
		public string Name { get; set; }
		

		public virtual ICollection<Product> Products { get; set; }
	}
}
