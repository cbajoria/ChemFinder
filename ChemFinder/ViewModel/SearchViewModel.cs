using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace ChemFinder
{
	public class SearchViewModel : ViewModelBase
	{
		public ICommand GetItemListCommand { get; private set; }

		public SearchViewModel() {

			ItemDetailsInList = new ObservableCollection<ItemDetailList>();
			RecentItemsList = new ObservableCollection<string>();
			LoadRecentItemList(GlobalHelper.Category);
			GetItemListCommand = new Command<string>(async (item) => await GetItemsList(item));

		}

		private Command deleteCommand;
		public Command DeleteCommand
		{
			get
			{
				return new Command<string>(item =>
				{
					string fileName = RecentSearchFileFinder.GetFileName(GlobalHelper.Category);
					DependencyService.Get<IRecentItems>().DeleteText(fileName,item);
					LoadRecentItemList(GlobalHelper.Category);

				});
			}

		}




		ObservableCollection<string> _recentItemsList;

		public ObservableCollection<string> RecentItemsList
		{
			get { return _recentItemsList; }
			set
			{
				_recentItemsList = value;
				RaisePropertyChanged("RecentItemsList");
			}
		}
		private string buttonValue="Search";
		public string ButtonValue
		{
			get { return buttonValue; }
			set
			{
				buttonValue = value;
				RaisePropertyChanged("ButtonValue");
			}
		}
		private string recentItemText = "";
		public string RecentItemText
		{
			get { return recentItemText; }
			set
			{
				recentItemText = value;
				RaisePropertyChanged("RecentItemText");
			}
		}
		private string autoComplete;
		public string AutoComplete
		{
			get { return autoComplete; }
			set
			{
				autoComplete = value;
				RaisePropertyChanged("AutoComplete");
			}
		}


		private bool isVisible;
		public bool IsVisible
		{
			get { return isVisible; }
			set
			{
				isVisible = value;
				RaisePropertyChanged("IsVisible");
			}
		}

		private bool isRecentVisible;
		public bool IsRecentVisible
		{
			get { return isRecentVisible; }
			set
			{
				isRecentVisible = value;
				RaisePropertyChanged("IsRecentVisible");
			}

		}




		private bool isLoading;
		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				isLoading = value;
				RaisePropertyChanged("IsLoading");
			}
		}


		private int totalRecords;
		public int TotalRecords
		{
			get { return totalRecords; }
			set
			{
				totalRecords = value;
				RaisePropertyChanged("TotalRecords");
			}
		}


		ObservableCollection<ItemDetailList> _itemDetailsInList;
		public ObservableCollection<ItemDetailList> ItemDetailsInList
		{
			get { return _itemDetailsInList; }
			set
			{
				_itemDetailsInList = value;
				RaisePropertyChanged("ItemDetailsInList");
			}
		}


		List<string> searchSuggestion = new List<string>();
		public List<string> SearchSuggestion
		{
			get { return searchSuggestion; }
			set
			{
				searchSuggestion = value;
				RaisePropertyChanged("SearchSuggestion");
			}
		}

		// Method to fetch the name of items related to display as suggestion popup in autocomplete box
		public async Task GetSuggestions(string searchValue)
		{
			try
			{
				SearchSuggestion = new List<string>();
				ISuggestions objSuggestions = new Suggestions();
				var result = await objSuggestions.GetData(searchValue, GlobalHelper.Category);
				if (result != null)
				{
					SearchSuggestion = result.autocp_array;
				}
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");

			}

		}
		// Method to fetch the list of items related to search item
		public async Task GetItemsList(string searchValue)
		{

			try
			{



				IsLoading = true;
				IsVisible = false;
				ItemDetailsInList = new ObservableCollection<ItemDetailList>();
				TotalRecords = 0;
			
				ItemsFactory objFactory = new ItemsFactory();
				IItems objItem = objFactory.CreateObj(GlobalHelper.Category);
				ItemDetailsInList = await objItem.GetData(searchValue, GlobalHelper.Category);

				TotalRecords = ItemDetailsInList!=null? ItemDetailsInList.Count:0;
				IsLoading = false;
				if (TotalRecords == 0)
				{
					IsVisible = false;

				}
				else {

					IsVisible = true;

				}
			
			}
			catch (Exception ex)
			{
				IsLoading = false;
				App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");

			}

		}
		// Function to save recent search items to txt file.
		public void SaveRecentSearch(string searchItem)
		{
			string fileName = RecentSearchFileFinder.GetFileName(GlobalHelper.Category);
			DependencyService.Get<IRecentItems>().SaveText(fileName, searchItem);
			RecentItemsList.Clear();
			LoadRecentItemList(GlobalHelper.Category);
		}
		// Function to load recent search items from txt file.
		public void LoadRecentItemList(string category)
		{

			RecentItemsList = new ObservableCollection<string>();
			string fileName = RecentSearchFileFinder.GetFileName(GlobalHelper.Category);
			RecentItemsList = DependencyService.Get<IRecentItems>().LoadText(fileName);
			RecentSearchText();
		}
		public void Clear()
		{
			ItemDetailsInList = new ObservableCollection<ItemDetailList>();
			IsVisible = false;
		}
		public void RecentSearchText()
		{
			if (RecentItemsList.Count > 0)
			{
				RecentItemText = "Recent Searches";
			}
			else
			{
				RecentItemText = "No Recent Searches";
			}
		}



	}
}
   