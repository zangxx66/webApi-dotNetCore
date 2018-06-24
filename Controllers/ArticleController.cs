using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.Controllers {
    [Route ("api/Article")]
    public class ArticleController : Controller {
        private readonly Context _dbContext;
        public ArticleController (Context db) {
            _dbContext = db;
        }

        [HttpGet]
        public IActionResult Get (int Page, string QueryStr) {
            var list = this._dbContext.Article.OrderByDescending (x => x.CreateDate).Skip (10 * (Page - 1)).Take (10).AsQueryable ();
            if (!string.IsNullOrEmpty (QueryStr)) { list = list.Where (x => x.Title.Contains (QueryStr)); }
            var result = JsonConvert.SerializeObject (list);
            return Ok (result);
        }

        [HttpPut ("Update")]
        public IActionResult Put (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<ArticleVM> (jsonStr);
                var id = Guid.Parse (args.Id);
                var article = this._dbContext.Article.Find (id);
                if (article == null) {
                    return NotFound ();
                }
                var categoryId = Guid.Parse (args.Category);
                var category = this._dbContext.Category.Find (categoryId);
                article.Title = args.Title;
                article.Context = args.Context;
                article.Category = category;
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

        [HttpPost ("Add")]
        public IActionResult Post (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<ArticleVM> (jsonStr);
                var categoryId = Guid.Parse (args.Category);
                var category = this._dbContext.Category.Find (categoryId);
                var session = HttpContext.Session.GetString ("usrInfo");
                var usr = new User ();
                if (!string.IsNullOrEmpty (session)) {
                    var usrId = JsonConvert.DeserializeObject<User> (session).Id;
                    usr = this._dbContext.User.Find (usrId);
                } else {
                    return NotFound ();
                }
                var article = new Article ();
                article.Title = args.Title;
                article.Context = args.Context;
                article.Summary = args.Context.Substring (0, 100);
                article.CreateDate = DateTime.Now;
                article.Category = category;
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

        [HttpDelete ("Del")]
        public IActionResult Delete (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<ArticleVM> (jsonStr);
                var id = Guid.Parse (args.Id);
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