using AirControllerWebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirControllerWebService.Controllers.REST
{
    public class GetStatusController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                var status = ManageDB.GetStatus();

                return Request.CreateResponse(HttpStatusCode.OK, status);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
