using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using TourOfHeroes.Constants;

namespace TourOfHeroes.DocuSign
{
    public static class DocuSignHelper
    {
        public static void configureApiClient(string basePath)
        {
            ApiClient apiClient = new ApiClient(basePath);
            Configuration.Default.ApiClient = apiClient;
        }

        public static string loginApi(string user, string password)
        {
            ApiClient apiClient = Configuration.Default.ApiClient;
            string integratorKey = System.Configuration.ConfigurationManager.AppSettings["INTEGRATOR_KEY"];
            string authHeader = $"{{\"Username\":\"{user}\",\"Password\":\"{password}\",\"IntegratorKey\":\"{integratorKey}\"}}";

            Configuration.Default.AddDefaultHeader(DocuSignConstants.DocuSignHeader, authHeader);

            string accountId = null;

            AuthenticationApi authApi = new AuthenticationApi();
            LoginInformation loginInfo = authApi.Login();

            foreach (LoginAccount loginAcct in loginInfo.LoginAccounts)
            {
                if (loginAcct.IsDefault == "true")
                {
                    accountId = loginAcct.AccountId;
                    break;
                }
            }

            if (accountId == null && loginInfo.LoginAccounts.Count > 0) accountId = loginInfo.LoginAccounts[0].AccountId;

            return accountId;
        }
    }
}