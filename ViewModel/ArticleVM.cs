using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace webApi_dotNetCore.ViewModel {
    public class ArticleVM {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Show { get; set; }

        public List<ArticleVM> ArticleList (IQueryable<Article> list) {
            var result = new List<ArticleVM> ();
            foreach (var item in list) {
                var args = new ArticleVM ();
                args.Id = item.Id;
                args.Title = item.Title;
                args.Summary = item.Summary;
                args.CreateDate = item.CreateDate;
                args.Show = item.Show;
                result.Add (args);
            }
            return result;
        }

        public ArticleVM () { }
    }
}