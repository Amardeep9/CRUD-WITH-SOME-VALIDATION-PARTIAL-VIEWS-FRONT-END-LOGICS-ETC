using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegisterationForminMVC.Models
{
    public class clsUserReg
    {
        string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Employee; Integrated Security=true;";

        public string EmpID { get; set; }
        [Required(ErrorMessage = "Employee Name can't be blank")]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Alphabets only")]
        [MaxLength(40, ErrorMessage = "Product name can be maximum 40 characters long")]
        [MinLength(2, ErrorMessage = "Product name should contain at least 2 characters")]
        public string EmpName { get; set; }
        [Required]
        public string bday { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string EmpAdress { get; set; }
        [Required]
        public string City { get; set; }
        [Required(ErrorMessage = "Mob Number  can't be blank")]
        [Range(1000000000, 9999999999, ErrorMessage = "Mob Number should be of 10 Digit ")]
        public string EmpMob { get; set; }
        [Required]
        public string DeptName { get; set; }
        
        public string DeptID { get; set; }
        public List<SelectListItem> Department { get; set; }



        public List<clsUserReg> GetUsers()
        {

            // object of list of type clsUserReg 
            List<clsUserReg> lstUsers = new List<clsUserReg>();


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    clsUserReg obj1 = new clsUserReg();
                    obj1.EmpName = Convert.ToString(rdr["EmpName"]);
                    obj1.City = Convert.ToString(rdr["City"]);
                    obj1.Gender = Convert.ToString(rdr["Gender"]);
                    obj1.EmpAdress = Convert.ToString(rdr["EmpAddress"]);
                    obj1.EmpMob = Convert.ToString(rdr["EmpMob"]);
                    obj1.EmpID = Convert.ToString(rdr["EmpID"]);
                    obj1.DeptName = Convert.ToString(rdr["DeptName"]);
                    obj1.EmpID = Convert.ToString(rdr["EmpID"]);
                    obj1.bday = Convert.ToString(rdr["bday"]);
                    obj1.bday = Convert.ToDateTime(obj1.bday).ToString("dd/MM/yyyy");


                    // end depart


                    lstUsers.Add(obj1);


                }

                con.Close();
            }
            return lstUsers;
        }


        public clsUserReg GetEUserData(int? id)
        {
            clsUserReg obj1 = new clsUserReg();


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblUserReg WHERE EmpID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    obj1.EmpName = Convert.ToString(rdr["EmpName"]);
                    obj1.City = Convert.ToString(rdr["City"]);
                    obj1.EmpAdress = Convert.ToString(rdr["EmpAddress"]);
                    obj1.Gender = Convert.ToString(rdr["Gender"]);
                    obj1.EmpMob = Convert.ToString(rdr["EmpMob"]);
                    obj1.EmpID = Convert.ToString(rdr["EmpID"]);
                    obj1.DeptID = Convert.ToString(rdr["DeptID"]);
                    obj1.EmpID = Convert.ToString(rdr["EmpID"]);
                    obj1.bday = Convert.ToString(rdr["bday"]);
                    obj1.bday = Convert.ToDateTime(obj1.bday).ToString("dd/MM/yyyy");


                    //Get all deprment 
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

                    obj1.Department = itemslst;
                    // end depart

                }
            }
            return obj1;
        }

        public void UpdateUser(clsUserReg obj21)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", obj21.EmpID);
                cmd.Parameters.AddWithValue("@EmpName", obj21.EmpName);
                cmd.Parameters.AddWithValue("@bday", obj21.bday);
                cmd.Parameters.AddWithValue("@Gender", obj21.Gender);
                cmd.Parameters.AddWithValue("@EmpAddress", obj21.EmpAdress);
                cmd.Parameters.AddWithValue("@City", obj21.City);
                cmd.Parameters.AddWithValue("@EmpMob", obj21.EmpMob);
                cmd.Parameters.AddWithValue("@DeptID", obj21.DeptID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        public void AddUser(string EmpName, DateTime bday, string Gender, int DeptID, string EmpAdress, string City, Double EmpMob)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpName", EmpName);
                cmd.Parameters.AddWithValue("@bday", bday);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@DeptID", DeptID);
                cmd.Parameters.AddWithValue("@EmpAdress", EmpAdress);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@EmpMob", EmpMob);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteUser(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }





    }
}