using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelApp.UI.Views
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	 [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Map", Icon = "map.png")]
	 public partial class MapView : MvxContentPage<MapViewModel>
	 {
		  public MapView()
		  {
			   InitializeComponent();

			   MapViewModel.Map = locationsMap;
		  }

		  protected override void OnAppearing()
		  {
			   base.OnAppearing();

			   ViewModel.OnAppearingCommand.Execute(null);
		  }

		  protected override async void OnDisappearing()
		  {
			   base.OnDisappearing();

			   await ViewModel.OnDisappearingCommand.ExecuteAsync();
		  }
	 }
}