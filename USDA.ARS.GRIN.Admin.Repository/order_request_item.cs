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
    
    public partial class order_request_item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order_request_item()
        {
            this.inventory_viability_data = new HashSet<inventory_viability_data>();
            this.order_request_item_action = new HashSet<order_request_item_action>();
        }
    
        public int order_request_item_id { get; set; }
        public int order_request_id { get; set; }
        public Nullable<int> web_order_request_item_id { get; set; }
        public Nullable<int> sequence_number { get; set; }
        public string name { get; set; }
        public Nullable<decimal> quantity_shipped { get; set; }
        public string quantity_shipped_unit_code { get; set; }
        public string distribution_form_code { get; set; }
        public string status_code { get; set; }
        public Nullable<System.DateTime> status_date { get; set; }
        public int inventory_id { get; set; }
        public string external_taxonomy { get; set; }
        public Nullable<int> source_cooperator_id { get; set; }
        public string note { get; set; }
        public string web_user_note { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual cooperator cooperator { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        public virtual cooperator cooperator3 { get; set; }
        public virtual inventory inventory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inventory_viability_data> inventory_viability_data { get; set; }
        public virtual order_request order_request { get; set; }
        public virtual web_order_request_item web_order_request_item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_request_item_action> order_request_item_action { get; set; }
    }
}
