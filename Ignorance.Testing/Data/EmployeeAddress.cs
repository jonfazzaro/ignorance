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
    public partial class EmployeeAddress
    {
        public int EmployeeID { get; set; }
        public int AddressID { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Address Address { get; set; }
    }
    
}
