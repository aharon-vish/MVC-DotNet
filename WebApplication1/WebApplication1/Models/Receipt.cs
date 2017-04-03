using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Sql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Receipt
    {
        [Key]
        public int IDReceipt { get; set; }
        public int PurchaseAmount { get; set; }
        public string DatePurchas { get; set; }
        public string StoreId { get; set; }
        public virtual int GiftCardID { get; set; }
        public virtual GiftCard GiftCard { get; set; }

    }

}