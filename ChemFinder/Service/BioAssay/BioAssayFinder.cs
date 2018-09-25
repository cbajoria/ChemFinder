/*
 Created by : Moni
 Created Date : 27/02/2017
 Purpose : To get data from  bio assay service and convert it to collection
 */
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChemFinder
{
	public class BioAssayFinder : IItems
	{
		public BioAssayFinder()
		{
		}

		public async Task<ObservableCollection<ItemDetailList>> GetData(string searchValue, string searchCategory)
		{
			try
			{
				ObservableCollection<ItemDetailList> ItemDetailsInList=new ObservableCollection<ItemDetailList>();
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				var url = searchInput.SearchBioAssayListUrlPathAndParameters;
				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);
				if (data != "")
				{
					if (GlobalHelper.Category == searchCategory)
					{
						var searchData = JsonConvert.DeserializeObject<BioAssayList>(data);
						CollectionGenerator obj = new CollectionGenerator();
					    ItemDetailsInList = obj.CreateBioAssayNameValueList(searchData, searchCategory);// obj.GetProperty(items, null, searchCategory);

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
