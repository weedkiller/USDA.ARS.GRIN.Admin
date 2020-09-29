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
    
    public partial class taxonomy_cwr_priority
    {
        public int taxonomy_cwr_priority_id { get; set; }
        public int taxonomy_species_id { get; set; }
        public int priority_year { get; set; }
        public string status_code { get; set; }
        public string in_situ_code { get; set; }
        public string ex_situ_code { get; set; }
        public Nullable<int> citation_id { get; set; }
        public string note { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual citation citation { get; set; }
        public virtual cooperator cooperator { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        public virtual taxonomy_species taxonomy_species { get; set; }
    }
}
