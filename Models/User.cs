using System;
using ComplantApi.DB;
namespace ComplaintApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property        
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
