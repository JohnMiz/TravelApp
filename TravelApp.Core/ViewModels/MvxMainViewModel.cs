using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Core.Helpers;
using Xamarin.Forms;

namespace TravelApp.Core.ViewModels
{
	 public class MvxMainViewModel : MvxViewModel
	 {
		  private readonly IMvxNavigationService _NavigationService;
		  
		  public ICommand ContinueWithEmailCommand { get; private set; }

		  public MvxMainViewModel(IMvxNavigationService navigationService)
		  {
			   _NavigationService = navigationService;

			   Task.Run(async () => { await HandleUserLoggedIn(); });

			   ContinueWithEmailCommand = new Command(async () =>
			   {
					await _NavigationService.Navigate<LoginViewModel>();
			   });
		  }

		  private async Task HandleUserLoggedIn()
		  {
			   if (Settings.IsLoggedIn)
			   {
					var locationPermissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

					if (locationPermissionStatus != PermissionStatus.Granted)
					{
						 await _NavigationService.Navigate<PermissionsViewModel>();
					}
					else
					{
						 await _NavigationService.Navigate<HomeViewModel>();
					}
			   }
		  }
	 }
}
