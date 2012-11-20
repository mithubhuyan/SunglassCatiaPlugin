using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SunglassCatiaPlugin.Utils
{
    public class LoginManager
    {


        Dictionary<string, object> properties = new Dictionary<string, object>();
        static string mixpanelToken = "d31aec620dcf580f840d91d19502c3b5";

        AnalyticsConnectionManager mpClient = new AnalyticsConnectionManager(mixpanelToken, false, true);



        private String url;
        private static readonly Encoding encoding = Encoding.UTF8;

        public LoginManager(String url, String Client, String HostSoftware, String OperatingSystem)
        {
            this.url = url;



            properties["Client"] = Client;
            properties["Host Software"] = HostSoftware;
            properties["Operating system"] = OperatingSystem;

        }

        public Account Login(String email, String password)
        {


            Dictionary<string, string> postParameters = new Dictionary<string, string>();

            postParameters.Add("email", email);
            postParameters.Add("password", password);

            properties["Email"] = email;

            string loginJson = this.LoginPostRequest(postParameters);
            if (loginJson.StartsWith("Error"))
            {
                Account account = null;
                return account;
            }
            else
            {

                Account account = JsonConvert.DeserializeObject<Account>(loginJson);

                return account;
            }


        }

        private string LoginPostRequest(Dictionary<string, string> postParameters)
        {

            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            byte[] formData = GetMultipartFormData(postParameters, boundary);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(this.url + "login");
            myRequest.Method = "POST";
            myRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            //myRequest.KeepAlive = true;
            myRequest.UserAgent = "Sunglass Connection Manager";
            myRequest.ContentLength = formData.Length;

            Stream rs = myRequest.GetRequestStream();

            rs.Write(formData, 0, formData.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = myRequest.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);

                mpClient.trackEvent("Login Success", properties);

                return (reader2.ReadToEnd());


            }
            catch (Exception ex)
            {

                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }

                mpClient.trackEvent("Login Fail", properties);

                return ("Error " + ex);




            }
            finally
            {
                myRequest = null;
            }

        }

        private static byte[] GetMultipartFormData(Dictionary<string, string> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;


                string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    param.Key,
                    param.Value);
                formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));

            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

    }
    public class ConnectionManager
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        Dictionary<string, object> properties = new Dictionary<string, object>();
        static string mixpanelToken = "d31aec620dcf580f840d91d19502c3b5";

        AnalyticsConnectionManager mpClient = new AnalyticsConnectionManager(mixpanelToken, false, true);


        private Boolean dev;
        private String sid;
        private String token;
        private String url;
        private String type;

        public ConnectionManager(String url, String sid, String token, String type, Boolean dev, String Email, String Client, String OperatingSystem)
        {
            this.dev = dev;
            this.url = url;
            this.sid = sid;
            this.token = token;
            this.type = type;

            properties["User_id"] = sid;
            properties["Email"] = Email;
            properties["Client"] = Client;
            //properties["Host Software"] = HostSoftware;
            properties["Operating system"] = OperatingSystem;

        }

        //Utility HTTP Requests

        //GET 
        //CHANGED FROM PRIVATE TO PUBLIC BY MITHU
        public String GetRequest(string path)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url + path);
            request.Method = "GET";
            request.Accept = type;
            request.ContentType = type;

            //for some reason this is broken
            //request.Credentials = new NetworkCredential(this.username, this.password);
            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(this.url), "Basic", new NetworkCredential(this.username, this.password));
            //request.Credentials = credentialCache;   
            //request.PreAuthenticate = true;

            //do it the old fashioned way for now
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
            request.Headers.Add("Authorization", "Basic " + credentials);

            //--------------------------------------------------------------------
            // FOR TEST SERVER ONLY
            //--------------------------------------------------------------------
            // allows for validation of SSL conversations
            if (dev)
            {
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            }
            //--------------------------------------------------------------------


            HttpWebResponse response = null;
            string responseString = "";



            try
            {
                response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode == HttpStatusCode.OK)
                {

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseString = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {

                    Console.WriteLine("HTTP Status Code: " + (int)((HttpWebResponse)ex.Response).StatusCode);
                    Console.WriteLine("Status Description : " + ((HttpWebResponse)ex.Response).StatusDescription);
                    return ((HttpWebResponse)ex.Response).StatusCode.ToString();
                }
                else
                {
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, url, ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", url, ex));
            }
            finally
            {
                //Unregister callback
                if (dev)
                {
                    ServicePointManager.ServerCertificateValidationCallback -= ValidateRemoteCertificate;
                }

                //Close Response
                if (response != null) response.Close();
            }

            return responseString;
        }
        //POST
        private String PostRequest(string path, string postData)
        {
            StreamWriter requestWriter;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url + path);
            request.Method = "POST";
            request.Accept = type;
            request.ContentType = type;
            //for some reason this is broken
            //request.Credentials = new NetworkCredential(this.username, this.password);
            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(this.url), "Basic", new NetworkCredential(this.username, this.password));
            //request.Credentials = credentialCache;   
            //request.PreAuthenticate = true;

            //do it the old fashioned way for now
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
            request.Headers.Add("Authorization", "Basic " + credentials);

            //--------------------------------------------------------------------
            // FOR TEST SERVER ONLY
            //--------------------------------------------------------------------
            // allows for validation of SSL conversations
            if (dev)
            {
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            }
            //--------------------------------------------------------------------

            using (requestWriter = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter.Write(postData);
            }

            HttpWebResponse response = null;
            string responseString = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode == HttpStatusCode.OK)
                {

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseString = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {

                    Console.WriteLine("HTTP Status Code: " + (int)((HttpWebResponse)ex.Response).StatusCode);
                    Console.WriteLine("Status Description : " + ((HttpWebResponse)ex.Response).StatusDescription);
                    return ((HttpWebResponse)ex.Response).StatusCode.ToString();
                }
                else
                {
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, url, ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", url, ex));
            }
            finally
            {
                //Unregister callback
                if (dev)
                {
                    ServicePointManager.ServerCertificateValidationCallback -= ValidateRemoteCertificate;
                }
                //Close Response
                if (response != null) response.Close();
            }

            return responseString;
        }
        //PUT
        private String PutRequest(string path, string putData)
        {
            StreamWriter requestWriter;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url + path + "?method=PUT");
            //request.Method = "PUT";
            request.Method = "POST";
            request.Accept = type;
            request.ContentType = type;

            //for some reason this is broken
            //request.Credentials = new NetworkCredential(this.username, this.password);
            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(this.url), "Basic", new NetworkCredential(this.username, this.password));
            //request.Credentials = credentialCache;   
            //request.PreAuthenticate = true;

            //do it the old fashioned way for now
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
            request.Headers.Add("Authorization", "Basic " + credentials);

            //--------------------------------------------------------------------
            // FOR TEST SERVER ONLY
            //--------------------------------------------------------------------
            // allows for validation of SSL conversations
            if (dev)
            {
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            }
            //--------------------------------------------------------------------

            using (requestWriter = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter.Write(putData);
            }

            HttpWebResponse response = null;
            string responseString = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode == HttpStatusCode.OK)
                {

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseString = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {

                    Console.WriteLine("HTTP Status Code: " + (int)((HttpWebResponse)ex.Response).StatusCode);
                    Console.WriteLine("Status Description : " + ((HttpWebResponse)ex.Response).StatusDescription);
                    return ((HttpWebResponse)ex.Response).StatusCode.ToString();
                }
                else
                {
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, url, ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", url, ex));
            }
            finally
            {
                //Unregister callback
                if (dev)
                {
                    ServicePointManager.ServerCertificateValidationCallback -= ValidateRemoteCertificate;
                }
                //Close Response
                if (response != null) response.Close();
            }

            return responseString;
        }
        //DELETE
        private String DeleteRequest(string path)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url + path);
            request.Method = "DELETE";
            request.Accept = type;
            request.ContentType = type;
            //for some reason this is broken
            //request.Credentials = new NetworkCredential(this.username, this.password);
            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(this.url), "Basic", new NetworkCredential(this.username, this.password));
            //request.Credentials = credentialCache;   
            //request.PreAuthenticate = true;

            //do it the old fashioned way for now
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
            request.Headers.Add("Authorization", "Basic " + credentials);

            //--------------------------------------------------------------------
            // FOR TEST SERVER ONLY
            //--------------------------------------------------------------------
            // allows for validation of SSL conversations
            if (dev)
            {
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            }
            //--------------------------------------------------------------------


            HttpWebResponse response = null;
            string responseString = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode == HttpStatusCode.OK)
                {

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseString = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {

                    Console.WriteLine("HTTP Status Code: " + (int)((HttpWebResponse)ex.Response).StatusCode);
                    Console.WriteLine("Status Description : " + ((HttpWebResponse)ex.Response).StatusDescription);
                    return ((HttpWebResponse)ex.Response).StatusCode.ToString();
                }
                else
                {
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, url, ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", url, ex));
            }
            finally
            {
                //Unregister callback
                if (dev)
                {
                    ServicePointManager.ServerCertificateValidationCallback -= ValidateRemoteCertificate;
                }
                //Close Response
                if (response != null) response.Close();
            }

            return responseString;

        }

        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            Boolean Shady = true;
            if (Shady)
            {
                // allow any old dodgy certificate...
                return true;
            }
            else
            {
                return policyErrors == SslPolicyErrors.None;
            }
        }
        //Simple File Downloader
        private void DownloadFile(string path, string filename, string url)
        {
            string filepath = path + filename;

            try
            {
                WebClient client = new WebClient();

                //This is broken
                //client.Credentials = new NetworkCredential(this.username, this.password);

                //do it the old fashioned way for now
                string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
                client.Headers.Add("Authorization", "Basic " + credentials);

                client.DownloadFile(url, filepath);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.GetBaseException().ToString());
            }
        }
        public Boolean Authenticate()
        {


            Boolean authenticated = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url + "projects");
            request.Method = "GET";
            request.Accept = this.type;
            request.ContentType = this.type;

            //for some reason this is broken
            //request.Credentials = new NetworkCredential(this.username, this.password);
            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(this.url), "Basic", new NetworkCredential(this.username, this.password));
            //request.Credentials = credentialCache;   
            //request.PreAuthenticate = true;

            //do it the old fashioned way for now
            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
            request.Headers.Add("Authorization", "Basic " + credentials);

            //--------------------------------------------------------------------
            // FOR TEST SERVER ONLY
            //--------------------------------------------------------------------
            // allows for validation of SSL conversations
            if (dev)
            {
                ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            }
            //--------------------------------------------------------------------

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    authenticated = true;
                    //mpClient.trackEvent("Authenticate Success", properties);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    Console.WriteLine("HTTP Status Code: " + (int)((HttpWebResponse)ex.Response).StatusCode);
                    Console.WriteLine("Status Description : " + ((HttpWebResponse)ex.Response).StatusDescription);
                    return false;
                }
                else
                {
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, url, ex));
                }
                //mpClient.trackEvent("Authenticate Fail", properties);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", url, ex));
            }
            finally
            {
                //Unregister callback
                ServicePointManager.ServerCertificateValidationCallback -= ValidateRemoteCertificate;
                //Close Response
                if (response != null) response.Close();
            }

            return authenticated;


        }

        ///<Summary>
        /// Get Projects
        ///</Summary>
        public ProjectList GetProjects()
        {
            string projectJson = this.GetRequest("projects");
            ProjectList projects = JsonConvert.DeserializeObject<ProjectList>(projectJson);
            return projects;
        }
        ///<Summary>
        /// Get Project By ID
        ///</Summary>
        public Project GetProject(string id)
        {
            string projectJson = this.GetRequest("projects/" + id);
            Project project = JsonConvert.DeserializeObject<Project>(projectJson);
            return project;
        }


        ///<Summary>
        ///Get Model Versions
        ///</Summary>
        public VersionList GetModelVersions(Project project, string id)
        {
            string versionListJson = this.GetRequest("projects/" + project.id + "/models/" + id + "/versions");
            VersionList versions = JsonConvert.DeserializeObject<VersionList>(versionListJson);
            return versions;
        }

        ///<Summary>
        ///Download Model From Project, returns the full url for the file
        ///</Summary>
        public string DownloadModel(Project project, string id, string path)
        {
            Model model = this.GetModelDetails(project, id);
            string url = this.url + "projects/" + project.id + "/models/" + model.id + "/data?format=original";
            string fileName = model.name;
            this.DownloadFile(path, fileName, url);

            mpClient.trackEvent("Model Downloaded", properties);

            return url;
        }
        public Model GetModelDetails(Project project, string id)
        {
            string modelJson = this.GetRequest("projects/" + project.id + "/models/" + id);
            Model model = JsonConvert.DeserializeObject<Model>(modelJson);
            return model;
        }
        public ModelList GetModels(Project project)
        {
            string modelJson = this.GetRequest("projects/" + project.id + "/models");
            ModelList models = JsonConvert.DeserializeObject<ModelList>(modelJson);
            return models;
        }

    }



    /*
        
     * Classses for Sunglass API Objects
            
    */

    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string emails { get; set; }
        public string href { get; set; }
        public AccountLinks links { get; set; }
        public string createdAt { get; set; }
        public string modifiedAt { get; set; }
        public string sid { get; set; }
        public string token { get; set; }
        public AccountUsage usage { get; set; }
    }

    public class AccountLinks
    {
        public string projects { get; set; }
    }

    public class AccountUsage
    {
        public int latestProject { get; set; }
        public string latestSpace { get; set; }
        public string latestSpaceRole { get; set; }
    }

    ///<Summary>
    ///A SG User Object
    ///</Summary>
    public class User
    {
        ///<Summary>
        ///Id of User
        ///</Summary>
        public int id { get; set; }
        ///<Summary>
        ///Name of User
        ///</Summary>
        public string emails { get; set; }
        ///<Summary>
        ///Email of User. Important because Sunglass works on uniqueness of Emails
        ///</Summary>
        public string href { get; set; }
        ///<Summary>
        ///See UserLinks Object
        ///</Summary>
        public UserLinks links { get; set; }
        ///<Summary>
        ///Creation date
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///last modified Date
        ///</Summary>
        public string modifiedAt { get; set; }
        ///<Summary>
        ///See Usage Object
        ///</Summary>
        public Usage usage { get; set; }
    }
    ///<Summary>
    /// Recent Usage Details of User
    ///</Summary>
    public class Usage
    {
        ///<Summary>
        /// The last project viewed by User
        ///</Summary>
        public int latestProject { get; set; }
        ///<Summary>
        /// The id of the latest Stage (Space?) Viewed by User
        ///</Summary>
        public string latestStage { get; set; }
        ///<Summary>
        /// The role of the User in that Stage (Space?)
        ///</Summary>
        public string latestStageRole { get; set; }
    }

    ///<Summary>
    ///Project Object
    ///</Summary>
    public class Project
    {
        ///<Summary>
        ///Id of Project
        ///</Summary>
        public int id { get; set; }
        ///<Summary>
        ///Name of the Project
        ///</Summary>
        public string name { get; set; }
        ///<Summary>
        ///Description of Project, if provided on creation
        ///</Summary>
        public string description { get; set; }
        ///<Summary>
        ///Public / Private project
        ///</Summary>
        public string visibility { get; set; }
        ///<Summary>
        ///Api endpoit for this project
        ///</Summary>
        public string href { get; set; }
        ///<Summary>
        ///See ProjectLinks Object
        ///</Summary>
        public ProjectLinks links { get; set; }
        ///<Summary>
        ///Creation date of the project
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///last modified Date of the Project
        ///</Summary>
        public string modifiedAt { get; set; }
    }

    ///<Summary>
    ///Project List
    ///</Summary>
    public class ProjectList
    {
        ///<Summary>
        ///List of User Projects with owner priveliges
        ///</Summary>
        public List<Project> owner { get; set; }
        ///<Summary>
        ///List of User Projects with admin priveliges
        ///</Summary>
        public List<Project> admin { get; set; }
        ///<Summary>
        ///List of User Projects with editor priveliges
        ///</Summary>
        public List<Project> editor { get; set; }
        ///<Summary>
        ///List of User Projects with guest priveliges
        ///</Summary>
        public List<Project> guest { get; set; }
    }

    ///<Summary>
    ///Space Object
    ///</Summary>
    public class Space
    {
        ///<Summary>
        ///Id of Space
        ///</Summary>
        public string id { get; set; }
        ///<Summary>
        ///name of Space
        ///</Summary>
        public string name { get; set; }
        ///<Summary>
        ///User's role in this project
        ///</Summary>
        public string role { get; set; }
        ///<Summary>
        ///Api Endpoint for this space
        ///</Summary>
        public string href { get; set; }
        ///<Summary>
        ///Id of Space to which this space belongs (if this equals this space's id then this is the root space)
        ///</Summary>
        public string parentSpaceId { get; set; }
        ///<Summary>
        ///Id of Project to which this space belongs
        ///</Summary>
        public int projectId { get; set; }
        ///<Summary>
        ///Transformation Matrix in parent space
        ///</Summary>
        public List<double> transformMatrix { get; set; }
        ///<Summary>
        ///List of all groups
        ///</Summary>
        public string groupsList { get; set; }
        ///<Summary>
        ///Creation date
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///last modified Date
        ///</Summary>
        public string modifiedAt { get; set; }
        ///<Summary>
        ///See SpaceLinks Object
        ///</Summary>
        public SpaceLinks links { get; set; }
    }

    ///<Summary>
    ///Space List
    ///</Summary>
    public class SpaceList
    {
        ///<Summary>
        ///Space List
        ///</Summary>
        public List<Space> spaces { get; set; }
    }

    ///<Summary>
    ///Space List
    ///</Summary>
    public class SubSpaceList
    {
        ///<Summary>
        ///Space List
        ///</Summary>
        public List<Space> descendants { get; set; }
    }

    public class MetaData
    {
    }

    public class NewModel
    {
        public Model model { get; set; }
    }

    ///<Summary>
    ///Model Object
    ///</Summary>
    public class Model
    {
        ///<Summary>
        ///Id of Model
        ///</Summary>
        public string id { get; set; }
        ///<Summary>
        ///Name of Model
        ///</Summary>
        public string name { get; set; }
        ///<Summary>
        ///can be Textured if the model has textures
        ///</Summary>
        public string modelType { get; set; }
        ///<Summary>
        ///Model Object
        ///</Summary>
        public int projectId { get; set; }
        ///<Summary>
        ///Description of Model, if added
        ///</Summary>
        public string description { get; set; }
        ///<Summary>
        ///Model Metadata
        ///</Summary>
        public MetaData metaData { get; set; }
        ///<Summary>
        ///Id of User who created this model
        ///</Summary>
        public int creatorId { get; set; }
        ///<Summary>
        ///Model Object
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///Creation date
        ///</Summary>
        public string modifiedAt { get; set; }
        ///<Summary>
        ///last modified Date
        ///</Summary>
        public string href { get; set; }
        ///<Summary>
        ///Api endpoint for this model
        ///</Summary>
        public string version { get; set; }
    }

    ///<Summary>
    ///Model List
    ///</Summary>
    public class ModelList
    {
        ///<Summary>
        ///Model List
        ///</Summary>
        public List<Model> models { get; set; }
    }

    ///<Summary>
    ///Model Reference Object (in the api these are called MetaModels but that name is subject to change)
    ///</Summary>
    public class MetaModel
    {
        ///<Summary>
        ///Id of Model Reference
        ///</Summary>
        public int id { get; set; }
        ///<Summary>
        /// The id the space that this Model Reference belongs to
        ///</Summary>
        public string spaceId { get; set; }
        ///<Summary>
        ///The id of the Project that this Model Reference belongs to
        ///</Summary>
        public int projectId { get; set; }
        ///<Summary>
        ///Id of Model that defines the geometry for this model reference
        ///</Summary>
        public string modelId { get; set; }
        ///<Summary>
        ///Transformation Matrix used to position model in space
        ///</Summary>
        public List<double> transformMatrix { get; set; }
        ///<Summary>
        ///Model Reference Metadata
        ///</Summary>
        public MetaData metaData { get; set; }
        ///<Summary>
        ///Creation date
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///last modified Date
        ///</Summary>
        public string modifiedAt { get; set; }
        ///<Summary>
        ///See ModelDetails Object
        ///</Summary>
        public ModelDetails modelDetails { get; set; }
        ///<Summary>
        ///Endpoint for this model in the API
        ///</Summary>
        public string href { get; set; }
    }

    ///<Summary>
    ///Model Reference List
    ///</Summary>
    public class MetaModelList
    {
        ///<Summary>
        ///Model Reference List
        ///</Summary>
        public List<MetaModel> MetaModels { get; set; }
    }

    ///<Summary>
    ///Details for a Model Defintion from a Model Reference List
    ///</Summary>
    public class ModelDetails
    {
        ///<Summary>
        ///Id of Model
        ///</Summary>   
        public string id { get; set; }
        ///<Summary>
        ///Name of Model
        ///</Summary>
        public string name { get; set; }
        ///<Summary>
        ///can be Textured if the model has textures
        ///</Summary>
        public string modelType { get; set; }
        ///<Summary>
        ///The id of the Project that this Model belongs to
        ///</Summary>
        public int projectId { get; set; }
        ///<Summary>
        ///Description of Model, if added
        ///</Summary>
        public string description { get; set; }
        ///<Summary>
        ///Model metadata
        ///</Summary>
        public MetaData metaData { get; set; }
        ///<Summary>
        ///Id of User who created this model
        ///</Summary>
        public int creatorId { get; set; }
        ///<Summary>
        ///Creation date
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///last modified Date
        ///</Summary>
        public string modifiedAt { get; set; }
        ///<Summary>
        ///Api endpoint for this model
        ///</Summary>
        public string href { get; set; }
        ///<Summary>
        ///Api endpoint for this model
        ///</Summary>
        public string version { get; set; }
    }



















    public class Author
    {
        public string email { get; set; }
        public string name { get; set; }
    }


    ///<Summary>
    ///Version Object
    ///</Summary>
    public class Version
    {
        ///<Summary>
        ///Id of this version
        ///</Summary>
        public string id { get; set; }
        ///<Summary>
        ///A descriptive message about this version.
        ///</Summary>
        public string message { get; set; }
        ///<Summary>
        ///List of IDs of versions from which this version is derived
        ///</Summary>
        public List<string> parents { get; set; }
        ///<Summary>
        ///A timestamp for the version
        ///</Summary>
        public string timestamp { get; set; }
        ///<Summary>
        ///created Date
        ///</Summary>
        public string createdAt { get; set; }
        ///<Summary>
        ///List of users involved in this version
        ///</Summary>
        public List<Author> authors { get; set; }

        ///<Summary>
        ///created Date
        ///</Summary>
        public string origChecksum { get; set; }

        ///<Summary>
        ///created Date
        ///</Summary>
        public string coverImage { get; set; }
    }

    ///<Summary>
    ///Version List
    ///</Summary>
    public class VersionList
    {
        ///<Summary>
        ///Version list
        ///</Summary>
        public List<Version> versions { get; set; }
    }

    ///<Summary>
    ///User Links
    ///</Summary>
    public class UserLinks
    {
        ///<Summary>
        ///Api endpoints for this user's projects
        ///</Summary>
        public string projects { get; set; }
    }

    ///<Summary>
    ///Space Links
    ///</Summary>
    public class SpaceLinks
    {
        ///<Summary>
        ///Api endpoint for the project that this space belongs to
        ///</Summary>
        public string project { get; set; }
        ///<Summary>
        ///Api endpoint for the space that this space belongs to
        ///</Summary>
        public string parentSpace { get; set; }
        ///<Summary>
        ///Api endpoint for the subspaces of this space
        ///</Summary>
        public string subSpaces { get; set; }
        ///<Summary>
        ///Api endpoint for Model References in this space
        ///</Summary>
        public string metaModels { get; set; }
        ///<Summary>
        ///Api endpoint for views in this space
        ///</Summary>
        public string views { get; set; }
    }

    ///<Summary>
    ///Project Links
    ///</Summary>
    public class ProjectLinks
    {
        ///<Summary>
        ///Api endpoint for this project's spaces
        ///</Summary>
        public string spaces { get; set; }
        ///<Summary>
        ///Api endpoint for the rootSpace of this project
        ///</Summary>
        public string rootSpace { get; set; }
    }

    ///<Summary>
    ///File Parameters for Multipart upload
    ///</Summary>
    public class FileParameter
    {
        ///<Summary>
        ///File Data
        ///</Summary>
        public byte[] File { get; set; }
        ///<Summary>
        ///Filename
        ///</Summary>
        public string FileName { get; set; }
        ///<Summary>
        ///File Content type
        ///</Summary>
        public string ContentType { get; set; }

        ///<Summary>
        ///File Parameter
        ///</Summary>
        public FileParameter(byte[] file) : this(file, null) { }
        ///<Summary>
        ///File Parameter
        ///</Summary>
        public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
        ///<Summary>
        ///File Parameter
        ///</Summary>
        public FileParameter(byte[] file, string filename, string contenttype)
        {
            File = file;
            FileName = filename;
            ContentType = contenttype;
        }
    }

}


