using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Coin;
using Business.CrawlNewsService.CoinNewsService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("any")] //设置跨域处理的代理
    [Produces("application/json")]
    [Route("api/News")]
    public class NewsController : Controller
    {
        private readonly IJinseService _jinseService;

        public NewsController(IJinseService jinseService)
        {
            _jinseService = jinseService;
        }

        /// <summary>
        /// 更新金色财经消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<NewsModel> UpdateJinseNews()
        {
            var reModel = new NewsModel();

            var news = await _jinseService.UpdatePushNewsFlash();

            reModel.Title = news.Result.Title;
            reModel.Content = news.Result.Content;
            reModel.PushTime= news.Result.PushTime;
            reModel.Tag = news.Result.Tag;
            reModel.From= news.Result.From;

            return reModel;
        }
    }
}