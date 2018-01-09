using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Coin;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

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
        public async Task<OTCPrice> OTCPrice()
        {
            var reModel=new OTCPrice();

            var huobiBuy1 = await _IhuobiService.LegalTenderBuy();
            var huobiSell1 = await _IhuobiService.LegalTenderSell();

            reModel.Access = true;
            reModel.Data = new[]{ string.Format("火币买一:{0}CNY,卖一:{1}CNY", huobiBuy1.Result, huobiSell1.Result) };

            return reModel;
        }
    }
}