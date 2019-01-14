using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Core.Helpers;
using Xamarin.Forms;

namespace TravelApp.Core.ViewModels
{
	 public class HomeViewModel : MvxNavigationViewModel
	 {
		  private readonly IMvxNavigationService _NavigationService;

		  public ICommand AddCommand { get; private set; }
		  public ICommand LogoutCommand { get; private set; }
		  
		  public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

		  public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
		  {
			   _NavigationService = navigationService;

			   AddCommand = new Command(() =>
			   {
					_NavigationService.Navigate<NewTravelViewModel>();
			   });

			   LogoutCommand = new Command(async ()=> {

					App.user = null;
					Settings.UserId = string.Empty;
					Settings.IsLoggedIn = false;

					await _NavigationService.Navigate<MvxMainViewModel>();

			   });

			   ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
		  }

		  private async Task ShowInitialViewModels()
		  {
			   await _NavigationService.Navigate<HistoryViewModel>();
			   await _NavigationService.Navigate<MapViewModel>();
			   await _NavigationService.Navigate<ProfileViewModel>();
		  }
	 }
}
