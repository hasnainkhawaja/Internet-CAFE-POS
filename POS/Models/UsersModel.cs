using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace POS.Models
{
    public class UsersModel
    {
         
        public IEnumerable<POSEntity.EmployeeInfomation_GetAll_Result> UserList;


        public string employeeid { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "userName")]
        public string userName { get; set; }



        [Required(ErrorMessage = "*")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "*")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "*")]
        public string employeeCode { get; set; }
        public string employeeGender { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string employeeEmail { get; set; }
        public bool active { get; set; }	
        public string aspnet_userid { get; set; }
        public int companyid { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }


//        [Required(ErrorMessage = "Username is Required")]  
//        //[RegularExpression(@"^[a-zA-Z0-9]+$",ErrorMessage = "user name must be combination of letters and numbers only.")]  
//        [Remote("UsernameExists", "Users", HttpMethod = "POST", ErrorMessage = "User name already registered.")]
//        public string userName  
//{  
//    get;  
//    set;  
//}  

        public class UsersContext : DbContext
        {
            public UsersContext()
                : base("DefaultConnection")
            {
            }

        }


        public IEnumerable<SelectListItem> employeeGenders { get; set; }

      


//        public DbSet<Users> Users { get; set; }

  //      DbSet<EmployeeInfomation> EmployeeInfomations { get; set; }

    }
}

public enum Gender
{
    Male,
    Female
}