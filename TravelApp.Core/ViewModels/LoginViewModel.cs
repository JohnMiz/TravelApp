using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using TravelApp.Core.Services;
using MvvmCross.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using TravelApp.Core.Models;
using System.Linq;
using TravelApp.Core.Helpers;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace TravelApp.Core.ViewModels
{
	 public class LoginViewModel : MvxViewModel
	 {
		  private string _Email;
		  private string _Password;
		  private bool _IsLoginLoaderRunning;

		  private readonly IPageService _PageService;
		  private readonly IDatabaseService _DatabaseService;
		  private readonly IMvxNavigationService _NavigationService;

		  public string Email
		  {
			   get { return _Email; }
			   set { SetProperty(ref _Email, value); }
		  }

		  public string Password
		  {
			   get { return _Password; }
			   set { SetProperty(ref _Password, value); }
		  }

		  public bool IsLoginLoaderRunning
		  {
			   get { return _IsLoginLoaderRunning; }
			   set { SetProperty(ref _IsLoginLoaderRunning, value); }
		  }


		  public ICommand RegisterCommand { get; private set; }
		  public ICommand LoginCommand { get; private set; }
		  public ICommand GoogleSignInCommand { get; private set; }

		  public LoginViewModel(IPageService pageService, IDatabaseService databaseService,
			   IMvxNavigationService navigationService)
		  {
			   _PageService = pageService;
			   _DatabaseService = databaseService;
			   _NavigationService = navigationService;

			   LoginCommand = new Command(async () =>
			   {

					bool isEmailEmpty = string.IsNullOrEmpty(Email);
					bool isPasswordEmpty = string.IsNullOrEmpty(Password);

					IsLoginLoaderRunning = true;

					if (isEmailEmpty || isPasswordEmpty)
					{
						 IsLoginLoaderRunning = false;
						 await _PageService.DisplayAlert("Error", "Email or password are incorrect", "Ok");
					}
					else
					{
						 var user = (await _DatabaseService.GetTable<User>()).Where(u => u.Email == Email).FirstOrDefault();

						 if (user != null)
						 {


							  bool IsPasswordMatch = false;

							  using (MD5 md5Hash = MD5.Create())
							  {
								   IsPasswordMatch = MD5Helpers.VerifyMd5Hash(md5Hash, Password, user.Password);
							  }

							  if (IsPasswordMatch)
							  {
								   IsLoginLoaderRunning = false;

								   App.user = user;
								   Settings.IsLoggedIn = true;
								   Settings.UserId = user.Id;

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
							  else
							  {
								   IsLoginLoaderRunning = false;
								   await _PageService.DisplayAlert("Error", "Email or password are incorrect", "Ok");

							  }
						 }
						 else
						 {
							  IsLoginLoaderRunning = false;
							  await _PageService.DisplayAlert("Error", "There was an error logging you in", "Ok");
						 }
					}
			   });


			   RegisterCommand = new Command(async () =>
			   {
					await _NavigationService.Navigate<RegisterViewModel>();
			   });
		  }
	 }
}
