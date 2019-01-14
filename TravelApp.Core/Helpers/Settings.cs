using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Core.Helpers
{
	 public static class Settings
	 {
		  private static ISettings AppSettings
		  {
			   get
			   {
					return CrossSettings.Current;
			   }
		  }

		  #region Setting Constants

		  private const string SettingsKey = "settings_key";
		  private static readonly string SettingsDefault = string.Empty;
		  private static readonly int DefaultUserId = -1;

		  #endregion


		  public static string GeneralSettings
		  {
			   get
			   {
					return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			   }
			   set
			   {
					AppSettings.AddOrUpdateValue(SettingsKey, value);
			   }
		  }


		  public static bool IsLoggedIn
		  {
			   get => AppSettings.GetValueOrDefault(nameof(IsLoggedIn), false);
			   set => AppSettings.AddOrUpdateValue(nameof(IsLoggedIn), value);
		  }

		  public static string UserId
		  {
			   get => AppSettings.GetValueOrDefault(nameof(UserId), string.Empty);
			   set => AppSettings.AddOrUpdateValue(nameof(UserId), value);
		  }

	 }
}
