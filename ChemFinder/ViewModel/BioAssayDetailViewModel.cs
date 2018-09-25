/*
 Created by : Moni
 Created Date : 02/03/2017
 Purpose : To bind detail of BioAssays
 */
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ChemFinder;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace ChemFinder
{
	public class BioAssayDetailViewModel : ViewModelBase
	{
		public ICommand GetChemImageCommand { get; private set; }
		public BioAssayDetailViewModel()
		{
			GetChemImageCommand = new Command<string>((item) => GetChemImage(item));

		}

		#region Properties
		ObservableCollection<ItemDetailInList> itemDetailsInList = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> ItemDetailsInList
		{
			get { return itemDetailsInList; }
			set
			{
				itemDetailsInList = value;
				RaisePropertyChanged("ItemDetailsInList");
			}
		}
		private string chemName = "";

		public string ChemName
		{
			get { return chemName; }
			set
			{
				chemName = value;
				RaisePropertyChanged("ChemName");
			}
		}
		private string imageurl2D = "";

		public string Imageurl2D
		{
			get { return imageurl2D; }
			set
			{
				imageurl2D = value;
				if (value != "")
				{
					IsImageVisible = true;
				}
				else
				{
					IsImageVisible = false;
				}
				RaisePropertyChanged("Imageurl2D");
			}
		}

		private bool isImageVisible = false;

		public bool IsImageVisible
		{
			get { return isImageVisible; }
			set
			{
				isImageVisible = value;
				RaisePropertyChanged("IsImageVisible");
			}
		}
		private string chemId = "";

		public string ChemId
		{
			get { return chemId; }
			set
			{
				chemId = value;
				RaisePropertyChanged("ChemId");
			}
		}


		string descriptionValue = "";

		public string DescriptionValue
		{
			get { return descriptionValue; }
			set
			{
				descriptionValue = value;
				RaisePropertyChanged("DescriptionValue");
			}
		}

		string protocolValue = "";

		public string ProtocolValue
		{
			get { return protocolValue; }
			set
			{
				protocolValue = value;
				RaisePropertyChanged("ProtocolValue");
			}
		}

		string primaryCitation = "";

		public string PrimaryCitation
		{
			get { return primaryCitation; }
			set
			{
				primaryCitation = value;
				RaisePropertyChanged("PrimaryCitation");
			}
		}

		ObservableCollection<ItemDetailInList> generalInfoList = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> GeneralInfoList
		{
			get { return generalInfoList; }
			set
			{
				generalInfoList = value;
				RaisePropertyChanged("GeneralInfoList");
			}
		}

		ObservableCollection<ItemDetailInList> bioAssayTargetList = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> BioAssayTargetList
		{
			get { return bioAssayTargetList; }
			set
			{
				bioAssayTargetList = value;
				RaisePropertyChanged("BioAssayTargetList");
			}
		}
		ObservableCollection<ItemDetailInList> categorizedCommentList = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> CategorizedCommentList
		{
			get { return categorizedCommentList; }
			set
			{
				categorizedCommentList = value;
				RaisePropertyChanged("CategorizedCommentList");
			}
		}
		ObservableCollection<ItemDetailInList> entrezCrosslinkList = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> EntrezCrosslinkList
		{
			get { return entrezCrosslinkList; }
			set
			{
				entrezCrosslinkList = value;
				RaisePropertyChanged("EntrezCrosslinkList");
			}
		}
		#endregion

		#region Methods
		public async Task GetItemDetail(string searchId)
		{
			try
			{
				string searchValue = searchId;
				BioAssayDetails objdetail = new BioAssayDetails();
				var result = await objdetail.GetData(searchValue, GlobalHelper.Category);
				GeneralInfoList = new ObservableCollection<ItemDetailInList>(result.Item1);
				DescriptionValue = result.Item2;
				ProtocolValue = result.Item3;
				BioAssayTargetList = new ObservableCollection<ItemDetailInList>(result.Item4);
				CategorizedCommentList = new ObservableCollection<ItemDetailInList>(result.Item5);
				EntrezCrosslinkList = new ObservableCollection<ItemDetailInList>(result.Item6);
				PrimaryCitation = result.Item7;
			}
			catch (Exception ex)
			{
				
			}

		}
		private async Task GetTopDetail(string searchId)
		{
			try
			{
				string searchValue = searchId;
				ItemDetailsFactory objFactory = new ItemDetailsFactory();
				IItemDetails objItem = objFactory.CreateObj(GlobalHelper.Category);
				Tuple<ObservableCollection<ItemDetailInList>, string> tuple = await objItem.GetTopSectionData(searchValue, GlobalHelper.Category);
				ItemDetailsInList = tuple.Item1;
				ChemName = tuple.Item2;
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}

		}

		public async Task GetData(string searchId)
		{
			try
			{
				await GetTopDetail(searchId);
				await GetItemDetail(searchId);
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}

		}
		void GetChemImage(string chemImageUrl)
		{
			Application.Current.MainPage.Navigation.PushPopupAsync(new ImagePopup(chemImageUrl), true);

		}
		#endregion

	}
}