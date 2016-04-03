using AirControllerWebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirControllerWebService.Controllers.REST
{
    public class SetCO2ValueController : ApiController
    {
        public HttpResponseMessage Get(int value)
        {
            try
            {
                ManageDB.SetCO2Value(value);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
