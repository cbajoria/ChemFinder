/*
 Created by : Moni
 Created Date : 24/02/2017
 Purpose : To get detail of selected substance based on sid
 */
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public class SubstanceDetails : IItemDetails
	{
		public SubstanceDetails()
		{
			
		}
		public async Task<JArray> GetData(string searchValue, string searchCategory)
		{
			try
			{
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				//searchInput.OutputFormat = "JSON";
				var url = searchInput.SearchDetailUrlPathAndParameters;
				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);
				var jdata = JObject.Parse(data);
				var items = (jdata["Record"]) as JArray;
				return items;
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
				return null;
			}
		}



		public async Task<Tuple<ObservableCollection<ItemDetailInList>, string>> GetTopSectionData(string searchValue, string searchCategory)
		{
			try
			{
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				//searchInput.OutputFormat = "JSON";
				var url = searchInput.SearchSubstanceDetailUrlPathAndParameters;
				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);
				if (data != "")
				{
					var searchData = JsonConvert.DeserializeObject<SubstanceList>(data);
					CollectionGenerator obj = new CollectionGenerator();
					var ItemDetailsInList = obj.GetSubstancePropertyDetail(searchData, searchCategory);// obj.GetProperty(items, null, searchCategory);
					return ItemDetailsInList;
				}
				else {

					return new Tuple<ObservableCollection<ItemDetailInList>, string>(new ObservableCollection<ItemDetailInList>(),"");
				}
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
				return null;
			}
		}
	}
}
