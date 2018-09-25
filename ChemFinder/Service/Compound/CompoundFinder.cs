/*
 Created by : Moni
 Created Date : 22/02/2017
 Purpose :  To get data from compound service and convert it to collection
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public class CompoundFinder : IItems
	{
		public CompoundFinder()
		{

		}
		// Function to get data from url and return collection of label and value
		public async Task<ObservableCollection<ItemDetailList>> GetData(string searchValue, string searchCategory)
		{
			try
			{
				ObservableCollection<ItemDetailList> ItemDetailsInList= new ObservableCollection<ItemDetailList>();
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				//searchInput.OutputFormat = "JSON";
				var url = searchInput.SearchUrlWithPathAndParameters;

				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);
				//var searchData = JsonConvert.DeserializeObject<CompoundsCollection>(data);
				if (data != "")
				{
					var jdata = JObject.Parse(data);
					var items = (jdata["PropertyTable"]["Properties"]) as JArray;
					//Code to get description
					url = searchInput.SearchUrlForDescription;
					string descriptions = await objHelper.GetData(url);
					if (descriptions != "")
					{
						if (GlobalHelper.Category == searchCategory)
						{
							var itemDescription = JObject.Parse(descriptions);
							//var descriptionsItems = (itemDescription["InformationList"]["Information"]) as JArray;

							//code to combine description to searchData object
							//CombineDescriptionResult(searchData, itemDescription);

							//Code to generate collection of binadable properties
							CollectionGenerator obj = new CollectionGenerator();
							 ItemDetailsInList = obj.GetProperty(items, itemDescription, searchCategory);

						}
					}

				}
				return ItemDetailsInList;
			}
			catch (Exception ex)
			{
				
				App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
				return null;
			}

		}






		// Commented this code to remove dependency on any specific model

		//Function to combine description to other property list of items
		//private void CombineDescriptionResult(CompoundsCollection compoundCollection, ItemDescription description)
		//{

		//	Dictionary<int, string> map = description.InformationList.Information.ToDictionary(x => x.CID, x => x.Title);

		//	foreach (var item in compoundCollection.PropertyTable.Properties)
		//		if (map.ContainsKey(item.CID))
		//			item.Description = map[item.CID];

		//	compoundCollection.PropertyTable.Properties.RemoveAll(x => x.Description == null);

		//}

		//public async Task<CompoundsCollection> GetData(string url)
		//{
		//	try
		//	{
		//		ServiceHelper objHelper = new ServiceHelper();
		//		string data = await objHelper.GetData(url);
		//		var searchData = JsonConvert.DeserializeObject<CompoundsCollection>(data);
		//		return searchData;

		//	}
		//	catch (Exception ex)
		//	{
		//		await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
		//		return null;
		//	}
		//}

	}
}
