﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USDA.ARS.GRIN.Admin.Models.Taxonomy
{
    public class Protologue : IEntity
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByCooperatorID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedByCooperatorID { get; set; }
        public DateTime OwnedDate { get; set; }
        public int OwnedByCooperatorID { get; set; }
    }
}
