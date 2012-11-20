using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using System.Web.Extension.Script.Serialization;
using System.Web.Script.Serialization;
using System.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace SunglassCatiaPlugin.Utils
{
    class AnalyticsConnectionManager
    {

        private string uri = "http://api.mixpanel.com/track/";
        private string token = "d31aec620dcf580f840d91d19502c3b5";
        private Boolean dev = true;
        private Boolean setTime = true;
        private static readonly Encoding encoding = Encoding.UTF8;


      
        public AnalyticsConnectionManager(string token, Boolean dev, Boolean setTime)
        {
            this.token = token;
            this.dev = dev;
            this.setTime = setTime;
        }

        public string Base64Encode(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        public string Base64Decode(string source)
        {
            byte[] bytes = Convert.FromBase64String(source);
            return Encoding.UTF8.GetString(bytes);
        }
        public String trackEvent(string @event, IDictionary<string, object> properties)
        {

            Dictionary<string, object> propertyBag = new Dictionary<string, object>(properties);

            propertyBag["token"] = this.token;

            if (this.setTime)
            {
                propertyBag["time"] = DateTime.UtcNow;
            }


            string eventJson = new JavaScriptSerializer().Serialize(new Dictionary<string, object>
            {
                   {"event", @event},
                   {"properties", propertyBag}
            });



            string data = "data=" + Base64Encode(eventJson);

            if (this.dev)
            {
                data += "&test=1";
            }

            Console.Write(data);



            String response = this.Get(this.uri, data);

            return response;

        }

        //public String trackEvent(string @event, IDictionary<string, object> properties)
        //{

        //    Dictionary<string, object> propertyBag = new Dictionary<string, object>(properties);

        //    propertyBag["token"] = this.token;

        //    if (this.setTime)
        //    {
        //        propertyBag["time"] = DateTime.UtcNow;
        //    }


        //    //string eventJson = new JavaScriptSerializer().Serialize(new Dictionary<string, object>
        //    //{
        //    //       {"event", @event},
        //    //       {"properties", propertyBag}
        //    //});



        //    //string data = "data=" + Base64Encode(eventJson);

        //    //if (this.dev)
        //    //{
        //    //    data += "&test=1";
        //    //}

        //    //Console.Write(data);



        //    //String response = this.Get(this.uri, data);

        //    //return response;

        //}

        public String Get(string uri, string query)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + "?" + query);
            request.Method = "GET";

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
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, uri, ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", uri, ex));
            }
            finally
            {
                if (response != null) response.Close();
            }

            return responseString;
        }

        public String Post(string uri, string body)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);

            request.GetRequestStream().Write(bodyBytes, 0, bodyBytes.Length);

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
                    Console.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}. {2}", ex.Status, uri, ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not test url {0}. {1}", uri, ex));
            }
            finally
            {
                if (response != null) response.Close();
            }

            return responseString;
        }

        private String GetRequest(string data)
        {
            string url = this.uri + "/track/?data=" + data + "&test=" + this.dev;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            // request.Accept = type;
            // request.ContentType = type;

            //for some reason this is broken
            //request.Credentials = new NetworkCredential(this.username, this.password);
            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(this.url), "Basic", new NetworkCredential(this.username, this.password));
            //request.Credentials = credentialCache;   
            //request.PreAuthenticate = true;

            //do it the old fashioned way for now
            //   string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.sid + ":" + this.token));
            //    request.Headers.Add("Authorization", "Basic " + credentials);

            //--------------------------------------------------------------------
            // FOR TEST SERVER ONLY
            //--------------------------------------------------------------------
            // allows for validation of SSL conversations
            //   ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
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
                Console.WriteLine(String.Format("Could not test url {0}. {1}", uri, ex));
            }
            finally
            {
                if (response != null) response.Close();
            }

            return responseString;
        }


    }

    public class Properties
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string ip { get; set; }
        public string token { get; set; }

        public string action { get; set; }
    }

    public class Event
    {
        public string @event { get; set; }
        public Properties properties { get; set; }
    }
}
