using System;
using System.Collections.Generic;

namespace WebAPI.Models {
    public class Article {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Context { get; set; }
        public User Anthor { get; set; }
        public string Category { get; set; }
        public bool Enable { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public DateTime CreateDate { get; set; }

        public Article () {
            this.Id = Guid.NewGuid ();
        }
    }
}