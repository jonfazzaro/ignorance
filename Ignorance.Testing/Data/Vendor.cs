//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Ignorance.Testing.Data
{
    public partial class Vendor
    {
        public Vendor()
        {
            this.ProductVendors = new HashSet<ProductVendor>();
            this.PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
            this.VendorAddresses = new HashSet<VendorAddress>();
            this.VendorContacts = new HashSet<VendorContact>();
        }
    
        public int VendorID { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public byte CreditRating { get; set; }
        public bool PreferredVendorStatus { get; set; }
        public bool ActiveFlag { get; set; }
        public string PurchasingWebServiceURL { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual ICollection<ProductVendor> ProductVendors { get; set; }
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public virtual ICollection<VendorAddress> VendorAddresses { get; set; }
        public virtual ICollection<VendorContact> VendorContacts { get; set; }
    }
    
}
