using MvvmCross.ViewModels;
using System.Windows.Input;
using TravelApp.Core.Models;
using TravelApp.Core.Services;
using Xamarin.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text;
using TravelApp.Core.Helpers;

namespace TravelApp.Core.ViewModels
{
	 public class RegisterViewModel : MvxViewModel
	 {

		  private string _Email;
		  private string _Password;
		  private string _ConfirmPassword;
		  private bool _IsRegisterLoaderRunning;

		  private readonly IPageService _PageService;
		  private readonly IDatabaseService _DatabaseService;

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

		  public string ConfirmPassword
		  {
			   get { return _ConfirmPassword; }
			   set { SetProperty(ref _ConfirmPassword, value); }
		  }

		  public bool IsRegisterLoaderRunning
		  {
			   get { return _IsRegisterLoaderRunning; }
			   set { SetProperty(ref _IsRegisterLoaderRunning, value); }
		  }


		  public ICommand RegisterCommand { get; private set; }
		  public ICommand LoginCommand { get; private set; }

		  public RegisterViewModel(IPageService pageService, IDatabaseService databaseService)
		  {
			   _PageService = pageService;
			   _DatabaseService = databaseService;

			   RegisterCommand = new Command(async () =>
			   {
					IsRegisterLoaderRunning = true;

					if (Password == ConfirmPassword)
					{

						 string encryptPassword; 
						 using (MD5 md5Hash = MD5.Create())
						 {
							  encryptPassword = MD5Helpers.GetMd5Hash(md5Hash, Password);
						 }

						 User user = new User
						 {
							  Email = Email,
							  Password = encryptPassword
						 };

						 await _DatabaseService.InsertAsync(user);
						 IsRegisterLoaderRunning = false;

						 await _PageService.PopAsync();
					}
					else
					{
						 IsRegisterLoaderRunning = false;
						 await _PageService.DisplayAlert("Error", "Passwords don't match", "Ok");
					}
			   });

			   LoginCommand = new Command(async () =>
			   {
					await _PageService.PopAsync();
			   });



		  }
	 }
}
