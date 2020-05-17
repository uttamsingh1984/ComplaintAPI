using System;
using System.Collections.Generic;
namespace ComplaintApi.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string LegalName { get; set; }
        public string Website {get;set;}
        public int Rating { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public class Address{
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string PinCode { get; set; }        
        public Country Country { get; set; }
    }
}


