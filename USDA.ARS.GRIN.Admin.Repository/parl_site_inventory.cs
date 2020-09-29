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
    
    public partial class parl_site_inventory
    {
        public int parl_site_inventory_id { get; set; }
        public int inventory_id { get; set; }
        public Nullable<int> seed_quantity { get; set; }
        public Nullable<int> seed_quantity_type { get; set; }
        public string seed_age { get; set; }
        public Nullable<decimal> hundred_weight { get; set; }
        public string increase_location { get; set; }
        public Nullable<int> increase_year { get; set; }
        public string lot_type_code { get; set; }
        public string pollination_vector_code { get; set; }
        public string pollination_environment { get; set; }
        public string pollination_procedure_code { get; set; }
        public string plot { get; set; }
        public string note { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual cooperator cooperator { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        public virtual inventory inventory { get; set; }
    }
}
