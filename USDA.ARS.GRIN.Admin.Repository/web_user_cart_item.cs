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
    
    public partial class web_user_cart_item
    {
        public int web_user_cart_item_id { get; set; }
        public int web_user_cart_id { get; set; }
        public int accession_id { get; set; }
        public int quantity { get; set; }
        public string form_type_code { get; set; }
        public string usage_code { get; set; }
        public string note { get; set; }
        public System.DateTime created_date { get; set; }
        public int created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public System.DateTime owned_date { get; set; }
        public int owned_by { get; set; }
    
        public virtual accession accession { get; set; }
        public virtual web_user web_user { get; set; }
        public virtual web_user web_user1 { get; set; }
        public virtual web_user web_user2 { get; set; }
        public virtual web_user_cart web_user_cart { get; set; }
    }
}
