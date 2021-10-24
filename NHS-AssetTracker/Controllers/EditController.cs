using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Nancy.Json;
using NHS_AssetTracker.Models;
using NHS_AssetTracker.Models.Edit;

namespace NHS_AssetTracker.Controllers
{
    public class EditController : Controller
    {
        public string Name(int n, string q)
        {
            return "";
        }

        // using the following code We can edit Name of an Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult EditName()
        {
            return Content("");
        }

        // using the following code We can edit Name of an Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult EditName(EditModel formData)
        {
            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            if (formData.f == "name")
            {
                bool result = Regex.IsMatch(formData.AssetType, @"^[a-zA-Z\s]*$");
                if (result)
                {
                    string id = int.Parse(formData.AssetID.ToString()).ToString();
                    int i;
                    bool res = int.TryParse(id, out i);
                    if (res && int.Parse(id) > 0)
                    {
                        int con = 0;
                        string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                        // Loading Asset Log Data and details  //03.05.20 - Athar
                        using (SqlConnection sqlconn = new SqlConnection(connection))
                        {
                            using (SqlCommand sqlcomm = new SqlCommand("update Assets Set AssetType = '" + formData.AssetType + "' where AssetID = '" + formData.AssetID + "'")) 
                            {

                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    sqlcomm.Connection = sqlconn;
                                    sqlconn.Open();
                                    sda.SelectCommand = sqlcomm;
                                    int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                    if (rows == 1)
                                    {
                                        con++;
                                        /*while (sdr.Read())
                                        {
                                            
                                        }*/

                                    }
                                }
                            }
                        }

                        if (con > 0)
                        {
                            obj.result = "1";
                            obj.info = "Asset name updated successfully.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }
                        else
                        {
                            obj.result = "0";
                            obj.info = "Save name failed. Cannot update the asset name at this moment. Please try again later.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }

                    }
                    else
                    {
                        obj.result = "0";
                        obj.info = "Save name failed. Please reload the page and try again.";
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }
                }
                else
                {
                    obj.result = "0";
                    obj.info = "Save name failed. Only alphabets are allowed. Please enter A-Z or a-z letters only.";
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
            }
            else
            {
                obj.result = "0";
                obj.info = "Asset information save failed.";
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }



            //return View();
        }

        // using the following code We can edit Location of an Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult EditLoc()
        {
            return Content("");
        }

        // using the following code We can edit Location of an Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult EditLoc(EditModel formData)
        {
            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            if (formData.f == "loc")
            {
                bool result = Regex.IsMatch(formData.LocationID.ToString(), @"^[0-9]*$");
                if (result)
                {
                    string id = int.Parse(formData.AssetID.ToString()).ToString();
                    int i;
                    bool res = int.TryParse(id, out i);
                    if (res && int.Parse(id) > 0)
                    {
                        int con = 0;
                        string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                        // Loading Asset Log Data and details  //03.05.20 - Athar
                        using (SqlConnection sqlconn = new SqlConnection(connection))
                        {
                            using (SqlCommand sqlcomm = new SqlCommand("update Assets Set AssetHome = '" + formData.LocationID + "' where AssetID = '" + formData.AssetID + "'")) 
                            {

                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    sqlcomm.Connection = sqlconn;
                                    sqlconn.Open();
                                    sda.SelectCommand = sqlcomm;
                                    int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                    if (rows == 1)
                                    {
                                        con++;
                                        /*while (sdr.Read())
                                        {
                                            
                                        }*/

                                    }
                                }
                            }
                        }

                        if (con > 0)
                        {
                            obj.result = "1";
                            obj.info = "Asset location updated successfully.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }
                        else
                        {
                            obj.result = "0";
                            obj.info = "Save location failed. Cannot update the asset location at this moment. Please try again later.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }

                    }
                    else
                    {
                        obj.result = "0";
                        obj.info = "Save location failed. Please enter numbers only.";
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }
                }
                else
                {
                    obj.result = "0";
                    obj.info = "Save location failed. Only numbers are allowed.";
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
            }
            else
            {
                obj.result = "0";
                obj.info = "Asset information save failed.";
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }



            //return View();
        }

        // using the following code We can edit Patient of an Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult EditPat()
        {
            return Content("");
        }

        // using the following code We can edit Patient of an Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult EditPat(EditModel formData)
        {
            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            if (formData.f == "pat")
            {
                bool result = Regex.IsMatch(formData.PatientID.ToString(), @"^[0-9]*$");
                if (result)
                {
                    string id = int.Parse(formData.AssetID.ToString()).ToString();
                    int i;
                    bool res = int.TryParse(id, out i);
                    if (res && int.Parse(id) > 0)
                    {
                        int con = 0;
                        string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                        // Loading Asset Log Data and details  //03.05.20 - Athar
                        using (SqlConnection sqlconn = new SqlConnection(connection))
                        {
                            using (SqlCommand sqlcomm = new SqlCommand("update Assets Set AssetUser = '" + formData.PatientID + "' where AssetID = '" + formData.AssetID + "'")) 
                            {

                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    sqlcomm.Connection = sqlconn;
                                    sqlconn.Open();
                                    sda.SelectCommand = sqlcomm;
                                    int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                    if (rows == 1)
                                    {
                                        con++;
                                        /*while (sdr.Read())
                                        {
                                            
                                        }*/

                                    }
                                }
                            }
                        }

                        if (con > 0)
                        {
                            obj.result = "1";
                            obj.info = "Asset patient updated successfully.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }
                        else
                        {
                            obj.result = "0";
                            obj.info = "Save patient failed. Cannot update the asset patient at this moment. Please try again later.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }

                    }
                    else
                    {
                        obj.result = "0";
                        obj.info = "Save patient failed. Please enter numbers only.";
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }
                }
                else
                {
                    obj.result = "0";
                    obj.info = "Save patient failed. Only numbers are allowed.";
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
            }
            else
            {
                obj.result = "0";
                obj.info = "Asset information save failed.";
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }



            //return View();
        }


        // using the following code We can edit Asset Value
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult EditVal()
        {
            return Content("");
        }

        // using the following code We can edit Asset Value
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult EditVal(EditModel formData)
        {
            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            if (formData.f == "val")
            {
                //bool result = Regex.IsMatch(formData.AssetValue.ToString(), @"^[0-9]*$");
                if (formData.AssetValue.ToLower().ToString() == "low" || formData.AssetValue.ToLower().ToString() == "moderate" || formData.AssetValue.ToLower().ToString() == "high")
                {
                    string id = int.Parse(formData.AssetID.ToString()).ToString();
                    int i;
                    bool res = int.TryParse(id, out i);
                    if (res && int.Parse(id) > 0)
                    {
                        int con = 0;
                        string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                        // Loading Asset Log Data and details  //03.05.20 - Athar
                        using (SqlConnection sqlconn = new SqlConnection(connection))
                        {
                            using (SqlCommand sqlcomm = new SqlCommand("update Assets Set AssetValue = '" + formData.AssetValue + "' where AssetID = '" + formData.AssetID + "'")) 
                            {

                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    sqlcomm.Connection = sqlconn;
                                    sqlconn.Open();
                                    sda.SelectCommand = sqlcomm;
                                    int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                    if (rows == 1)
                                    {
                                        con++;
                                        /*while (sdr.Read())
                                        {
                                            
                                        }*/

                                    }
                                }
                            }
                        }

                        if (con > 0)
                        {
                            obj.result = "1";
                            obj.info = "Asset value updated successfully.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }
                        else
                        {
                            obj.result = "0";
                            obj.info = "Save value failed. Cannot update the asset value at this moment. Please try again later.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }

                    }
                    else
                    {
                        obj.result = "0";
                        obj.info = "Save value failed. Unable to identify the Asset. Please try again later.";
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }
                }
                else
                {
                    obj.result = "0";
                    obj.info = "Save value failed. Only Low,Moderate,High are allowed.";
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
            }
            else
            {
                obj.result = "0";
                obj.info = "Asset information save failed.";
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }



            //return View();
        }

        // using the following code We can edit Asset Usability
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult EditUse()
        {
            return Content("");
        }

        // using the following code We can edit Asset Usability
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult EditUse(EditModel formData)
        {
            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            if (formData.f == "use")
            {
                //bool result = Regex.IsMatch(formData.AssetValue.ToString(), @"^[0-9]*$");
                if (formData.AssetUsable.ToString() == "0" || formData.AssetUsable.ToString() == "1")
                {
                    string id = int.Parse(formData.AssetID.ToString()).ToString();
                    int i;
                    bool res = int.TryParse(id, out i);
                    if (res && int.Parse(id) > 0)
                    {
                        int con = 0;
                        string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                        // Loading Asset Log Data and details  //03.05.20 - Athar
                        using (SqlConnection sqlconn = new SqlConnection(connection))
                        {
                            using (SqlCommand sqlcomm = new SqlCommand("update Assets Set AssetUsable = '" + formData.AssetUsable + "' where AssetID = '" + formData.AssetID + "'")) 
                            {

                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    sqlcomm.Connection = sqlconn;
                                    sqlconn.Open();
                                    sda.SelectCommand = sqlcomm;
                                    int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                    if (rows == 1)
                                    {
                                        con++;
                                        /*while (sdr.Read())
                                        {
                                            
                                        }*/

                                    }
                                }
                            }
                        }

                        if (con > 0)
                        {
                            obj.result = "1";
                            obj.info = "Asset value updated successfully.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }
                        else
                        {
                            obj.result = "0";
                            obj.info = "Save value failed. Cannot update the asset value at this moment. Please try again later.";
                            assetModels.Add(obj);
                            string output = new JavaScriptSerializer().Serialize(assetModels);
                            return Content(output);
                        }

                    }
                    else
                    {
                        obj.result = "0";
                        obj.info = "Save value failed. Unable to identify the Asset. Please try again later.";
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }
                }
                else
                {
                    obj.result = "0";
                    obj.info = "Save value failed. Only Low,Moderate,High are allowed.";
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
            }
            else
            {
                obj.result = "0";
                obj.info = "Asset information save failed.";
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }



            //return View();
        }

        // using the following code We can get List of Asset Locations
        // It will show data related to an asset on track page
        // Athar - 06.05.20
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


        // using the following code We can Add An Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult AddAsset()
        {
            getAssetLocList();
            return View();
        }

        // using the following code We can Add An Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult AddAsset(EditModel formData)
        {
            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            bool flag = true;

            string errors = "";

            if (formData.AssetID > 0)
            {
                if (!Regex.IsMatch(formData.AssetID.ToString(), @"^[0-9]*$"))
                {
                    errors += "<li>Error: Asset ID is not valid. Only numbers are allowed.</li>";
                    flag = false;
                }
            }
            else
            {
                errors += "<li>Error: Asset ID is not valid</li>";
                flag = false;
            }


            if (formData.AssetType != null)
            {
                if (!Regex.IsMatch(formData.AssetType.ToString(), @"^[A-Za-z ]+$"))
                {
                    errors += "<li>Error: Asset Type is not valid. Only aplphabets (A-Z) and spaces allowed.</li>";
                    flag = false;
                }
            }
            else
            {
                errors += "<li>Error: Asset Type is not valid</li>";
                flag = false;
            }

            if (formData.AssetValue == "1" || formData.AssetValue == "2" || formData.AssetValue == "3")
            {
                if (formData.AssetValue == "1")
                {
                    formData.AssetValue = "Low";
                }
                if (formData.AssetValue == "2")
                {
                    formData.AssetValue = "Moderate";
                }
                if (formData.AssetValue == "3")
                {
                    formData.AssetValue = "High";
                }

            }
            else
            {
                errors += "<li>Error: Asset Value is not valid</li>";
                flag = false;
            }



            if (formData.AssetUsableString == "0" || formData.AssetUsableString == "1")
            {
                if (formData.AssetUsableString == "0")
                {
                    formData.AssetUsable = 0;
                }

                if (formData.AssetUsableString == "1")
                {
                    formData.AssetUsable = 1;
                }
            }
            else
            {
                errors += "<li>Error: Asset Usability Value is not valid</li>";
                flag = false;
            }
            if (formData.AssetHome > 0)
            {
                if (!Regex.IsMatch(formData.AssetHome.ToString(), @"^[0-9]*$"))
                {
                    errors += "<li>Error: Asset Home Value is not valid. Only numbers are allowed.</li>";
                    flag = false;
                }
                else
                {
                    string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                    int con = 0;                                                                                                                                                                       // Loading Asset Log Data and details  //03.05.20 - Athar
                    using (SqlConnection sqlconn = new SqlConnection(connection))
                    {
                        using (SqlCommand sqlcomm = new SqlCommand("select * from LOCATIONS WHERE LocationID =" + formData.AssetHome))
                        {

                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                sqlcomm.Connection = sqlconn;
                                sqlconn.Open();
                                sda.SelectCommand = sqlcomm;
                                //int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                SqlDataReader sdr = sqlcomm.ExecuteReader();
                                if (sdr.HasRows)
                                {
                                    while (sdr.Read())
                                    {
                                        con++;
                                    }
                                }
                            }
                        }
                    }

                    if (con > 0)
                    {
                        //errors += "<li>Asset addedd successfully.</li>";
                    }
                    else
                    {

                        errors += "<li>Add Asset Home Value is not valid. Please try again later.</li>";
                        flag = false;
                    }
                }
            }
            else
            {
                errors += "<li>Error: Asset Home Value is not valid</li>";
                flag = false;
            }


            //bool result = Regex.IsMatch(formData.LocationID.ToString(), @"^[0-9]*$");



            if (flag && formData.f == "add")
            {
                int con = 0;
                string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                                                                                                                                                                                                  // Loading Asset Log Data and details  //03.05.20 - Athar
                using (SqlConnection sqlconn = new SqlConnection(connection))
                {

                    using (SqlCommand sqlcomm = new SqlCommand("insert into Assets (AssetID,AssetType,AssetValue,AssetUsable,AssetHome) values (" + formData.AssetID + ",'" + formData.AssetType + "','" + formData.AssetValue + "'," + formData.AssetUsable + "," + formData.AssetHome + ")"))
                    {

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            sqlcomm.Connection = sqlconn;
                            sqlconn.Open();
                            sda.SelectCommand = sqlcomm;
                            int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                            if (rows == 1)
                            {
                                con++;

                            }
                        }
                    }
                }



                if (con > 0)
                {
                    errors += "<li>Asset addedd successfully.</li>";
                    obj.result = "1";
                    obj.info = errors;
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
                else
                {

                    errors += "<li>Add Asset failed. Cannot Add a new asset at this moment. Please try again later.</li>";
                    obj.result = "0";
                    obj.info = errors;
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }

            }
            else
            {
                errors += "<li>Asset information save failed.</li>";
                obj.result = "0";
                obj.info = errors;
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }

        }

        // using the following code We can Delete An Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        public IActionResult DeleteAsset()
        {
            return Content("");
        }

        // using the following code We can Delete An Asset
        // It will show data related to an asset on track page
        // Athar - 06.05.20
        [HttpPost]
        public IActionResult DeleteAsset(EditModel formData)
        {

            List<EditControlModel> assetModels = new List<EditControlModel>();
            EditControlModel obj = new EditControlModel();

            bool flag = true;

            string errors = "";

            if (formData.AssetID > 0)
            {
                if (!Regex.IsMatch(formData.AssetID.ToString(), @"^[0-9]*$"))
                {
                    errors += "<li>Error: Asset ID is not valid. Only numbers are allowed.</li>";
                    flag = false;
                }
            }
            else
            {
                errors += "<li>Error: Asset ID is not valid</li>";
                flag = false;
            }


            if (flag)
            {
                if (flag && formData.f == "delete")
                {
                    int con = 0;
                    string connection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-NHS_AssetTracker-C5CB4079-FBF1-4886-9069-D73B69BED349;Trusted_Connection=True;MultipleActiveResultSets=true"; //Connection string
                                                                                                                                                                                                      // Loading Asset Log Data and details  //03.05.20 - Athar
                    using (SqlConnection sqlconn = new SqlConnection(connection))
                    {

                        using (SqlCommand sqlcomm = new SqlCommand("delete from Assets where AssetID='" + formData.AssetID+"'"))
                        {

                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                sqlcomm.Connection = sqlconn;
                                sqlconn.Open();
                                sda.SelectCommand = sqlcomm;
                                int rows = sqlcomm.ExecuteNonQuery(); //03.05.20 - Athar: Reading data
                                if (rows == 1)
                                {
                                    con++;

                                }
                            }
                        }
                    }

                    if (con > 0)
                    {
                        errors += "<li>Asset deleted successfully.</li>";
                        obj.result = "1";
                        obj.info = errors;
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }
                    else
                    {

                        errors += "<li>Asset delete failed. Cannot delete Asset at this moment. Please try again later.</li>";
                        obj.result = "0";
                        obj.info = errors;
                        assetModels.Add(obj);
                        string output = new JavaScriptSerializer().Serialize(assetModels);
                        return Content(output);
                    }

                }
                else
                {
                    errors += "<li>Asset delete failed.</li>";
                    obj.result = "0";
                    obj.info = errors;
                    assetModels.Add(obj);
                    string output = new JavaScriptSerializer().Serialize(assetModels);
                    return Content(output);
                }
            }
            else
            {
                errors += "<li>Asset delete failed.</li>";
                obj.result = "0";
                obj.info = errors;
                assetModels.Add(obj);
                string output = new JavaScriptSerializer().Serialize(assetModels);
                return Content(output);
            }
        }
    }
}