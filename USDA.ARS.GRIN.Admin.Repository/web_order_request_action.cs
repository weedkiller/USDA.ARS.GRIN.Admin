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
    
    public partial class web_order_request_action
    {
        public int web_order_request_action_id { get; set; }
        public int web_order_request_id { get; set; }
        public string action_code { get; set; }
        public System.DateTime acted_date { get; set; }
        public string action_for_id { get; set; }
        public string note { get; set; }
        public Nullable<int> web_cooperator_id { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual web_cooperator web_cooperator { get; set; }
        public virtual web_order_request web_order_request { get; set; }
        public virtual web_user web_user { get; set; }
        public virtual web_user web_user1 { get; set; }
        public virtual web_user web_user2 { get; set; }
    }
}
