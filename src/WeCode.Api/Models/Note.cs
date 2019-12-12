using System;

namespace WeCode.Api.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Important { get; set; }
    }
}
