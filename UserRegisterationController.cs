using RegisterationForminMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace RegisterationForminMVC.Controllers
{
    public class UserRegisterationController : Controller
    {


        clsUserReg clsUser = new clsUserReg();

        public ActionResult GetAllUser(string search)
        {
            clsUserReg obj2 = new clsUserReg();

            List<clsUserReg> lst = obj2.GetUsers();
            lst = clsUser.GetUsers().ToList();

            if (search != "" && search != null)

            {

                
                    return View(lst.Where(x => x.EmpName.StartsWith(search) || search == null).ToList());

               

            }


            return View(lst);
        }

        [HttpPost]

        public ActionResult Search(string search)

        {

            List<clsUserReg> lstEmp = new List<clsUserReg>();

            lstEmp = clsUser.GetUsers().ToList();

            if (search != "")

            {


                {

                    return View(lstEmp.Where(x => x.EmpName.StartsWith(search) || search == null).ToList());

                }

            }



            return View("GetAllUser", lstEmp);

        }








        [HttpGet]
        
        public ActionResult CreateRegForm(string search)
        {
            clsUserReg obj = new clsUserReg();
            obj.Department = GetDepartment();
            return View(obj);
        }

        private List<SelectListItem> GetDepartment()
        {
            //to Get all deprment 
            clsDepartment objclsDepartment = new clsDepartment();
            List<clsDepartment> lstDept = objclsDepartment.GetDept();

            List<SelectListItem> itemslst = new List<SelectListItem>();

            foreach (clsDepartment obj in lstDept)
            {
                SelectListItem item = new SelectListItem();
                item.Value = Convert.ToString(obj.DeptID);
                item.Text = obj.DeptName;
                //  adding object to list 
                itemslst.Add(item);
            }

            return itemslst;
        }

        [HttpPost]
        public ActionResult CreateRegForm (string EmpName, string bday, string Gender, string DeptID, string EmpAdress, string City, string EmpMob)
        {
            clsUserReg obj = new clsUserReg();

            DateTime dtBday = Convert.ToDateTime(bday);
            int intDeptID = Convert.ToInt32(DeptID);
            double dblMob = Convert.ToDouble(EmpMob);

            obj.AddUser(EmpName, dtBday,Gender,  intDeptID, EmpAdress, City, dblMob);




            obj.Department = GetDepartment();
            ViewBag.message = "Registeration done successfully";
            return View(obj);

          //  return RedirectToActionPermanent("GetAllUser");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            clsUserReg objnew = new clsUserReg();

            objnew = objnew.GetEUserData(id);

            return View("EditUser", objnew);
        }

        [HttpPost]


        public ActionResult Edit(int id, clsUserReg objuser)
        {
            clsUserReg objnew = new clsUserReg();
            objnew.UpdateUser(objuser);

            //List<clsUserReg> lst = objnew.GetUsers();

            //return View("GetUsers", lst);

            return RedirectToActionPermanent("GetAllUser");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
           
            clsUserReg cls = new clsUserReg();
            cls = cls.GetEUserData(id);
           
            return View(cls);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            clsUser.DeleteUser(id);
            return RedirectToAction("GetAllUser");
        }




        [HttpGet]
        public ActionResult Details(int id)
        {
           
            clsUserReg objnew = new clsUserReg();
            objnew = objnew.GetEUserData(id);
           

           
            return View(objnew);
        }





    }
}