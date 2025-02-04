﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Models.GRINGlobal;
using USDA.ARS.GRIN.Admin.Repository;
using USDA.ARS.GRIN.Admin.Repository.GRINGlobal;

namespace USDA.ARS.GRIN.Admin.Service
{
    public class GRINGlobalService : BaseService
    {
        protected AccessionInventoryAttachmentDAO _accessionInventoryAttachmentDAO = null;
        protected WebOrderRequestDAO _webOrderRequestDAO = null;
        protected EmailTemplateDAO _emailTemplateDAO = null;
        
        public GRINGlobalService(string context)
        {
            _accessionInventoryAttachmentDAO = new AccessionInventoryAttachmentDAO(context);
            _webOrderRequestDAO = new WebOrderRequestDAO(context);
            _emailTemplateDAO = new EmailTemplateDAO(context);
        }

        public AccessionInventoryAttachment GetAccessionInventoryAttachment(int id)
        {
            return _accessionInventoryAttachmentDAO.Get(id);
        }

        public IQueryable<USDA.ARS.GRIN.Admin.Models.GRINGlobal.AccessionInventoryAttachment> GetAccessionInventoryAttachments(int batchSize, string validationStatus = "")
        {
            IQueryable<USDA.ARS.GRIN.Admin.Models.GRINGlobal.AccessionInventoryAttachment> accessionInventoryAttachments = new List<Models.GRINGlobal.AccessionInventoryAttachment>().AsQueryable();
            accessionInventoryAttachments = _accessionInventoryAttachmentDAO.Find(batchSize, validationStatus);
            return accessionInventoryAttachments;
        }

        public void AddValidationRecord(int accessionInventoryAttachId, bool imageUrlIsValid, bool thumbnailImageUrlIsValid)
        {
            _accessionInventoryAttachmentDAO.AddValidation(accessionInventoryAttachId, imageUrlIsValid, thumbnailImageUrlIsValid);
        }

        public WebOrderRequest GetWebOrderRequest(int id)
        {
            return _webOrderRequestDAO.Get(id);   
        }

        public IQueryable<WebOrderRequest> GetWebOrderRequests(string statusCode, int timeFrameCode)
        {
            return _webOrderRequestDAO.SearchByStatus(statusCode, timeFrameCode);
        }
        public IQueryable<WebOrderRequest> SearchWebOrderRequests(Query query)
        {
            return _webOrderRequestDAO.Search(query);
        }

        public List<ReferenceItem> GetWebOrderRequestStatuses()
        {
            return _webOrderRequestDAO.GetStatuses();
        }
        public List<ReferenceItem> GetWebOrderRequestIntendedUseCodes()
        {
            return _webOrderRequestDAO.GetIntendedUseCodes();
        }

        public ResultContainer AddWebOrderRequestAction(WebOrderRequestAction webOrderRequestAction)
        {
            return _webOrderRequestDAO.AddAction(webOrderRequestAction);
        }

        public ResultContainer UpdateWebOrderRequest(WebOrderRequest webOrderRequest)
        {
            return _webOrderRequestDAO.Update(webOrderRequest);
        }
        public ResultContainer SetReviewStatus(int id, int webCooperatorId, bool locked)
        {
            return _webOrderRequestDAO.UpdateLockStatus(id, webCooperatorId, locked);
        }

        public Dictionary<string, int> GetValidationCounts()
        {
            return _accessionInventoryAttachmentDAO.GetValidationCounts();
        }

        #region Email

        public IQueryable<EmailTemplate> GetEmailTemplates()
        {
            return _emailTemplateDAO.FindAll(); 
        }

        #endregion Email
    }
}
