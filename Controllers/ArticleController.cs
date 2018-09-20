using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapi.Helper;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [Route ("api/Article")]
    public class ArticleController : Controller {
        private readonly Context _dbContext;
        public ArticleController (Context db) {
            _dbContext = db;
        }

        [HttpGet ("Get")]
        public IActionResult Get (string QueryStr, int Page = 1) {
            var list = this._dbContext.Article.OrderByDescending (x => x.CreateDate).Skip (10 * (Page - 1)).Take (10).AsQueryable ();
            if (!string.IsNullOrEmpty (QueryStr)) { list = list.Where (x => x.Title.Contains (QueryStr)); }
            var obj = new { data = list, total = list.Count (), current = Page };
            return Ok (obj);
        }

        [HttpGet ("Detail")]
        public IActionResult Detail (string param) {
            var id = Guid.Parse (param);
            var acticle = this._dbContext.User.Find (id);
            if (acticle == null) {
                return NotFound ();
            }
            return Ok (acticle);
        }

        [HttpPut ("{jsonStr}")]
        public IActionResult Put (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<Article> (jsonStr);
                var id = args.Id;
                var article = this._dbContext.Article.Find (id);
                if (article == null) {
                    return NotFound ();
                }
                article.Title = args.Title;
                article.Context = args.Context;
                article.Category = args.Category;
                this._dbContext.Entry (article).CurrentValues.SetValues (article);
                this._dbContext.SaveChanges ();
                return NoContent ();
            } catch (Exception ex) {
                var log = new Logs ();
                log.Expcetion = ex.Message;
                log.CreateDate = DateTime.Now;
                this._dbContext.Logs.Add (log);
                this._dbContext.SaveChanges ();
                return StatusCode (500);
            }

        }

        [HttpPost ("{jsonStr}")]
        public IActionResult Post (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<Article> (jsonStr);
                var usr = new User ();
                var session = HttpContext.Session.GetString("userInfo");
                if (session != null) {
                    var usrId = Guid.Parse (session);
                    usr = this._dbContext.User.Find (usrId);
                } else {
                    return NotFound ();
                }
                var article = new Article ();
                article.Title = args.Title;
                article.Context = args.Context;
                article.Summary = args.Context.Substring (0, 100);
                article.CreateDate = DateTime.Now;
                article.Category = args.Category;
                article.Anthor = usr;
                this._dbContext.Article.Add (article);
                this._dbContext.SaveChanges ();
                return Ok ();
            } catch (Exception ex) {
                var log = new Logs ();
                log.Expcetion = ex.Message;
                log.CreateDate = DateTime.Now;
                this._dbContext.Logs.Add (log);
                this._dbContext.SaveChanges ();
                return StatusCode (500);
            }
        }

        [HttpDelete ("{param}")]
        public IActionResult Delete (string param) {
            try {
                var id = Guid.Parse (param);
                var article = this._dbContext.Article.Find (id);
                if (article == null) {
                    return NotFound ();
                }
                this._dbContext.Article.Remove (article);
                this._dbContext.SaveChanges ();
                return NoContent ();
            } catch (Exception ex) {
                var log = new Logs ();
                log.Expcetion = ex.Message;
                log.CreateDate = DateTime.Now;
                this._dbContext.Logs.Add (log);
                this._dbContext.SaveChanges ();
                return StatusCode (500);
            }
        }

    }
}