﻿using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            WebOrderRequestSearchViewModel viewModel = new WebOrderRequestSearchViewModel();

            try
            {
                return View("~/Views/GRINGlobal/WebOrder/Index.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public PartialViewResult _List()
        {
            IQueryable<WebOrderRequest> webOrderRequests = null;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);

            try
            {
                webOrderRequests = service.GetWebOrderRequests("NRR_FLAGGED");
                return PartialView("~/Views/GRINGlobal/WebOrder/_List.cshtml", webOrderRequests);
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
            IQueryable<WebOrderRequest> webOrderRequests = null;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);

            webOrderRequest.ID = webOrderRequestId;
            webOrderRequest.WebCooperatorID = webCooperatorId;
            webOrderRequest.StatusCode = statusCode;
            webOrderRequest.Note = actionNote;
           
            resultContainer = service.UpdateWebOrderRequest(webOrderRequest);
            webOrderRequests = service.GetWebOrderRequests("NRR_FLAGGED");
            return PartialView("~/Views/GRINGlobal/WebOrder/_List.cshtml", webOrderRequests);

        }

        public ActionResult SetStatus(string statusCode)
        {
            WebOrderRequestEditViewModel viewModel = null;
            //TO DO
            return View("~/Views/GRINGlobal/WebOrder/Edit.cshtml", viewModel);
        }

        public ActionResult Edit(int id)
        {
            TempData["context"] = "Review Web Order Request #" + id;
            ResultContainer resultContainer = null;
            WebOrderRequestEditViewModel viewModel = new WebOrderRequestEditViewModel();
            WebOrderRequest webOrderRequest = null;
            GRINGlobalService service = new GRINGlobalService(this.AuthenticatedUserSession.Environment);

            try
            {
                // Lock the web order request record.
                resultContainer = service.SetReviewStatus(id, AuthenticatedUser.WebCooperatorID, true);

                // Create a "review began" action record.
                resultContainer = service.AddWebOrderRequestAction(new WebOrderRequestAction { WebOrderRequestID = id, ActionCode = "NRR_REVIEW_BEGUN", CreatedByCooperatorID = AuthenticatedUser.WebCooperatorID });

                webOrderRequest = service.GetWebOrderRequest(id);
                viewModel.ID = webOrderRequest.ID;
                viewModel.OrderDate = webOrderRequest.OrderDate;
                viewModel.IntendedUseCode = webOrderRequest.IntendedUseCode;
                viewModel.IntendedUseNote = webOrderRequest.IntendedUseNote;
                viewModel.Note = webOrderRequest.Note;
                viewModel.SpecialInstruction = webOrderRequest.SpecialInstruction;
                viewModel.Cooperator = webOrderRequest.Cooperators.Where(x => x.Type == 1).FirstOrDefault();
                viewModel.WebCooperator = webOrderRequest.Cooperators.Where(x => x.Type == 2).FirstOrDefault();
                viewModel.OwnedDate = webOrderRequest.OwnedDate;
                viewModel.OwnedByCooperatorName = webOrderRequest.OwnedByCooperatorName;
                viewModel.WebOrderRequestItems = webOrderRequest.WebOrderRequestItems;

                // EXTRACT GROUPED DATA
                var queryWebOrderRequestDates =
                    from action in webOrderRequest.WebOrderRequestActions
                    group action by action.ActionDate into webOrderRequestActionGroup
                    orderby webOrderRequestActionGroup.Key descending
                    select webOrderRequestActionGroup;

                foreach (var group in queryWebOrderRequestDates)
                {
                    string DEBUG = DateTime.Parse(group.Key.ToString()).ToShortDateString();

                    WebOrderRequestActionGroupViewModel webOrderRequestActionGroupViewModel = new WebOrderRequestActionGroupViewModel();
                    webOrderRequestActionGroupViewModel.ActionDate = DateTime.Parse(group.Key.ToString());
                    foreach (var subGroup in group)
                    {
                        WebOrderRequestAction webOrderRequestAction = new WebOrderRequestAction();
                        webOrderRequestAction.ID = subGroup.ID;
                        webOrderRequestAction.ActionCode = subGroup.ActionCode;
                        webOrderRequestAction.Note = subGroup.Note;
                        webOrderRequestAction.ActionDate = subGroup.ActionDateTime;
                        webOrderRequestAction.CreatedByCooperatorName = subGroup.CreatedByCooperatorName;
                        webOrderRequestActionGroupViewModel.WebOrderRequestActions.Add(webOrderRequestAction);
                    }
                    viewModel.WebOrderRequestActionGroupViewModels.Add(webOrderRequestActionGroupViewModel);
                }
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
                webOrderRequest.Note = viewModel.Note;

                if (viewModel.Action == OrderRequestAction.NRRReviewEnd)
                {
                    service.SetReviewStatus(viewModel.ID, AuthenticatedUser.WebCooperatorID, false);
                }
                else
                {
                    service.UpdateWebOrderRequest(webOrderRequest);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return View("~/Views/GRINGlobal/WebOrder/Index.cshtml");
        }
    }
}