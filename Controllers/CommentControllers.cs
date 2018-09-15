using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [Route ("api/[controller]")]
    public class CommentControllers : Controller {
        private readonly Context _dbContext;
        public CommentControllers(Context db){
            this._dbContext = db;
        }

        [HttpGet]
        public IActionResult Get()
    }
}