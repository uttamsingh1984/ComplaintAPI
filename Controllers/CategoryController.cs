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
    public class CategoryController: ControllerBase
    {
         private ComplaintDbContext _complaintDbContext;
        public CategoryController(ComplaintDbContext complaintDbContext)
        {
            _complaintDbContext=complaintDbContext;
        }

        [HttpGet]
        [Route("list")]
        public List<Category> GetCountries()
        {            
            return _complaintDbContext.Categories.ToList();
        }
    }
}