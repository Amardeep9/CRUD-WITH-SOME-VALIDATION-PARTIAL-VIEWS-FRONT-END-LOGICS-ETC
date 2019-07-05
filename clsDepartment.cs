using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using System.Linq;
using System.Web;

namespace RegisterationForminMVC.Models
{
    public class clsDepartment
    {
        string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Employee; Integrated Security=true;";

        //To View all employees details   
        public int DeptID { get; set; }
        public string DeptName { get; set; }

        

        public List<clsDepartment> GetDept()
        {

            List<clsDepartment> lstDept = new List<clsDepartment>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetDept", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    clsDepartment obj = new clsDepartment();
                    obj.DeptID = Convert.ToInt32(rdr["DeptID"]);
                    obj.DeptName = Convert.ToString(rdr["DeptName"]);


                    lstDept.Add(obj);
                }
                con.Close();
            }
            return lstDept;
        }



    }
}