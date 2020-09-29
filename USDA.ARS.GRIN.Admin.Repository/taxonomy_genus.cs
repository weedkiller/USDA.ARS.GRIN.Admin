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
    
    public partial class taxonomy_genus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public taxonomy_genus()
        {
            this.citations = new HashSet<citation>();
            this.taxonomy_alt_family_map = new HashSet<taxonomy_alt_family_map>();
            this.taxonomy_attach = new HashSet<taxonomy_attach>();
            this.taxonomy_common_name = new HashSet<taxonomy_common_name>();
            this.taxonomy_family = new HashSet<taxonomy_family>();
            this.taxonomy_genus1 = new HashSet<taxonomy_genus>();
            this.taxonomy_species = new HashSet<taxonomy_species>();
        }
    
        public int taxonomy_genus_id { get; set; }
        public Nullable<int> current_taxonomy_genus_id { get; set; }
        public int taxonomy_family_id { get; set; }
        public string qualifying_code { get; set; }
        public string hybrid_code { get; set; }
        public string genus_name { get; set; }
        public string genus_authority { get; set; }
        public string subgenus_name { get; set; }
        public string section_name { get; set; }
        public string subsection_name { get; set; }
        public string series_name { get; set; }
        public string subseries_name { get; set; }
        public string note { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<citation> citations { get; set; }
        public virtual cooperator cooperator { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_alt_family_map> taxonomy_alt_family_map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_attach> taxonomy_attach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_common_name> taxonomy_common_name { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_family> taxonomy_family { get; set; }
        public virtual taxonomy_family taxonomy_family1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_genus> taxonomy_genus1 { get; set; }
        public virtual taxonomy_genus taxonomy_genus2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_species> taxonomy_species { get; set; }
    }
}
