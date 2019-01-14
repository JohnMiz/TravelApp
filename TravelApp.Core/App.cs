using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelApp.Core.Models;
using TravelApp.Core.Services;
using TravelApp.Core.ViewModels;

namespace TravelApp.Core
{
	 public class App : MvxApplication
	 {
		  public static string DatabaseLocation = string.Empty;

		  public static MobileServiceClient MobileService = new MobileServiceClient("https://travelrecordsapp.azurewebsites.net");

		  public static User user = new User();

		  public static IMobileServiceSyncTable<Post> postsTable;

		  public override void Initialize()
		  {
			   Mvx.IoCProvider.RegisterType<IDatabaseService, AzureDatabaseService>();
			   Mvx.IoCProvider.RegisterType<IPageService, PageService>();

			   string dbName = "travel_db.sqlite";
			   string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "library");
			   string fullPath = Path.Combine(folderPath, dbName);

			   DatabaseLocation = fullPath;
			   
			   RegisterAppStart<MvxMainViewModel>();
		  }
	 }
}
