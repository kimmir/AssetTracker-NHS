using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHS_AssetTracker.Models;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using Nancy.Json;
using System.Text.RegularExpressions;
using System.Data;

namespace NHS_AssetTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public static List<AssetModel> getAssets() //24.03.20 - Kim: Method to get data
        {   // updated to load asset data with location details and User details, asset cleaning data - Athar - 30.04.20

            List<AssetModel> assetModels = new List<AssetModel>(); ////24.03.20 - Kim: List to return when populated
            string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            //Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true

            // Loading Asset details in track model - Athar - 30.04.2020
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("select * from ASSETS")) ////24.03.20 - Kim: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader(); //24.03.20 - Kim: Reading data
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                AssetModel assObj = new AssetModel();
                                assObj.AssetID = int.Parse(sdr["AssetID"].ToString());
                                assObj.AssetType = sdr["AssetType"].ToString();
                                assObj.AssetHome = int.Parse(sdr["AssetHome"].ToString());
                                if (sdr.IsDBNull(sdr.GetOrdinal("AssetUser")))
                                {
                                    assObj.AssetUser = 0;
                                }
                                else
                                {
                                    assObj.AssetUser = int.Parse(sdr["AssetUser"].ToString()); //02.04.20 - Kim: Updated controller with new model 
                                }
                                assObj.AssetValue = sdr["AssetValue"].ToString();
                                assObj.AssetUsable = int.Parse(sdr["AssetUsable"].ToString());
                                assObj.AssetNotes = sdr["AssetNotes"].ToString();

                                if (assObj.AssetUsable == 0)
                                {
                                    assObj.AssetUsableString = "No";
                                }
                                else
                                {
                                    assObj.AssetUsableString = "Yes";
                                }

                                // Loading location details in track model - Athar - 30.04.2020
                                using (SqlConnection sqlconn1 = new SqlConnection(connection))
                                {
                                    using (SqlCommand sqlcomm1 = new SqlCommand("select * from LOCATIONS where LocationID = " + assObj.AssetHome.ToString())) ////24.03.20 - Kim: Getting data
                                    {
                                        using (SqlDataAdapter sda1 = new SqlDataAdapter())
                                        {
                                            sqlcomm1.Connection = sqlconn1;
                                            sqlconn1.Open();
                                            sda1.SelectCommand = sqlcomm1;
                                            SqlDataReader sdr1 = sqlcomm1.ExecuteReader(); //29.04.20 - Athar: Reading data
                                            if (sdr1.HasRows)
                                            {
                                                while (sdr1.Read())
                                                {
                                                    assObj.AssetLocationName = sdr1["LocationName"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }

                                // Loading patient details in track model - Athar - 30.04.2020
                                using (SqlConnection sqlconn2 = new SqlConnection(connection))
                                {
                                    using (SqlCommand sqlcomm2 = new SqlCommand("select * from Patient where PatientID = '" + assObj.AssetUser.ToString() + "'")) ////24.03.20 - Kim: Getting data
                                    {
                                        using (SqlDataAdapter sda2 = new SqlDataAdapter())
                                        {
                                            sqlcomm2.Connection = sqlconn2;
                                            sqlconn2.Open();
                                            sda2.SelectCommand = sqlcomm2;
                                            SqlDataReader sdr2 = sqlcomm2.ExecuteReader(); //29.04.20 - Athar: Reading data
                                            if (sdr2.HasRows)
                                            {
                                                while (sdr2.Read())
                                                {
                                                        assObj.PatientName = sdr2["Forename"].ToString() + ", " + sdr2["Surname"].ToString();
                                                    
                                                }
                                            }
                                        }
                                    }
                                }

                                // Loading Asset log details in track model - Athar - 30.04.2020
                                using (SqlConnection sqlconn3 = new SqlConnection(connection))
                                {
                                    using (SqlCommand sqlcomm3 = new SqlCommand("select * from AssetLog where AssignedAssetID = '" + assObj.AssetID.ToString() + "'")) ////24.03.20 - Kim: Getting data
                                    {
                                        assObj.AssetLastCleaned = "N/A";

                                        using (SqlDataAdapter sda3 = new SqlDataAdapter())
                                        {
                                            sqlcomm3.Connection = sqlconn3;
                                            sqlconn3.Open();
                                            sda3.SelectCommand = sqlcomm3;
                                            SqlDataReader sdr3 = sqlcomm3.ExecuteReader(); //29.04.20 - Athar: Reading data
                                            if (sdr3.HasRows)
                                            {
                                                while (sdr3.Read())
                                                {
                                                    assObj.AssetLastCleaned = sdr3["LastCleaningDate"].ToString();



                                                }
                                            }
                                        }
                                    }
                                }


                                assetModels.Add(assObj);
                            }
                        }
                    }
                    return assetModels; //Returning data

                }
            }
        }

        public static List<AssetLogModel> GetAssetLogs() //02.04.20 - Kim: Method to get asset log data
        {
            List<AssetLogModel> assetLogModels = new List<AssetLogModel>(); //02.04.20 - Kim: List that is returned
            string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("select * from ASSETLOG"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader(); //02.04.20 - Kim: Reading data
                        if (sdr.HasRows) {
                            while (sdr.Read())
                            {
                                AssetLogModel asslogObj = new AssetLogModel();

                                asslogObj.TimeIn = sdr["TimeIn"].ToString();
                                asslogObj.TimeOut = sdr["TimeOut"].ToString();
                                asslogObj.AssignedStaffID = int.Parse(sdr["AssignedStaffID"].ToString());
                                asslogObj.AssignedLocationID = int.Parse(sdr["AssignedLocationID"].ToString());
                                asslogObj.AssignedAssetID = int.Parse(sdr["AssignedAssetID"].ToString());


                                if (sdr["ATaskID"] == null)
                                {
                                    asslogObj.ATaskID = 0;
                                }
                                else
                                {

                                    int result;
                                    if (int.TryParse(sdr["ATaskID"].ToString(), out result))
                                    {
                                        asslogObj.ATaskID = int.Parse(sdr["ATaskID"].ToString()); //30.04.20 - Kim: Fixed the issue where null database values was breaking the log page
                                    }
                                }
                                if (sdr["AssignedTeam"] == null)
                                {
                                    asslogObj.AssignedTeam = 0;
                                }
                                else
                                {
                                    int result;
                                    if (int.TryParse(sdr["AssignedTeam"].ToString(), out result))
                                    {
                                        asslogObj.AssignedTeam = int.Parse(sdr["AssignedTeam"].ToString()); //02.04.20 - Kim: Assigning data to the object
                                    }

                                }

                                asslogObj.LastMaintenence = sdr["LastMaintenence"].ToString();
                                asslogObj.NextMaintenence = sdr["NextMaintenence"].ToString();
                                asslogObj.LastCleaningDate = sdr["LastCleaningDate"].ToString();
                                asslogObj.NextCleaningDate = sdr["NextCleaningDate"].ToString();

                                assetLogModels.Add(asslogObj); //02.04.20 - Kim: Adding to the list
                            }
                        }
                    }
                    return assetLogModels; //02.04.20 - Kim: Returning object
                }
            }



        }




        public ActionResult SignUp() //23.03.20 - Kim: Sign up
        {
            return View();
        }
        [HttpPost] //Kim - Where data is sent to from sign up
        [ValidateAntiForgeryToken] // Kim - Validating token
        public ActionResult SignUp(StaffModel model) //Kim - model being passed in
        {
            if (ModelState.IsValid) //Kim - is the model valid (does it pass all constraints - in case someone disables js) 2fa client-side validation
            {
                return RedirectToAction("Index"); //Kim - If passes then return to index
                //Data must then be passed to db
            }
            return View(); //Kim - If not valid return to previous view
        }
        public IActionResult Index() //28.02.20 - Kim: Controller which directs users to the desired page
        {
            return View(); //28.02.20 - Kim: Home
        }

        public IActionResult Assets()
        {
            return View(getAssets()); //28.02.20 - Kim: Assets
        }

        // using the following code the search bar on the Asset page will get results according to the rearch paramteres
        // It will search either by name, Asset ID or by location name
        // Athar - 06.05.20
        public string GetAssetSearchBar(int n, string q)
        {
            // This is used in case the search returns no data  //30.04.20 - Athar
            List<AssetModel> assetModels1 = new List<AssetModel>();
            AssetModel assObj1 = new AssetModel();

            // This is used in case the search returns no data  //30.04.20 - Athar
            string output = "0";

            // Checking whether the user select name,id or location in seearch Asset Page  //30.04.20 - Athar
            if (n == 0 || n == 1 || n == 2)
            {
                List<AssetModel> assetModels = new List<AssetModel>();

                string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

                using (SqlConnection sqlconn = new SqlConnection(connection))
                {
                    using (SqlCommand sqlcomm = new SqlCommand(q))
                    {

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sqlcomm.Connection = sqlconn;
                            sqlconn.Open();
                            sda.SelectCommand = sqlcomm;
                            SqlDataReader sdr = sqlcomm.ExecuteReader();
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {

                                    AssetModel assObj = new AssetModel();

                                    if (n == 0 || n == 1)
                                    {
                                        // Loading Asset Data and details  //30.04.20 - Athar
                                        assObj1.result = "1";
                                        assObj.result = "1";
                                        assObj.AssetID = int.Parse(sdr["AssetID"].ToString());
                                        assObj.AssetType = sdr["AssetType"].ToString();
                                        assObj.AssetHome = int.Parse(sdr["AssetHome"].ToString());
                                        if (sdr.IsDBNull(sdr.GetOrdinal("AssetUser")))
                                        {
                                            assObj.AssetUser = 0;
                                        }
                                        else
                                        {
                                            assObj.AssetUser = int.Parse(sdr["AssetUser"].ToString()); //06.05.20 - Athar: Updated controller with new model 
                                        }
                                        assObj.AssetValue = sdr["AssetValue"].ToString();
                                        assObj.AssetUsable = int.Parse(sdr["AssetUsable"].ToString());
                                        assObj.AssetNotes = sdr["AssetNotes"].ToString();

                                        if (assObj.AssetUsable == 0)
                                        {
                                            assObj.AssetUsableString = "No";
                                        }
                                        else
                                        {
                                            assObj.AssetUsableString = "Yes";
                                        }

                                        // Loading Location Data and details  //30.04.20 - Athar
                                        using (SqlConnection sqlconn2 = new SqlConnection(connection))
                                        {
                                            using (SqlCommand sqlcomm2 = new SqlCommand("Select * from LOCATIONS where LocationID='" + assObj.AssetHome.ToString() + "'")) ////24.03.20 - Athar: Getting data
                                            {
                                                using (SqlDataAdapter sda2 = new SqlDataAdapter())
                                                {
                                                    sqlcomm2.Connection = sqlconn2;
                                                    sqlconn2.Open();
                                                    sda2.SelectCommand = sqlcomm2;
                                                    SqlDataReader sdr2 = sqlcomm2.ExecuteReader();
                                                    if (sdr2.HasRows)
                                                    {
                                                        while (sdr2.Read())
                                                        {
                                                            LocationModel assObjLoc = new LocationModel();

                                                            assObjLoc.LocationName = sdr2["LocationName"].ToString();
                                                            assObjLoc.LocationPostCode = sdr2["LocationPostCode"].ToString();
                                                            assObjLoc.LocationHead = sdr2["LocationHead"].ToString();

                                                            assObj.AssetLocationName = assObjLoc.LocationName;

                                                            assObj.LocationPostCode = assObjLoc.LocationPostCode;

                                                            assObj.LocationHead = assObjLoc.LocationHead;

                                                        }
                                                    }
                                                }

                                            }
                                        }

                                        // Loading Patient Data and details  //30.04.20 - Athar
                                        using (SqlConnection sqlconn3 = new SqlConnection(connection))
                                        {
                                            using (SqlCommand sqlcomm3 = new SqlCommand("Select * from PATIENT where PatientID='" + assObj.AssetUser.ToString() + "'")) ////24.03.20 - Athar: Getting data
                                            {
                                                using (SqlDataAdapter sda3 = new SqlDataAdapter())
                                                {
                                                    sqlcomm3.Connection = sqlconn3;
                                                    sqlconn3.Open();
                                                    sda3.SelectCommand = sqlcomm3;
                                                    SqlDataReader sdr3 = sqlcomm3.ExecuteReader();
                                                    if (sdr3.HasRows)
                                                    {
                                                        while (sdr3.Read())
                                                        {

                                                            TrackModel assObjpat = new TrackModel();

                                                            assObjpat.PatientID = sdr3["PatientID"].ToString();
                                                            assObjpat.Forename = sdr3["Forename"].ToString();
                                                            assObjpat.Surname = sdr3["Surname"].ToString();
                                                            assObjpat.Address = sdr3["Address"].ToString();
                                                            assObjpat.ContactNo = sdr3["ContactNo"].ToString();
                                                            assObjpat.CarerID = sdr3["CarerID"].ToString();

                                                            assObj.PatientID = assObjpat.PatientID.ToString();
                                                            assObj.Forename = assObjpat.Forename.ToString();
                                                            assObj.Surname = assObjpat.Surname.ToString();
                                                            assObj.Address = assObjpat.Address.ToString();
                                                            assObj.ContactNo = assObjpat.ContactNo.ToString();
                                                            assObj.CarerID = assObjpat.CarerID.ToString();


                                                        }
                                                    }
                                                }

                                            }
                                        }

                                        // Loading Asset Log Data and details  //30.04.20 - Athar
                                        using (SqlConnection sqlconn4 = new SqlConnection(connection))
                                        {
                                            using (SqlCommand sqlcomm4 = new SqlCommand("select * from AssetLog where AssignedAssetID = '" + assObj.AssetID.ToString() + "'")) ////06.05.20 - Athar: Getting data
                                            {


                                                assObj.AssetLastCleaned = "N/A";

                                                using (SqlDataAdapter sda4 = new SqlDataAdapter())
                                                {
                                                    sqlcomm4.Connection = sqlconn4;
                                                    sqlconn4.Open();
                                                    sda4.SelectCommand = sqlcomm4;
                                                    SqlDataReader sdr4 = sqlcomm4.ExecuteReader(); //29.04.20 - Athar: Reading data
                                                    if (sdr4.HasRows)
                                                    {
                                                        while (sdr4.Read())
                                                        {
                                                            assObj.AssetLastCleaned = sdr4["LastCleaningDate"].ToString();

                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else if (n == 2) // Incase user enter location in Asset page search bar  //30.04.20 - Athar
                                    {
                                        Debug.WriteLine("2222");
                                        assObj1.result = "1";
                                        assObj.result = "1";

                                        // Loading Location Data and details  //30.04.20 - Athar
                                        LocationModel assObjLoc = new LocationModel();
                                        assObjLoc.LocationID = int.Parse(sdr["LocationID"].ToString());
                                        assObjLoc.LocationName = sdr["LocationName"].ToString();
                                        assObjLoc.LocationPostCode = sdr["LocationPostCode"].ToString();
                                        assObjLoc.LocationHead = sdr["LocationHead"].ToString();

                                        assObj.AssetLocationName = assObjLoc.LocationName;

                                        assObj.LocationPostCode = assObjLoc.LocationPostCode;

                                        assObj.LocationHead = assObjLoc.LocationHead;

                                        // Loading Asset Data and details  //30.04.20 - Athar
                                        using (SqlConnection sqlconn7 = new SqlConnection(connection))
                                        {
                                            using (SqlCommand sqlcomm7 = new SqlCommand("Select * from Assets WHERE AssetHome =" + assObjLoc.LocationID.ToString())) ////24.03.20 - Athar: Getting data
                                            {
                                                using (SqlDataAdapter sda7 = new SqlDataAdapter())
                                                {
                                                    sqlcomm7.Connection = sqlconn7;
                                                    sqlconn7.Open();
                                                    sda7.SelectCommand = sqlcomm7;
                                                    SqlDataReader sdr7 = sqlcomm7.ExecuteReader();
                                                    if (sdr7.HasRows)
                                                    {
                                                        while (sdr7.Read())
                                                        {
                                                            assObj.AssetID = int.Parse(sdr7["AssetID"].ToString());
                                                            assObj.AssetType = sdr7["AssetType"].ToString();
                                                            assObj.AssetHome = int.Parse(sdr7["AssetHome"].ToString());
                                                            //assObj.AssetUser = int.Parse(sdr7["AssetUser"].ToString()); //06.05.20 - Athar: Updated controller with new model
                                                            if (sdr7.IsDBNull(sdr7.GetOrdinal("AssetUser")))
                                                            {
                                                                assObj.AssetUser = 0;
                                                            }
                                                            else
                                                            {
                                                                //assObj.AssetUser = int.Parse(sdr["AssetUser"].ToString()); //06.05.20 - Athar: Updated controller with new model 
                                                                //patientID = 
                                                                assObj.AssetUser = int.Parse(sdr7["AssetUser"].ToString());
                                                                //patientID = assObj.AssetUser;
                                                            }
                                                            assObj.AssetValue = sdr7["AssetValue"].ToString();
                                                            assObj.AssetUsable = int.Parse(sdr7["AssetUsable"].ToString());
                                                            assObj.AssetNotes = sdr7["AssetNotes"].ToString();

                                                            if (assObj.AssetUsable == 0)
                                                            {
                                                                assObj.AssetUsableString = "No";
                                                            }
                                                            else
                                                            {
                                                                assObj.AssetUsableString = "Yes";
                                                            }

                                                        }
                                                    }
                                                }

                                            }
                                        }

                                        // Loading Patient Data and details  //30.04.20 - Athar
                                        using (SqlConnection sqlconn3 = new SqlConnection(connection))
                                        {
                                            using (SqlCommand sqlcomm3 = new SqlCommand("Select * from PATIENT where PatientID='" + assObj.AssetUser.ToString() + "'")) ////24.03.20 - Athar: Getting data
                                            {
                                                using (SqlDataAdapter sda3 = new SqlDataAdapter())
                                                {
                                                    sqlcomm3.Connection = sqlconn3;
                                                    sqlconn3.Open();
                                                    sda3.SelectCommand = sqlcomm3;
                                                    SqlDataReader sdr3 = sqlcomm3.ExecuteReader();
                                                    if (sdr3.HasRows)
                                                    {
                                                        while (sdr3.Read())
                                                        {
                                                            TrackModel assObjpat = new TrackModel();

                                                            assObjpat.PatientID = sdr3["PatientID"].ToString();
                                                            assObjpat.Forename = sdr3["Forename"].ToString();
                                                            assObjpat.Surname = sdr3["Surname"].ToString();
                                                            assObjpat.Address = sdr3["Address"].ToString();
                                                            assObjpat.ContactNo = sdr3["ContactNo"].ToString();
                                                            assObjpat.CarerID = sdr3["CarerID"].ToString();

                                                            assObj.PatientID = assObjpat.PatientID.ToString();
                                                            assObj.Forename = assObjpat.Forename.ToString();
                                                            assObj.Surname = assObjpat.Surname.ToString();
                                                            assObj.Address = assObjpat.Address.ToString();
                                                            assObj.ContactNo = assObjpat.ContactNo.ToString();
                                                            assObj.CarerID = assObjpat.CarerID.ToString();


                                                        }
                                                    }
                                                }

                                            }
                                        }

                                        // Loading Asset Log Data and details  //30.04.20 - Athar
                                        using (SqlConnection sqlconn4 = new SqlConnection(connection))
                                        {
                                            using (SqlCommand sqlcomm4 = new SqlCommand("select * from AssetLog where AssignedAssetID = '" + assObj.AssetID.ToString() + "'")) ////06.05.20 - Athar: Getting data
                                            {
                                                //30.04.20 - Athar: In case the cleaning date is not mentioned for the Asset
                                                assObj.AssetLastCleaned = "N/A";

                                                using (SqlDataAdapter sda4 = new SqlDataAdapter())
                                                {
                                                    sqlcomm4.Connection = sqlconn4;
                                                    sqlconn4.Open();
                                                    sda4.SelectCommand = sqlcomm4;
                                                    SqlDataReader sdr4 = sqlcomm4.ExecuteReader(); //29.04.20 - Athar: Reading data
                                                    if (sdr4.HasRows)
                                                    {
                                                        while (sdr4.Read())
                                                        {
                                                            assObj.AssetLastCleaned = sdr4["LastCleaningDate"].ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    assetModels.Add(assObj);


                                }
                            }
                            if (assObj1.result == "1")
                            {
                                // Changing data to json  //30.04.20 - Athar
                                output = new JavaScriptSerializer().Serialize(assetModels);
                            }
                        }


                    }
                }

                // checking whether search gave any results  //30.04.20 - Athar
                if (assObj1.result == "0")
                {
                    assObj1.result = "0";
                    assetModels1.Add(assObj1);
                    output = new JavaScriptSerializer().Serialize(assetModels1);
                    return output; //Returning data
                }
                else
                {
                    return output; //Returning data
                }





            }
            else
            {
                // in case no data returns the form will show an error  //30.04.20 - Athar
                assObj1.result = "0";
                assetModels1.Add(assObj1);
                output = new JavaScriptSerializer().Serialize(assetModels1);
                return output; //Returning data


            }



        }

        // using the following code the search bar on the Asset page will get results according to the rearch paramteres
        // It will search either by name, Asset ID or by location name
        // Athar - 06.05.20
        [HttpPost] // Created a http request to show search bar data without page load - Athar - 30.04.20
        public IActionResult Assets(SearchBarModel formData)
        {
            bool result = Regex.IsMatch(formData.SearchText, @"^[\p{L}\p{N}]+$");
            if (ModelState.IsValid && result)
            {
                string q = "";

                //30.04.20 - Athar: If user enter Asset name then using the following query
                if (formData.SearchType == "0")
                {
                    q = "Select * from Assets WHERE AssetType like '%" + formData.SearchText + "%'";
                }

                //30.04.20 - Athar: If user enter Asset ID then using the following query
                if (formData.SearchType == "1")
                {
                    q = "Select * from Assets WHERE AssetID like '%" + formData.SearchText + "%'";
                }

                //30.04.20 - Athar: If user enter Asset location then using the following query
                if (formData.SearchType == "2")
                {
                    q = "Select * from Locations WHERE LocationName like '%" + formData.SearchText + "%'";
                }



                return Json(GetAssetSearchBar(int.Parse(formData.SearchType), q));
            }
            else
            {
                return View(GetAssetSearchBar(5, "")); //30.04.20 - Athar: Assets Search system
            }

        }


        public IActionResult Collection()
        {
            return View(); //28.02.20 - Kim: Collections
        }

        public IActionResult LostAssets()
        {
            return View(); //28.02.20 - Kim: Lost assets page
        }

        public IActionResult Log()
        {
            return View(GetAssetLogs()); //28.02.20 - Kim: Transaction log
        }


        public IActionResult Track()
        {
            return View(); //29.04.20 - Athar: Tracking an asset etc.
        }

        // using the following code the track page will get results according to the Asset ID
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public static List<TrackModel> getAssetTrack(string q) //30.04.20 - Athar: Method to get data
        {
            string locationID = "";

            string patientID = "";

            TrackModel assObj = new TrackModel();

            List<TrackModel> TrackModel = new List<TrackModel>(); ////30.04.20 - Athar: List to return when populated
            string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

            // Getting Asset details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {

                using (SqlCommand sqlcomm = new SqlCommand(q)) ////30.04.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader(); //30.04.20 - Athar: Reading data
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                assObj.AssetID = int.Parse(sdr["AssetID"].ToString());
                                assObj.AssetType = sdr["AssetType"].ToString();
                                locationID = assObj.AssetHome = sdr["AssetHome"].ToString();
                                if (sdr.IsDBNull(sdr.GetOrdinal("AssetUser")))
                                {
                                    assObj.AssetUser = "0";
                                }
                                else
                                {
                                    //assObj.AssetUser = int.Parse(sdr["AssetUser"].ToString()); //06.05.20 - Athar: Updated controller with new model 
                                    patientID = assObj.AssetUser = sdr["AssetUser"].ToString();
                                }

                                assObj.AssetValue = sdr["AssetValue"].ToString();
                                assObj.AssetUsable = sdr["AssetUsable"].ToString();
                                if (assObj.AssetUsable == "0")
                                {
                                    assObj.AssetUsable = "Asset is not Usable";
                                }
                                else { assObj.AssetUsable = "Asset is Usable"; }


                            }
                        }
                    }





                }
            }

            // After Getting Asset details and Now loading Location Details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Select * from LOCATIONS where LocationID='" + locationID + "'")) ////24.03.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //LocationModel assObjLoc = new LocationModel();

                                //assObj.LocationName = int.Parse(sdr["LocationName"].ToString());

                                LocationModel assObjLoc = new LocationModel();
                                assObjLoc.LocationID = int.Parse(sdr["LocationID"].ToString());
                                assObjLoc.LocationName = sdr["LocationName"].ToString();
                                assObjLoc.LocationPostCode = sdr["LocationPostCode"].ToString();
                                assObjLoc.LocationHead = sdr["LocationHead"].ToString();

                                assObj.LocationName = assObjLoc.LocationName;

                                assObj.LocationPostCode = assObjLoc.LocationPostCode;

                                assObj.LocationHead = assObjLoc.LocationHead;

                            }
                        }
                    }



                    //return TrackModel; //Returning data

                }
            }

            if (assObj.AssetUser != "0")
            {
                // After Location details and Now loading patient details and storing in the TrackModel List 30.04.20 - Athar
                using (SqlConnection sqlconn = new SqlConnection(connection))
                {
                    using (SqlCommand sqlcomm = new SqlCommand("Select * from PATIENT where PatientID='" + patientID + "'")) ////24.03.20 - Athar: Getting data
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sqlcomm.Connection = sqlconn;
                            sqlconn.Open();
                            sda.SelectCommand = sqlcomm;
                            SqlDataReader sdr = sqlcomm.ExecuteReader();
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    //LocationModel assObjLoc = new LocationModel();

                                    //assObj.LocationName = int.Parse(sdr["LocationName"].ToString());

                                    TrackModel assObjpat = new TrackModel();

                                    assObjpat.PatientID = sdr["PatientID"].ToString();
                                    assObjpat.Forename = sdr["Forename"].ToString();
                                    assObjpat.Surname = sdr["Surname"].ToString();
                                    assObjpat.Address = sdr["Address"].ToString();
                                    assObjpat.ContactNo = sdr["ContactNo"].ToString();
                                    assObjpat.CarerID = sdr["CarerID"].ToString();

                                    assObj.PatientID = assObjpat.PatientID.ToString();
                                    assObj.Forename = assObjpat.Forename.ToString();
                                    assObj.Surname = assObjpat.Surname.ToString();
                                    assObj.Address = assObjpat.Address.ToString();
                                    assObj.ContactNo = assObjpat.ContactNo.ToString();
                                    assObj.CarerID = assObjpat.CarerID.ToString();

                                    TrackModel.Add(assObj);
                                }
                            }
                        }



                        //return TrackModel; //Returning data

                    }
                }
            }
            else
            {
                /*assObj.LocationName = "";

                assObj.LocationPostCode = "";

                assObj.LocationHead = "";*/

                TrackModel.Add(assObj);
            }

            return TrackModel; //Returning data
        }

        // using the following code the track page will get results according to the Asset ID
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpGet] // Created a http request to track asset and show details on track page - Athar - 30.04.20
        public IActionResult Track(TrackModel formData)
        {
            string id = formData.AssetID.ToString();

            int i;
            bool result = int.TryParse(id, out i);
            if (result && int.Parse(id) > 0)
            {
                string q = "select * from ASSETS where AssetID = '" + id + "'";

                //return View(getAssetTrack(q)); //Returning data - Athar 30.04.20

                ViewData["error"] = "";
                return View(getAssetTrack(q));
            }
            else
            {
                ViewData["error"] = "Error: Cannot retreive Asset Information";
                return View();
            }

        }


        //01.03.20 - Athar: Tracking an asset etc.

        public IActionResult Gdpr()
        {
            return View(); //23.03.20 - Kim: GDPR page
        }


        // 02.05.20 - Athar
        public IActionResult EditAsset()
        {
            return View(); //29.04.20 - Athar: Tracking an asset etc.
        }

        // using the following code the page will get results according to the Asset ID
        // It will show data related on the page.
        // Athar - 06.05.20
        public static List<TrackModel> getAssetInfo(string q) //30.04.20 - Athar: Method to get data
        {
            string locationID = "";

            string patientID = "";

            TrackModel assObj = new TrackModel();

            List<TrackModel> TrackModel = new List<TrackModel>(); ////30.04.20 - Athar: List to return when populated
            string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

            // Getting Asset details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {

                using (SqlCommand sqlcomm = new SqlCommand(q)) ////30.04.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader(); //30.04.20 - Athar: Reading data
                        if (sdr.HasRows)
                        { 
                            while (sdr.Read())
                            {

                                assObj.AssetID = int.Parse(sdr["AssetID"].ToString());
                                assObj.AssetType = sdr["AssetType"].ToString();
                                locationID = assObj.AssetHome = sdr["AssetHome"].ToString();
                                //patientID = assObj.AssetUser = sdr["AssetUser"].ToString();
                                if (sdr.IsDBNull(sdr.GetOrdinal("AssetUser")))
                                {
                                    assObj.AssetUser = "0";
                                }
                                else
                                {
                                    //assObj.AssetUser = int.Parse(sdr["AssetUser"].ToString()); //02.04.20 - Kim: Updated controller with new model 
                                    patientID = assObj.AssetUser = sdr["AssetUser"].ToString();
                                }
                                assObj.AssetValue = sdr["AssetValue"].ToString();
                                assObj.AssetUsable = sdr["AssetUsable"].ToString();
                                if (assObj.AssetUsable == "0")
                                {
                                    assObj.AssetUsable = "Asset is not Usable";
                                }
                                else { assObj.AssetUsable = "Asset is Usable"; }


                            }
                        }
                    }





                }
            }

            // After Getting Asset details and Now loading Location Details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Select * from LOCATIONS where LocationID='" + locationID + "'")) ////24.03.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //LocationModel assObjLoc = new LocationModel();

                                //assObj.LocationName = int.Parse(sdr["LocationName"].ToString());

                                LocationModel assObjLoc = new LocationModel();

                                assObjLoc.LocationName = sdr["LocationName"].ToString();
                                assObjLoc.LocationPostCode = sdr["LocationPostCode"].ToString();
                                assObjLoc.LocationHead = sdr["LocationHead"].ToString();

                                assObj.LocationName = assObjLoc.LocationName;

                                assObj.LocationPostCode = assObjLoc.LocationPostCode;

                                assObj.LocationHead = assObjLoc.LocationHead;

                            }
                        }
                    }



                    //return TrackModel; //Returning data

                }
            }

            // After Location details and Now loading patient details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Select * from PATIENT where PatientID='" + patientID + "'")) ////24.03.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //LocationModel assObjLoc = new LocationModel();

                                //assObj.LocationName = int.Parse(sdr["LocationName"].ToString());

                                TrackModel assObjpat = new TrackModel();

                                assObjpat.PatientID = sdr["PatientID"].ToString();
                                assObjpat.Forename = sdr["Forename"].ToString();
                                assObjpat.Surname = sdr["Surname"].ToString();
                                assObjpat.Address = sdr["Address"].ToString();
                                assObjpat.ContactNo = sdr["ContactNo"].ToString();
                                assObjpat.CarerID = sdr["CarerID"].ToString();

                                assObj.PatientID = assObjpat.PatientID.ToString();
                                assObj.Forename = assObjpat.Forename.ToString();
                                assObj.Surname = assObjpat.Surname.ToString();
                                assObj.Address = assObjpat.Address.ToString();
                                assObj.ContactNo = assObjpat.ContactNo.ToString();
                                assObj.CarerID = assObjpat.CarerID.ToString();

                                
                            }
                        }
                    }



                    //return TrackModel; //Returning data

                }
            }

            TrackModel.Add(assObj);
            return TrackModel; //Returning data
        }

        public string getAssetLocList() //30.04.20 - Athar: Method to get data
        {
            LocationModel assObj = new LocationModel();

            List<LocationModel> LocationModel = new List<LocationModel>(); ////30.04.20 - Athar: List to return when populated
            string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

            // After Getting Asset details and Now loading Location Details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Select * from LOCATIONS")) ////24.03.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //LocationModel assObjLoc = new LocationModel();

                                //assObj.LocationName = int.Parse(sdr["LocationName"].ToString());

                                LocationModel assObjLoc = new LocationModel();

                                assObjLoc.LocationID = int.Parse(sdr["LocationID"].ToString());

                                assObjLoc.LocationName = sdr["LocationName"].ToString();
                                assObjLoc.LocationPostCode = sdr["LocationPostCode"].ToString();
                                assObjLoc.LocationHead = sdr["LocationHead"].ToString();


                                LocationModel.Add(assObjLoc);

                            }
                        }
                    }



                    //return TrackModel; //Returning data

                }
            }

            

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewData["jLoc"] = serializer.Serialize(LocationModel);


            string output = "";
            //output = new JavaScriptSerializer().Serialize(LocationModel);
            return output; //Returning data
            //LocationModel
        }

        public string getAssetPatList() //30.04.20 - Athar: Method to get data
        {
            

            List<PatientModel> patientModel = new List<PatientModel>(); ////30.04.20 - Athar: List to return when populated
            string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

            // After Getting Asset details and Now loading Location Details and storing in the TrackModel List 30.04.20 - Athar
            using (SqlConnection sqlconn = new SqlConnection(connection))
            {
                using (SqlCommand sqlcomm = new SqlCommand("Select * from PATIENT")) ////24.03.20 - Athar: Getting data
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlcomm.Connection = sqlconn;
                        sqlconn.Open();
                        sda.SelectCommand = sqlcomm;
                        SqlDataReader sdr = sqlcomm.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //LocationModel assObjLoc = new LocationModel();

                                //assObj.LocationName = int.Parse(sdr["LocationName"].ToString());

                                PatientModel assObj = new PatientModel();

                                assObj.PatientID = int.Parse(sdr["PatientID"].ToString());
                                assObj.Forename = sdr["Forename"].ToString();
                                assObj.Surname = sdr["Surname"].ToString();
                                assObj.Address = sdr["Address"].ToString();
                                assObj.ContactNoString = sdr["ContactNo"].ToString();
                                assObj.CarerID = int.Parse(sdr["CarerID"].ToString());


                                patientModel.Add(assObj);

                            }
                        }
                    }



                    //return TrackModel; //Returning data

                }
            }



            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewData["jpat"] = serializer.Serialize(patientModel);


            string output = "";
            //output = new JavaScriptSerializer().Serialize(LocationModel);
            return output; //Returning data
            //LocationModel
        }

        [HttpGet] // Created a http request to track asset and show details on track page - Athar - 30.04.20
        public IActionResult EditAsset(TrackModel formData)
        {
            ViewData["noData"] = "0";
            string id = formData.AssetID.ToString();
            bool flag = false;
            int i;
            bool result = int.TryParse(id, out i);
            if (result && int.Parse(id) > 0)
            {

                if (!Regex.IsMatch(formData.AssetID.ToString(), @"^[0-9]*$"))
                {
                    ViewData["error"] = "Error: Cannot retreive Asset Information";
                    return View();
                }
                else
                {
                    string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string

                    // Getting Asset details and storing in the TrackModel List 30.04.20 - Athar
                    using (SqlConnection sqlconn = new SqlConnection(connection))
                    {

                        using (SqlCommand sqlcomm = new SqlCommand("select * from Assets where AssetID=" + formData.AssetID)) ////30.04.20 - Athar: Getting data
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                sqlcomm.Connection = sqlconn;
                                sqlconn.Open();
                                sda.SelectCommand = sqlcomm;
                                SqlDataReader sdr = sqlcomm.ExecuteReader(); //30.04.20 - Athar: Reading data
                                if (sdr.HasRows)
                                {
                                    while (sdr.Read())
                                    {

                                        flag = true;


                                    }
                                }
                            }





                        }
                    }
                }

                if (flag) {
                    string q = "select * from ASSETS where AssetID = '" + id + "'";

                    //return View(getAssetTrack(q)); //Returning data - Athar 30.04.20
                    ViewData["error"] = "";
                    //ViewData["jLoc"] = getAssetLocList();
                    getAssetLocList();
                    getAssetPatList();
                    return View(getAssetInfo(q));
                }
                else
                {
                    ViewData["error"] = "Error: Cannot retreive Asset Information";
                    ViewData["noData"] = "1";
                    return View();
                }
            }
            else
            {
                ViewData["error"] = "Error: Cannot retreive Asset Information";
                ViewData["noData"] = "1";
                return View();
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
