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
    
    public partial class taxonomy_alt_family_map
    {
        public int taxonomy_alt_family_map_id { get; set; }
        public int taxonomy_genus_id { get; set; }
        public int taxonomy_family_id { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual cooperator cooperator { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        public virtual taxonomy_family taxonomy_family { get; set; }
        public virtual taxonomy_genus taxonomy_genus { get; set; }
    }
}
