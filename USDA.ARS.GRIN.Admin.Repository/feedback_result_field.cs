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
    
    public partial class feedback_result_field
    {
        public int feedback_result_field_id { get; set; }
        public int feedback_result_id { get; set; }
        public int feedback_form_field_id { get; set; }
        public string string_value { get; set; }
        public string admin_value { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual cooperator cooperator { get; set; }
        public virtual cooperator cooperator1 { get; set; }
        public virtual cooperator cooperator2 { get; set; }
        public virtual feedback_form_field feedback_form_field { get; set; }
        public virtual feedback_result feedback_result { get; set; }
    }
}
