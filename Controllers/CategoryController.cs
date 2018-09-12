using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace webapi.Controllers
{
    [Route ("api/Category")]
    public class CategoryController:Controller
    {
        private readonly Context _dbContext;
        public CategoryController (Context db) {
            _dbContext = db;
        }

        [HttpGet]
        public IActionResult Get (int Page, string QueryStr) {
            var list = this._dbContext.Category.OrderByDescending (x => x.Sort).Skip (10 * (Page - 1)).Take (10).AsQueryable ();
            if (!string.IsNullOrEmpty (QueryStr)) { list = list.Where (x => x.Name.Contains (QueryStr)); }
            var result = JsonConvert.SerializeObject (list);
            return Ok (result);
        }

        [HttpPut ("Update")]
        public IActionResult Put (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<Category> (jsonStr);
                var category = this._dbContext.Category.Find (args.Id);
                if (category == null) {
                    return NotFound ();
                }
                var categoryId = args.Id;
                category.Name = args.Name;
                category.Sort = args.Sort;
                this._dbContext.Entry (category).CurrentValues.SetValues (category);
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
                var args = JsonConvert.DeserializeObject<Category> (jsonStr);
                var category = new Category ();
                category.Name = args.Name;
                category.Sort = args.Sort;
                this._dbContext.Category.Add (category);
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
                var args = JsonConvert.DeserializeObject<Category> (jsonStr);
                var id = args.Id;
                var category = this._dbContext.Category.Find (id);
                if (category == null) {
                    return NotFound ();
                }
                this._dbContext.Category.Remove (category);
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