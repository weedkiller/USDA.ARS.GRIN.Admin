﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using GrinGlobal.Core;
using USDA.ARS.GRIN.Admin.Models;
using USDA.ARS.GRIN.Admin.Repository;
using System.Security.Authentication;


namespace USDA.ARS.GRIN.Admin.Service
{
    public class SecurityService
    {
        protected UserDAO _userDAO;
        protected ApplicationDAO _applicationDAO;
        protected SmtpService _smtpService;

        public SecurityService(string context)
        {
            _userDAO = new UserDAO(context);
            _applicationDAO = new ApplicationDAO(context);
            _smtpService = new SmtpService();
        }

        public string GenerateRandomPassword(int length)
        {
            RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider();
            byte[] tokenBuffer = new byte[length];
            cryptRNG.GetBytes(tokenBuffer);
            return Convert.ToBase64String(tokenBuffer);
        }

        public ResultContainer UpdatePassword(User user)
        {
            string hashedPassword = String.Empty;

            Crypto crypto = new Crypto();
            ResultContainer resultContainer = new ResultContainer();
            hashedPassword = new Crypto().Hash(user.Password);
            hashedPassword = Crypto.HashText(hashedPassword);
            user.Password = hashedPassword;
            resultContainer = _userDAO.Update(user);
            return resultContainer;
        }

        public ResultContainer Login(string userName, string password, out User user)
        {
            string hashedPassword = String.Empty;
            Query query = new Query();
            ResultContainer resultContainer = new ResultContainer();
            bool passwordIsValid = false;
            user = null;
           
            try
            {
                query.QueryCriteria.Add(new QueryCriterion { FieldName = "su.user_name", SearchOperatorCode = "LIKE", DataType="NVARCHAR", FieldValue = userName });
                user = _userDAO.Search(query).FirstOrDefault();
                if (user.ID == 0)
                {
                    resultContainer.ResultCode = LoginResult.USER_NOT_FOUND.ToString();
                    return resultContainer;
                }

                hashedPassword = Crypto.HashText(password);
                passwordIsValid = (validateHashedPassword(password, user.Password) || validateHashedPassword(hashedPassword, user.Password));

                if (passwordIsValid)
                    resultContainer.ResultCode = LoginResult.SUCCESS.ToString();
                else
                {
                    resultContainer.ResultCode = LoginResult.INVALID_PASSWORD.ToString();
                    return resultContainer;
                }
            }
            catch (Exception aex)
            {
                resultContainer.ResultCode = LoginResult.SYSTEM_ERROR.ToString();
                resultContainer.ResultDescription = aex.Message;
            }
            return resultContainer;
        }

        public Application GetApplication(string appContext)
        {
            return _applicationDAO.Get(appContext);
        }

        //public List<Cooperator> GetAuthorizedCooperators(string applicationName)
        //{ 
        //    //return _userDAO.G
        //}

        private bool validateHashedPassword(string password, string storedSaltHash)
        {
            string crypt;
            string salt;
            string storedHash;

            // parse the stored password field for hash type, salt, and hashed password
            string[] hashes = storedSaltHash.Split(':');
            string[] passField = hashes[0].Split('$');
            if (passField.Length == 1)
            {
                // original format of SHA1 hash with no salt
                crypt = "SHA1";
                salt = "";
                storedHash = passField[0];
            }
            else if (passField.Length == 2)
            {
                // two fields means salt and hash
                crypt = "SHA256";
                salt = passField[0];
                storedHash = passField[1];
            }
            else if (passField.Length == 3)
            {
                // with three fields the first is the hash type
                crypt = passField[0];
                salt = passField[1];
                storedHash = passField[2];
            }
            else
            {
                // can't figure out what is stored in the hash field
                return false;
            }

            string hashedPassword;
            if (crypt == "SHA1")
            {
                hashedPassword = Crypto.HashText(salt + password);
            }
            else if (crypt == "SHA256")
            {
                hashedPassword = Crypto.HashTextSHA256(salt + password);
            }
            else
            {
                // don't understand the hash type
                return false;
            }

            // Finally we test whether it is a match
            if (hashedPassword == storedHash) return true;

            return false;
        }

    }

    public enum LoginResult 
    { 
        SUCCESS,
        USER_NOT_FOUND,
        INVALID_PASSWORD,
        SYSTEM_ERROR
    }
}
