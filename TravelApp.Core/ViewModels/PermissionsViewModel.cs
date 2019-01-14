using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Core.Services;
using Xamarin.Forms;

namespace TravelApp.Core.ViewModels
{
	 public class PermissionsViewModel : MvxViewModel
	 {
		  private string _AllowPermissionButtonText;
		  private readonly IPageService _PageService;
		  private readonly IMvxNavigationService _NavigationService;

		  public ICommand AllowPermissionCommand { get; private set; }
		  
		  public string AllowPermissionButtonText
		  {
			   get { return _AllowPermissionButtonText; }
			   set { SetProperty(ref _AllowPermissionButtonText, value); }
		  }

		  private string _RadioBarLocationImagePath;

		  public string RadioBarLocationImagePath
		  {
			   get { return _RadioBarLocationImagePath; }
			   set { SetProperty(ref _RadioBarLocationImagePath, value); }
		  }


		  public PermissionsViewModel(IPageService pageService, IMvxNavigationService navigationService)
		  {
			   _PageService = pageService;
			   _NavigationService = navigationService;

			   AllowPermissionButtonText = "Allow Permission";
			   RadioBarLocationImagePath = "button_unchecked.png";

			   AllowPermissionCommand = new Command(async () =>
			   {
					if (_AllowPermissionButtonText == "Next")
					{
						 await _NavigationService.Navigate<HomeViewModel>();
						 return;
					}

					if (await GetPermissions())
					{
						 AllowPermissionButtonText = "Next";
						 RadioBarLocationImagePath = "button_checked.png";
					}
					else
					{
						 RadioBarLocationImagePath = "button_unchecked.png";
					}
			   });
		  }


		  private async Task<bool> GetPermissions()
		  {
			   bool hasLocationPermission = false;

			   var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

			   if (status != PermissionStatus.Granted)
			   {
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
					{
						 await _PageService.DisplayAlert("Need your location", "We need to access your location", "Ok");
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
					if (results.ContainsKey(Permission.LocationWhenInUse))
					{
						 status = results[Permission.LocationWhenInUse];
					}

					if (status == PermissionStatus.Granted)
					{
						 hasLocationPermission = true;
					}
					else
					{
						 await _PageService.DisplayAlert("Location denied", "You didn't give us permission to access location, so we can't show your location", "Ok");
						 hasLocationPermission = false;
					}
			   }
			   else if (status == PermissionStatus.Granted)
			   {
					hasLocationPermission = true;
			   }

			   return hasLocationPermission;
		  }

	 }
}
