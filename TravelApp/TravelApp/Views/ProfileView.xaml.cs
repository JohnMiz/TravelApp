using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TravelApp.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace TravelApp.UI.Views
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	 [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Profile", Icon = "user.png")]
	 public partial class ProfileView : MvxContentPage<ProfileViewModel>
	 {
		  public ProfileView()
		  {
			   InitializeComponent();
		  }
	 }
}