using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TravelApp.UI.Views
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	 public partial class MainView : MvxContentPage<MvxMainViewModel>
	 {
		  public MainView()
		  {
			   InitializeComponent();

			   iconImage.Source = ImageSource.FromResource("TravelApp.UI.Assets.Images.plane.png", typeof(MainView));
		  }
	 }
}