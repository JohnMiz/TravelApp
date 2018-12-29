using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Core.Services
{
	 public class AzureDatabaseService : IDatabaseService
	 {
		  public async Task<bool> DeleteAsync<T>(T item) where T : new()
		  {
			   await App.MobileService.GetTable<T>().DeleteAsync(item);
			   return true;
		  }

		  public async Task<List<T>> GetTable<T>() where T : new()
		  {
			   return await App.MobileService.GetTable<T>().ToListAsync();
		  }

		  public async Task<bool> InsertAsync<T>(T item) where T : new()
		  {
			   await App.MobileService.GetTable<T>().InsertAsync(item);
			   return true;
		  }

		  public async Task<bool> UpdateAsync<T>(T item) where T : new()
		  {
			   await App.MobileService.GetTable<T>().UpdateAsync(item);
			   return true;

		  }
	 }
}
