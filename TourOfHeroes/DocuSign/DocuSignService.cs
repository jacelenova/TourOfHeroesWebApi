using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using Config = System.Configuration.ConfigurationManager;
using Newtonsoft.Json;

namespace TourOfHeroes.DocuSign
{
    public static class DocuSignService
    {
        public static ViewUrl createEmbeddedSigningViewTest()
        {
            DocuSignHelper.configureApiClient(Constants.DocuSignConstants.DemoPath);
            string accountId = DocuSignHelper.loginApi(Config.AppSettings["USERNAME"], Config.AppSettings["PASSWORD"]);

            EnvelopeDefinition envDef = new EnvelopeDefinition();
            envDef.EmailSubject = "[DocuSign C# SDK Jacel] - Please sign this doc";

            Document doc = new Document();
            doc.DocumentBase64 = Constants.DocuSignConstants.base64pdf;
            doc.Name = "SamplePDF.pdf";
            doc.DocumentId = "1";

            envDef.Documents = new List<Document>();
            envDef.Documents.Add(doc);

            Signer signer = new Signer();
            signer.Email = "jupos@pcmylife.com";
            signer.Name = "Steph Curry";
            signer.RecipientId = "1";
            signer.ClientUserId = "1234";

            envDef.Recipients = new Recipients();
            envDef.Recipients.Signers = new List<Signer>();
            envDef.Recipients.Signers.Add(signer);

            signer.Tabs = new Tabs();
            SignHere signHere = new SignHere();
            signHere.DocumentId = "1";
            signHere.PageNumber = "1";
            signHere.RecipientId = "1";
            signHere.XPosition = "100";
            signHere.YPosition = "100";
            signer.Tabs.SignHereTabs.Add(signHere);

            envDef.Status = "sent";

            EnvelopesApi envelopesApi = new EnvelopesApi();
            EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(accountId, envDef);

            RecipientViewRequest viewOptions = new RecipientViewRequest()
            {
                ReturnUrl = "https://www.docusign.com/devcenter",
                ClientUserId = "1234",  // must match clientUserId set in step #2!
                AuthenticationMethod = "email",
                UserName = envDef.Recipients.Signers[0].Name,
                Email = envDef.Recipients.Signers[0].Email
            };

            ViewUrl recipientView = envelopesApi.CreateRecipientView(accountId, envelopeSummary.EnvelopeId, viewOptions);
            Console.WriteLine("ViewUrl:\n{0}", JsonConvert.SerializeObject(recipientView));

            return new ViewUrl();
        }
    }
}