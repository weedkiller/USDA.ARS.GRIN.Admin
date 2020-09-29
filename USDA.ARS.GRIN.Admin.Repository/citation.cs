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
    
    public partial class citation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public citation()
        {
            this.taxonomy_common_name = new HashSet<taxonomy_common_name>();
            this.taxonomy_cwr = new HashSet<taxonomy_cwr>();
            this.taxonomy_cwr_priority = new HashSet<taxonomy_cwr_priority>();
            this.taxonomy_geography_map = new HashSet<taxonomy_geography_map>();
            this.taxonomy_use = new HashSet<taxonomy_use>();
        }
    
        public int citation_id { get; set; }
        public Nullable<int> literature_id { get; set; }
        public string citation_title { get; set; }
        public string author_name { get; set; }
        public Nullable<int> citation_year { get; set; }
        public string reference { get; set; }
        public string doi_reference { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<int> accession_id { get; set; }
        public Nullable<int> method_id { get; set; }
        public Nullable<int> taxonomy_species_id { get; set; }
        public Nullable<int> taxonomy_genus_id { get; set; }
        public Nullable<int> taxonomy_family_id { get; set; }
        public Nullable<int> accession_ipr_id { get; set; }
        public Nullable<int> accession_pedigree_id { get; set; }
        public Nullable<int> genetic_marker_id { get; set; }
        public string type_code { get; set; }
        public Nullable<int> unique_key { get; set; }
        public string is_accepted_name { get; set; }
        public string note { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual accession accession { get; set; }
        public virtual accession_ipr accession_ipr { get; set; }
        public virtual accession_pedigree accession_pedigree { get; set; }
        public virtual cooperator cooperator { get; set; }
        public virtual genetic_marker genetic_marker { get; set; }
        public virtual literature literature { get; set; }
        public virtual method method { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        public virtual taxonomy_family taxonomy_family { get; set; }
        public virtual taxonomy_genus taxonomy_genus { get; set; }
        public virtual taxonomy_species taxonomy_species { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_common_name> taxonomy_common_name { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_cwr> taxonomy_cwr { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_cwr_priority> taxonomy_cwr_priority { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_geography_map> taxonomy_geography_map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxonomy_use> taxonomy_use { get; set; }
    }
}
