﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USDA.ARS.GRIN.Admin.Models.GRINGlobal;

namespace USDA.ARS.GRIN.Admin.Models
{
    public class Cooperator : BaseModel, IEntity
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Organization { get; set; }
        public string Job { get; set; }
        public string EmailAddress { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public WebCooperator WebCooperator { get; set; }
        public Address Address { get; set; }
        public Site Site { get; set; }
        public Cooperator()
        {
            WebCooperator = new WebCooperator();
            Address = new Address();
            Site = new Site();
        }
    }
}
