﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Models.Taxonomy;
using USDA.ARS.GRIN.Admin.Service;
using USDA.ARS.GRIN.Admin.WebUI.ViewModels;
using USDA.ARS.GRIN.Admin.WebUI.ViewModels.Taxonomy;
using NLog;
using System.Web.UI.WebControls.WebParts;
using USDA.ARS.GRIN.Admin.WebUI.ViewModels.Taxonomy;

namespace USDA.ARS.GRIN.Admin.WebUI.Controllers
{
    [GrinGlobalAuthentication]
    public class TaxonomyController : BaseController
    {
        const string BASE_PATH = "~/Views/Taxonomy/";
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            IndexViewModel viewModel = null; 
            try
            {
                TempData["context"] = "Taxonomy Home";
                viewModel = new IndexViewModel();
                //taxonomyService.FindCachedSpecies();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error Occurred");
                return RedirectToAction("InternalServerError", "Error");
            }
            return View("~/Views/Taxonomy/Index.cshtml", viewModel);
        }

        //***********************************************************************************************************
        // CROP FOR CWR
        //***********************************************************************************************************
        #region Crop for CWR

        [Route("Taxonomy/CropForCWR/Home")]
        public ActionResult CropForCWRHome()
        {
            TempData["context"] = "Crop For CWR Home";
            CropForCWRHomeViewModel viewModel = new CropForCWRHomeViewModel();
            viewModel.Cooperators = new SelectList(AuthenticatedUserSession.Application.Cooperators, "ID", "FullName");
            viewModel.DefaultCooperatorID = AuthenticatedUser.CooperatorID;
            return View("~/Views/Taxonomy/CropForCWR/Index.cshtml", viewModel);
        }
       
        [HttpGet]
        public ActionResult CropForCWREdit(int id = 0)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            CropForCWRViewModel cropForCWRViewModel = new CropForCWRViewModel();

