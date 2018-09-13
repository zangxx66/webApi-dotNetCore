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
    public class CommentController:Controller
    {
        private readonly Context _dbContext;
        public CommentController (Context db) {
            _dbContext = db;
        }

        [HttpDelete ("Del")]
        public IActionResult Delete (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<Comment> (jsonStr);
                var id = args.Id;
                var comment = this._dbContext.Comment.Find (id);
                if (comment == null) {
                    return NotFound ();
                }
                this._dbContext.Comment.Remove (comment);
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