using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SunglassCatiaPlugin.Utils;
using System.Reflection;



namespace SunglassCatiaPlugin.Core
{
    public static class SunglassMgr
    {
        public const string SUNGLASS_URL = "https://sunglass.io/";
        public const string TERMS_OF_USER_URL = "https://sunglass.io/static/tnc.html";

#if INTERNAL_RELEASE 
        const string SUNGLASS_SERVER_URL = "https://sngls.io/api/v1/";
        const bool DEV = true;
#else
        const string SUNGLASS_SERVER_URL = "https://sunglass.io/api/v1/";
        const bool DEV = false;
#endif
        const string TYPE = "application/json";

        private static ConnectionManager m_ConnectionMgr;
     

        public static void Init()
        {
            Connect(Properties.Settings.Default.LastUsername, Properties.Settings.Default.LastPassword);
        }
        public static bool Connect(string userEmail, string password)
        {
            Boolean blnconnect;
            blnconnect = true;
            if (m_ConnectionMgr == null)
            {
                Assembly thisAssem = Assembly.GetExecutingAssembly();

                string client = Utils.Utils.Utility.GetPluginVersion();

                //by mithu, need to write code
                string hostSoft = "Catia";
                string os = Utils.Utils.Utility.GetOSVersion();

                LoginManager loginMgr = new LoginManager(SUNGLASS_SERVER_URL, client,hostSoft, os);
                SunglassCatiaPlugin.Utils.Account account = loginMgr.Login(userEmail, password);

                if (account != null)
                {
                    m_ConnectionMgr = new ConnectionManager(SUNGLASS_SERVER_URL, account.sid, account.token, TYPE, DEV, userEmail, client, os);
                    if (!m_ConnectionMgr.Authenticate())
                    {
                        m_ConnectionMgr = null;
                        blnconnect= false;
                    }
                    else
                    {
                        return blnconnect;
                    }
                
                }

                else if( account == null)
                {
                   blnconnect = false;
                }
               

            }
            return blnconnect;
          
        }

        public static bool IsLoggedIn()
        {
      
           return m_ConnectionMgr != null;
        
        }
        public static List<Project> GetAllProjects()
        {
            if (m_ConnectionMgr != null)
            {
                ProjectList prjsList = m_ConnectionMgr.GetProjects();

                List<Project> retVal = new List<Project>();
                retVal.AddRange(prjsList.admin);
                retVal.AddRange(prjsList.owner);
                retVal.AddRange(prjsList.editor);

                return retVal;
            }
            else
            {
                System.Diagnostics.Debug.Assert(false);
                return null;
            }
        }

        public static bool ProjectExists(int id)
        {
            foreach (Project prj in GetAllProjects())
            {
                if (prj.id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static Project GetProjectById(int id)
        {
            if (m_ConnectionMgr != null)
            {
                return m_ConnectionMgr.GetProject(id.ToString());
            }
            else
            {
                System.Diagnostics.Debug.Assert(false);
                return null;
            }
        }

        ///<Summary>
        /// Get Project By ID
        ///</Summary>

        public static bool DownloadModels(Project prj, string downloadPath, out int modelsDownloaded, out int modelsSkipped)
        {
            modelsDownloaded = 0;
            modelsSkipped = 0;
            try
            {
                if (m_ConnectionMgr != null)
                {
                    ModelList models = m_ConnectionMgr.GetModels(prj);

                    for (int i = 0; i < models.models.Count; i++)
                    {
                        Model model = models.models[i];

                        //if (!AreModelsEqual(prj, model, SwUtils.GetStructDocPathByName(model.name)))
                        //{
                        //    m_ConnectionMgr.DownloadModel(prj, model.id, downloadPath);
                        //    modelsDownloaded++;
                        //}
                        //else
                        //{
                        //    modelsSkipped++;
                        //}


                        //need to check
                        //BackgroundProcessor.UpdateProgress(i + 1, models.models.Count);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                    return false;
                }
            }
            catch
            {
                System.Diagnostics.Debug.Assert(false);
                return false;
            }

            return true;
        }

        ////private static bool AreModelsEqual(Project prj, Model model, string localModel)
        //{
        //    try
        //    {
        //        //commented by mithu
        //        //List<Sunglass.Version> vers = m_ConnectionMgr.GetModelVersions(prj, model.id).versions;
        //        //string origChecksum = vers[vers.Count - 1].origChecksum;
        //        //string newChecksum = Utils.Utility.GetSHA1Hash(localModel);

        //        //return newChecksum.Equals(origChecksum);
        //    }
        //    catch
        //    {
        //        System.Diagnostics.Debug.Assert(false);
        //        return false;
        //    }
        //}

        public static void Logout()
        {
            m_ConnectionMgr = null;
            SunglassCatiaPlugin.Properties.Settings.Default.LastUsername = "";
            SunglassCatiaPlugin.Properties.Settings.Default.LastPassword = "";
            SunglassCatiaPlugin.Properties.Settings.Default.Save();
        }

      
        
    }
}
