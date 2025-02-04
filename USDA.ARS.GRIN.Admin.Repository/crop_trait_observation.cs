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
    
    public partial class crop_trait_observation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public crop_trait_observation()
        {
            this.crop_trait_observation_data = new HashSet<crop_trait_observation_data>();
        }
    
        public int crop_trait_observation_id { get; set; }
        public int inventory_id { get; set; }
        public int crop_trait_id { get; set; }
        public Nullable<int> crop_trait_code_id { get; set; }
        public Nullable<decimal> numeric_value { get; set; }
        public string string_value { get; set; }
        public int method_id { get; set; }
        public string is_archived { get; set; }
        public string data_quality_code { get; set; }
        public string original_value { get; set; }
        public Nullable<decimal> frequency { get; set; }
        public Nullable<int> rank { get; set; }
        public Nullable<decimal> mean_value { get; set; }
        public Nullable<decimal> maximum_value { get; set; }
        public Nullable<decimal> minimum_value { get; set; }
        public Nullable<decimal> standard_deviation { get; set; }
        public Nullable<int> sample_size { get; set; }
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
        public virtual crop_trait crop_trait { get; set; }
        public virtual crop_trait_code crop_trait_code { get; set; }
        public virtual inventory inventory { get; set; }
        public virtual method method { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crop_trait_observation_data> crop_trait_observation_data { get; set; }
    }
}
