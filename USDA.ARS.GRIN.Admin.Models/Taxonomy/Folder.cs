﻿using System.Data;

namespace USDA.ARS.GRIN.Admin.Models.Taxonomy
{
    public class Folder : BaseModel
    {
        public int ID { get; set; }
        public string DataSourceName { get; set; }
        public string DataSourceTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsShared { get; set; }
        public bool IsFavorite { get; set; }
        public int TotalItems { get; set; }
        public string ItemList { get; set; }
        public string QuerySQL { get; set; }
        public DataTable SearchResults { get; set; }
        public string Note { get; set; }
        public DataTable FolderItems { get; set; }
    }
}
