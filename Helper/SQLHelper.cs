using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using webApi_dotNetCore.ViewModel;

namespace webApi_dotNetCore
{
    public class SQLHelper
    {
        private readonly Context _dbContext;
        public SQLHelper(Context db)
        {
            this._dbContext = db;
        }

        public object QueryArticlePagination(string columns, string tablename, string orderby, string swhere, int pageIndex, int pageSize)
        {
            var param = $"'{columns}','{tablename}','{orderby}','{swhere}',{pageSize},{pageIndex},10";
            var list = this._dbContext.Article.FromSql("EXECUTE dbo.QueryPagination @p0",param).ToList();
            var count = list.Count();
            var result = new { data = list, total = count, current = pageIndex };
            return result;

        }
    }
}