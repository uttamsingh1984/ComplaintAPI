using System;
namespace ComplaintApi.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public Organization Organization {get;set;} 

        public ComplaintStatus Status { get; set; }

        public Category Category { get; set; }
    }

    public enum ComplaintStatus
    {
        Pending,
        Resolved        
    }
}
