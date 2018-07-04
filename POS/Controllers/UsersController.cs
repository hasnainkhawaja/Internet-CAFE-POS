using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSEntity;
using POS.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using POS.Helpers;

namespace POS.Controllers
{
    public class UsersController : Controller
    {
        Int32 _hdnUserId = 0;
        onlineinternetposEntities db = new onlineinternetposEntities();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }




        public ActionResult ShowGrid()
        {

            UsersModel mod = new UsersModel();
            return View(mod);
        }

        public ActionResult ManageUsers()
        {

            UsersModel mod = new UsersModel();
            return View(mod);
        }


        public ActionResult Delete(String ID)
        {

            //UsersModel mod = new UsersModel();
            //return View(mod);
            return View();
        }

        //Delete
        //public JsonResult GetUserList()
        //{
        //    //
        //    try
        //    {
        //        IEnumerable<EmployeeInfomation_GetAll_Result> usr;
        //        usr = UserList();
        //        return Json(new { Result = "OK", Records = usr }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}


      
        public JsonResult GetUserList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //public JsonResult UserList()
        {
            try
            {
                //Users users = new Users();
                IEnumerable<EmployeeInfomation_GetAll_Result> usr;
                usr = UserList();
                //if (clientID > 0)
                //    usr = users.UsersListByFilter(name).Where(o => o.ClientID == clientID);
                //else
                //    usr = users.UsersListByFilter(name);
                //var userCount = usr.Count();
                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Username ASC"))
                {
                    usr = usr.OrderBy(o => o.userName);
                }
                else if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Username DESC"))
                {
                    usr = usr.OrderByDescending(o => o.userName);
                }
                else
                {
                    usr = usr.OrderBy(o => o.userName);
                }

                //usr = usr.OrderBy(o => o.userName);
                usr = jtPageSize > 0 ? usr.Skip(jtStartIndex).Take(jtPageSize).ToList() : usr.ToList();

                var userCount = usr.Count();
                
             //   return Json(new { Result = "OK", Records = usr });

             // return Json(new { Result = "OK", Records = usr}, JsonRequestBehavior.AllowGet);


                //return Json(new { Result = "OK", Records = usr, TotalRecordCount = userCount }, JsonRequestBehavior.AllowGet);
                return Json(new { Result = "OK", recordsFiltered = userCount, recordsTotal = userCount, data = usr });  
  


            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }





        public JsonResult LoadDataForDataTable()
        {
            try
            {
                //var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                //// Skiping number of Rows count
                //var start = Request.Form["start"].FirstOrDefault();
                //// Paging Length 10,20
                //var length = Request.Form["length"].FirstOrDefault();
                //// Sort Column Name
                //var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                //// Sort Column Direction ( asc ,desc)
                //var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                //// Search Value from (Search box)
                ////String searchValue = Request.Form["search[value]"].FirstOrDefault();

                //string searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                ////Paging Size (10,20,50,100)
                //int pageSize = length != null ? Convert.ToInt32(length) : 0;
                //int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data

                //IEnumerable<EmployeeInfomation_GetAll_Result> usr;
                //usr = UserList();
                //var customerData = usr; 
                var customerData =  (from tempcustomer  in db.EmployeeInfomations
                                         select tempcustomer);
                customerData.Where(a => a.companyid == 1);
                //Sorting
                //if (!(String.IsNullOrEmpty(sortColumn)) && (String.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}
                //////Search
                
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    //    //customerdata = customerdata.where(m => m.username == searchvalue || m.employeecode == searchvalue || m.city == searchvalue);
                //    customerData = customerData.Where(m => m.userName == searchValue);
                //}

                //total number of rows count 
                recordsTotal = customerData.Count();
                var data = customerData.ToList();
                //Paging 
                //var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                
                //return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });


                return Json(new { Result = "OK", recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });  

            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpGet]
        public string GetUserDataByUserID(int employeeID)
        {
            //EF.User objUser = objEntities.Users.Where(o => o.UserID == userID && o.SiteID==Utility.SiteID).SingleOrDefault();

            string data = JsonConvert.SerializeObject(db.EmployeeInfomation_GetByID(employeeID));
            return "" + data;
        }

        public IEnumerable<POSEntity.EmployeeInfomation_GetAll_Result> UserList()
        {
            //return db.EmployeeInfomation_GetAll(1).ToList().Where(u => u.active == true).ToList();
            return db.EmployeeInfomation_GetAll(ClientSession.CompanyID).ToList();
        }


        [HttpPost]
        public Boolean DelteUser(int employeeID)
        {
            EmployeeInfomation objEmp = db.EmployeeInfomations.Where(o => o.employeeID == employeeID).SingleOrDefault();
            objEmp.active = false;
            db.SaveChanges();
            return true;
        }
        [HttpPost]
        public ActionResult AddEditUserSave(FormCollection frm, HttpPostedFileBase logoUpload)
        {
            String _retval = "0";
            int _companyId = ClientSession.CompanyID;
            int _hdnUserId = 0;
            String _isActive = "";
            EmployeeInfomation objEmp;
            String n1 = _hdnUserId.ToString();


            if (ModelState.IsValid)
            {
                _hdnUserId = Convert.ToInt32(frm["hdnEmployeeid"].ToString());


               


                if (_hdnUserId > 0)
                {

                    objEmp = db.EmployeeInfomations.Where(o => o.employeeID == _hdnUserId).SingleOrDefault();


                }
                else
                {
                    objEmp = new EmployeeInfomation();
                }






                objEmp.userName = frm["userName"].ToString();
                objEmp.firstName = frm["firstName"].ToString();
                objEmp.lastName = frm["lastName"].ToString();
                objEmp.employeeCode = frm["employeeCode"].ToString();
                objEmp.employeeGender = frm["employeeGender"].ToString();
                objEmp.employeeEmail = frm["employeeEmail"].ToString();
                _isActive = frm["active"].ToString();
                if (_isActive == "true,false" || _isActive == "true")
                {
                    objEmp.active = true;


                }
                else
                {

                    objEmp.active = false;

                }

           
                    if (_hdnUserId > 0)
                    {
                        objEmp.updatedBy = 1;
                        objEmp.updatedDate = DateTime.Now;
                        db.Entry(objEmp).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        objEmp.aspnet_userid = "2132332332";
                        objEmp.companyid = 1;
                        objEmp.createdBy = 1233;
                        objEmp.createdDate = DateTime.Now;
                        db.EmployeeInfomations.Add(objEmp);
                    }

                    db.SaveChanges();
            }

            //Users/LoadDataForDataTable
            return RedirectToAction("ShowGrid");

            //return View("AddEditUserSave");
        }


        [HttpGet]
        public ActionResult AddEditUser(String id)
        {
            int _companyid = ClientSession.CompanyID;
            Random rand = new Random();
            int _newValue = rand.Next();


            UsersModel mod = new UsersModel();
            EmployeeInfomation objEmp;
            _hdnUserId = Convert.ToInt32(id);
            if (_hdnUserId > 0)
            {

                objEmp = db.EmployeeInfomations.Where(o => o.employeeID == _hdnUserId).SingleOrDefault();
                mod.employeeCode = objEmp.employeeCode;

            }
            else
            {
                objEmp = new EmployeeInfomation();
                mod.employeeCode = _companyid + "-" + _newValue;
            }


            mod.employeeid = _hdnUserId.ToString();
            mod.userName = objEmp.userName;
            mod.firstName = objEmp.firstName;
            mod.lastName = objEmp.lastName;
         
            mod.employeeGender = objEmp.employeeGender;
            mod.employeeEmail = objEmp.employeeEmail;
            mod.active = Convert.ToBoolean(objEmp.active);
           
            return View(mod);


        }



        [HttpGet]
        public String ValidateUserName(String userName, String employeeid, String employeeEmail)
        {
            List<String> list = new List<String>();
            String _retval = "0";
            int _companyId = ClientSession.CompanyID;
            IEnumerable<String> objEmp = db.ValidateUser_UserNameByCompanyid(Convert.ToInt32(employeeid), userName, employeeEmail, ClientSession.CompanyID).ToList();
            if (objEmp != null && objEmp.Count() > 0)
            {
                foreach (var item in objEmp)
                {
                    list.Add(item);
                }

                _retval = list[0].ToString();
            }

            return _retval;
        }

    }
}
