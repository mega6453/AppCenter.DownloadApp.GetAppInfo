using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Net;

namespace AppCenterDownloadAppGetAppInfo
{
    /// <summary>
    /// Provides methods to Download and Get Application information from Microsoft AppCenter. Create AppCenter instance with following parameters:AppCenter API Token and Owner name.
    /// </summary>
    public class AppCenter
    {
        private readonly string APIToken;
        private readonly string Owner;
        /// <summary>
        /// App Center API Token is mandatory for authentication and OwnerName is required for most of the methods.
        /// </summary>
        /// <param name="AppCenterAPIToken">Login to Appcenter->Account Settings->Use API Tokens->New API Token</param>
        /// <param name="OwnerName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {owner_name} would be Microsoft</param>
        public AppCenter(string AppCenterAPIToken, string OwnerName)
        {
            APIToken = AppCenterAPIToken.Trim();
            Owner = OwnerName.Trim();
        }

        /// <summary>
        /// Returns the application's display name.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>       
        public string GetApplicationDisplayName(string AppName)
        {
            AppName = AppName.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/latest";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            return token["app_display_name"].ToString();
        }

        /// <summary>
        /// Returns the application supported OS Type.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        public string GetApplicationSupportedOS(string AppName)
        {
            AppName = AppName.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/latest";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            return token["app_os"].ToString();
        }

        /// <summary>
        /// Returns the application's build number. 
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        public string GetApplicationBuildNumber(string AppName, string ReleaseID)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            return token["version"].ToString();
        }

