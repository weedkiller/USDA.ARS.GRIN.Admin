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
    
    public partial class feedback_report_attach
    {
        public int feedback_report_attach_id { get; set; }
        public int feedback_report_id { get; set; }
        public string virtual_path { get; set; }
        public string thumbnail_virtual_path { get; set; }
        public Nullable<int> sort_order { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string content_type { get; set; }
        public string category_code { get; set; }
        public string is_web_visible { get; set; }
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
        public virtual feedback_report feedback_report { get; set; }
    }
}
