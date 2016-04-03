using AirControllerWebService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirControllerWebService.Controllers.REST
{
    public class GetCO2LastValueController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                var value = ManageDB.GetCO2LastValue();

                var valueInt = Convert.ToInt32(value);

                int valueInPercent;

                if (value < 350)
                {
                    valueInPercent = 0;
                }
                else if (value > 1000)
                {
                    valueInPercent = 100;
                }
                else
                {
                    valueInPercent = Convert.ToInt32((value / 100) * 6.5);
                }

                //var valueInPercent =  / 70);

                return Request.CreateResponse(HttpStatusCode.OK, valueInPercent);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
