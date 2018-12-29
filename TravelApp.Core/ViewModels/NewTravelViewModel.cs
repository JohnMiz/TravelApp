using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TravelApp.Core.Models;
using TravelApp.Core.Services;
using Xamarin.Forms;

namespace TravelApp.Core.ViewModels
{
	 public class NewTravelViewModel : MvxViewModel
	 {
		  private Venue _SelectedVenue;
		  private ObservableCollection<Venue> _Venues;
		  private string _Experience;

		  private readonly IPageService _PageService;
		  private readonly IMvxNavigationService _NavigationService;

		  public ObservableCollection<Venue> Venues
		  {
			   get { return _Venues; }
			   set { SetProperty(ref _Venues, value); }
		  }

		  public Venue SelectedVenue
		  {
			   get { return _SelectedVenue; }
			   set { SetProperty(ref _SelectedVenue, value); }
		  }

		  public string Experience
		  {
			   get { return _Experience; }
			   set { SetProperty(ref _Experience, value); }
		  }


		  public ICommand SaveCommand { get; private set; }
		  public ICommand LoadVenues { get; private set; }

		  public NewTravelViewModel(IPageService pageService, IMvxNavigationService navigationService)
		  {
			   _PageService = pageService;
			   _NavigationService = navigationService;

			   Venues = new ObservableCollection<Venue>();
			   SelectedVenue = new Venue();

			   SaveCommand = new Command(async () => {

					try
					{
						 var firstCatagory = SelectedVenue.categories.FirstOrDefault();

						 Post post = new Post()
						 {
							  UserId = App.user.Id,
							  Experience = Experience,
							  CategoryId = firstCatagory.id,
							  CategoryName = firstCatagory.name,
							  Address = SelectedVenue.location.address,
							  Distance = SelectedVenue.location.distance,
							  Latitude = SelectedVenue.location.lat,
							  Longitude = SelectedVenue.location.lng,
							  VenueName = SelectedVenue.name
						 };

						 await App.MobileService.GetTable<Post>().InsertAsync(post);
						 await _PageService.DisplayAlert("Success", "Experience successfully inserted.", "Ok");
						 await _NavigationService.Navigate<HomeViewModel>();
					}
					catch (NullReferenceException)
					{
						 await _PageService.DisplayAlert("Faliure", "Experience failed to be inserted.", "Ok");
					}
					catch (Exception)
					{
						 await _PageService.DisplayAlert("Faliure", "Experience failed to be  inserted.", "Ok");
					}

			   });

			   LoadVenues = new Command(async () =>
			   {
					try
					{
						 var locator = CrossGeolocator.Current;
						 Position position = await locator.GetPositionAsync();

						 Venues = new ObservableCollection<Venue>(await Venue.GetVenues(position.Latitude, position.Longitude));
					}
					catch (Exception e)
					{
						 await _PageService.DisplayAlert("Permission", e.Message);
						 await _NavigationService.Navigate<HomeViewModel>();
					}

			   });
		  }

	 }
}