        /// <summary>
        /// Returns the application's short version. 
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        public string GetApplicationShortVersion(string AppName, string ReleaseID)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            return token["short_version"].ToString();
        }

        /// <summary>
        /// Returns the application's Full(short+build) version.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        public string GetApplicationFullVersion(string AppName, string ReleaseID)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            return token["short_version"].ToString() + " (" + token["version"].ToString() + ")";
        }

        /// <summary>
        /// Returns the application's size. 
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        public string GetApplicationSize(string AppName, string ReleaseID)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            int size = (int)token["size"];
            return FormatSize(size);
        }

        /// <summary>
        /// Returns the application's minimum supported OS version.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        public string GetMinimumOSVersionSupportedByApplication(string AppName, string ReleaseID)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            string os = GetApplicationSupportedOS(AppName);
            return os + " " + token["min_os"].ToString();
        }

        /// <summary>
        /// Returns the application's Bundle ID / Package name.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        public string GetApplicationBundleID(string AppName)
        {
            AppName = AppName.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/latest";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            return token["bundle_identifier"].ToString();
        }

        /// <summary>
        /// Downloads the application from the AppCenter for the given Release ID with app's default file name. Returns the full path of the downloaded application.
        /// </summary>
        /// <param name="AppName">Login to Appcenter->Select an app->Observe the URL. e.g. URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        /// <param name="DownloadLocation">Folder path to download the application(e.g. "C:\\Users\\Owner\\Desktop\\")</param>
        /// <param name="DeleteExistingApps">Condition to Delete all the existing applications(same type what user downloads) from the Download location ; Delete->true ; Don't Delete->false</param>
        public string DownloadApplication(string AppName, string ReleaseID, string DownloadLocation, bool DeleteExistingApps)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            if (String.IsNullOrEmpty(DownloadLocation) | String.IsNullOrWhiteSpace(DownloadLocation))
            {
                throw new Exception("\n\nPlease enter a valid Path to Download application. e.g. C:\\Users\\Owner\\Desktop\\\n");
            }
            if (!DownloadLocation.EndsWith("\\"))
            {
                DownloadLocation = DownloadLocation + "\\";
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            DownloadLocation = DownloadLocation.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            dynamic obj = JsonConvert.DeserializeObject(restResponse.Content);
            Uri DownloadURL = obj.download_url;
            string fileName = obj.app_display_name;
            string os = obj.app_os;
            string shortVersion = obj.short_version;
            string Version = obj.version;
            string extension;
            if (os.Equals("android", StringComparison.InvariantCultureIgnoreCase))
            {
                extension = ".apk";
            }
            else if (os.Equals("ios", StringComparison.InvariantCultureIgnoreCase))
            {
                extension = ".ipa";
            }
            else
            {
                throw new Exception("\n\nOnly Android and iOS applications are supported by this method. Use Overloaded DownloadApplication method for other formats.\n");
            }
            string FileNameWithExtension = fileName + "_v" + shortVersion + "_build" + Version + extension;
            WebClient webClient = new WebClient();
            FileInfo file = new FileInfo(FileNameWithExtension);
            string currentDateTime = GetDateMonth() + GetHourMinuteSecond();
            if (DeleteExistingApps)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(DownloadLocation, "*" + extension);
                    foreach (string filePath in filePaths)
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception While Deleting Existing Apps from" + DownloadLocation + ": " + e);
                }
            }
            else
            {
                FileNameWithExtension = fileName + "_v" + shortVersion + "_build" + Version + "_" + currentDateTime + extension;
            }
            try
            {
                webClient.DownloadFile(DownloadURL, DownloadLocation + FileNameWithExtension);
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType().Name.Equals("DirectoryNotFoundException", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("\n\nPlease make sure the download location is valid and accessible. See the Original Exception below:\n" + e);
                }
                else
                {
                    throw e;
                }
            }
            return DownloadLocation + FileNameWithExtension;
        }



        /// <summary>
        /// Downloads the application from the AppCenter for the given Release ID with user defined file name. Returns the full path of the downloaded application.
        /// </summary>
        /// <param name="AppName">Login to Appcenter->Select an app->Observe the URL. e.g. URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        /// <param name="DownloadLocation">Folder path to download the application(e.g. "C:\\Users\\Owner\\Desktop\\")</param>
        /// <param name="FileNameWithExtension">Expected File name and the app extension(e.g. "AppCenter.apk")</param>
        /// <param name="DeleteExistingApps">Condition to Delete all the existing applications(same type what user downloads) from the Download location ; Delete->true ; Don't Delete->false</param>
        public string DownloadApplication(string AppName, string ReleaseID, string DownloadLocation, string FileNameWithExtension, bool DeleteExistingApps)
        {
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            if (String.IsNullOrEmpty(DownloadLocation) | String.IsNullOrWhiteSpace(DownloadLocation))
            {
                throw new Exception("\n\nPlease enter a valid Path to Download application. e.g. C:\\Users\\Owner\\Desktop\\\n");
            }
            if (String.IsNullOrEmpty(FileNameWithExtension) | String.IsNullOrWhiteSpace(FileNameWithExtension))
            {
                throw new Exception("\n\nPlease enter a valid File name with extension. e.g. Appcenter.apk\n");
            }
            if (!DownloadLocation.EndsWith("\\"))
            {
                DownloadLocation = DownloadLocation + "\\";
            }
            AppName = AppName.Trim();
            ReleaseID = ReleaseID.Trim();
            DownloadLocation = DownloadLocation.Trim();
            FileNameWithExtension = FileNameWithExtension.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            dynamic obj = JsonConvert.DeserializeObject(restResponse.Content);
            Uri DownloadURL = obj.download_url;
            WebClient webClient = new WebClient();
            FileInfo file = new FileInfo(FileNameWithExtension);
            string fileName = Path.GetFileNameWithoutExtension(FileNameWithExtension);
            string extension = file.Extension;
            string currentDateTime = GetDateMonth() + GetHourMinuteSecond();
            string shortVersion = obj.short_version;
            string Version = obj.version;
            if (DeleteExistingApps)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(DownloadLocation, "*" + extension);
                    foreach (string filePath in filePaths)
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception While Deleting Existing Apps from" + DownloadLocation + ": " + e);
                }
            }
            else
            {
                FileNameWithExtension = fileName + "_v" + shortVersion + "_build" + Version + "_" + currentDateTime + extension;
            }
            try
            {
                webClient.DownloadFile(DownloadURL, DownloadLocation + FileNameWithExtension);
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType().Name.Equals("DirectoryNotFoundException", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("\n\nPlease make sure the download location is valid and accessible. See the Original Exception below:\n" + e);
                }
                else
                {
                    throw e;
                }
            }
            return DownloadLocation + FileNameWithExtension;
        }


        /// <summary>
        /// Downloads the application from the AppCenter for the given Short Version and Build Number with user defined file name. Returns the full path of the downloaded application.
        /// </summary>
        /// <param name="AppName">Login to Appcenter->Select an app->Observe the URL. e.g. URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ShortVersion">Login to Appcenter->Select an app->See all releases->"Version" column-> Text before the open brace e.g If Version is 1.2(567) and the {ShortVersion} would be 1.2</param>
        /// <param name="BuildNumber">Login to Appcenter->Select an app->See all releases->"Version" column-> Text within the braces () e.g If Version is 1.2(567) and the {BuildNumber} would be 567</param>
        /// <param name="DownloadLocation">Folder path to download the application(e.g. "C:\\Users\\Owner\\Desktop\\")</param>
        /// <param name="FileNameWithExtension">File name and the app extension(e.g. "AppCenter.apk")</param>
        /// <param name="DeleteExistingApps">Condition to Delete all the existing applications(same type what user downloads) from the Download location ; Delete->true ; Don't Delete->false</param>
        public string DownloadApplication(string AppName, string ShortVersion, string BuildNumber, string DownloadLocation, string FileNameWithExtension, bool DeleteExistingApps)
        {
            if (String.IsNullOrEmpty(DownloadLocation) | String.IsNullOrWhiteSpace(DownloadLocation))
            {
                throw new Exception("\n\nPlease enter a valid Path to Download application. e.g. C:\\Users\\Owner\\Desktop\\\n");
            }
            if (String.IsNullOrEmpty(FileNameWithExtension) | String.IsNullOrWhiteSpace(FileNameWithExtension))
            {
                throw new Exception("\n\nPlease enter a valid File name with extension. e.g. Appcenter.apk\n");
            }
            if (!DownloadLocation.EndsWith("\\"))
            {
                DownloadLocation = DownloadLocation + "\\";
            }
            AppName = AppName.Trim();
            int ReleaseID = GetReleaseID(AppName, ShortVersion, BuildNumber);
            DownloadLocation = DownloadLocation.Trim();
            FileNameWithExtension = FileNameWithExtension.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            dynamic obj = JsonConvert.DeserializeObject(restResponse.Content);
            Uri DownloadURL = obj.download_url;
            WebClient webClient = new WebClient();
            FileInfo file = new FileInfo(FileNameWithExtension);
            string fileName = Path.GetFileNameWithoutExtension(FileNameWithExtension);
            string extension = file.Extension;
            string currentDateTime = GetDateMonth() + GetHourMinuteSecond();
            string shortVersion = obj.short_version;
            string Version = obj.version;
            if (DeleteExistingApps)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(DownloadLocation, "*" + extension);
                    foreach (string filePath in filePaths)
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception While Deleting Existing Apps from" + DownloadLocation + ": " + e);
                }
            }
            else
            {
                FileNameWithExtension = fileName + "_v" + shortVersion + "_build" + Version + "_" + currentDateTime + extension;
            }
            try
            {
                webClient.DownloadFile(DownloadURL, DownloadLocation + FileNameWithExtension);
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType().Name.Equals("DirectoryNotFoundException", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("\n\nPlease make sure the download location is valid and accessible. See the Original Exception below:\n" + e);
                }
                else
                {
                    throw e;
                }
            }
            return DownloadLocation + FileNameWithExtension;
        }


        /// <summary>
        /// Downloads the application from the AppCenter for the given build number with app's default file name.This will overwrite the existing file which has same name. Returns the full path of the downloaded application.
        /// </summary>
        /// <param name="AppName">Login to Appcenter->Select an app->Observe the URL. e.g. URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="BuildNumber">Login to Appcenter->Select an app->See all releases->"Version" column-> Text within the braces () e.g If Version is 1.2(567) and the {BuildNumber} would be 567</param>
        /// <param name="DownloadLocation">Folder path to download the application(e.g. "C:\\Users\\Owner\\Desktop\\")</param>
        public string DownloadApplication(string AppName, string BuildNumber, string DownloadLocation)
        {
            if (String.IsNullOrEmpty(DownloadLocation) | String.IsNullOrWhiteSpace(DownloadLocation))
            {
                throw new Exception("\n\nPlease enter a valid Path to Download application. e.g. C:\\Users\\Owner\\Desktop\\\n");
            }
            if (!DownloadLocation.EndsWith("\\"))
            {
                DownloadLocation = DownloadLocation + "\\";
            }
            AppName = AppName.Trim();
            int ReleaseID = GetReleaseID(AppName, BuildNumber);
            DownloadLocation = DownloadLocation.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + ReleaseID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            dynamic obj = JsonConvert.DeserializeObject(restResponse.Content);
            Uri DownloadURL = obj.download_url;
            string fileName = obj.app_display_name;
            string os = obj.app_os;
            string extension;
            string shortVersion = obj.short_version;
            string Version = obj.version;
            if (os.Equals("android", StringComparison.InvariantCultureIgnoreCase))
            {
                extension = ".apk";
            }
            else if (os.Equals("ios", StringComparison.InvariantCultureIgnoreCase))
            {
                extension = ".ipa";
            }
            else
            {
                throw new Exception("\n\nOnly Android and iOS applications are supported by this method. Use Overloaded DownloadApplication method for other formats.\n");
            }
            string FileNameWithExtension = fileName + "_v" + shortVersion + "_build" + Version + extension;
            WebClient webClient = new WebClient();
            FileInfo file = new FileInfo(FileNameWithExtension);
            string currentDateTime = GetDateMonth() + GetHourMinuteSecond();
            try
            {
                webClient.DownloadFile(DownloadURL, DownloadLocation + FileNameWithExtension);
            }
            catch (Exception e)
            {
                if (e.InnerException.GetType().Name.Equals("DirectoryNotFoundException", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("\n\nPlease make sure the download location is valid and accessible. See the Original Exception below:\n" + e);
                }
                else
                {
                    throw e;
                }
            }
            return DownloadLocation + FileNameWithExtension;
        }


        /// <summary>
        /// Returns information about the provisioning profile. Only available for iOS.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ReleaseID">Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" column</param>
        /// <param name="key">Type IOSProfileKey. -> will display list of options. Select one from the list. e.g. IOSProfileKey.provisioning_bundle_id</param>
        public string GetiOSProvisioningProfileInformation(string AppName, string ReleaseID, IOSProfileKey key)
        {
            ReleaseID = ReleaseID.Trim();
            int Release;
            if (String.IsNullOrEmpty(ReleaseID) | String.IsNullOrWhiteSpace(ReleaseID))
            {
                throw new Exception("\n\nRelease ID should be 'Latest' or a Valid numeric value.\n");
            }
            if (ReleaseID.Equals("latest", StringComparison.InvariantCultureIgnoreCase))
            {
                Release = GetLatestReleaseID(AppName);
                Console.WriteLine(Release);
            }
            else
            {
                Release = int.Parse(ReleaseID);
            }
            AppName = AppName.Trim();
            string updatedKey = key.ToString();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases/" + Release + "/provisioning_profile";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            if (restResponse.IsSuccessful)
            {
                if (updatedKey.Equals("all"))
                {
                    return restResponse.Content;
                }
                else
                {
                    return token[updatedKey].ToString();
                }
            }
            else
            {
                throw new Exception("\n\nPlease verify the AppName(iOS) and ReleaseID are correct\n" + restResponse.Content);
            }
        }

        /// <summary>
        /// Returns the specific(UDID) device detail registered with the account(APIToken).
        /// </summary>
        /// <param name="Device_UDID">Login to Appcenter->Account Settings->My Devices->Select Device->More->Copy UDID</param>
        /// <param name="key">Type DeviceKey. -> will display list of options. Select one from the list. e.g. DeviceKey.imei</param>
        public string GetDeviceDetails(string Device_UDID, DeviceKey key)
        {
            if (String.IsNullOrEmpty(Device_UDID) | String.IsNullOrWhiteSpace(Device_UDID))
            {
                throw new Exception("\n\nDevice UDID can't be null or empty or whitespace.\n");
            }

            Device_UDID = Device_UDID.Trim();
            string updatedKey = key.ToString();
            string URL = "https://api.appcenter.ms/v0.1/user/devices/" + Device_UDID;
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            JToken token = JToken.Parse(restResponse.Content);
            if (restResponse.IsSuccessful)
            {
                if (updatedKey.Equals("all"))
                {
                    return restResponse.Content;
                }
                else
                {
                    return token[updatedKey].ToString();
                }
            }
            else
            {
                throw new Exception(restResponse.Content);
            }
        }

        /// <summary>
        /// Returns all devices associated with the given user(APIToken).
        /// </summary>
        public string GetAllDeviceDetails()
        {
            string URL = "https://api.appcenter.ms/v0.1/user/devices";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            if (restResponse.IsSuccessful)
            {
                return restResponse.Content;
            }
            else
            {
                throw new Exception(restResponse.Content);
            }
        }

        /// <summary>
        /// Returns basic information about releases.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        public string GetAllReleasesInformation(string AppName)
        {
            AppName = AppName.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/releases";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            return restResponse.Content;
        }

        /// <summary>
        /// Returns the latest release from every distribution group associated with an application.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        public string GetRecentReleasesInformation(string AppName)
        {
            AppName = AppName.Trim();
            string URL = "https://api.appcenter.ms/v0.1/apps/" + Owner + "/" + AppName + "/recent_releases";
            IRestResponse restResponse = GetRestResponse(APIToken, URL);
            return restResponse.Content;
        }


        /// <summary>
        /// Returns the latest Release ID of an application.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        public int GetLatestReleaseID(string AppName)
        {
            AppName = AppName.Trim();
            string response = GetRecentReleasesInformation(AppName);
            JToken token = JToken.Parse(response).First;
            return (int)token["id"];
        }

        /// <summary>
        /// Returns the Release ID of an application for the given build number.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="BuildNumber">Login to Appcenter->Select an app->See all releases->"Version" column-> Text within the braces () e.g If Version is 1.2(567) and the {BuildNumber} would be 567</param>
        public int GetReleaseID(string AppName, string BuildNumber)
        {
            AppName = AppName.Trim();
            BuildNumber = BuildNumber.Trim();
            string response = GetAllReleasesInformation(AppName);
            JArray textArray = JArray.Parse(response);
            int count = textArray.Count;
            string internalVersion = null;
            int ReleaseID = -1;
            int NoOfReleasesWithSameVersion = 0;
            string MultipleReleases = null;
            for (int i = count - 1; i > 0; i--)
            {
                internalVersion = (string)textArray[i]["version"];
                if (internalVersion.Equals(BuildNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    ReleaseID = (int)textArray[i]["id"];
                    NoOfReleasesWithSameVersion++;
                    MultipleReleases = MultipleReleases + " , " + ReleaseID.ToString();
                }
            }
            if (ReleaseID == -1)
            {
                throw new Exception("\n\nNo release found with the Build Number : " + BuildNumber);
            }
            else
            {
                if (NoOfReleasesWithSameVersion != 1)
                {
                    MultipleReleases = MultipleReleases.TrimStart(' ', ',');
                    Console.WriteLine("FYI: There are " + NoOfReleasesWithSameVersion + " releases ( " + MultipleReleases + " ) found with the Build Number " + BuildNumber + ". Returning the latest release " + ReleaseID + ".");
                }
                return ReleaseID;
            }
        }


        /// <summary>
        /// Returns the Release ID of an application for the given Short version and build number.
        /// </summary>
        /// <param name="AppName">URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {app_name} would be APIExample</param>
        /// <param name="ShortVersion">Login to Appcenter->Select an app->See all releases->"Version" column-> Text before the open brace e.g If Version is 1.2(567) and the {ShortVersion} would be 1.2</param>
        /// <param name="BuildNumber">Login to Appcenter->Select an app->See all releases->"Version" column-> Text within the braces () e.g If Version is 1.2(567) and the {BuildNumber} would be 567</param>
        public int GetReleaseID(string AppName, string ShortVersion, string BuildNumber)
        {
            AppName = AppName.Trim();
            ShortVersion = ShortVersion.Trim();
            BuildNumber = BuildNumber.Trim();
            string response = GetAllReleasesInformation(AppName);
            JArray textArray = JArray.Parse(response);
            int count = textArray.Count;
            string internalShortVersion = null;
            string internalVersion = null;
            int ReleaseID = -1;
            int NoOfReleasesWithSameVersion = 0;
            string MultipleReleases = null;
            for (int i = count - 1; i > 0; i--)
            {
                internalShortVersion = (string)textArray[i]["short_version"];
                internalVersion = (string)textArray[i]["version"];
                if (internalShortVersion.Equals(ShortVersion, StringComparison.InvariantCultureIgnoreCase) & internalVersion.Equals(BuildNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    ReleaseID = (int)textArray[i]["id"];
                    NoOfReleasesWithSameVersion++;
                    MultipleReleases = MultipleReleases + " , " + ReleaseID.ToString();
                }
            }
            if (ReleaseID == -1)
            {
                throw new Exception("\n\nNo release found with the ShortVersion: " + ShortVersion + ", Build Number: " + BuildNumber);
            }
            else
            {
                if (NoOfReleasesWithSameVersion != 1)
                {
                    MultipleReleases = MultipleReleases.TrimStart(' ', ',');
                    Console.WriteLine("FYI: There are " + NoOfReleasesWithSameVersion + " releases ( " + MultipleReleases + " ) found with the ShortVersion " + ShortVersion + ", Build Number " + BuildNumber + ". Returning the latest release " + ReleaseID + ".");
                }
                return ReleaseID;
            }
        }


        //******************************** Private methods ********************************

        static readonly string[] suffixes =
        { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        private string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }

        private IRestResponse GetRestResponse(string AppCenterAPIToken, string URL)
        {
            string RequestBody = null;
            var client = new RestClient(URL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-API-Token", AppCenterAPIToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", RequestBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (!response.IsSuccessful)
            {
                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode.ToString() == "401")
                {
                    throw new Exception("\n\nPlease make sure AppCenter API Token is valid." + "\n\n" + response.Content + "\n");
                }
                else if (numericStatusCode.ToString() == "404")
                {
                    throw new Exception("\n\nPlease make sure Owner Name / App Name / Release ID / UDID are valid." + "\n\n" + response.Content + "\n");
                }
                else if (numericStatusCode.ToString() == "400")
                {
                    throw new Exception("\n\nPlease make sure your input is valid. Check message for more information." + "\n\n" + response.Content + "\n");
                }
                else
                {
                    //Any other Exceptions, Just throw.
                    throw new Exception(response.Content);
                }
            }
            return response;
        }

        private string GetHourMinuteSecond()
        {
            var date = DateTime.Now;
            return date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString();
        }

        private string GetDateMonth()
        {
            DateTime todaysDate = DateTime.Now.Date;
            int day = todaysDate.Day;

            int month = todaysDate.Month;
            return day.ToString() + month.ToString();
        }
    }

    /// <summary>
    /// Options available to Get iOS Provisioning Profile Information
    /// </summary>
    public enum IOSProfileKey
    {
        /// <summary>
        /// Returns all Provisioning profile information about the application
        /// </summary>
        all = 0,
        /// <summary>
        /// Returns only Provisioning profile Type
        /// </summary>
        provisioning_profile_type = 1,
        /// <summary>
        /// Returns only Provisioning profile Name
        /// </summary>
        provisioning_profile_name = 2,
        /// <summary>
        /// Returns only Provisioning profile Bundle ID
        /// </summary>
        provisioning_bundle_id = 3,
        /// <summary>
        /// Returns only Provisioning profile Team Identifier
        /// </summary>
        team_identifier = 4
    }

    /// <summary>
    /// Options available to Get Device Details
    /// </summary>
    public enum DeviceKey
    {
        /// <summary>
        /// Returns all information about the Device
        /// </summary>
        all = 0,
        /// <summary>
        /// Returns only Owner ID of the Device
        /// </summary>
        owner_id = 1,
        /// <summary>
        /// Returns only UDID of the Device
        /// </summary>
        udid = 2,
        /// <summary>
        /// Returns only IMEI of the Device
        /// </summary>
        imei = 3,
        /// <summary>
        /// Returns only OS BUILD of the Device
        /// </summary>
        os_build = 4,
        /// <summary>
        /// Returns only OS Version of the Device
        /// </summary>
        os_version = 5,
        /// <summary>
        /// Returns only Model of the Device
        /// </summary>
        model = 6,
        /// <summary>
        /// Returns only Serial Number of the Device
        /// </summary>
        serial = 7,
        /// <summary>
        /// Returns only Name of the Device
        /// </summary>
        device_name = 8,
        /// <summary>
        /// Returns only Registered At DateTime of the Device
        /// </summary>
        registered_at = 9
    }
}
