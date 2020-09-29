//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace USDA.ARS.GRIN.Admin.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class web_order_request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public web_order_request()
        {
            this.order_request = new HashSet<order_request>();
            this.web_order_request_action = new HashSet<web_order_request_action>();
            this.web_order_request_address = new HashSet<web_order_request_address>();
            this.web_order_request_attach = new HashSet<web_order_request_attach>();
            this.web_order_request_item = new HashSet<web_order_request_item>();
        }
    
        public int web_order_request_id { get; set; }
        public Nullable<int> web_cooperator_id { get; set; }
        public Nullable<System.DateTime> ordered_date { get; set; }
        public string intended_use_code { get; set; }
        public string intended_use_note { get; set; }
        public string status_code { get; set; }
        public string note { get; set; }
        public string special_instruction { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_request> order_request { get; set; }
        public virtual web_cooperator web_cooperator { get; set; }
        public virtual web_user web_user { get; set; }
        public virtual web_user web_user1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<web_order_request_action> web_order_request_action { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<web_order_request_address> web_order_request_address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<web_order_request_attach> web_order_request_attach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<web_order_request_item> web_order_request_item { get; set; }
    }
}
