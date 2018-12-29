using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Core.Models;
using TravelApp.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TravelApp.Core.ViewModels
{
	 public class MapViewModel : MvxViewModel
	 {
		  private readonly IPageService _PageService;
		  private readonly IDatabaseService _DatabaseService;
		  private bool hasLocationPermission;

		  private bool _IsShowingUserLocation;

		  public bool IsShowingUserLocation
		  {
			   get { return _IsShowingUserLocation; }
			   set { SetProperty(ref _IsShowingUserLocation, value); }
		  }

		  public static Map Map { get; set; }

		  public ICommand OnAppearingCommand { get; private set; }
		  public IMvxAsyncCommand OnDisappearingCommand { get; private set; }

		  public MapViewModel(IPageService pageService, IDatabaseService databaseService)
		  {
			   _PageService = pageService;
			   _DatabaseService = databaseService;

			   hasLocationPermission = false;

			   Task.Run(async () => { await GetPermissions(); }).Wait();

			   OnAppearingCommand = new Command(async () =>
			   {

					if (hasLocationPermission)
					{
						 var locator = CrossGeolocator.Current;
						 locator.PositionChanged += Locator_PositionChanged;
						 await locator.StartListeningAsync(TimeSpan.Zero, 100);

						 await GetLocation();
					}

					await LoadPinsFromPosts();
			   });

			   OnDisappearingCommand = new MvxAsyncCommand(async () =>
			   {
					await CrossGeolocator.Current.StopListeningAsync();
					CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
			   });
		  }

		  private void Locator_PositionChanged(object sender, PositionEventArgs e)
		  {
			   MoveMap(e.Position);
		  }

		  private async Task LoadPinsFromPosts()
		  {
			   var posts = (await _DatabaseService.GetTable<Post>()).Where(p => p.UserId == App.user.Id);

			   foreach (var post in posts)
			   {
					Map.Pins.Add(new Xamarin.Forms.Maps.Pin()
					{
						 Address = post.Address,
						 Position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude),
						 Type = PinType.SearchResult,
						 Label = post.VenueName

					});
			   }
		  }


		  private async Task GetPermissions()
		  {
			   var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

			   if (status != PermissionStatus.Granted)
			   {
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
					{
						 await _PageService.DisplayAlert("Need your location", "We need to access your location", "Ok");
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
					if (results.ContainsKey(Permission.LocationWhenInUse))
					{
						 status = results[Permission.LocationWhenInUse];
					}

					if (status == PermissionStatus.Granted)
					{
						 hasLocationPermission = true;
						 IsShowingUserLocation = true;

						 await GetLocation();
					}
					else
					{
						 await _PageService.DisplayAlert("Location denied", "You didn't give us permission to access location, so we can't show your location", "Ok");
					}
			   }
			   else if (status == PermissionStatus.Granted)
			   {
					hasLocationPermission = true;
			   }
		  }

		  private async Task GetLocation()
		  {

			   var locator = CrossGeolocator.Current;
			   var position = await locator.GetPositionAsync(new TimeSpan(0, 0, 0, 0, 1000));

			   MoveMap(position);

		  }

		  private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
		  {
			   Map.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), 1, 1));
		  }
	 }
}
