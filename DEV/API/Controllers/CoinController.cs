using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Coin;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [EnableCors("any")] //设置跨域处理的代理
    [Produces("application/json")]
    [Route("api/Coin")]
    public class CoinController : Controller
    {
        private readonly IhuobiService _IhuobiService;

        public CoinController(IhuobiService huobiService)
        {
            _IhuobiService = huobiService;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            var reStr = string.Empty;

            reStr = string.Format("火币买一:{0}CNY,卖一:{0}CNY", _IhuobiService.LegalTenderBuy());

            return reStr;
        }
    }
}