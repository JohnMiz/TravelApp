using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelApp.Core.Services
{
	 public class PageService : IPageService
	 {
		  public async Task PushAsync(Page page)
		  {
			   await Application.Current.MainPage.Navigation.PushAsync(page);
		  }

		  public async Task PushModalAsync(Page page)
		  {
			   await Application.Current.MainPage.Navigation.PushModalAsync(page);
		  }

		  public async Task PopAsync()
		  {
			   await Application.Current.MainPage.Navigation.PopAsync();
		  }

		  public async Task PopModalAsync()
		  {
			   await Application.Current.MainPage.Navigation.PopModalAsync();
		  }

		  public async Task DisplayAlert(string title, string message, string button)
		  {
			   await Application.Current.MainPage.DisplayAlert(title, message, button);
		  }

	 }
}
