using AirControllerWebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirControllerWebService.Controllers.REST
{
    public class SetStatusController : ApiController
    {
        public HttpResponseMessage Get(int mode)
        {
            var change = ManageDB.ChangeStatus(Convert.ToByte(mode));

            if (change)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
