using System;

namespace WebAPI.Models {
    public class User {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
        public int QQ { get; set; }
        public string Git { get; set; }
        public string Twitter { get; set; }
        public string Weibo { get; set; }
        public string RegIp { get; set; }
        public DateTime RegDate { get; set; }
        public string LastIp { get; set; }
        public DateTime LastDate { get; set; }
        public int Role { get; set; }
    }
}