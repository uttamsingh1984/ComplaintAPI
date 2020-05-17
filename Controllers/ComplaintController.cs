using System;
using System.Collections.Generic;
using ComplaintApi.Models;
using Microsoft.AspNetCore.Mvc;
using ComplantApi.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController: ControllerBase
    {
        private ComplaintDbContext _complaintDbContext;
        public ComplaintController(ComplaintDbContext complaintDbContext)
        {
            _complaintDbContext=complaintDbContext;
        }

        [HttpGet]
        [Route("list/{searchText}")]
        public List<Complaint> GetComplaints(string searchText)
        {            
            return _complaintDbContext.Complaints.Include(x=>x.Organization).Where(x=> x.Title.Contains(searchText)).ToList();
        }

        [HttpGet]
        [Route("get/{Id}")]
        public Complaint Get(int Id)
        {
            return _complaintDbContext.Complaints.Include(x=>x.Organization)
            .Include(x=>x.Organization.Addresses)
            .ThenInclude(y=>y.Country)                        
            .Include(x=>x.Category)
            .Where(x=>x.Id==Id).FirstOrDefault();
        }

        [HttpPost]
        [Route("add")]
        [Authorize("AuthPolicy")]
        public bool Post(Complaint complaint)
        {
            if(complaint.Organization != null && complaint.Organization.Id != 0){
                var organization = _complaintDbContext.Organizations.FirstOrDefault(x=>x.Id== complaint.Organization.Id);
                if(organization != null){
                    complaint.Organization=organization;
                }                    
            }
            else{
                if(complaint.Organization != null && complaint.Organization.Addresses.Count > 0 && complaint.Organization.Addresses[0].Country.Id != 0)
                {
                    var country = _complaintDbContext.Countries.FirstOrDefault(x=>x.Id== complaint.Organization.Addresses[0].Country.Id);
                    if(country != null){
                        complaint.Organization.Addresses[0].Country=country;
                    }                    
                }                
            }
            if(complaint.Category != null && complaint.Category.Id != 0){
                var category = _complaintDbContext.Categories.FirstOrDefault(x=>x.Id== complaint.Category.Id);
                if(category != null)
                    complaint.Category=category;
            }

            
            _complaintDbContext.Complaints.Add(complaint);
            _complaintDbContext.SaveChanges();
            return true;

        }
    }

}
