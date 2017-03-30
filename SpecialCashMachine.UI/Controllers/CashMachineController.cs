using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SpecialCashMachine.Service;

namespace SpecialCashMachine.UI.Controllers
{
    public class CashMachineController : ApiController
    {
   

        

        public async Task<IHttpActionResult> Get(string amount,int algorithm)
        {
            decimal arg;
            Decimal.TryParse(amount, out arg);

            var change = CashMachineService.Instance.Dispense(arg, (Algorithm)algorithm);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(change);
            return Ok(json);
        }


       
    }
}
