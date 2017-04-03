using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Store
    {
        public int StoreID { set; get; }
        public string NameOfStroe { get; set; }
        [MaxLength]
        public byte[] Image { get; set; }
        public string ContactUs { get; set;}
        public string StoreHours { get; set; }
       

    }

}