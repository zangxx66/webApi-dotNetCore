using System;

namespace WebAPI.Models {
    public class Logs {
        public Guid Id { get; set; }
        public string Expcetion { get; set; }
        public DateTime CreateDate { get; set; }

        public Logs(){
            this.Id = Guid.NewGuid();
        }
    }
}