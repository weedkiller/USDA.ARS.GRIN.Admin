﻿using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USDA.ARS.GRIN.Admin.WebUI.ViewModels.Taxonomy
{
    public class SpeciesHomeViewModel : BaseViewModel
    {
        public SpeciesHomeViewModel()
        {
            DataSourceName = "taxonomy_species";
        }

    }
}