using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace ChemFinder
{

	public partial class SearchPage : ContentPage
	{
		EventHandler externalEvent;
		SearchViewModel search;
		public SearchPage()
		{
			InitializeComponent();
			search = App.Locator.Search;
			BindingContext = search;
			search.IsRecentVisible = true;

		}
		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			
			try
			{
				
				string s = autoComplete.Text;
				if (s == "")
				{
					search.ItemDetailsInList = new ObservableCollection<ItemDetailList>();
					search.TotalRecords = search.ItemDetailsInList.Count;
					search.IsVisible = false;
					search.IsRecentVisible = true;
					GlobalHelper.cts.Cancel();
					search.ButtonValue = "Search";


				}
				else if (search.ButtonValue == "Cancel")
				{
					//string s1 = "";
					//autoComplete.Text = s1;
					search.ButtonValue = "Search";
					search.ItemDetailsInList = new ObservableCollection<ItemDetailList>();
					search.TotalRecords = search.ItemDetailsInList.Count;
					search.IsVisible = false;


				//	externalEvent +=(se, obj) => { GlobalHelper.cts.Cancel(); };

					GlobalHelper.cts.Cancel();
					//Task.Run(() => search.GetItemsList(s, GlobalHelper.token));
					//GlobalHelper.cts.Dispose();
					search.IsRecentVisible = true;
				}
				else {
					GenerateNewCancellationToken();
					search.ButtonValue = "Cancel";
					search.IsRecentVisible = false;
					search.SaveRecentSearch(s);
					search.GetItemsList(s);

		
				
				}
			}
			catch (Exception ex)
			{


			}
		}
		async void Handle_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
			search.ButtonValue = "Cancel";
			GenerateNewCancellationToken();
			search.IsRecentVisible = false;
			string s = e.Value;
			//search.ButtonValue = "Cancel";
			search.SaveRecentSearch(s);
			await search.GetItemsList(s);

		}
        void btnCompound_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				GlobalHelper.cts.Cancel();
				search.ButtonValue = "Search";
				search.IsRecentVisible = true;
				cntAssay.BackgroundColor = Color.FromHex("#ffcb04");
				cntSubstance.BackgroundColor = Color.FromHex("#ffcb04");
				cntCompound.BackgroundColor = Color.White;
				btnCompound.BackgroundColor = Color.White;
				btnAssay.BackgroundColor = Color.FromHex("#ffcb04");
				btnSubstance.BackgroundColor = Color.FromHex("#ffcb04");
				GlobalHelper.Category=CategoryEnum.Compound.ToString();
				search.LoadRecentItemList(GlobalHelper.Category);
				Clear();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.Message, "Ok");
			}
		}
		void btnSubstance_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				GlobalHelper.cts.Cancel();
				search.ButtonValue = "Search";
				search.IsRecentVisible = true;
				cntAssay.BackgroundColor = Color.FromHex("#ffcb04");
				cntCompound.BackgroundColor = Color.FromHex("#ffcb04");
				cntSubstance.BackgroundColor = Color.White;
				btnSubstance.BackgroundColor = Color.White;
				btnAssay.BackgroundColor = Color.FromHex("#ffcb04");
				btnCompound.BackgroundColor = Color.FromHex("#ffcb04");
				GlobalHelper.Category = CategoryEnum.Substance.ToString();
				search.LoadRecentItemList(GlobalHelper.Category);
				Clear();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.Message, "Ok");
			}
		}
		void btnAssay_Clicked(object sender, System.EventArgs e)
		{
			try
			{
				GlobalHelper.cts.Cancel();
				search.ButtonValue = "Search";
				search.IsRecentVisible = true;
				cntSubstance.BackgroundColor = Color.FromHex("#ffcb04");
				cntCompound.BackgroundColor = Color.FromHex("#ffcb04");
				cntAssay.BackgroundColor = Color.White;
				btnAssay.BackgroundColor = Color.White;
				btnSubstance.BackgroundColor = Color.FromHex("#ffcb04");
				btnCompound.BackgroundColor = Color.FromHex("#ffcb04");
				GlobalHelper.Category = CategoryEnum.BioAssay.ToString();
				search.LoadRecentItemList(GlobalHelper.Category);
				Clear();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.Message, "Ok");
			}
		}
		async void Handle_ValueChanged(object sender, Syncfusion.SfAutoComplete.XForms.ValueChangedEventArgs e)
		{
			GenerateNewCancellationToken();
			search.IsRecentVisible = false;
			search.ButtonValue = "Search";
			if (e.Value != "")
			{
				await search.GetSuggestions(e.Value.Trim());

			}
			else
			{
				search.ButtonValue = "Cancel";
			}



		}
		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			if (e == null) return; 
			((ListView)sender).SelectedItem = null;
		}
		private void Clear()
		{
			search.Clear();
			//autoComplete.Text = String.Empty;

		}

		void Handle_RecentItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			GenerateNewCancellationToken();
			search.ButtonValue = "Cancel";
			search.IsRecentVisible = false;
			string s = e.Item.ToString();
			search.ItemDetailsInList = new ObservableCollection<ItemDetailList>();
			Task.Run(() => search.GetItemsList(s));

			
		}


		void GenerateNewCancellationToken() { 
			GlobalHelper.cts = new CancellationTokenSource();
			GlobalHelper.token = GlobalHelper.cts.Token;
		}


	}
}