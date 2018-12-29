using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Core.Services
{
	 public interface IDatabaseService
	 {
		  Task<bool> InsertAsync<T>(T item) where T : new();

		  Task<bool> DeleteAsync<T>(T item) where T : new();

		  Task<bool> UpdateAsync<T>(T item) where T : new();

		  Task<List<T>> GetTable<T>() where T : new();
	 }
}
