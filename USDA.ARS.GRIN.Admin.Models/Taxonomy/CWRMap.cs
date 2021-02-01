﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USDA.ARS.GRIN.Admin.Models.Taxonomy
{
    public class CWRMap : BaseModel
    {
        public int ID { get; set; }
        public int SpeciesID { get; set; }
        public string SpeciesName { get; set; }
        public int CropID { get; set; }
        public string CommonName { get; set; }
        public bool IsCrop { get; set; }
        public string GenepoolCode { get; set; }
        public bool IsGraftStock { get; set; }
        public bool IsPotential { get; set; }
        public int CitationID { get; set; }
        public string Note { get; set; }
        public List<CWRTrait> CWRTraits { get; set; }

        public CWRMap()
        {
            CWRTraits = new List<CWRTrait>();
        }
    }
}
