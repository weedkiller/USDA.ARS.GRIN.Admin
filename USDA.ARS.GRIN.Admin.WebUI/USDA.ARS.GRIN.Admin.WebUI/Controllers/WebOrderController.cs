﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Models.GRINGlobal;
using USDA.ARS.GRIN.Admin.Service;
using USDA.ARS.GRIN.Admin.WebUI.ViewModels.GRINGlobal;
using NLog;

namespace USDA.ARS.GRIN.Admin.WebUI.Controllers
{
    [GrinGlobalAuthentication]
    public class WebOrderController : BaseController
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            TempData["context"] = "Home";
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
                Log.Error(ex, ex.Message);
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
                Log.Error(ex, ex.Message);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        public PartialViewResult Update(int webOrderRequestId, int webCooperatorId, string statusCode, string actionNote)
        {
            TempData["context"] = "Review";
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

        public ActionResult EmailTemplateEdit(int id = 0)
        {
            TempData["context"] = "Edit Email Templates";
            GRINGlobalService grinGlobalService = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
            EmailTemplateEditViewModel emailTemplateEditViewModel = new EmailTemplateEditViewModel();
            emailTemplateEditViewModel.EmailTemplates = grinGlobalService.GetEmailTemplates();

            return View("~/Views/GRINGlobal/WebOrder/Email/Edit.cshtml", emailTemplateEditViewModel);
        }

        public PartialViewResult _EmailTemplateEdit(int id)
        {
            try 
            {
                GRINGlobalService grinGlobalService = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
                EmailTemplateEditViewModel emailTemplateEditViewModel = new EmailTemplateEditViewModel();
                //emailTemplateEditViewModel.EmailTemplates = grinGlobalService.GetEmailTemplates();
                return PartialView("~/Views/GRINGlobal/WebOrder/Email/_Detail.cshtml", emailTemplateEditViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult EmailTemplateEdit(EmailTemplateEditViewModel emailTemplateEditViewModel)
        {
            try 
            {
                return RedirectToAction("EmailTemplateEdit","WebOrder", new { id = emailTemplateEditViewModel.ID });
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(WebOrderRequestEditViewModel viewModel)
        {
            ResultContainer resultContainer = null;
            GRINGlobalService grinGlobalService = new GRINGlobalService(this.AuthenticatedUserSession.Environment);
            SmtpService smtpService = new SmtpService();

            try
            {
                WebOrderRequest webOrderRequest = new WebOrderRequest();
                webOrderRequest.ID = viewModel.ID;
                webOrderRequest.StatusCode = viewModel.Action;

                if (viewModel.Action == OrderRequestAction.NRRReviewEnd)
                {
                    grinGlobalService.SetReviewStatus(viewModel.ID, AuthenticatedUser.WebCooperatorID, false);
                }
                else
                {
                    if ((viewModel.Action != "NRR_NOTE") && (viewModel.Action != "NRR_INFO"))
                    {
                        resultContainer = grinGlobalService.UpdateWebOrderRequest(webOrderRequest);
                    }
                    resultContainer = grinGlobalService.AddWebOrderRequestAction(new WebOrderRequestAction { WebOrderRequestID = viewModel.ID, ActionCode = viewModel.Action, Note = viewModel.ActionNote, CreatedByCooperatorID = AuthenticatedUser.WebCooperatorID });

                    EmailMessage emailMessage = new EmailMessage();
                    emailMessage.From = "gringlobal.orders@usda.gov";
                    emailMessage.IsHtml = true;

                    if (viewModel.Action == "NRR_APPROVE")
                    {
                        emailMessage.To = "benjamin.haag@usda.gov";
                        emailMessage.Subject = "NRR Review Update: Order #" + webOrderRequest.ID + " Approved";
                        emailMessage.Body = "Order # " + webOrderRequest.ID + " has been approved. You may now process it normally via the Order Wizard.";
                        smtpService.SendMessage(emailMessage);
                    }
                    else
                        if (viewModel.Action == "NRR_REJECT")
                    {
                        emailMessage.To = viewModel.WebCooperator.EmailAddress;
                        emailMessage.Subject = "Your Germplasm Request (Order #" + webOrderRequest.ID + ")";

                        System.Text.StringBuilder sbEmailBody = new System.Text.StringBuilder();
                        sbEmailBody.Append("Dear Germplasm Requestor,<p>Thank you for your interest in our germplasm collection.The mission of the National Plant Germplasm System(NPGS)");
                        sbEmailBody.Append("is to provide materials in small quantities to research and education entities when genetic diversity or genetic standards are a");
                        sbEmailBody.Append("requirement.The accessions maintained by NPGS are not intended for home or personal use that can");
                        sbEmailBody.Append("be better served by commercially - available varieties.");
                        sbEmailBody.Append("*** PLACEHOLDER FOR TEMPLATE TEXT ***");
                        emailMessage.Body = sbEmailBody.ToString();
                        smtpService.SendMessage(emailMessage);

                        // EMAIL TO CURATORS
                        emailMessage.To = "benjamin.haag@usda.gov";
                        emailMessage.Subject = "NRR Review Update: Order " + webOrderRequest.ID + " Canceled";
                        emailMessage.Body = "Order # " + webOrderRequest.ID + " has been determined to be a Non-Research Request (NRR), and has been cancelled. You may reference this order within the Order Wizard.";
                        smtpService.SendMessage(emailMessage);
                    }
                    else
                        if (viewModel.Action == "NRR_INFO")
                        {
                        emailMessage.To = viewModel.WebCooperator.EmailAddress;
                        emailMessage.Subject = "Request For Additional Information: Order " + webOrderRequest.ID;
                        emailMessage.Body = viewModel.InformationRequestText;
                        smtpService.SendMessage(emailMessage);
                            
                    }
                }
                if ((viewModel.Action != "NRR_NOTE") && (viewModel.Action != "NRR_INFO"))
                {
                    return RedirectToAction("Index", "WebOrder");
                }
                else
                {
                    return RedirectToAction("Edit", "WebOrder", new { id = viewModel.ID });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                //TODO: CHANGE
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
                viewModel.WebCooperator = webOrderRequest.Cooperators.First();
                viewModel.IsReviewMode = reviewMode;
                viewModel.IsLocked = webOrderRequest.IsLocked;
                viewModel.ID = webOrderRequest.ID;
                viewModel.OrderDate = webOrderRequest.OrderDate;
                viewModel.IntendedUseCode = webOrderRequest.IntendedUseCode;
                viewModel.IntendedUseNote = webOrderRequest.IntendedUseNote;
                viewModel.Note = webOrderRequest.Note;
                viewModel.EmailAddressList = webOrderRequest.EmailAddressList;

                // TODO: Refactor; use stored template BH 7/2/21
                viewModel.InformationRequestText = "Germplasm Requestor:";
                viewModel.InformationRequestText += "The U.S.National Plant Germplasm System(NPGS) provides plant material in small quantities to research and educational entities for projects where genetic diversity is required. Accessions maintained by the NPGS are not intended or available for home, personal, or community gardening.";
                viewModel.InformationRequestText += "Please provide additional relevant information about your project to justify the need for specific NPGS germplasm, instead of using commercially available plant material.";
                viewModel.InformationRequestText += "Send your email to gringlobal.feedback @usda.gov, and include the web order number.";
                viewModel.InformationRequestText += "For more information about the NPGS, please view a 6 - minute video that describes our mission and purpose at https://www.youtube.com/watch?v=uHOclGNELuw.";
                viewModel.InformationRequestText += "Thank you.";
                
                viewModel.SpecialInstruction = webOrderRequest.SpecialInstruction;
                viewModel.OwnedDate = webOrderRequest.OwnedDate;
                viewModel.OwnedByCooperatorName = webOrderRequest.OwnedByCooperatorName;
                viewModel.WebOrderRequestItems = webOrderRequest.WebOrderRequestItems;
                viewModel.WebOrderRequestAddresses = webOrderRequest.Addresses;

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
                Log.Error(ex, ex.Message);
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
                Log.Error(ex, ex.Message);
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        public PartialViewResult _Search(string statusCode, int timeFrameCode, string requestorEmail, string requestorFirstName, string requestorLastName, string intendedUseCode, string selectedDateRange, string startDate, string endDate)
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

                if (!String.IsNullOrEmpty(selectedDateRange))
                {
                    string[] dateRangeTokens = selectedDateRange.Replace(" to ", "_").Split('_');
                    if (dateRangeTokens != null)
                    {
                        QueryCriterion queryCriterionStart = new QueryCriterion { FieldName = "wor.ordered_date", FieldValue = dateRangeTokens[0], SearchOperatorCode = ">=", DataType = "DATETIME" };
                        query.QueryCriteria.Add(queryCriterionStart);

                        QueryCriterion queryCriterionEnd = new QueryCriterion { FieldName = "wor.ordered_date", FieldValue = dateRangeTokens[1], SearchOperatorCode = "<=", DataType = "DATETIME" };
                        query.QueryCriteria.Add(queryCriterionEnd);
                    }
                }

                webOrderRequestListViewModel.WebOrderRequests = grinGlobalService.SearchWebOrderRequests(query);

                return PartialView("~/Views/GRINGlobal/WebOrder/_List.cshtml", webOrderRequestListViewModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return PartialView("~/Views/Error/_Error.cshtml");
            }
        }

        protected string GetEmailAddressList(int emailCategory, string siteShortName)
        {
            string emailAddressList = String.Empty;

            // TODO

            //         CASE s.site_short_name
            //   when 'BRW' then 'lj.grauke@ars.usda.gov'
            //  -- when 'CLO' then 'merrelyn.spinks@ars.usda.gov'
            //   when 'COR' then 'Missy.Fix@ars.usda.gov'
            //   when 'COT' then 'james.frelichowski@ars.usda.gov;janna.love@ars.usda.gov'
            //   when 'DAV' then 'ordersNCGR.davis@ars.usda.gov'
            //   when 'DBMU' then 'benjamin.haag@usda.gov'
            //   when 'DLEG' then 'mjohnson@ag.arizona.edu'
            //  -- when 'FLAX' then 'nc7mb@ars-grin.gov'
            //  -- when 'FRA' then 'esnake@mis.net'
            //   when 'GEN' then 'dawn.dellefave@ars.usda.gov;ben.gutierrez@ars.usda.gov'
            //   --grin:
            //         --gsho:
            //   when 'GSOR' then 'lorie.bernhardt@ars.usda.gov'
            //   when 'GSPI' then 'barbara.hellier@ars.usda.gov;alec.mccall@wsu.edu;stoutd@wsu.edu;lisa.taylor@usda.gov'
            //   --gstr:
            //         when 'GSZE' then 'maize@uiuc.edu'
            //   when 'HILO' then 'Carol.MayoRiley@ars.usda.gov;Tracie.Matsumoto@ars.usda.gov'
            //   when 'MAY' then 'tomas.ayala-silva@usda.gov;ricardo.goenaga@usda.gov'
            //   when 'MIA' then 'Mike.Winterstein@usda.gov;Ricardo.Goenaga@usda.gov'
            //   when 'NA' then 'kevin.conrad@ars.usda.gov'
            //   when 'NC7' then 'nc7orders@ars.usda.gov;lisa.burke@ars.usda.gov'
            //   when 'NE9' then 'Joanne.Labate@ARS.USDA.GOV;sherri.tennies@ars.usda.gov'
            //   when 'NR6' then 'jesse.schartner@ars.usda.gov;mwmarti1@wisc.edu'
            //   when 'NSGC' then 'harold.bockelman@ars.usda.gov'
            //   when 'NSSL' then 'renee.white@ars.usda.gov'
            //   when 'OPGC' then 'stieve.1@osu.edu'
            //   --orders:
            //         --when 'PALM' then 'danny.barney@ars.usda.gov'
            //   when 'PARL' then 'Claire.Heinitz@ars.usda.gov'
            //   when 'PEO' then 'karen.williams@ars.usda.gov'
            //  -- when 'PGQO' then 'steven.a.king@aphis.usda.gov'
            //   --puborder:
            //         when 'RIV' then 'Robert.krueger@ars.usda.gov'
            //   when 'SBML' then 'melanie.schori@ars.usda.gov'
            //   when 'SOY' then 'Todd.Bedford@ars.usda.gov;esther.peregrine@ars.usda.gov'
            //   when 'S9' then 'tiffany.fields@ars.usda.gov;ARS-S9Orders@ars.usda.gov'
            //   when 'TGRC' then 'trchetelat@ucdavis.edu'
            //   when 'TOB' then 'jessica_nifong@ncsu.edu'
            //   when 'W6' then 'stoutd@wsu.edu;barbara.hellier@ars.usda.gov;lisa.taylor@usda.gov;david.vanklaveren@wsu.edu;alec.mccall@wsu.edu;'
            //   else 'benjamin.haag@usda.gov'
            //END AS email,
            return emailAddressList;
        }

        protected ResultContainer SendEmail(int emailCategory, WebOrderRequest webOrderRequest)
        {
            ResultContainer resultContainer = new ResultContainer();

            // TODO


            return resultContainer;
        }
    }
}