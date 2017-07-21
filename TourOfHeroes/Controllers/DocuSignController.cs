using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;

namespace TourOfHeroes.Controllers
{
    public class DocuSignController : ApiController
    {
        public ViewUrl Get()
        {
            ViewUrl x = TourOfHeroes.DocuSign.DocuSignService.createEmbeddedSigningViewTest();
            return x;
        }
    }
}
