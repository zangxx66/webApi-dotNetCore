using System;

namespace WebAPI.Models
{
    public class TimeLine
    {
        public Guid Id {get;set;}

        public string Content{get;set;}

        public DateTime CreateDate{get;set;}

        public TimeLine(){
            this.Id = Guid.NewGuid();
        }
    }
}