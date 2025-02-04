﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Caching;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Service;

namespace USDA.ARS.GRIN.Admin.WebUI.ViewModels.Taxonomy
{
    public class FolderEditViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string DataSourceName { get; set; }
        public string DataSourceTitle { get; set; }
        public bool IsShared { get; set; }
        public bool IsFavorite { get; set; }
        public DataTable SearchResults { get; set; }
    }
}