using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace TravelApp.UI.Views
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	 [MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true)]
	 public partial class HomeView : MvxTabbedPage<HomeViewModel>
	 {
		  public HomeView()
		  {
			   InitializeComponent();

			   On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
		  }

		  private bool firstTime = true;

		  protected override async void OnAppearing()
		  {
			   base.OnAppearing();
			   if(firstTime)
			   {
					await ViewModel.ShowInitialViewModelsCommand.ExecuteAsync();
					firstTime = false;
			   }
		  }

		  protected override bool OnBackButtonPressed()
		  {
			   return true;
		  }

		  protected override void OnChildAdded(Element child)
		  {
		  }
	 }
}