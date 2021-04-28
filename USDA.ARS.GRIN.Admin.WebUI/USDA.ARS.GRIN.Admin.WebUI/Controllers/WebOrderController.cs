﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Models.GRINGlobal;
using USDA.ARS.GRIN.Admin.Service;
using USDA.ARS.GRIN.Admin.WebUI.ViewModels.GRINGlobal;

namespace USDA.ARS.GRIN.Admin.WebUI.Controllers
{
    [GrinGlobalAuthentication]
    public class WebOrderController : BaseController
    {
        public ActionResult Index()
        {
            TempData["context"] = "NRR Review Tool";
            GRINGlobalService grinGlobalService = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
            WebOrderRequestSearchViewModel viewModel = new WebOrderRequestSearchViewModel();

            try
            {
                viewModel.Statuses = grinGlobalService.GetWebOrderRequestStatuses();
                viewModel.IntendedUseCodes = new SelectList(grinGlobalService.GetWebOrderRequestIntendedUseCodes(), "Name", "Description");
                return View("~/Views/GRINGlobal/WebOrder/Index.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public PartialViewResult _List(string status, int timeFrameCode)
        {
            WebOrderRequestListViewModel webOrderRequestListViewModel = new WebOrderRequestListViewModel();
            webOrderRequestListViewModel.AuthenticatedUser = AuthenticatedUser;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);

            try
            {
                webOrderRequestListViewModel.WebOrderRequests = service.GetWebOrderRequests(status, timeFrameCode);
                return PartialView("~/Views/GRINGlobal/WebOrder/_List.cshtml", webOrderRequestListViewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        public PartialViewResult Update(int webOrderRequestId, int webCooperatorId, string statusCode, string actionNote)
        {
            ResultContainer resultContainer = null;
            WebOrderRequest webOrderRequest = new WebOrderRequest();
            List<WebOrderRequest> webOrderRequests = null;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);

            webOrderRequest.ID = webOrderRequestId;
            webOrderRequest.WebCooperatorID = webCooperatorId;
            webOrderRequest.StatusCode = statusCode;
            webOrderRequest.Note = actionNote;
           
            resultContainer = service.UpdateWebOrderRequest(webOrderRequest);
            //webOrderRequests = service.GetWebOrderRequests("NRR_FLAGGED");
            return PartialView("~/Views/GRINGlobal/WebOrder/_List.cshtml", webOrderRequests);

        }

        public ActionResult SetStatus(string statusCode)
        {
            WebOrderRequestEditViewModel viewModel = null;
            //TO DO
            return View("~/Views/GRINGlobal/WebOrder/Edit.cshtml", viewModel);
        }

        public ActionResult View(int id)
        {
            WebOrderRequestEditViewModel viewModel = new WebOrderRequestEditViewModel();
            TempData["context"] = "View Web Order Request #" + id;
            viewModel = LoadViewModel(id, false);
            return View("~/Views/GRINGlobal/WebOrder/Edit.cshtml", viewModel);
        }

        public ActionResult Edit(int id)
        {
            TempData["context"] = "Review Web Order Request #" + id;
            WebOrderRequestEditViewModel viewModel = new WebOrderRequestEditViewModel();

            try
            {
                viewModel = LoadViewModel(id, true);
                return View("~/Views/GRINGlobal/WebOrder/Edit.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                return View("~/Views/Error/_Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Edit(WebOrderRequestEditViewModel viewModel)
        {
            ResultContainer resultContainer = null;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
        
            try
            {
                WebOrderRequest webOrderRequest = new WebOrderRequest();
                webOrderRequest.ID = viewModel.ID;
                webOrderRequest.StatusCode = viewModel.Action;

                if (viewModel.Action == OrderRequestAction.NRRReviewEnd)
                {
                    service.SetReviewStatus(viewModel.ID, AuthenticatedUser.WebCooperatorID, false);
                    return RedirectToAction("Index", "WebOrder");
                }
                else
                {
                    resultContainer = service.UpdateWebOrderRequest(webOrderRequest);
                    resultContainer = service.AddWebOrderRequestAction(new WebOrderRequestAction { WebOrderRequestID = viewModel.ID, ActionCode = viewModel.Action, Note = viewModel.ActionNote, CreatedByCooperatorID = AuthenticatedUser.WebCooperatorID });
                    if ((viewModel.Action == "NRR_APPROVE") || (viewModel.Action == "NRR_DENY"))
                    {
                        return RedirectToAction("Index", "WebOrder");
                    }
                    else
                        return RedirectToAction("Edit", "WebOrder", new { id = viewModel.ID });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                //TO DO: CHANGE
                return View("~/Views/GRINGlobal/WebOrder/Index.cshtml");
            }
        }
        private WebOrderRequestEditViewModel LoadViewModel(int id, bool reviewMode)
        {
            ResultContainer resultContainer = null;
            WebOrderRequest webOrderRequest = null;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
            WebOrderRequestEditViewModel viewModel = new WebOrderRequestEditViewModel();

            try
            {
                if (reviewMode)
                {
                    resultContainer = service.SetReviewStatus(id, AuthenticatedUser.WebCooperatorID, true);
                    resultContainer = service.AddWebOrderRequestAction(new WebOrderRequestAction { WebOrderRequestID = id, ActionCode = "NRR_REVIEW", CreatedByCooperatorID = AuthenticatedUser.WebCooperatorID });
                }

                webOrderRequest = service.GetWebOrderRequest(id);
                if (webOrderRequest == null)
                {
                    throw new NullReferenceException(String.Format("Null web order request returned for ID {0}", id));
                }
                viewModel.StatusCode = webOrderRequest.StatusCode;
                viewModel.OrderDate = webOrderRequest.OrderDate;
                viewModel.Cooperator = webOrderRequest.Cooperators.First();
                viewModel.IsReviewMode = reviewMode;
                viewModel.IsLocked = webOrderRequest.IsLocked;
                viewModel.ID = webOrderRequest.ID;
                viewModel.OrderDate = webOrderRequest.OrderDate;
                viewModel.IntendedUseCode = webOrderRequest.IntendedUseCode;
                viewModel.IntendedUseNote = webOrderRequest.IntendedUseNote;
                viewModel.Note = webOrderRequest.Note;
                viewModel.SpecialInstruction = webOrderRequest.SpecialInstruction;
                viewModel.WebCooperator = webOrderRequest.Cooperators.Where(x => x.Type == 2).FirstOrDefault();
                viewModel.OwnedDate = webOrderRequest.OwnedDate;
                viewModel.OwnedByCooperatorName = webOrderRequest.OwnedByCooperatorName;
                viewModel.WebOrderRequestItems = webOrderRequest.WebOrderRequestItems;

                var queryWebOrderRequestDates =
                    from action in webOrderRequest.WebOrderRequestActions
                    group action by action.ActionDate into webOrderRequestActionGroup
                    orderby webOrderRequestActionGroup.Key descending
                    select webOrderRequestActionGroup;

                foreach (var group in queryWebOrderRequestDates)
                {
                    WebOrderRequestActionGroupViewModel webOrderRequestActionGroupViewModel = new WebOrderRequestActionGroupViewModel();
                    webOrderRequestActionGroupViewModel.ActionDate = DateTime.Parse(group.Key.ToString());
                    foreach (var subGroup in group)
                    {
                        WebOrderRequestAction webOrderRequestAction = new WebOrderRequestAction();
                        webOrderRequestAction.ID = subGroup.ID;
                        webOrderRequestAction.ActionCode = subGroup.ActionCode;
                        webOrderRequestAction.Title = subGroup.Title;
                        webOrderRequestAction.Note = subGroup.Note;
                        webOrderRequestAction.ActionDate = subGroup.ActionDateTime;
                        webOrderRequestAction.CreatedByCooperatorName = subGroup.CreatedByCooperatorName;
                        webOrderRequestActionGroupViewModel.WebOrderRequestActions.Add(webOrderRequestAction);
                    }
                    viewModel.WebOrderRequestActionGroupViewModels.Add(webOrderRequestActionGroupViewModel);
                }
            }
            catch (Exception ex)
            {

            }
            return viewModel;
        }
        public ActionResult Search(WebOrderRequestSearchViewModel webOrderRequestSearchViewModel)
        {
            GRINGlobalService grinGlobalService = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
            Models.Query query = new Models.Query();

            try
            {
                //if (webOrderRequestSearchViewModel.SelectedStatusCode != "ANY")
                //{
                //    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wor.status_code", FieldValue = webOrderRequestSearchViewModel.SelectedStatusCode, SearchOperatorCode = "=", DataType = "NVARCHAR" };
                //    query.QueryCriteria.Add(queryCriterion);
                //}

                //if (webOrderRequestSearchViewModel.SelectedTimeFrameCode > 0)
                //{
                //    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "time_frame_code", FieldValue = webOrderRequestSearchViewModel.SelectedTimeFrameCode.ToString(), SearchOperatorCode = "=", DataType = "INT" };
                //    query.QueryCriteria.Add(queryCriterion);
                //}

                if (!String.IsNullOrEmpty(webOrderRequestSearchViewModel.RequestorEmailAddress))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wc.email", FieldValue = webOrderRequestSearchViewModel.RequestorEmailAddress.ToString(), SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(webOrderRequestSearchViewModel.RequestorFirstName))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wc.first_name", FieldValue = webOrderRequestSearchViewModel.RequestorFirstName.ToString(), SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(webOrderRequestSearchViewModel.RequestorLastName))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wc.last_name", FieldValue = webOrderRequestSearchViewModel.RequestorLastName, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }


                // Re-initialize main index view model, adding search results.
                webOrderRequestSearchViewModel.WebOrderRequests = grinGlobalService.SearchWebOrderRequests(query);
                webOrderRequestSearchViewModel.Statuses = grinGlobalService.GetWebOrderRequestStatuses();
                webOrderRequestSearchViewModel.IntendedUseCodes = new SelectList(grinGlobalService.GetWebOrderRequestIntendedUseCodes(), "Name", "Description");
                return View("~/Views/GRINGlobal/WebOrder/Index.cshtml", webOrderRequestSearchViewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
                return View("~/Views/Error/_Error.cshtml");
            }
        }

        public PartialViewResult _Search(string statusCode, int timeFrameCode, string requestorEmail, string requestorFirstName, string requestorLastName, string intendedUseCode, string selectedDateRange)
        {
            WebOrderRequestListViewModel webOrderRequestListViewModel = new WebOrderRequestListViewModel();
            GRINGlobalService grinGlobalService = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
            Models.Query query = new Models.Query();

            try
            {
                if (!String.IsNullOrEmpty(requestorEmail))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wc.email", FieldValue = requestorEmail, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(requestorFirstName))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wc.first_name", FieldValue = requestorFirstName, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(requestorLastName))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wc.last_name", FieldValue = requestorLastName, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                if (!String.IsNullOrEmpty(intendedUseCode))
                {
                    QueryCriterion queryCriterion = new QueryCriterion { FieldName = "wor.intended_use_code", FieldValue = intendedUseCode, SearchOperatorCode = "LIKE", DataType = "NVARCHAR" };
                    query.QueryCriteria.Add(queryCriterion);
                }

                webOrderRequestListViewModel.WebOrderRequests = grinGlobalService.SearchWebOrderRequests(query);

                return PartialView("~/Views/GRINGlobal/WebOrder/_List.cshtml", webOrderRequestListViewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }
    }
}