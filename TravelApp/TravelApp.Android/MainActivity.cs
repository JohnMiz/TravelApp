using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Platforms.Android.Core;
using TravelApp.Core;
using TravelApp.UI;
using Plugin.Permissions;
using Microsoft.WindowsAzure.MobileServices;
using System.IO;

namespace TravelApp.Droid
{
	 [Activity(Label = "TravelApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainThemePurple", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	 public class MainActivity : MvxFormsAppCompatActivity
	 {
		  protected override void OnCreate(Bundle savedInstanceState)
		  {
			   TabLayoutResource = Resource.Layout.Tabbar;
			   ToolbarResource = Resource.Layout.Toolbar;

			   base.OnCreate(savedInstanceState);

			   Xamarin.FormsMaps.Init(this, savedInstanceState);
			   CurrentPlatform.Init();
			  
			   Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

			   string dbName = "travel_db.sqlite";
			   string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			   string fullPath = Path.Combine(folderPath, dbName);
			   
		  }

		  public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		  {
			   base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			   PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		  }
	 }
}