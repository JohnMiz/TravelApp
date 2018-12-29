using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TravelApp.Core.ViewModels
{
	 public class HomeViewModel : MvxNavigationViewModel
	 {
		  private readonly IMvxNavigationService _NavigationService;

		  public ICommand AddCommand { get; private set; }
		  
		  public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

		  public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
		  {
			   _NavigationService = navigationService;

			   AddCommand = new Command(() =>
			   {
					_NavigationService.Navigate<NewTravelViewModel>();
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
