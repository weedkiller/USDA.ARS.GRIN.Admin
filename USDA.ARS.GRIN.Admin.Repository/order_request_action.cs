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
    
    public partial class order_request_action
    {
        public int order_request_action_id { get; set; }
        public int order_request_id { get; set; }
        public string action_name_code { get; set; }
        public System.DateTime started_date { get; set; }
        public string started_date_code { get; set; }
        public Nullable<System.DateTime> completed_date { get; set; }
        public string completed_date_code { get; set; }
        public string action_information { get; set; }
        public Nullable<decimal> action_cost { get; set; }
        public Nullable<int> cooperator_id { get; set; }
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
        public virtual cooperator cooperator3 { get; set; }
        public virtual order_request order_request { get; set; }
    }
}
