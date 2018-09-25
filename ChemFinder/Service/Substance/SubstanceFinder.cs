/*
 Created by : Moni
 Created Date : 24/02/2017
 Purpose :  To get data from sbustance service and convert it to collection
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public class SubstanceFinder: IItems
	{
		public SubstanceFinder()
		{
			
		}
		public async Task<ObservableCollection<ItemDetailList>> GetData(string searchValue, string searchCategory)
		{
			try
			{
				ObservableCollection<ItemDetailList> ItemDetailsInList = new ObservableCollection<ItemDetailList>();
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				//searchInput.OutputFormat = "JSON";
				var url = searchInput.SearchForSubstanceListUrlPathAndParameters;
				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);

				if (data != "")
				{
					if (GlobalHelper.Category == searchCategory)
					{
						var searchData = JsonConvert.DeserializeObject<SubstanceList>(data);
						CollectionGenerator obj = new CollectionGenerator();
					    ItemDetailsInList = obj.CreateSubstanceNameValueList(searchData, searchCategory);// obj.GetProperty(items, null, searchCategory);
						return ItemDetailsInList;
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
	

	}
}
