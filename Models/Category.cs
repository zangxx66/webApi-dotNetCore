using System;

namespace WebAPI.Models {
    public class Category {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public Category(){
            this.Id = Guid.NewGuid();
        }
    }
}