            try
            {
                cropForCWRViewModel.CWRTraitViewModel = new CWRTraitViewModel(_taxonomyService.GetTraitClassCodes(), _taxonomyService.GetBreedingTypeCodes());
                if (id > 0)
                {
                    TempData["context"] = "Edit Crop for CWR";
                    
                    CropForCWR cropForCwr = _taxonomyService.GetCropForCWR(id);
                    cropForCWRViewModel.ID = cropForCwr.ID;
                    cropForCWRViewModel.CropName = cropForCwr.CropName;
                    cropForCWRViewModel.Note = cropForCwr.Note;
                    cropForCWRViewModel.CreatedDate = cropForCwr.CreatedDate;
                    cropForCWRViewModel.CreatedByCooperatorID = cropForCwr.CreatedByCooperatorID;
                    cropForCWRViewModel.CreatedByCooperatorName = cropForCwr.CreatedByCooperatorName;
                    cropForCWRViewModel.ModifiedDate = cropForCwr.ModifiedDate;
                    cropForCWRViewModel.ModifiedByCooperatorID = cropForCwr.ModifiedByCooperatorID;
                    cropForCWRViewModel.ModifiedByCooperatorName = cropForCwr.ModifiedByCooperatorName;
                    cropForCWRViewModel.CWRMaps = cropForCwr.CWRMaps;
                    cropForCWRViewModel.CWRTraitViewModel.Citations = new SelectList(_taxonomyService.FindCitations(cropForCWRViewModel.SpeciesID), "ID", "Title");
                }
                else
                {
                    TempData["context"] = "Add Crop for CWR";
                    
                    cropForCWRViewModel.CWRTraitViewModel.Citations = new SelectList(new List<Citation>(), "ID", "Title");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return View("~/Views/Taxonomy/CropForCWR/Edit.cshtml", cropForCWRViewModel);
        }

        [HttpPost]
        public ActionResult CropForCWREdit(CropEditViewModel viewModel)
        {
            CropForCWR crop = new CropForCWR();
            ResultContainer resultContainer = new ResultContainer();
             TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            if (!ModelState.IsValid)
            {
                return View("~/Views/Taxonomy/CropForCWR/Edit.cshtml", viewModel);
            }

            try
            {
                crop.ID = viewModel.ID;
                crop.CropName = viewModel.CropName;
                crop.Note = viewModel.Note;

                if (viewModel.ID > 0)
                {
                    crop.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.UpdateCropForCWR(crop);
                }
                else
                {
                    crop.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.AddCropForCWR(crop);
                }

                //if (resultContainer.ResultCode == "2601")
                //{
                //    viewModel.ErrorMessage = "The crop name must be unique.";
                //    return View("~/Views/Taxonomy/CropForCWR/Edit.cshtml", viewModel);
                //}
                crop.ID = resultContainer.EntityID;
                return RedirectToAction("CropForCWREdit", "Taxonomy", new { id = crop.ID });
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CropForCWRListByUser(int cooperatorId = 0)
        {
            CropForCWRSearchViewModel cropForCWRSearchViewModel = new CropForCWRSearchViewModel();
            cropForCWRSearchViewModel.AuthenticatedUser = AuthenticatedUser;
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            cropForCWRSearchViewModel.CropsForCWR = _taxonomyService.FindUserCropsForCWR(cooperatorId);
            return PartialView(BASE_PATH + "CropForCWR/_SearchResults.cshtml", cropForCWRSearchViewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CropForCWRListRecent()
        {
            CropForCWRSearchViewModel cropForCWRSearchViewModel = new CropForCWRSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            cropForCWRSearchViewModel.CropsForCWR = _taxonomyService.FindRecentCropsForCWR();
            return PartialView(BASE_PATH + "CropForCWR/_SearchResults.cshtml", cropForCWRSearchViewModel);
        }
 
        [HttpGet]
        public PartialViewResult CropForCWRSearch(string searchText)
        {
            CropForCWRSearchViewModel cropForCWRSearchViewModel = new CropForCWRSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            cropForCWRSearchViewModel.CropsForCWR = _taxonomyService.FindCropsForCWR(searchText);
            return PartialView(BASE_PATH + "CropForCWR/_SearchResults.cshtml", cropForCWRSearchViewModel);
        }

        public ActionResult CWRMapList(int cropId)
        {
            CWRMapSearchViewModel viewModel = new CWRMapSearchViewModel();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.CropForCWRID = cropId;
                viewModel.CWRMaps = taxonomyService.GetCWRMaps(cropId);
                return PartialView("~/Views/Taxonomy/CWRMap/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }


        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public PartialViewResult RecentCrops()
        //{
        //    CropSearchViewModel viewModel = new CropSearchViewModel();
        //    TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

        //    viewModel.CropsForCWR = _taxonomyService.FindRecentCrops(); 
        //    return PartialView("~/Views/Taxonomy/Crop/_SearchResults.cshtml", viewModel);
        //}

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public PartialViewResult UserCrops()
        //{
        //    CropSearchViewModel viewModel = new CropSearchViewModel();
        //    TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

        //    viewModel.CropsForCWR = _taxonomyService.FindUserCrops(AuthenticatedUser.CooperatorID);
        //    return PartialView("~/Views/Taxonomy/Crop/_SearchResults.cshtml", viewModel);
        //}

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public ActionResult CropSearch(string searchText)
        //{
        //    TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

        //    List<CropForCWR> crops = new List<CropForCWR>();
        //    CropSearchViewModel viewModel = new CropSearchViewModel();

        //    viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "CropName", FieldValue = searchText, SearchOperatorCode = "CNT" });
        //    viewModel.CropsForCWR = _taxonomyService.FindCropsForCWR("WHERE crop_name LIKE '%" + searchText + "%'");
        //    return PartialView("~/Views/Taxonomy/Crop/_SearchResults.cshtml", viewModel);
        //}

        //public ActionResult CropForCWREdit(int id = 0)
        //{
        //    CropEditViewModel viewModel = new CropEditViewModel();
        //    TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

        //    try
        //    {
        //        if (id > 0)
        //        {
        //            TempData["context"] = "Edit Crop for CWR";

        //            CropForCWR crop = _taxonomyService.GetCropForCWR(id);
        //            viewModel.ID = crop.ID;
        //            viewModel.CropName = crop.CropName;
        //            viewModel.Note = crop.Note;
        //            viewModel.CreatedDate = crop.CreatedDate;
        //            viewModel.CreatedByCooperatorID = crop.CreatedByCooperatorID;
        //            viewModel.CreatedByCooperatorName = crop.CreatedByCooperatorName;
        //            viewModel.ModifiedDate = crop.ModifiedDate;
        //            viewModel.ModifiedByCooperatorID = crop.ModifiedByCooperatorID;
        //            viewModel.ModifiedByCooperatorName = crop.ModifiedByCooperatorName;
        //        }
        //        else 
        //        {
        //            TempData["context"] = "Add Crop for CWR";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message + ex.StackTrace);
        //        return RedirectToAction("InternalServerError", "Error");
        //    }
        //    return View("~/Views/Taxonomy/Crop/Edit.cshtml", viewModel);
        //}

        //[HttpPost]
        //public ActionResult CropEdit(CropEditViewModel viewModel)
        //{
        //    CropForCWR crop = new CropForCWR();
        //    ResultContainer resultContainer = new ResultContainer();
        //    TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

        //    if (!ModelState.IsValid)
        //    {
        //        return View("~/Views/Taxonomy/Crop/Edit.cshtml", viewModel);
        //    }

        //    try
        //    {
        //        crop.ID = viewModel.ID;
        //        crop.CropName = viewModel.CropName;
        //        crop.Note = viewModel.Note;

        //        if (viewModel.ID > 0)
        //        {
        //            crop.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
        //            resultContainer = _taxonomyService.UpdateCropForCWR(crop);
        //        }
        //        else
        //        {
        //            crop.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
        //            resultContainer = _taxonomyService.AddCropForCWR(crop);
        //        }

        //        if (resultContainer.ResultCode == "2601")
        //        {
        //            viewModel.ErrorMessage = "The crop name must be unique.";
        //            return View("~/Views/Taxonomy/Crop/Edit.cshtml", viewModel);
        //        }
        //        crop.ID = resultContainer.EntityID;

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message + ex.StackTrace);
        //        return RedirectToAction("InternalServerError", "Error");

        //    }
        //    return RedirectToAction("CropEdit", "Taxonomy", new { id = crop.ID });
        //}

        #endregion

        //***********************************************************************************************************
        // CWR MAP
        //***********************************************************************************************************
        #region CWR Map

        public ActionResult CWRMapHome()
        {
            CWRMapHomeViewModel cWRMapHomeViewModel = new CWRMapHomeViewModel();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                cWRMapHomeViewModel.SysTable = taxonomyService.GetSysTable(242);
                cWRMapHomeViewModel.DataSourceName = cWRMapHomeViewModel.SysTable.Name;
                cWRMapHomeViewModel.DataSourceTitle = cWRMapHomeViewModel.SysTable.Title;
                TempData["context"] = "CWR Map Home";

                cWRMapHomeViewModel.Cooperators = new SelectList(AuthenticatedUserSession.Application.Cooperators, "ID", "FullName");
                cWRMapHomeViewModel.DefaultCooperatorID = AuthenticatedUser.CooperatorID;
                cWRMapHomeViewModel.CWRTraitViewModel = new CWRTraitViewModel(taxonomyService.GetTraitClassCodes(), taxonomyService.GetBreedingTypeCodes());
                return View(BASE_PATH + "CWRMap/Index.cshtml", cWRMapHomeViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CWRMapListByUser(int cooperatorId)
        {
            CWRMapSearchViewModel viewModel = new CWRMapSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.AuthenticatedUser = AuthenticatedUser;
                viewModel.DefaultCooperatorID = cooperatorId;
                viewModel.CWRMaps = _taxonomyService.FindUserCWRMaps(cooperatorId);
                return PartialView("~/Views/Taxonomy/CWRMap/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Error/_Error.cshtml", viewModel);
            }


            //CropForCWRSearchViewModel cropForCWRSearchViewModel = new CropForCWRSearchViewModel();
            //cropForCWRSearchViewModel.AuthenticatedUser = AuthenticatedUser;
            //TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            //cropForCWRSearchViewModel.CropsForCWR = _taxonomyService.FindUserCropsForCWR(cooperatorId);
            //return PartialView(BASE_PATH + "CropForCWR/_SearchResults.cshtml", cropForCWRSearchViewModel);

        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CWRMapListRecent()
        {
            CWRMapSearchViewModel viewModel = new CWRMapSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.CWRMaps = _taxonomyService.FindRecentCWRMaps();
                return PartialView("~/Views/Taxonomy/CWRMap/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Error/_Error.cshtml", viewModel);
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CWRMapListByCWRCrop(int cropForCwrId)
        {
            CWRMapSearchViewModel viewModel = new CWRMapSearchViewModel();
            try
            {
                TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
                viewModel.CWRMaps = taxonomyService.FindRelatedCWRMaps(cropForCwrId);
                return PartialView("~/Views/Taxonomy/CWRMap/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Error/_Error.cshtml", viewModel);
            }


        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CWRMapSearch(string searchText)
        {
            CWRMapSearchViewModel viewModel = new CWRMapSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            viewModel.CWRMaps = _taxonomyService.FindCWRMaps(searchText);
            return PartialView("~/Views/Taxonomy/CWRMap/_SearchResults.cshtml", viewModel);
        }

        public ActionResult CWRMapEdit(int cropForCwrId = 0, int cwrMapId = 0)
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            CWRMapViewModel viewModel = null;
           
            try
            {
                if (cwrMapId > 0)
                {
                    TempData["context"] = "Edit CWR Map";

                    CWRMap cwrMap = taxonomyService.GetCWRMap(cwrMapId);

                    viewModel = new CWRMapViewModel(taxonomyService.GetGenePoolCodes(), taxonomyService.GetCropsForCWR());
                    viewModel.ID = cwrMap.ID;
                    viewModel.CropForCWRID = cwrMap.CropForCWRID;
                    viewModel.SpeciesID = cwrMap.SpeciesID;
                    viewModel.SpeciesName = cwrMap.SpeciesName;
                    viewModel.CommonName = cwrMap.CommonName;
                    viewModel.GenepoolCode = cwrMap.GenepoolCode;
                    viewModel.IsCrop = cwrMap.IsCrop;
                    viewModel.IsGraftStock = cwrMap.IsGraftStock;
                    viewModel.IsPotential = cwrMap.IsPotential;
                    viewModel.Note = cwrMap.Note;
                    viewModel.CitationID = cwrMap.CitationID;
                    viewModel.CreatedDate = cwrMap.CreatedDate;
                    viewModel.CreatedByCooperatorID = cwrMap.CreatedByCooperatorID;
                    viewModel.CreatedByCooperatorName = cwrMap.CreatedByCooperatorName;
                    viewModel.ModifiedDate = cwrMap.ModifiedDate;
                    viewModel.ModifiedByCooperatorID = cwrMap.ModifiedByCooperatorID;
                    viewModel.ModifiedByCooperatorName = cwrMap.ModifiedByCooperatorName;
                    viewModel.Citations = new SelectList(new List<Citation>(), "ID", "Title");
                    viewModel.CWRTraits = cwrMap.CWRTraits;
                }
                else
                {
                    viewModel = new CWRMapViewModel(taxonomyService.GetGenePoolCodes(), taxonomyService.GetCropsForCWR());
                    viewModel.CropForCWRID = cropForCwrId;
                    viewModel.Citations = new SelectList(new List<Citation>(), "ID", "Title");
                    TempData["context"] = "Add CWR Map";
                }
                viewModel.Genera = new SelectList(taxonomyService.GetGenera(), "ID","Name");
                return View("~/Views/Taxonomy/CWRMap/Edit.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        public ActionResult CWRMapEdit(CWRMapViewModel viewModel)
        {
            CWRMap cwrMap = new CWRMap();
            ResultContainer resultContainer = null;
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            //if (!ModelState.IsValid)
            //{
            //    return View("~/Views/Taxonomy/CWRMap/Edit.cshtml", viewModel);
            //}

            try
            {
                cwrMap.ID = viewModel.ID;
                cwrMap.SpeciesID = viewModel.SpeciesID;
                cwrMap.CropForCWRID = viewModel.CropForCWRID;
                cwrMap.CommonName = viewModel.CommonName;
                cwrMap.IsCrop = viewModel.IsCrop;
                cwrMap.GenepoolCode = viewModel.GenepoolCode;
                cwrMap.IsGraftStock = viewModel.IsGraftStock;
                cwrMap.IsPotential = viewModel.IsPotential;
                cwrMap.CitationID = viewModel.CitationID;
                cwrMap.Note = viewModel.Note;

                if (viewModel.ID > 0)
                {
                    cwrMap.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = taxonomyService.UpdateCWRMap(cwrMap);
                }
                else
                {
                    cwrMap.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = taxonomyService.AddCWRMap(cwrMap);

                    if (String.IsNullOrEmpty(resultContainer.ResultCode))
                    {
                        cwrMap.ID = resultContainer.EntityID;
                    }
                    else
                    {
                        throw new Exception(resultContainer.ResultDescription);
                    }
                }
                return RedirectToAction("CWRMapEdit", "Taxonomy", new { cropForCwrId = cwrMap.CropForCWRID, cwrMapId = cwrMap.ID });
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message); 
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public ActionResult Manager()
        {
            TempData["context"] = "Crop Mapper";
            ManagerViewModel managerViewModel = new ManagerViewModel();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            return View("~/Views/Taxonomy/CWRMap/Manager.cshtml", managerViewModel);
        }

        //[HttpPost]
        //public ActionResult CWRMapSearch(CWRMapSearchViewModel viewModel)
        //{
        //    viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "CropCommonName", FieldValue = viewModel.CommonName, SearchOperatorCode = viewModel.CommonNameComparisonOperator });
        //    viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "TaxonomyCWRCropID", FieldValue = viewModel.SelectedCropID.ToString(), SearchOperatorCode = "EQL" });
        //    viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "Genepool", FieldValue = viewModel.SelectedGenepoolCode, SearchOperatorCode = "MTE" });
        //    //viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "IsCrop?", FieldValue = viewModel.IsCrop.ToString(), SearchOperatorCode = "EQL" });
        //    viewModel.SearchData.QueryCriteria = viewModel.SearchData.QueryCriteria.Where(x => x.FieldValue != null).ToList();

        //    viewModel.SearchResults = _taxonomyService.FindCWRMaps(viewModel.SearchData);

        //    return View("~/Views/Taxonomy/Crop/Map/Index.cshtml", viewModel);
        //}

        public ActionResult CWRMapDelete(int cropId, int cropMapId)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            _taxonomyService.DeleteCWRMap(cropMapId);
            return RedirectToAction("CropEdit", "Taxonomy", new { id = cropId });
        }

        //public ActionResult CWRMapsDelete(int cropId, string cropMapIdList)
        //{
        //    CWRMapSearchViewModel viewModel = new CWRMapSearchViewModel();

        //    _taxonomyService.DeleteCWRMaps(cropMapIdList);
        //    viewModel.CropID = cropId;
        //    viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "TaxonomyCWRCropID", FieldValue = cropId.ToString(), SearchOperatorCode = "EQL" });
        //    viewModel.CWRMaps = _taxonomyService.FindCWRMaps(viewModel.SearchData);
        //    return PartialView("~/Views/Taxonomy/Crop/Map/_List.cshtml", viewModel);
        //}

        #endregion

        //***********************************************************************************************************
        // CWR TRAIT
        //***********************************************************************************************************
        #region CWR Trait

        public ActionResult CWRTraitHome()
        {
            CropTraitHomeViewModel viewModel = new CropTraitHomeViewModel();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            TempData["context"] = "Crop Trait Home";
            return View("~/Views/Taxonomy/CWRTrait/Index.cshtml", viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CWRTraitSearch(string searchText)
        {
            CWRTraitSearchViewModel viewModel = new CWRTraitSearchViewModel();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            viewModel.CWRTraits = taxonomyService.FindCWRTraits(searchText);
            return PartialView("~/Views/Taxonomy/CWRTrait/_SearchResults.cshtml", viewModel);
        }

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CWRTraitListByUser()
        {
            CWRTraitSearchViewModel viewModel = new CWRTraitSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.CWRTraits = _taxonomyService.FindUserCWRTraits(AuthenticatedUser.CooperatorID);
                return PartialView("~/Views/Taxonomy/CWRTrait/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Error/_Error.cshtml", viewModel);
            }
        }

        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult CWRTraitListRecent()
        {
            CWRTraitSearchViewModel viewModel = new CWRTraitSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.CWRTraits = _taxonomyService.FindRecentCWRTraits();
                return PartialView("~/Views/Taxonomy/CWRTrait/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Error/_Error.cshtml", viewModel);
            }
        }

        public ActionResult CWRTraitEdit(int speciesId = 0, int cropForCwrId = 0, int cwrMapId = 0, int cwrTraitId = 0)
        {
            CWRTraitViewModel cwrTraitViewModel = null;
            CWRTrait cwrTrait = new CWRTrait();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                cwrTraitViewModel = new CWRTraitViewModel(taxonomyService.GetTraitClassCodes(), taxonomyService.GetBreedingTypeCodes());

                cwrTraitViewModel.CWRMap = taxonomyService.GetCWRMap(cwrMapId);
                
                cwrTraitViewModel.CWRMapID = cwrMapId;
                if (cwrTraitId > 0)
                {
                    TempData["context"] = "Edit CWR Trait";

                    cwrTrait = taxonomyService.GetCWRTrait(cwrTraitId);
                    cwrTraitViewModel.ID = cwrTrait.ID;
                    cwrTraitViewModel.CWRMapID = cwrTrait.CWRMapID;
                    cwrTraitViewModel.CropForCWRID = cwrTrait.CropForCWRID;
                    cwrTraitViewModel.CropForCWRName = cwrTrait.CropForCWRName;
                    cwrTraitViewModel.SpeciesID = cwrTrait.SpeciesID;
                    cwrTraitViewModel.SpeciesName = cwrTrait.SpeciesName;
                    cwrTraitViewModel.TraitClassCode = cwrTrait.TraitClassCode;
                    cwrTraitViewModel.IsPotential = cwrTrait.IsPotential;
                    cwrTraitViewModel.BreedingTypeCode = cwrTrait.BreedingTypeCode;
                    cwrTraitViewModel.BreedingUsageNote = cwrTrait.BreedingUsageNote;
                    cwrTraitViewModel.OntologyTraitIdentifier = cwrTrait.OntologyTraitIdentifier;
                    cwrTraitViewModel.CitationID = cwrTrait.CitationID;
                    cwrTraitViewModel.Note = cwrTrait.Note;
                    cwrTraitViewModel.CreatedDate = cwrTrait.CreatedDate;
                    cwrTraitViewModel.CreatedByCooperatorID = cwrTrait.CreatedByCooperatorID;
                    cwrTraitViewModel.CreatedByCooperatorName = cwrTrait.CreatedByCooperatorName;
                    cwrTraitViewModel.ModifiedDate = cwrTrait.ModifiedDate;
                    cwrTraitViewModel.ModifiedByCooperatorID = cwrTrait.ModifiedByCooperatorID;
                    cwrTraitViewModel.ModifiedByCooperatorName = cwrTrait.ModifiedByCooperatorName;
                    //cwrTraitViewModel.Citations = new SelectList(taxonomyService.FindCitations(cwrTraitViewModel.SpeciesID), "ID", "Title");
                    cwrTraitViewModel.Citations = new SelectList(new List<Citation>(), "ID", "Title");
                }
                else
                {
                    TempData["context"] = "Add CWR Trait";
                    cwrTraitViewModel.CropForCWRID = cropForCwrId;
                    cwrTraitViewModel.SpeciesID = speciesId;
                    cwrTraitViewModel.Citations = new SelectList(new List<Citation>(), "ID", "Title");
                }
                cwrTraitViewModel.CropForCWR = taxonomyService.GetCropForCWR(cwrTraitViewModel.CropForCWRID);
                cwrTraitViewModel.Species = taxonomyService.GetSpecies(cwrTraitViewModel.SpeciesID);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return View("~/Views/Taxonomy/CWRTrait/Edit.cshtml", cwrTraitViewModel);
        }

        [HttpPost]
        public ActionResult CWRTraitEdit(CWRTraitViewModel viewModel)
        {
            ResultContainer resultContainer = new ResultContainer();
            CWRTrait cropTrait = new CWRTrait();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                cropTrait.ID = viewModel.ID;
                cropTrait.CWRMapID = viewModel.CWRMapID;
                cropTrait.TraitClassCode = viewModel.TraitClassCode;
                cropTrait.IsPotential = viewModel.IsPotential;
                cropTrait.BreedingTypeCode = viewModel.BreedingTypeCode;
                cropTrait.BreedingUsageNote = viewModel.BreedingUsageNote;
                cropTrait.OntologyTraitIdentifier = viewModel.OntologyTraitIdentifier;
                cropTrait.CitationID = viewModel.CitationID;
                cropTrait.Note = viewModel.Note;

                if (viewModel.ID == 0)
                {
                    cropTrait.CreatedDate = viewModel.CreatedDate;
                    cropTrait.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.AddCropTrait(cropTrait);
                    if (String.IsNullOrEmpty(resultContainer.ResultCode))
                    {
                        viewModel.ID = resultContainer.EntityID;
                    }
                }
                else
                {
                    cropTrait.ModifiedDate = viewModel.ModifiedDate;
                    cropTrait.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
                    _taxonomyService.UpdateCWRTrait(cropTrait);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return RedirectToAction("CWRTraitEdit", "Taxonomy", new { speciesId = viewModel.SpeciesID, cropForCwrId = viewModel.CropForCWRID, cwrMapId = viewModel.CWRMapID, cwrTraitId = viewModel.ID });
        }

        public ActionResult CWRTraitDelete(int cropForCwrId, int cwrMapId, int cwrTraitId)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
             
            _taxonomyService.DeleteCWRTrait(new CWRTrait { ID = cwrTraitId });
            return RedirectToAction("CWRMapEdit", "Taxonomy", new { cropForCwrId = cropForCwrId, cwrMapId = cwrMapId });
        }

        //public ActionResult CropTraitsDelete(int speciesId, int cropId, int cropMapId, string cropTraitIdList)
        //{
        //    CropTraitListViewModel viewModel = new CropTraitListViewModel();
        //    TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

        //    _taxonomyService.DeleteCropTraits(cropTraitIdList);

        //    viewModel.SpeciesID = speciesId;
        //    viewModel.CropId = cropId;
        //    viewModel.CropMapID = cropMapId;
        //    viewModel.CropTraits = _taxonomyService.GetCWRTraits(cropMapId);
        //    return PartialView("~/Views/Taxonomy/Crop/Trait/_List.cshtml", viewModel);
        //}


        //traitClassCode = $("#CWRTraitViewModel_TraitClassCode").val();
        //breedingTypeCode = $("#CWRTraitViewModel_BreedingTypeCode").val();
        //breedingUsageNote = $("#CWRTraitViewModel_BreedingUsageNote").val();
        //ontologyTraitIdentifier



        public JsonResult ApplyCWRTrait(string traitClassCode, string breedingTypeCode, string breedingUsageNote, string ontologyTraitIdentifier, string cwrMapIdList)
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            string[] valueList = cwrMapIdList.Split(',');
            foreach (var cwrMapId in valueList)
            {
                taxonomyService.AddCropTrait(new CWRTrait { CWRMapID = Int32.Parse(cwrMapId), TraitClassCode = traitClassCode, BreedingTypeCode = breedingTypeCode, BreedingUsageNote = breedingUsageNote, OntologyTraitIdentifier = ontologyTraitIdentifier, CreatedByCooperatorID = AuthenticatedUser.CooperatorID });
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //***********************************************************************************************************
        // REPORTS
        //***********************************************************************************************************
        #region Reports

        public ActionResult Reports()
        {
            return View("~/Views/Taxonomy/Reports/Index.cshtml");
        }
        public ActionResult Report()
        {
            ReportService reportService = new ReportService(this.AuthenticatedUserSession.Environment);
            List<ISTAReportRecord> iSTAReportRecords = new List<ISTAReportRecord>();

            try
            {
                iSTAReportRecords = reportService.ISTAReport();
                return View("~/Views/Taxonomy/Reports/Detail.cshtml", iSTAReportRecords);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error", new { loginStatus = LoginStatusEnum.ERROR });
            }
        }

        public PartialViewResult FiscalYearSummary()
        {
            SummaryReport summaryReport = null;

            try
            {
                TaxonomyService taxonomyService = new TaxonomyService(this.AuthenticatedUserSession.Environment);
                summaryReport = taxonomyService.FiscalYearSummary();
                return PartialView("~/Views/Taxonomy/Reports/_FiscalYearSummary.cshtml", summaryReport);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        #endregion Reports

        //***********************************************************************************************************
        // SPECIES
        //***********************************************************************************************************
        #region Species

        public ActionResult SpeciesHome()
        {
            SpeciesHomeViewModel viewModel = new SpeciesHomeViewModel();

            try
            {
                TempData["context"] = "Species";
                return View(BASE_PATH + "Species/Index.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Species Controller");
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public PartialViewResult SpeciesListRecent()
        {
            SpeciesSearchViewModel viewModel = new SpeciesSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            viewModel.Species = _taxonomyService.FindRecentSpecies();
            return PartialView("~/Views/Taxonomy/Species/_SearchResults.cshtml", viewModel);
        }

        public PartialViewResult SpeciesListByUser()
        {
            SpeciesSearchViewModel viewModel = new SpeciesSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            viewModel.Species = _taxonomyService.FindUserSpecies(this.AuthenticatedUser.CooperatorID);
            return PartialView("~/Views/Taxonomy/Species/_SearchResults.cshtml", viewModel);
        }

        public ActionResult FindGenus(string searchString)
        {
            IQueryable<Genus> genusList = null;
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                genusList = new List<Genus>().AsQueryable();
                genusList = _taxonomyService.FindGenus(searchString);
                return PartialView("~/Views/Taxonomy/Genus/_List.cshtml", genusList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public ActionResult FindSpecies(string searchString, bool includeSynonyms)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                List<Species> speciesList = new List<Species>();
                speciesList = _taxonomyService.FindSpecies(searchString, includeSynonyms);
                return PartialView("~/Views/Taxonomy/Species/_List.cshtml", speciesList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("_Error", "Error");
            }
        }

        public ActionResult SpeciesList()
        {
            return View();
        }

        public ActionResult SpeciesEdit(int id = 0, int currentId=0, string synonymCode = "")
        {
            Species species = null;
            SpeciesEditViewModel viewModel = null;

            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                // TODO
                // SYN CODE
                // =, A, B, I, S
                // 1. Show parent species widget (see trait page)
                // 2. Configure edit section for new-record addition
 
                if (id > 0)
                {
                    TempData["context"] = "Edit Species";
                    species = _taxonomyService.GetSpecies(id);
                    viewModel = new SpeciesEditViewModel();
                    viewModel.ID = species.ID;
                    viewModel.CurrentTaxonomySpeciesID = species.CurrentTaxonomySpeciesID;
                    viewModel.NomenNumber = species.NomenNumber;
                    viewModel.IsSpecificHybrid = species.IsSpecificHybrid;
                    viewModel.SpeciesName = species.SpeciesName;
                    viewModel.Name = species.Name;
                    viewModel.IsAcceptedName = species.IsAcceptedName;
                    viewModel.Authority = species.Authority;
                    viewModel.IsSubSpecificHybrid = species.IsSubSpecificHybrid;
                    viewModel.SubSpeciesName = species.SubSpeciesName;
                    viewModel.SubSpeciesAuthority = species.SubSpeciesAuthority;
                    viewModel.IsVarietalHybrid = species.IsVarietalHybrid;
                    viewModel.VarietyName = species.VarietyName;
                    viewModel.VarietyAuthority = species.VarietyAuthority;
                    viewModel.FormaName = species.FormaName;
                    viewModel.FormaAuthority = species.FormaAuthority;
                    viewModel.FormaRankType = species.FormaRankType;
                    viewModel.GenusID = species.GenusID;
                    viewModel.GenusName = species.GenusName;
                    viewModel.Protologue = species.Protologue;
                    viewModel.NameAuthority = species.NameAuthority;
                    viewModel.GenusID = species.GenusID;
                    viewModel.GenusName = species.GenusName;
                    viewModel.Authority = species.Authority;
                    viewModel.CreatedDate = species.CreatedDate;
                    viewModel.CreatedByCooperatorID = species.CreatedByCooperatorID;
                    viewModel.CreatedByCooperatorName = species.CreatedByCooperatorName;
                    viewModel.ModifiedDate = species.ModifiedDate;
                    viewModel.ModifiedByCooperatorID = species.ModifiedByCooperatorID;
                    viewModel.ModifiedByCooperatorName = species.ModifiedByCooperatorName;
                    viewModel.Note = species.Note;
                    viewModel.Citations = species.Citations;
                    viewModel.CommonNames = species.CommonNames;
                    //viewModel.Usages = species.Usages;
                    //viewModel.RegulationMappings = species.RegulationMappings;
                }
                else
                {
                    if (currentId > 0)
                    {
                        // TODO: GET PARENT SPECIES DATA
                        viewModel.ParentSpecies = _taxonomyService.GetSpecies(currentId);
                    }
                    else
                    {

                        TempData["context"] = "Add Species";
                        viewModel = new SpeciesEditViewModel();
                        viewModel.CurrentTaxonomySpeciesID = currentId;
                        viewModel.SynonymCode = synonymCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return RedirectToAction("InternalServerError", "Error");
            }
            return View("~/Views/Taxonomy/Species/Edit.cshtml", viewModel);
        }
        [HttpPost]
        public ActionResult SpeciesEdit(SpeciesEditViewModel viewModel)
        {
            Species species = new Species();
            ResultContainer resultContainer = new ResultContainer();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            if (!ModelState.IsValid)
            {
                return View("~/Views/Taxonomy/Species/Edit.cshtml", viewModel);
            }

            try
            {
                species.ID = viewModel.ID;
                species.CurrentTaxonomySpeciesID = viewModel.CurrentTaxonomySpeciesID;
                species.NomenNumber = viewModel.NomenNumber;
                species.IsSpecificHybrid = viewModel.IsSpecificHybrid;
                species.SpeciesName = viewModel.SpeciesName;
                species.Name = viewModel.Name;
                species.IsAcceptedName = viewModel.IsAcceptedName;
                species.Authority = viewModel.Authority;
                species.IsSubSpecificHybrid = viewModel.IsSubSpecificHybrid;
                species.SubSpeciesName = viewModel.SubSpeciesName;
                species.SubSpeciesAuthority = viewModel.SubSpeciesAuthority;
                //viewModel.IsVarietalHybrid = species.IsVarietalHybrid;
                //viewModel.VarietyName = species.VarietyName;
                //viewModel.VarietyAuthority = species.VarietyAuthority;
                species.FormaName = viewModel.FormaName;
                species.FormaAuthority = viewModel.FormaAuthority;
                species.FormaRankType = viewModel.FormaRankType;
                species.GenusID = viewModel.GenusID;
                species.Protologue = viewModel.Protologue;
                species.NameAuthority = viewModel.NameAuthority;
                species.GenusID = viewModel.GenusID;
                species.Authority = viewModel.Authority;
                species.Note = viewModel.Note;

                if (viewModel.ID > 0)
                {
                    species.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.UpdateSpecies(species);
                }
                else
                {
                    species.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.AddSpecies(species);
                    viewModel.ID = resultContainer.EntityID;
                }

                if (resultContainer.ResultCode == "2601")
                {
                    viewModel.ErrorMessage = "The species name must be unique.";
                    return View("~/Views/Taxonomy/Species/Edit.cshtml", viewModel);
                }

                return RedirectToAction("SpeciesEdit", "Taxonomy", new { id = viewModel.ID });
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SpeciesSearch(string searchText)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            List<Species> species = new List<Species>();
            SpeciesSearchViewModel viewModel = new SpeciesSearchViewModel();

            //TODO: CONVERT TO COMPLEX SEARCH
            //viewModel.SearchData.QueryCriteria.Add(new QueryCriterion { FieldName = "SpeciesName", FieldValue = searchText, SearchOperatorCode = "CNT" });
            
            viewModel.Species = _taxonomyService.FindSpecies(searchText);
            return PartialView("~/Views/Taxonomy/Species/_SearchResults.cshtml", viewModel);
        }

        [HttpPost]
        public JsonResult SpeciesNameSearch(string searchText)
        {
            List<Species> speciesList = new List<Species>();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            speciesList = taxonomyService.FindSpecies(searchText, false);
            return Json(speciesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SpeciesProtologueSearch(string searchText)
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            List<ReferenceItem> referenceItems = new List<ReferenceItem>();
            
            try
            {
                referenceItems = taxonomyService.FindSpeciesNotes(searchText);
                return PartialView(BASE_PATH + "Species/_NoteSearchResults.cshtml", referenceItems);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        public ActionResult SpeciesNameAuthoritySearch(string searchText)
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            List<ReferenceItem> referenceItems = new List<ReferenceItem>();

            try
            {
                referenceItems = taxonomyService.FindSpeciesAuthors(searchText);
                return PartialView(BASE_PATH + "Species/_NoteSearchResults.cshtml", referenceItems);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        public ActionResult SpeciesNoteSearch(string searchText)
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            List<ReferenceItem> referenceItems = new List<ReferenceItem>();

            try
            {
                referenceItems = taxonomyService.FindSpeciesNotes(searchText);
                return PartialView(BASE_PATH + "Species/_NoteSearchResults.cshtml", referenceItems);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        [HttpPost]
        public JsonResult SpeciesCache(string prefix)
        {
            IEnumerable<Species> speciesList = null;
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            speciesList = taxonomyService.FindSpecies(prefix,false);
            return Json(speciesList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCitations(int speciesId)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            IEnumerable<Citation> citations = _taxonomyService.FindCitations(speciesId);

            return Json(citations, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBreedingTypes(string traitClassCode)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            IEnumerable <CodeValueReferenceItem> breedingTypeCodes = _taxonomyService.GetBreedingTypeCodes(traitClassCode);
            return Json(breedingTypeCodes, JsonRequestBehavior.AllowGet);
        }

        #endregion Species

        #region Citation
        public ActionResult CitationHome()
        {
            TempData["context"] = "Citation Home";
            CitationHomeViewModel viewModel = new CitationHomeViewModel();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            // TO DO: SET UP SEARCH HERE
            SearchViewModel searchViewModel = new SearchViewModel();
            List<Field> fields = new List<Field>();
            
            fields.Add(new Field { Name = "citation.citation_title", Title = "Title" });
            fields.Add(new Field { Name = "citation.author_name", Title = "Author" });
            fields.Add(new Field { Name = "citation.citation_year", Title = "Year" });
            searchViewModel.Fields = new SelectList(fields, "Name", "Title");

            List<SearchOperator> searchOperators = new List<SearchOperator>();
            searchOperators.Add(new SearchOperator { Title = "Starts With", SearchOperatorSyntax= "LIKE" });
            searchOperators.Add(new SearchOperator { Title = "Matches", SearchOperatorSyntax = "=" });
            searchOperators.Add(new SearchOperator { Title = "Ends In", SearchOperatorSyntax = "LIKE" });
            searchViewModel.SearchOperators = new SelectList(searchOperators, "SearchOperatorSyntax","Title");

            List<ReferenceItem> citationTypeCodes = new List<ReferenceItem>();
            citationTypeCodes.Add(new ReferenceItem { ID = 1, Name = "MEDICINE", Description = "" });
            citationTypeCodes.Add(new ReferenceItem { ID = 2, Name = "NODULATION", Description = "" });
            citationTypeCodes.Add(new ReferenceItem { ID = 3, Name = "RELATIVE", Description = "" });
            citationTypeCodes.Add(new ReferenceItem { ID = 4, Name = "NULL", Description = "" });
            viewModel.CitationTypeCodes = new SelectList(citationTypeCodes, "ID", "Name");

            viewModel.SearchViewModel = searchViewModel;

            return View("~/Views/Taxonomy/Citation/Index.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult CitationSearch(CitationHomeViewModel citationHomeViewModel)
        {
            TempData["context"] = "Citation Search";
            CitationSearchViewModel viewModel = new CitationSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            Query query = new Query();
            QueryCriterion queryCriterion = null;

            if (!String.IsNullOrEmpty(citationHomeViewModel.SpeciesName))
            {
                queryCriterion = new QueryCriterion { FieldName = "ts.name", FieldValue = citationHomeViewModel.SpeciesName, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                query.QueryCriteria.Add(queryCriterion);
            }

            if (!String.IsNullOrEmpty(citationHomeViewModel.GenusName))
            {
                queryCriterion = new QueryCriterion { FieldName = "tg.name", FieldValue = citationHomeViewModel.GenusName, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                query.QueryCriteria.Add(queryCriterion);
            }

            if (!String.IsNullOrEmpty(citationHomeViewModel.FamilyName))
            {
                queryCriterion = new QueryCriterion { FieldName = "tf.name", FieldValue = citationHomeViewModel.FamilyName, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                query.QueryCriteria.Add(queryCriterion);
            }

            if (!String.IsNullOrEmpty(citationHomeViewModel.Abbreviation))
            {
                queryCriterion = new QueryCriterion { FieldName = "l.abbreviation", FieldValue = citationHomeViewModel.Abbreviation, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                query.QueryCriteria.Add(queryCriterion);
            }

            if (!String.IsNullOrEmpty(citationHomeViewModel.Note))
            {
                queryCriterion = new QueryCriterion { FieldName = "cit.abbreviation", FieldValue = citationHomeViewModel.Note, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                query.QueryCriteria.Add(queryCriterion);
            }

            if (!String.IsNullOrEmpty(citationHomeViewModel.TypeCode))
            {
                if (!citationHomeViewModel.TypeCode.Contains("Select"))
                {
                    if (citationHomeViewModel.TypeCode == "NULL")
                    {
                        queryCriterion = new QueryCriterion { FieldName = "cit.type_code", FieldValue = "NULL", SearchOperatorCode = "IS", DataType = "NVARCHAR" };
                    }
                    else
                    {
                        queryCriterion = new QueryCriterion { FieldName = "cit.type_code", FieldValue = citationHomeViewModel.TypeCode, SearchOperatorCode = "=", DataType = "NVARCHAR" };
                    }
                    query.QueryCriteria.Add(queryCriterion);
                }
            }
            citationHomeViewModel.Citations = _taxonomyService.FindCitations(1,query);
            //return PartialView("~/Views/Taxonomy/Citation/_SearchResults.cshtml", viewModel);

            List<ReferenceItem> citationTypeCodes = new List<ReferenceItem>();
            citationTypeCodes.Add(new ReferenceItem { ID = 1, Name = "MEDICINE", Description = "" });
            citationTypeCodes.Add(new ReferenceItem { ID = 2, Name = "NODULATION", Description = "" });
            citationTypeCodes.Add(new ReferenceItem { ID = 3, Name = "RELATIVE", Description = "" });
            citationTypeCodes.Add(new ReferenceItem { ID = 4, Name = "NULL", Description = "" });
            citationHomeViewModel.CitationTypeCodes = new SelectList(citationTypeCodes, "ID", "Name");

            return View(BASE_PATH + "Citation/Index.cshtml", citationHomeViewModel);
        }

        [HttpPost]
        public PartialViewResult CitationList(string category, int id)
        {
            CitationSearchViewModel viewModel = new CitationSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.Citations = _taxonomyService.GetCitationsByCategory(category, id);
                return PartialView("~/Views/Taxonomy/Citation/_SearchResults.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Error/_Error.cshtml", viewModel);
            }
        }

        public ActionResult CitationEdit(int id = 0)
        {
            TempData["context"] = "Edit Citation";
            CitationViewModel viewModel = new CitationViewModel();
            Citation citation = new Citation();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                if (id > 0)
                {
                    TempData["context"] = "Edit Citation";
                    citation = _taxonomyService.GetCitation(id);
                    viewModel.ID = citation.ID;
                    viewModel.LiteratureID = citation.LiteratureID;
                    viewModel.LiteratureReferenceTitle = citation.LiteratureReferenceTitle;
                    viewModel.CitationTitle = citation.CitationTitle;
                    viewModel.AuthorName = citation.AuthorName;
                    viewModel.Year = citation.CitationYear;
                    viewModel.Reference = citation.Reference;
                    viewModel.DOIReference = citation.DOIReference;
                    viewModel.URL = citation.URL;
                    viewModel.CitationTitle = citation.Title;
                    viewModel.Description = citation.Description;
                    viewModel.AccessionID = citation.AccessionID;
                    viewModel.SpeciesID = citation.SpeciesID;
                    viewModel.GenusID = citation.GenusID;
                    viewModel.FamilyID = citation.FamilyID;
                    viewModel.TypeCode = citation.TypeCode;
                    viewModel.CreatedByCooperatorID = citation.CreatedByCooperatorID;
                    viewModel.CreatedDate = citation.CreatedDate;
                    viewModel.CreatedByCooperatorName = citation.CreatedByCooperatorName;
                    viewModel.ModifiedByCooperatorID = citation.ModifiedByCooperatorID;
                    viewModel.ModifiedDate = citation.ModifiedDate;
                    viewModel.ModifiedByCooperatorName = citation.ModifiedByCooperatorName;
                    viewModel.Species = _taxonomyService.GetSpecies(citation.SpeciesID);
                }
                else
                {
                    TempData["context"] = "Add  Citation";
                }
                List<ReferenceItem> citationTypeCodes = new List<ReferenceItem>();
                citationTypeCodes.Add(new ReferenceItem { ID = 1, Name = "MEDICINE", Description = "MEDICINE" });
                citationTypeCodes.Add(new ReferenceItem { ID = 2, Name = "NODULATION", Description = "NODULATION" });
                citationTypeCodes.Add(new ReferenceItem { ID = 3, Name = "RELATIVE", Description = "RELATIVE" });
                viewModel.CitationTypeCodes = new SelectList(citationTypeCodes, "Name", "Description");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return View(BASE_PATH + "Citation/Edit.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult CitationEdit(CitationViewModel viewModel)
        {
            TempData["context"] = "Edit Citation";
            Citation citation = new Citation();
            ResultContainer resultContainer = null;
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                citation.ID = viewModel.ID;
                citation.LiteratureID = viewModel.LiteratureID;
                citation.Title = viewModel.CitationTitle;
                citation.AuthorName = viewModel.AuthorName;
                citation.CitationYear = viewModel.Year;
                citation.Reference = viewModel.Reference;
                citation.URL = viewModel.URL;
                citation.AccessionID = viewModel.AccessionID;
                citation.SpeciesID = viewModel.SpeciesID;
                citation.GenusID = viewModel.GenusID;
                citation.FamilyID = viewModel.FamilyID;
                citation.TypeCode = viewModel.TypeCode;
                citation.CreatedByCooperatorID = viewModel.CreatedByCooperatorID;
                citation.CreatedDate = viewModel.CreatedDate;
                citation.CreatedByCooperatorName = viewModel.CreatedByCooperatorName;
                citation.ModifiedByCooperatorID = viewModel.ModifiedByCooperatorID;
                citation.ModifiedDate = viewModel.ModifiedDate;
                citation.ModifiedByCooperatorName = viewModel.ModifiedByCooperatorName;

                if (viewModel.ID > 0)
                {
                    citation.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.UpdateCitation(citation);
                }
                else
                {
                    citation.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
                    resultContainer = _taxonomyService.AddCitation(citation);
                    viewModel.ID = resultContainer.EntityID;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return RedirectToAction("CitationEdit", "Taxonomy", new { id = citation.ID });

        }

        public PartialViewResult CitationDetail(int citationId)
        {
            Citation citation = new Citation();

            return PartialView("~/Views/Taxonomy/Citation/_Detail.cshtml", citation);
        }

        public PartialViewResult FindCitations(int speciesId, string searchString)
        {
            IEnumerable<Citation> citationList = new List<Citation>().AsEnumerable();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                //TODO
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return PartialView("~/Views/Taxonomy/Citation/_List.cshtml", citationList);
        }

        #endregion

        #region Notes

        public ActionResult FindNotes(string searchString, string context)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                IEnumerable<Note> noteList = new List<Note>().AsEnumerable();
                noteList = _taxonomyService.FindNotes(searchString, context);
                return PartialView("~/Views/Taxonomy/Shared/_NoteList.cshtml", noteList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message); 
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        #endregion

        #region Literature

        public ActionResult LiteratureHome()
        {
            TempData["context"] = "Literature";
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            LiteratureHomeViewModel literatureHomeViewModel = new LiteratureHomeViewModel();
            literatureHomeViewModel.SysTable = taxonomyService.GetSysTable(48);
            literatureHomeViewModel.DataSourceName = literatureHomeViewModel.SysTable.Name;
            literatureHomeViewModel.DataSourceTitle = literatureHomeViewModel.SysTable.Title;
         
            try
            {
                List<CodeValueReferenceItem> literatureTypeCodes = taxonomyService.GetCodeValues("LITERATURE_TYPE");
                literatureHomeViewModel.LiteratureTypeCodes = new SelectList(literatureTypeCodes, "CodeValue", "Title");
                return View(BASE_PATH + "Literature/Index.cshtml", literatureHomeViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public ActionResult LiteratureEdit(int id)
        {
            LiteratureEditViewModel literatureEditViewModel = new LiteratureEditViewModel();
            Literature literature = null;
            try 
            {
                TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
                List<CodeValueReferenceItem> literatureTypeCodes = taxonomyService.GetCodeValues("LITERATURE_TYPE");
                literatureEditViewModel.LiteratureTypeCodes = new SelectList(literatureTypeCodes, "CodeValue", "Title");

                if (id > 0)
                {
                    TempData["context"] = "Edit Literature";
                    literature = taxonomyService.GetLiterature(id);
                    literatureEditViewModel.ID = literature.ID;
                    literatureEditViewModel.Abbreviation = literature.Abbreviation;
                    literatureEditViewModel.StandardAbbreviation = literature.StandardAbbreviation;
                    literatureEditViewModel.ReferenceTitle = literature.ReferenceTitle;
                    literatureEditViewModel.Author = literature.EditorAuthorName;
                    literatureEditViewModel.TypeCode = literature.TypeCode;
                    literatureEditViewModel.Year = literature.PublicationYear;
                    literatureEditViewModel.URL = literature.URL;

                    if (!String.IsNullOrEmpty(literature.URL))
                    {
                        FileMetaData fileMetaData = taxonomyService.GetFileMetaData(literature.URL);
                        literatureEditViewModel.URLIsValid = fileMetaData.IsValid;
                    }
                    literatureEditViewModel.CreatedDate = literature.CreatedDate;
                    literatureEditViewModel.CreatedByCooperatorID = literature.CreatedByCooperatorID;
                    literatureEditViewModel.CreatedByCooperatorName = literature.CreatedByCooperatorName;
                    literatureEditViewModel.ModifiedDate = literature.ModifiedDate;
                    literatureEditViewModel.ModifiedByCooperatorID = literature.ModifiedByCooperatorID;
                    literatureEditViewModel.ModifiedByCooperatorName = literature.ModifiedByCooperatorName;
                    literatureEditViewModel.Citations = taxonomyService.GetCitationsByLiterature(literatureEditViewModel.ID);
                }
                else
                {
                    TempData["context"] = "Add Literature";
                }

                return View(BASE_PATH + "Literature/Edit.cshtml", literatureEditViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        public ActionResult LiteratureEdit(LiteratureEditViewModel literatureEditViewModel)
        {
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            Literature literature = new Literature();
            try 
            {
                literature.ID = literatureEditViewModel.ID;


                taxonomyService.UpdateLiterature(literature);
                return RedirectToAction("LiteratureEdit", "Taxonomy", new { id = literatureEditViewModel.ID });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        public ActionResult LiteratureSearch(LiteratureHomeViewModel literatureHomeViewModel)
        {
            CitationSearchViewModel viewModel = new CitationSearchViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            Query query = new Query();
            QueryCriterion queryCriterion = null;
            try
            {
                if (!String.IsNullOrEmpty(literatureHomeViewModel.Abbreviation))
                {
                    queryCriterion = new QueryCriterion { FieldName = "abbreviation", FieldValue = literatureHomeViewModel.Abbreviation, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(literatureHomeViewModel.ReferenceTitle))
                {
                    queryCriterion = new QueryCriterion { FieldName = "reference_title", FieldValue = literatureHomeViewModel.ReferenceTitle, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(literatureHomeViewModel.Author))
                {
                    queryCriterion = new QueryCriterion { FieldName = "editor_author_name", FieldValue = literatureHomeViewModel.Author, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(literatureHomeViewModel.Year))
                {
                    queryCriterion = new QueryCriterion { FieldName = "publication_year", FieldValue = literatureHomeViewModel.Year, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }


                if (!String.IsNullOrEmpty(literatureHomeViewModel.TypeCode))
                {
                    if (!literatureHomeViewModel.TypeCode.Contains("Select"))
                    {
                        if (literatureHomeViewModel.TypeCode == "NULL")
                        {
                            queryCriterion = new QueryCriterion { FieldName = "literature_type_code", FieldValue = "NULL", SearchOperatorCode = "IS", DataType = "NVARCHAR" };
                        }
                        else
                        {
                            queryCriterion = new QueryCriterion { FieldName = "literature_type_code", FieldValue = literatureHomeViewModel.TypeCode, SearchOperatorCode = "=", DataType = "NVARCHAR" };
                        }
                        query.QueryCriteria.Add(queryCriterion);
                    }
                }

                if (!String.IsNullOrEmpty(literatureHomeViewModel.Note))
                {
                    queryCriterion = new QueryCriterion { FieldName = "note", FieldValue = literatureHomeViewModel.Note, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
                List<CodeValueReferenceItem> literatureTypeCodes = taxonomyService.GetCodeValues("LITERATURE_TYPE");
                literatureHomeViewModel.LiteratureTypeCodes = new SelectList(literatureTypeCodes, "CodeValue", "Title");
                literatureHomeViewModel.LiteratureResults = _taxonomyService.SearchLiterature(query);

                //TEST
                foreach (var literature in literatureHomeViewModel.LiteratureResults)
                {
                    if (!String.IsNullOrEmpty(literature.URL))
                    {
                        FileMetaData fileMetaData = GetFileMetaData(literature.URL);
                        literature.UrlIsValid = fileMetaData.IsValid;
                    }
                }

                return View(BASE_PATH + "Literature/Index.cshtml", literatureHomeViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public ActionResult FindLiterature(string searchString)
        {
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                IEnumerable<Literature> literatureList = new List<Literature>().AsEnumerable();
                literatureList = _taxonomyService.SearchLiterature(searchString);
                return PartialView("~/Views/Taxonomy/Citation/_LiteratureSearchResults.cshtml", literatureList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        
        [HttpGet]
        public PartialViewResult _LiteratureDetail(int id)
        {
            Literature literature = new Literature();
            TaxonomyService taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                literature = taxonomyService.GetLiterature(id); 
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            return PartialView(BASE_PATH + "Citation/_LiteratureDetail.cshtml", literature);
        }
        

        #endregion Literature

        #region Dashboard
        public ActionResult _MySearches()
        {
            return PartialView("~/Views/Taxonomy/Shared/_UserSearchList.cshtml");
        }
        public ActionResult _MyFolders()
        {
            IEnumerable<Folder> folders = new List<Folder>().AsEnumerable();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            folders = _taxonomyService.GetFolders(AuthenticatedUser.CooperatorID);
            return PartialView("~/Views/Taxonomy/Folder/_List.cshtml", folders);
        }

        public ActionResult GetFolder(int folderId)
        {
            FolderEditViewModel viewModel = new FolderEditViewModel();
            Folder folder = new Folder();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            folder = _taxonomyService.GetFolder(folderId);
            viewModel.ID = folder.ID;
            viewModel.Title = folder.Title;
            viewModel.Category = folder.Category;
            viewModel.Description = folder.Description;
            viewModel.DataSourceName = folder.DataSourceName;
            viewModel.DataSourceTitle = folder.DataSourceTitle;
            viewModel.IsShared = folder.IsShared;
            viewModel.ModifiedByCooperatorID = folder.ModifiedByCooperatorID;
            viewModel.SearchResults = folder.SearchResults;
            return View(BASE_PATH + "Folder/Edit.cshtml", viewModel);
        }

        #region Geography
        public ActionResult GeographySearch()
        {
            TempData["context"] = "Geography Search";
            GeographySearchViewModel geographySearchViewModel = new GeographySearchViewModel();

            try
            {
                // TO DO

                return View(BASE_PATH + "Geography/Search.cshtml", geographySearchViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public JsonResult MapGeography(string keyValues = "")
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegions(string continentIds)
        {
            List<Region> regions = new List<Region>();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            regions = _taxonomyService.SearchRegions(continentIds);
            return Json(regions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountries(string regionIds)
        {
            List<Country> countries = new List<Country>();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            countries = _taxonomyService.SearchCountries(regionIds);
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMunicipalities(string countryCodes)
        {
            List<Geography> geographies = new List<Geography>();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            geographies = _taxonomyService.SearchMunicipalities(countryCodes);
            return Json(geographies, JsonRequestBehavior.AllowGet);
        }

        #endregion Geography

        public JsonResult AddToFolder(int folderId, string folderTitle, string folderCategory, string folderDescription, Boolean isShared, string dataSourceName, string dataSourceTitle, string values)
        {
            Folder folder = new Folder();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            if (folderId == 0)
            {
                folder.Title = folderTitle;
                folder.Category = folderCategory;
                folder.Description = folderDescription;
                folder.Note = folderDescription;
                folder.IsShared = isShared;
                folder.DataSourceName = dataSourceName;
                folder.DataSourceTitle = dataSourceTitle;
                folder.CreatedByCooperatorID = AuthenticatedUser.CooperatorID;
                folder.ItemList = values;
                folderId = _taxonomyService.AddFolder(folder);
            }
            else
            {
                string[] valueList = values.Split(',');
                foreach (var id in valueList)
                {
                    // TO DO: ADD EACH ITEM TO FOLDER
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion Dashboard

        #region Folder

        public PartialViewResult FolderList()
        {
            IEnumerable<Folder> folderList = null;
            try 
            {
                TaxonomyService taxonomyService = new TaxonomyService(this.AuthenticatedUserSession.Environment);
                folderList = taxonomyService.GetFolders(AuthenticatedUser.CooperatorID);
                return PartialView("~/Views/Taxonomy/Folder/_List.cshtml", folderList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        public ActionResult FolderEdit(int id)
        {
            FolderEditViewModel viewModel = new FolderEditViewModel();
            Folder folder = new Folder();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);
            TempData["context"] = "Edit Folder";

            try
            {
                folder = _taxonomyService.GetFolder(id);
                viewModel.ID = folder.ID;
                viewModel.Title = folder.Title;
                viewModel.Category = folder.Category;
                viewModel.Description = folder.Description;
                viewModel.DataSourceName = folder.DataSourceName;
                viewModel.DataSourceTitle = folder.DataSourceTitle;
                viewModel.IsShared = folder.IsShared;
                viewModel.IsFavorite = folder.IsFavorite;
                viewModel.SearchResults = folder.SearchResults;
                viewModel.CreatedDate = folder.CreatedDate;
                viewModel.CreatedByCooperatorID = folder.CreatedByCooperatorID;
                viewModel.CreatedByCooperatorName = folder.CreatedByCooperatorName;
                viewModel.ModifiedDate = folder.ModifiedDate;
                viewModel.ModifiedByCooperatorID = folder.ModifiedByCooperatorID;
                viewModel.ModifiedByCooperatorName = folder.ModifiedByCooperatorName;
                return View(BASE_PATH + "Folder/Edit.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        public ActionResult FolderEdit(FolderEditViewModel viewModel)
        {
            int retVal = 0;
            Folder folder = new Folder();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                folder.ID = viewModel.ID;
                folder.Title = viewModel.Title;
                folder.Category = viewModel.Category;
                folder.Description = viewModel.Description;
                folder.DataSourceName = viewModel.DataSourceName;
                folder.DataSourceTitle = viewModel.DataSourceTitle;
                folder.IsShared = viewModel.IsShared;
                folder.IsFavorite = viewModel.IsFavorite;
                folder.ModifiedByCooperatorID = AuthenticatedUser.CooperatorID;
                retVal = _taxonomyService.UpdateFolder(folder);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return RedirectToAction("FolderEdit", "Taxonomy", new { @id = viewModel.ID } );
        }

        public ActionResult FolderDelete(int id)
        {
            int retVal = 0;
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                _taxonomyService.DeleteFolder(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
            return RedirectToAction("Index", "Taxonomy");
        }

        public PartialViewResult FolderItemList(int targetFolderId, string dataSource)
        {
            const string VIEW_ROOT = "~/Views/Taxonomy/Folder/";
            string partialViewName = String.Empty;
            FolderItemListViewModel viewModel = new FolderItemListViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.FolderID = targetFolderId;
                viewModel.Results = _taxonomyService.GetFolderItems(targetFolderId, dataSource);
                partialViewName = GetViewName(dataSource);
                return PartialView(VIEW_ROOT + partialViewName, viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        public PartialViewResult FolderItemDelete(int targetFolderId, int itemId, string dataSource)
        {
            const string VIEW_ROOT = "~/Views/Taxonomy/Folder/";
            string partialViewName = String.Empty;
            int retVal = 0;
            FolderItemListViewModel viewModel = new FolderItemListViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                retVal = _taxonomyService.DeleteFolderItem(itemId);
                viewModel.FolderID = targetFolderId;
                viewModel.Results = _taxonomyService.GetFolderItems(targetFolderId, dataSource);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }

            partialViewName = GetViewName(dataSource);
            return PartialView(VIEW_ROOT + partialViewName, viewModel);
        }

        public PartialViewResult FolderItemsDelete(int folderId, string folderItemIdList, string dataSource)
        {
            const string VIEW_ROOT = "~/Views/Taxonomy/Folder/";
            string partialViewName = String.Empty;
            ResultContainer resultContainer = new ResultContainer();
            FolderItemListViewModel viewModel = new FolderItemListViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                resultContainer = _taxonomyService.DeleteFolderItems(folderItemIdList);
                viewModel.FolderID = folderId;
                viewModel.Results = _taxonomyService.GetFolderItems(folderId, dataSource);
                partialViewName = GetViewName(dataSource);
                return PartialView(VIEW_ROOT + partialViewName, viewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        private string GetViewName(string dataSource)
        {
            switch (dataSource)
            {
                case "taxonomy_cwr_crop":
                    return "_CropForCWRItemList.cshtml";
                case "taxonomy_cwr_map":
                    return "_CWRMapItemList.cshtml";
                case "taxonomy_cwr_trait":
                    return "_CWRTraitItemList.cshtml";
                case "taxonomy_species":
                    return "_SpeciesItemList.cshtml";
                case "taxonomy_genus":
                    return "_GenusItemList.cshtml";
                case "taxonomy_family":
                    return "_FamilyItemList.cshtml";
                case "citation":
                    return "_CitationItemList.cshtml";
                case "literature":
                    return "_LiteratureItemList.cshtml";
                default:
                    return String.Empty;
            }
        }

        #endregion

        #region Regulation

        public ActionResult RegulationHome()
        {
            RegulationHomeViewModel viewModel = new RegulationHomeViewModel();
            return View("~/Views/Taxonomy/Regulation/Index.cshtml", viewModel);
        }

        public ActionResult RegulationEdit(int id)
        {
            //TODO
            return View("~/Views/Taxonomy/Regulation/Edit.cshtml");
        }
        public ActionResult RegulationEdit(RegulationEditViewModel viewModel)
        {
            //TODO
            return View("~/Views/Taxonomy/Regulation/Edit.cshtml");
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult UserRegulations()
        {
            RegulationListViewModel viewModel = new RegulationListViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.Regulations = _taxonomyService.FindUserRegulations(AuthenticatedUser.CooperatorID);
            }
            catch (Exception ex)
            { 
            
            }
            return PartialView("~/Views/Taxonomy/Regulation/_List.cshtml", viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult RecentRegulations()
        {
            RegulationListViewModel viewModel = new RegulationListViewModel();
            TaxonomyService _taxonomyService = new TaxonomyService(AuthenticatedUserSession.Environment);

            try
            {
                viewModel.Regulations = _taxonomyService.FindRecentRegulations();
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Views/Taxonomy/Regulation/_List.cshtml", viewModel);
        }

        #endregion

        #region Reference


        #endregion

        #region Geography

        public ActionResult Search()
        {
            return View(BASE_PATH + "Geography/Search.cshtml");
        }

        #endregion
    }
}