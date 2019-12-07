﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSTech.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SMSTech.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        string Connection = ConnectionString.Path;
        public ActionResult AdmissionForm()
        {
            //ViewBag.Gender = new SelectList(dbGender.tb_Gender, "Gender_ID","G_Name");

            return View();
        }
        HttpClient Client = new HttpClient();

        [HttpPost]
        public ActionResult StudentInformation(Student std)
        {


            return View();


        }

        [HttpPost]
        public ActionResult ParentDetails(ParentDetail objPD)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connection))
            {


                sqlCon.Open();
                string query = "Insert into tb_ParentDetail values(@F_Name,@FCNIC,@FContactNo,@F_Job,@M_Name,@MCNIC,@MContactNo,@M_Job)";
                SqlCommand sqlmd = new SqlCommand(query, sqlCon);
                sqlmd.Parameters.AddWithValue("@F_Name", objPD.FFirstName);
                sqlmd.Parameters.AddWithValue("@FCNIC", objPD.FCNIC);
                sqlmd.Parameters.AddWithValue("@FContactNo", objPD.Fcontactno);
                sqlmd.Parameters.AddWithValue("@F_Job", objPD.FOccupation);
                sqlmd.Parameters.AddWithValue("@M_Name", objPD.MFirstName);
                sqlmd.Parameters.AddWithValue("@MCNIC", objPD.MCNIC);
                sqlmd.Parameters.AddWithValue("@MContactNo", objPD.MContactNo);
                sqlmd.Parameters.AddWithValue("@M_Job", objPD.MOccupation);

                sqlmd.ExecuteNonQuery();
            }

            //return RedirectToAction("Index");

            return View("AdmissionForm");
        }

        [HttpGet]
        public ActionResult StudentDetails()
        {
            DataTable dt = new DataTable();

            using (WebClient webClient = new System.Net.WebClient())
            {
                var url = "https://light-insect-88.localtunnel.me/student";
                var json = webClient.DownloadString(url);
                JArray jsonArray = JArray.Parse(json);
                int lenght = jsonArray.Count;
                dynamic data;
                Student Stud = new Student();
                dt.Columns.Add("StudentID");
                dt.Columns.Add("Name");
                dt.Columns.Add("DOB");
                dt.Columns.Add("POB");
                dt.Columns.Add("Fname");
                dt.Columns.Add("FNIC");
                dt.Columns.Add("Address");
                dt.Columns.Add("Paddress");
                dt.Columns.Add("contact");
                dt.Columns.Add("DOJ");
                dt.Columns.Add("Fess");
                dt.Columns.Add("ImagePath");
                dt.Columns.Add("ClassID");


                for (int i = 0; i < lenght; i++)
                {
                    data = JObject.Parse(jsonArray[i].ToString());
                    //Stud.ID = int.Parse(data.Std_RollNo);
                    Stud.Name = data.Std_Name;
                    Stud.DOB = data.Std_DOB;
                    Stud.POB = data.Std_PlacOfBirth;
                    Stud.Fname = data.Std_FatherName;
                    Stud.FNIC = data.Std_FNIC;
                    Stud.Address = data.Std_Address;
                    Stud.Paddress = data.Std_PermenantAddress;
                    Stud.contact = data.Std_Contact;
                    Stud.DOJ = data.Std_DOJ;
                    Stud.Fess = data.Std_Fees;
                    Stud.ImagePath = data.Std_Image;
                    Stud.ClassID = data.Class;


                    dt.Rows.Add();
                    dt.Rows[i]["Name"] = Stud.Name;
                    dt.Rows[i]["DOB"] = Stud.DOB;
                    dt.Rows[i]["POB"] = Stud.POB;
                    dt.Rows[i]["Fname"] = Stud.Fname;
                    dt.Rows[i]["FNIC"] = Stud.FNIC;
                    dt.Rows[i]["Address"] = Stud.Address;
                    dt.Rows[i]["Paddress"] = Stud.Paddress;
                    dt.Rows[i]["contact"] = Stud.contact;
                    dt.Rows[i]["DOJ"] = Stud.DOJ;
                    dt.Rows[i]["Fess"] = Stud.Fess;
                    dt.Rows[i]["ImagePath"] = Stud.ImagePath;
                    dt.Rows[i]["ClassID"] = Stud.ClassID;

                    
                }

            }
            return View(dt);
        }

        public ActionResult Edit(int id)
        {
            Student Stud = new Student();
            DataTable dtblStud = new DataTable();
            DataTable dtblGender = new DataTable();
            DataTable dtblClass = new DataTable();
            DataTable dtblSection = new DataTable();
            DataTable dtblReligion = new DataTable();
            DataTable dtblBGroup = new DataTable();
            DataTable dtblPOB = new DataTable();
            DataTable dtblhouse = new DataTable();



            using (SqlConnection sqlCon = new SqlConnection(Connection))
            {
                sqlCon.Open();
                string query = "Select * From tb_Student Where Std_ID= @Std_ID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Std_ID", id);
                sqlDa.Fill(dtblStud);

                string query1 = "Select * From tb_Gender Where Gender_ID='" + dtblStud.Rows[0][7].ToString() + "'";
                sqlDa = new SqlDataAdapter(query1, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Gender_ID", id);
                sqlDa.Fill(dtblGender);

                string query2 = "Select * From tb_Class Where Class_ID='" + dtblStud.Rows[0][3].ToString() + "'";
                sqlDa = new SqlDataAdapter(query2, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@J_Class", id);
                sqlDa.Fill(dtblClass);


                string query3 = "Select * From tb_Section Where Section_ID='" + dtblStud.Rows[0][4].ToString() + "'";
                sqlDa = new SqlDataAdapter(query3, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Section_ID", id);
                sqlDa.Fill(dtblSection);


                string query4 = "Select * From tb_Religion Where Religion_ID='" + dtblStud.Rows[0][9].ToString() + "'";
                sqlDa = new SqlDataAdapter(query4, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Religion_ID", id);
                sqlDa.Fill(dtblReligion);


                string query5 = "Select * From tb_City Where CityID='" + dtblStud.Rows[0][10].ToString() + "'";
                sqlDa = new SqlDataAdapter(query5, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@CityID", id);
                sqlDa.Fill(dtblPOB);



                string query6 = "Select * From tb_BGroup Where BG_ID='" + dtblStud.Rows[0][11].ToString() + "'";
                sqlDa = new SqlDataAdapter(query6, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@BG_ID", id);
                sqlDa.Fill(dtblBGroup);

                string query7 = "Select * From tb_stdHouse Where SHouse_ID='" + dtblStud.Rows[0][12].ToString() + "'";
                sqlDa = new SqlDataAdapter(query7, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@SHouse_ID", id);
                sqlDa.Fill(dtblhouse);


            }


            if (dtblStud.Rows.Count == 1)
            {

                //Stud.GRNo = dtblStud.Rows[0][1].ToString();
                //Stud.RollNo = dtblStud.Rows[0][2].ToString();
                //Stud.Class = dtblClass.Rows[0][1].ToString();
                //Stud.Section = dtblSection.Rows[0][1].ToString();
                //Stud.StdFirstName = dtblStud.Rows[0][5].ToString();
                //Stud.StdLastName = dtblStud.Rows[0][6].ToString();
                //Stud.Gender = dtblGender.Rows[0][1].ToString();
                //Stud.DOB = dtblStud.Rows[0][8].ToString();
                //Stud.Religion = dtblReligion.Rows[0][1].ToString();
                //Stud.POB = dtblPOB.Rows[0][1].ToString();
                //Stud.BloodGroup = dtblBGroup.Rows[0][1].ToString();
                //Stud.StdHouse = dtblhouse.Rows[0][1].ToString();
                //Stud.Street = dtblStud.Rows[0][13].ToString();
                //Stud.Area = dtblStud.Rows[0][14].ToString();
                //Stud.City = dtblStud.Rows[0][15].ToString();
                //Stud.PCode = dtblStud.Rows[0][16].ToString();
                //Stud.Country = dtblStud.Rows[0][17].ToString();

                return View(Stud);
            }
            else
            {
                return RedirectToAction("StudentDetails");
            }

        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Student std)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connection))
            {
                sqlCon.Open();
                string query = "Update tb_Student Set Std_GRNo=@Std_GRNo,Std_RollNo=@Std_RollNo,J_Class=@J_Class,Section=@Section,StdFirstname=@StdFirstname,stdLastname=@stdLastname,Gender=@Gender,Stud_DOB=@Stud_DOB,Religion=@Religion,POB=@POB,BloodGroup=@BloodGroup,StdHouse=@StdHouse,Street=@Street,Area=@Area,City=@City ,P_Code=@P_Code,Country=@Country where Std_ID=@Std_ID";

                SqlCommand sqlmd = new SqlCommand(query, sqlCon);
                //sqlmd.Parameters.AddWithValue("@Std_ID", std.GRNo);
                //sqlmd.Parameters.AddWithValue("@Std_GRNo", std.GRNo);
                //sqlmd.Parameters.AddWithValue("@Std_RollNo", std.RollNo);
                //sqlmd.Parameters.AddWithValue("@J_Class", std.Class);
                //sqlmd.Parameters.AddWithValue("@Section", std.Section);
                //sqlmd.Parameters.AddWithValue("@StdFirstname", std.StdFirstName);
                //sqlmd.Parameters.AddWithValue("@stdLastname", std.StdLastName);
                //sqlmd.Parameters.AddWithValue("@Gender", std.Gender);
                //sqlmd.Parameters.AddWithValue("@Stud_DOB", std.DOB);
                //sqlmd.Parameters.AddWithValue("@Religion", std.Religion);
                //sqlmd.Parameters.AddWithValue("@POB", std.POB);
                //sqlmd.Parameters.AddWithValue("@BloodGroup", std.BloodGroup);
                //sqlmd.Parameters.AddWithValue("@StdHouse", std.StdHouse);
                //sqlmd.Parameters.AddWithValue("@Street", std.Street);
                //sqlmd.Parameters.AddWithValue("@Area", std.Area);
                //sqlmd.Parameters.AddWithValue("@City", std.City);
                //sqlmd.Parameters.AddWithValue("@P_Code", std.PCode);
                //sqlmd.Parameters.AddWithValue("@Country", std.Country);
                sqlmd.ExecuteNonQuery();



            }

            return RedirectToAction("StudentDetails");
        }


        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connection))
            {
                sqlCon.Open();
                string query = "Delete From tb_Student  where Std_ID=@Std_ID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@Std_ID", id);

                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction("StudentDetails");
        }



    }

}