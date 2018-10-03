using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace webApi_dotNetCore.Controllers
{
    [Route ("api/TimeLine")]
    public class TimeLineController : Controller
    {
        private readonly Context _dbContext;
        public TimeLineController (Context db) {
            _dbContext = db;
        }

        [HttpGet ("Get")]
        public IActionResult Get () {
            var list = this._dbContext.TimeLines.OrderByDescending (x => x.CreateDate).AsQueryable ();
            return Ok (list);
        }

        [HttpPut ("{jsonStr}")]
        public IActionResult Put (string jsonStr) {
            try {
                var args = JsonConvert.DeserializeObject<TimeLine> (jsonStr);
                var id = args.Id;
                var tl = this._dbContext.TimeLines.Find (id);
                if (tl == null) {
                    return NotFound ();
                }
                tl.Content = args.Content;
                this._dbContext.Entry (tl).CurrentValues.SetValues (tl);
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
                var args = JsonConvert.DeserializeObject<TimeLine> (jsonStr);
                var tl = new TimeLine ();
                tl.Content = args.Content;
                tl.CreateDate = DateTime.Now;
                this._dbContext.TimeLines.Add (tl);
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
                var tl = this._dbContext.TimeLines.Find (id);
                if (tl == null) {
                    return NotFound ();
                }
                this._dbContext.TimeLines.Remove (tl);
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