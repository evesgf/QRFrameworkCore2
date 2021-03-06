﻿using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Core.Crawler;
using Data.Entity;
using Infrastructure.Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.CrawlNewsService.CoinNewsService
{
    public class JinseService:IJinseService
    {
        /// <summary>
        /// 更新金色财经推送的快讯
        /// </summary>
        /// <returns></returns>
        public async Task<CrawlerResult<CrawlNews>> UpdatePushNewsFlash()
        {
            var result=new CrawlerResult<CrawlNews>();

            var crawler = Singleton<CrawlerManager>.Instance.GetCrawler();
            crawler.OnCompleted += (s, re) =>
            {
                result.Success = true;
                result.Msg = "爬虫抓取成功！耗时:" + re.Milliseconds;
            };
            crawler.OnError += (s, ex) =>
            {
                result.Success = false;
                result.Msg = "爬虫抓取失败:" + ex;
            };

            //启动爬虫
            var rePageStr = await crawler.Start(new Uri("http://www.jinse.com/lives"), null);

            if (!result.Success) return result;

            try
            {
                var dom = new HtmlParser().Parse(rePageStr);

                //页面元素
                var newsList = dom.QuerySelectorAll(".clearfix").FirstOrDefault();
                var first=NewsFlashItem(newsList);

                //返回
                result.Success = true;
                result.Result = first;
            }
            catch (JsonException ex)
            {
                result.Success = false;
                result.Msg = "Json解析失败:" + ex;
            }

            return result;
        }

        /// <summary>
        /// 将dom元素处理为CrawlNews
        /// </summary>
        /// <returns></returns>
        private CrawlNews NewsFlashItem(IElement element)
        {
            if (element == null) return null;

            //标题
            var title = element.QuerySelector(".live-info").TextContent;

            //重要等级
            var importantLevel = EnumImportantLevel.Level0;
            var star = element.QuerySelector("p.star-wrap-new");
            if (star!=null && star.HasChildNodes)
            {
                importantLevel =(EnumImportantLevel) element.QuerySelector("span.star-wrap-new").QuerySelectorAll("span.star-bright").Count();
            }

            //来源
            var from = CrawlNewsFromDef.JinseFlashFrom;

            //来源地址，快讯类型没必要填
            var fromUrl = string.Empty;

            //来源推送时间
            var pushTime = DateTime.Now.ToString("yyyy-MM-dd");
            pushTime += " " + element.QuerySelector(".live-time").TextContent+":00";

            //这里的内容填充的是原文链接，如果没有则为空，因为快讯没有标题，标题就是内容
            var link = element.QuerySelector(".live-link").NodeValue;
            var content = link;

            //标签，暂时不填
            var tag = "";
            
            //推送等级，根据重要程度判断
            var pushLevel = EnumPushLevel.Level0;
            if (importantLevel == EnumImportantLevel.Level5 || importantLevel == EnumImportantLevel.Level4)
            {
                pushLevel = EnumPushLevel.Level3;

            }
            else if (importantLevel == EnumImportantLevel.Level3 || importantLevel == EnumImportantLevel.Level2)
            {
                pushLevel = EnumPushLevel.Level2;
            }
            else
            {
                pushLevel = EnumPushLevel.Level1;
            }

            //抓取时间
            var addTime = DateTime.Now;

            var reItem = new CrawlNews
            {
                Title = title,
                ImportantLevel = (int)importantLevel,
                From = from,
                FromUrl = from,
                PushTime = pushTime,
                Content = content,
                Tag = tag,
                PushLevel = (int)pushLevel,
                AddTime = addTime.ToMysqlTime()
            };

            return reItem;
        }
    }
}
