# AppCenterDownloadAppGetAppInfo <img src="https://user-images.githubusercontent.com/50325649/87393929-8d216e00-c5cc-11ea-9493-d144245bb6a8.png" width="25" height="25" title="AppCenterDownload" alt="AppCenterDownload">

Library to Download application and Get application information from Microsoft AppCenter.

### Prerequisites

```
A .NetStandard v2.0/ .NETCore v2.0 / .NetFramework v4.6.1 project

Microsoft AppCenter Access
```

### Installing

```
Right Click on your project in the visual studio solution explorer -> Manage nuget packages -> Search for AppCenterDownloadAppGetAppInfo -> Select AppCenterDownloadAppGetAppInfo By Meganathan from the list -> Select project -> Install.

Gathering dependency information may take a minute or more.
```

## How to Use? It's simple! 

Import "AppCenterDownloadAppGetAppInfo". 
```
using AppCenterDownloadAppGetAppInfo;
```
Create a new instance of AppCenter with the API Token and Owner Name.
```
AppCenter appCenter = new AppCenter("APIToken", "OwnerName");

APIToken = Login to Appcenter->Account Settings->Use API Tokens->New API Token->Copy.
OwnerName = URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {owner_name} would be Microsoft.

e.g. AppCenter appCenter = new AppCenter("xxxxxxxxxx", "Microsoft");
```
Use the created instance to call the available methods.e.g.
```
appCenter.DownloadApplication(string AppName, string ReleaseID, string DownloadLocation, bool DeleteExistingApps);

AppName = URL might be https://appcenter.ms/orgs/Microsoft/apps/APIExample and the {AppName} would be APIExample.
ReleaseID = Enter as "Latest" to get the latest version OR Login to Appcenter->Select an app->See all releases->"Release" value under Release history.

e.g. appCenter.DownloadApplication("APIExample", "latest", "c:\\users\\mega\\desktop\\", false);
This will download the application in to the specified location and will return the file path as string.
```
## Available Methods
```
DownloadApplication() (+ 1 overload)
GetAllDeviceDetails()
GetAllReleasesInformation()
GetApplicationBuildVersion()
GetApplicationBundleID()
GetApplicationFullVersion()
GetApplicationName()
GetApplicationShortVersion()
GetApplicationSize()
GetApplicationSupportedOS()
GetDeviceDetails()
GetiOSProvisioningProfileInformation()
GetLatestReleaseID()
GetMinimumOSVersionSupportedByApplication()
GetRecentReleasesInformation()
```
More methods will be added in the future releases.

## Built With

* [AppCenter APIs](https://openapi.appcenter.ms) - The Official Raw Rest APIs
* [Restsharp](https://www.nuget.org/packages/RestSharp) - API Management
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) - Json Management

## Authors

* [**Meganathan C**](https://mega6453.carrd.co)

## License

This project is licensed under the [MIT License](https://licenses.nuget.org/MIT)
