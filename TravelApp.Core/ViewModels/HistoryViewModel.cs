using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelApp.Core.Models;
using TravelApp.Core.Services;
using Xamarin.Forms;

namespace TravelApp.Core.ViewModels
{
	 public class HistoryViewModel : MvxViewModel
	 {
		  private ObservableCollection<Post> _Posts;
		  private bool _IsRefreshing;

		  private readonly IDatabaseService _DatabaseService;

		  public ICommand DeleteItemCommand { get; private set; }
		  public ICommand RefreshCommand { get; private set; }

		  public ObservableCollection<Post> Posts
		  {
			   get { return _Posts; }
			   set { SetProperty(ref _Posts, value); }
		  }

		  public bool IsRefreshing
		  {
			   get { return _IsRefreshing; }
			   set { SetProperty(ref _IsRefreshing, value); }
		  }


		  public ICommand LoadPostsCommand { get; private set; }

		  public HistoryViewModel(IDatabaseService databaseService)
		  {
			   _DatabaseService = databaseService;

			   Posts = new ObservableCollection<Post>();

			   LoadPostsCommand = new Command(async () => {

					await UpdatePosts();

			   });

			   DeleteItemCommand = new Command<Post>(async (Post p) => {

					await _DatabaseService.DeleteAsync(p);

					await UpdatePosts();

			   });

			   RefreshCommand = new Command(async () => {
					await UpdatePosts();

					IsRefreshing = false;

			   });

		  }

		  async Task UpdatePosts()
		  {
			   Posts = new ObservableCollection<Post>((await _DatabaseService.GetTable<Post>()).Where(p => p.UserId == App.user.Id));
		  }
	 }
}
