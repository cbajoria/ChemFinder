using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace ChemFinder
{
	public class ItemDetailList : ObservableCollection<ItemDetailInList>
	{
		public ICommand GetItemDetailCommand { get; private set; }
		public ICommand GetChemImageCommand { get; private set; }

		public ItemDetailList()
		{
			GetItemDetailCommand = new Command<string>((item) => GetItemsDetail(item));
			GetChemImageCommand = new Command<string>((item) => GetChemImage(item));
		}


		void GetChemImage(string chemImageUrl) 
		{
			Application.Current.MainPage.Navigation.PushPopupAsync(new ImagePopup(chemImageUrl), true);

		}


		void GetItemsDetail(string chemId)
		{
			if (GlobalHelper.Category == CategoryEnum.Compound.ToString())
			{
				Application.Current.MainPage.Navigation.PushAsync(new CompoundDetailPage(chemId));
			}
			else if (GlobalHelper.Category == CategoryEnum.BioAssay.ToString())
			{
				Application.Current.MainPage.Navigation.PushAsync(new BioAssayDetailPage(chemId));
			}
			else
			{
				Application.Current.MainPage.Navigation.PushAsync(new SubstanceDetailPage(chemId));
			}


		}

		public string ChemId { get; set; }


		public string Imageurl2D { get; set; }


		private string chemName;

		public string ChemName
		{
			get
			{
				return chemName;
			}
			set
			{
				chemName = UppercaseFirst(value);
			}
		}

		private bool isImageVisible =true;

		public bool IsImageVisible
		{
			get
			{
				return isImageVisible;
			}
			set
			{
				isImageVisible =value;
			}
		}
		//public ObservableCollection<ItemDetailInList> ItemsList { get; set; }

		static string UppercaseFirst(string s)
		{
			// Check for empty string.
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			// Return char and concat substring.
			return char.ToUpper(s[0]) + s.Substring(1);
		}
	}
}
