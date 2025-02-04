﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Models.Taxonomy;
using USDA.ARS.GRIN.Admin.Service;

namespace USDA.ARS.GRIN.Admin.WebUI.ViewModels.Taxonomy
{
    public class ManagerViewModel: BaseViewModel
    {
        public List<CropForCWR> CropsForCWR { get; set; }
        public List<Species> Species { get; set; }
        public List<CWRTrait> CWRTraits { get; set; }
    }
}