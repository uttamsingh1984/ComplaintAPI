using System;
using System.Collections.Generic;
using ComplaintApi.Models;
using Microsoft.AspNetCore.Mvc;
using ComplantApi.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComplaintApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController: ControllerBase
    {
         private ComplaintDbContext _complaintDbContext;
        public OrganizationController(ComplaintDbContext complaintDbContext)
        {
            _complaintDbContext=complaintDbContext;
        }

        [HttpGet]
        [Route("list")]
        public List<Organization> GetOrgs()
        {            
            return _complaintDbContext.Organizations.ToList();
        }

        [HttpGet]
        [Route("get/{orgId}")]
        public Organization GetOrganization(int orgId)
        {            
            return _complaintDbContext.Organizations.Include(x=>x.Addresses)
            .ThenInclude(y=>y.Country)
            .FirstOrDefault(x=>x.Id== orgId);
        }
    }
}