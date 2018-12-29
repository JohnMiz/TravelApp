using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views;
using TravelApp.Core;
using TravelApp.UI;

namespace TravelApp.Droid
{
	 [Activity(Label = "@string/app_name",
		  MainLauncher = true,
		  NoHistory = true,
		  Theme = "@style/Theme.Splash")]
	 public class SplashScreenActivity : MvxFormsSplashScreenActivity<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>
	 {
		  public SplashScreenActivity() : base(Resource.Layout.SplashScreen)
		  {
		  }

		  protected override Task RunAppStartAsync(Bundle bundle)
		  {
			   StartActivity(typeof(MainActivity));
			   return Task.CompletedTask;
		  }
	 }
}