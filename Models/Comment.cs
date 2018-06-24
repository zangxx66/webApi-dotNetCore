using System;

namespace WebAPI.Models {
    public class Comment {
        public Guid Id { get; set; }
        public string Context { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public Comment parentComment { get; set; }
        public DateTime CreateDate { get; set; }
    }
}