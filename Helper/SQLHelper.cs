using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webApi_dotNetCore.ViewModel;
using WebAPI.Models;

namespace webApi_dotNetCore {
    public class SQLHelper {
        private readonly Context _dbContext;
        public SQLHelper (Context db) {
            this._dbContext = db;
        }

        public object QueryArticlePagination (string columns, string tablename, string orderby, string swhere, int pageIndex, int pageSize) {
            var list = this._dbContext.Article.FromSql($"EXEC dbo.QueryPagination @columns=@p0,@tablename=@p1,@orderby=@p2,@swhere=@p3,@pagesize=@p4,@pageindex=@p5,@rowCount=10",columns,tablename,orderby,swhere,pageSize,pageIndex).ToList();
            var count = list.Count ();
            var result = new { data = list, total = count, current = pageIndex };
            return result;

        }
    }
}