/*
 Created by : Moni
 Created Date : 17/02/2017
 Purpose : To Get data for displaying suggestions when user types the search string.
 */
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public class Suggestions : ISuggestions
	{
		public Suggestions()
		{
			
		}
		public async Task<SearchSuggestions> GetData(string searchValue, string searchCategory)
		{
			try
			{
				SearchSuggestions searchData = new SearchSuggestions();
				ServiceHelper objHelper = new ServiceHelper();
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;

				var url = searchInput.SearchForSuggestionUrlPathAndParameters;
				string data = await objHelper.GetData(url);
				if (data != "")
				{
					if (data.Contains("JSON("))
					{
						data = data.Replace("JSON(", "");
					}
					if (data.Contains(");"))
					{
						data = data.Replace(");", "");
					}
					searchData = JsonConvert.DeserializeObject<SearchSuggestions>(data);
				}
				return searchData;

			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");

				return null;
			}
		}
	}
}
