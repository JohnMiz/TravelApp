using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Helpers;
using TravelApp.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.UI.Views
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	 [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "History", Icon = "home.png")]
	 public partial class HistoryView : MvxContentPage<HistoryViewModel>
	 {
		  public HistoryView()
		  {
			   InitializeComponent();
		  }

		  protected override void OnAppearing()
		  {
			   base.OnAppearing();

			   ViewModel.LoadPostsCommand.Execute(null);
		  }
	 }
}