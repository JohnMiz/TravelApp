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
	 public partial class RegisterView : MvxContentPage<RegisterViewModel>
	 {
		  public RegisterView()
		  {
			   InitializeComponent();
		  }
	 }
}