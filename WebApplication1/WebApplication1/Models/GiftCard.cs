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
    public class GiftCard
    {
        public GiftCard()
        {
            Receipts = new List<Receipt>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GiftCardID { get; set; }
        [Required]
        public string StoreID { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        [Range(50.00 ,1000.00)]
        public Double Credit { get; set; }
        [Required(ErrorMessage = "First Name  is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name  is required")]
        public string LastName { get; set; }
        [StringLength(450)]
        [Index("Email", 4, IsUnique = false)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string  GiftCardValid { get; set; }
        public int Items { get; set; }  
        public string FromWho { get; set; }
       
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }


        public virtual ICollection<Receipt> Receipts { get; set; }


    }
}