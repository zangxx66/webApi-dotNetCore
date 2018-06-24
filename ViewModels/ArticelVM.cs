using System;
using WebAPI.Models;

namespace WebAPI.ViewModels {
    public class ArticleVM {
        public string Id { get; set; }
        public string Page { get; set; }
        public string QueryStr { get; set; }
        public string Title{get;set;}
        public string Context{get;set;}
        public string Category{get;set;}
    }
}