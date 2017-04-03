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
    public class User
    {
        public User()
        {
             GiftCards = new List<GiftCard>();
        }
        [Key]
        public int IDUser { get; set; }
        [StringLength(450)]
        [Remote("doesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        [Index("Email", 1, IsUnique = true)]
        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name  is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name  is required.")]
        public string LastName { get; set; }       
        [DataType(DataType.Password)]
        [StringLength(350, MinimumLength = 5, ErrorMessage = "Minimum 5 characters required")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string PasswardSalt { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
      
       
        public virtual ICollection <GiftCard> GiftCards { get; set; }

    }

}