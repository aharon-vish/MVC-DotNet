//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularStart1
{
    using System;
    using System.Collections.Generic;
    
    public partial class GiftCards
    {
        public GiftCards()
        {
            this.Receipts = new HashSet<Receipts>();
        }
    
        public int GiftCardID { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public double Credit { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string GiftCardValid { get; set; }
        public string FromWho { get; set; }
        public int UserId { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ICollection<Receipts> Receipts { get; set; }
    }
}
