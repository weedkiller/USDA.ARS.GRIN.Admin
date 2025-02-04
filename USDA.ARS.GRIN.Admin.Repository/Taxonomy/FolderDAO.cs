﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Models.Taxonomy;


namespace USDA.ARS.GRIN.Admin.Repository
{
    public class FolderDAO : BaseDAO
    {
        public FolderDAO(string contextName)
        {
            _context = contextName;
        }

        public IEnumerable<Folder> Find(int cooperatorId)
        {
            const string COMMAND_TEXT = "usp_TaxonomyFoldersByUser_Select";
            List<Folder> folders = new List<Folder>();

            try
            {
                using (SqlConnection cn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    using (SqlCommand cmd = new SqlCommand(COMMAND_TEXT, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cooperator_id", cooperatorId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Folder folder = new Folder();
                                folder.ID = GetInt(reader["taxonomy_folder_id"].ToString());
                                folder.Title = reader["title"].ToString();
                                folder.Category = reader["category"].ToString();
                                folder.Description = reader["description"].ToString();
                                folder.DataSourceName = reader["data_source_name"].ToString();
                                folder.DataSourceTitle = reader["data_source_title"].ToString();
                                folder.IsShared = ParseBool(reader["is_shared"].ToString());
                                folder.IsFavorite = ParseBool(reader["is_favorite"].ToString());
                                folder.CreatedDate = GetDate(reader["created_date"].ToString());
                                folder.TotalItems = GetInt(reader["item_count"].ToString());
                                folders.Add(folder);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return folders.AsEnumerable();

        }
        public Folder Get(int id)
        {
            Folder folder = null;
            try
            {
                const string COMMAND_TEXT = "usp_TaxonomyFolder_Select";

                using (SqlConnection conn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    using (SqlCommand cmd = new SqlCommand(COMMAND_TEXT, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@taxonomy_folder_id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    folder = new Folder();
                                    folder.ID = GetInt(reader["taxonomy_folder_id"].ToString());
                                    folder.Title = reader["title"].ToString();
                                    folder.Category = reader["category"].ToString();
                                    folder.Description = reader["description"].ToString();
                                    folder.DataSourceName = reader["data_source_name"].ToString();
                                    folder.DataSourceTitle = reader["data_source_title"].ToString();
                                    folder.IsShared = ParseBool(reader["is_shared"].ToString()); 
                                    folder.IsFavorite = ParseBool(reader["is_favorite"].ToString());
                                    folder.CreatedDate = GetDate(reader["created_date"].ToString());
                                    folder.CreatedByCooperatorID = GetInt(reader["created_by"].ToString());
                                    folder.CreatedByCooperatorName = reader["created_by_name"].ToString();
                                    folder.ModifiedDate = GetDate(reader["modified_date"].ToString());
                                    folder.ModifiedByCooperatorID = GetInt(reader["modified_by"].ToString());
                                    folder.ModifiedByCooperatorName = reader["modified_by_name"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return folder;
        }
        public int Add(Folder folder)
        {
            int returnCode = 0;
            int taxonomyFolderId = 0;
            const string COMMAND_TEXT = "usp_TaxonomyFolder_Insert";
            string[] folderItemIdList;

            try
            {
                using (SqlConnection cn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    using (SqlCommand cmd = new SqlCommand(COMMAND_TEXT, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@title", folder.Title);

                        if (String.IsNullOrEmpty(folder.Category))
                            cmd.Parameters.AddWithValue("@category", "General");
                        else
                            cmd.Parameters.AddWithValue("@category", folder.Category);

                        if (String.IsNullOrEmpty(folder.Description))
                            cmd.Parameters.AddWithValue("@description", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@description", folder.Description);
                       
                        cmd.Parameters.AddWithValue("@is_shared", folder.IsShared);
                        cmd.Parameters.AddWithValue("@created_by", folder.CreatedByCooperatorID);
                        cmd.Parameters.AddWithValue("@data_source_name", folder.DataSourceName);

                        SqlParameter retErrorParam = new SqlParameter();
                        retErrorParam.SqlDbType = System.Data.SqlDbType.Int;
                        retErrorParam.ParameterName = "@out_error_number";
                        retErrorParam.Direction = System.Data.ParameterDirection.Output;
                        retErrorParam.Value = 0;
                        cmd.Parameters.Add(retErrorParam);

                        SqlParameter retParam = new SqlParameter();
                        retParam.SqlDbType = System.Data.SqlDbType.Int;
                        retParam.ParameterName = "@out_taxonomy_folder_id";
                        retParam.Direction = System.Data.ParameterDirection.Output;
                        retParam.Value = 0;
                        cmd.Parameters.Add(retParam);
                        cmd.ExecuteNonQuery();

                        //returnCode = Int32.Parse(cmd.Parameters["@out_error_number"].Value.ToString());
                        taxonomyFolderId = Int32.Parse(cmd.Parameters["@out_taxonomy_folder_id"].Value.ToString());

                        if (!String.IsNullOrEmpty(folder.ItemList))
                        {
                            folderItemIdList = folder.ItemList.Split(',');
                            foreach (var folderItemId in folderItemIdList)
                            {
                                int convertedFolderItemId = Int32.Parse(folderItemId);
                                FolderItem folderItem = new FolderItem { FolderID = taxonomyFolderId, ItemID = convertedFolderItemId, DataSource = folder.DataSourceName, CreatedByCooperatorID = folder.CreatedByCooperatorID };
                                AddFolderItemMap(folderItem);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }

        private string GetStoredProcedureName(string key)
        {
            switch (key)
            {
                case "taxonomy_cwr_crop":
                    return "usp_TaxonomyFolderCropForCWRItems_Select";
                case "taxonomy_cwr_map":
                    return "usp_TaxonomyFolderCWRMapItems_Select";
                case "taxonomy_cwr_trait":
                    return "usp_TaxonomyFolderCWRTraitItems_Select";
                case "taxonomy_species":
                    return "usp_TaxonomyFolderSpeciesItems_Select";
                case "taxonomy_genus":
                    return "usp_TaxonomyFolderGenusItems_Select";
                case "taxonomy_family":
                    return "usp_TaxonomyFolderFamilyItems_Select";
                case "citation":
                    return "usp_TaxonomyFolderCitationItems_Select";
                case "literature":
                    return "usp_TaxonomyFolderLiteratureItems_Select";
                default:
                    return "";
            }
        }
   
        public DataTable FindFolderItems(int folderId, string dataSource)
        {
            DataTable results = new DataTable();
            Folder folder = new Folder();

            try
            {
                using (SqlConnection conn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = GetStoredProcedureName(dataSource);
                        cmd.Parameters.AddWithValue("@taxonomy_folder_id", folderId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(results);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return results;
        }
        public int AddFolderItemMap(FolderItem folderItem)
        {
            int returnCode = 0;
            int taxonomyFolderItemMapId = 0;
            const string COMMAND_TEXT = "usp_TaxonomyFolderItemMap_Insert";
           
            try
            {
                using (SqlConnection cn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    using (SqlCommand cmd = new SqlCommand(COMMAND_TEXT, cn))
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = COMMAND_TEXT;

                        cmd.Parameters.AddWithValue("@taxonomy_folder_id", folderItem.FolderID);
                        cmd.Parameters.AddWithValue("@item_id", folderItem.ItemID);
                        cmd.Parameters.AddWithValue("@data_source", folderItem.DataSource);
                        cmd.Parameters.AddWithValue("@created_by", folderItem.CreatedByCooperatorID);

                        SqlParameter retErrorParam = new SqlParameter();
                        retErrorParam.SqlDbType = System.Data.SqlDbType.Int;
                        retErrorParam.ParameterName = "@out_error_number";
                        retErrorParam.Direction = System.Data.ParameterDirection.Output;
                        retErrorParam.Value = 0;
                        cmd.Parameters.Add(retErrorParam);

                        SqlParameter retParam = new SqlParameter();
                        retParam.SqlDbType = System.Data.SqlDbType.Int;
                        retParam.ParameterName = "@out_taxonomy_folder_item_map_id";
                        retParam.Direction = System.Data.ParameterDirection.Output;
                        retParam.Value = 0;
                        cmd.Parameters.Add(retParam);
                        cmd.ExecuteNonQuery();

                        //returnCode = Int32.Parse(cmd.Parameters["@out_error_number"].Value.ToString());
                        taxonomyFolderItemMapId = Int32.Parse(cmd.Parameters["@out_taxonomy_folder_item_map_id"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public int Update(Folder folder)
        {
            int returnCode = 0;
            const string COMMAND_TEXT = "usp_TaxonomyFolder_Update";

            try
            {
                using (SqlConnection cn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    using (SqlCommand cmd = new SqlCommand(COMMAND_TEXT, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@taxonomy_folder_id", folder.ID);
                        cmd.Parameters.AddWithValue("@title", folder.Title);
                        cmd.Parameters.AddWithValue("@category", folder.Category);
                        if (String.IsNullOrEmpty(folder.Description))
                        {
                            cmd.Parameters.AddWithValue("@description", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@description", folder.Description);
                        }

                        if (String.IsNullOrEmpty(folder.Note))
                        {
                            cmd.Parameters.AddWithValue("@note", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@note", folder.Note);
                        }
                        
                        cmd.Parameters.AddWithValue("is_shared", ConvertBool(folder.IsShared));
                        cmd.Parameters.AddWithValue("is_favorite", ConvertBool(folder.IsFavorite));
                        cmd.Parameters.AddWithValue("modified_by", folder.ModifiedByCooperatorID);

                        SqlParameter errorParam = new SqlParameter();
                        errorParam.SqlDbType = System.Data.SqlDbType.Int;
                        errorParam.ParameterName = "@out_error_number";
                        errorParam.Direction = System.Data.ParameterDirection.Output;
                        errorParam.Value = 0;
                        cmd.Parameters.Add(errorParam);

                        returnCode = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public int Delete(int folderId)
        {
            int returnCode = 0;
            const string COMMAND_TEXT = "usp_TaxonomyFolder_Delete";

            try
            {
                using (SqlConnection cn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = COMMAND_TEXT;

                    cmd.Parameters.AddWithValue("@taxonomy_folder_id", folderId);
                    returnCode = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public int AddItem(FolderItem folderItem)
        {

            return 0;
        }
        public int RemoveItem(int itemId)
        {
            int returnCode = 0;
            const string COMMAND_TEXT = "usp_TaxonomyFolderItemMap_Delete";

            try
            {
                using (SqlConnection cn = DataContext.GetConnection(this.GetConnectionStringKey(_context)))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = COMMAND_TEXT;

                    cmd.Parameters.AddWithValue("@taxonomy_folder_item_map_id", itemId);
                    returnCode = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnCode;
        }
        public ResultContainer RemoveFolderItems(string folderItemIdList)
        {
            ResultContainer resultContainer = new ResultContainer();

            try
            {
                string[] folderItemIdArray = folderItemIdList.Split(',');
                foreach(string folderItemId in folderItemIdArray)
                {
                    int convertedFolderItemId = Int32.Parse(folderItemId);
                    RemoveItem(convertedFolderItemId);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultContainer;
        }
    }
}
