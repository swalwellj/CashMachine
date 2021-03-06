﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SpecialCashMachine.Service;

namespace SpecialCashMachine.UI.Controllers
{
    public class CashMachineController : ApiController
    {
   

        

        public async Task<IHttpActionResult> Get(string amount,int algorithm)
        {
            try
            {
                decimal arg;
                Decimal.TryParse(amount, out arg);

                var change = await CashMachineService.Instance.Dispense(arg, (Algorithm)algorithm);
              
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(change);

                return Ok(json);
            }
            catch (Exception)
            {
                
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            
        }


       
    }
}
