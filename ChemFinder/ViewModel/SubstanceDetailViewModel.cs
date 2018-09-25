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
	public class SubstanceDetailViewModel : ViewModelBase
	{
		public ICommand GetChemImageCommand { get; private set; }
		public SubstanceDetailViewModel()
		{
			GetChemImageCommand = new Command<string>((item) => GetChemImage(item));

		}
		void GetChemImage(string chemImageUrl)
		{
			Application.Current.MainPage.Navigation.PushPopupAsync(new ImagePopup(chemImageUrl), true);

		}

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
		private string imageurl2D ="";

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

		//public async Task GetItemDetail(string searchId)
		//{
			

		//	string searchValue = searchId;
		//	ItemDetailsFactory objFactory = new ItemDetailsFactory();
		//	IItemDetails objItem = objFactory.CreateObj(GlobalHelper.Category);
		//	var result = await objItem.GetData(searchValue,GlobalHelper.Category);


		//}
		private async Task GetTopDetail(string searchId)
		{
			
			string searchValue = searchId;
			ItemDetailsFactory objFactory = new ItemDetailsFactory();
			IItemDetails objItem = objFactory.CreateObj(GlobalHelper.Category);

			Tuple < ObservableCollection<ItemDetailInList>, string>  tuple= await objItem.GetTopSectionData(searchValue, GlobalHelper.Category);
			ItemDetailsInList = tuple.Item1;
			ChemName = tuple.Item2;
		}
		public  void Get2DImage(string searchId)
		{
			
			string searchValue = searchId;
			CollectionGenerator objgenerate = new CollectionGenerator();
			Imageurl2D = objgenerate.Get2DImage(searchValue, GlobalHelper.Category,false);

		}


		public async Task GetData(string searchId)
		{
			await GetTopDetail(searchId);
			Get2DImage(searchId);

		}





		//private RelayCommand detailCommand;

		//public RelayCommand DetailCommand
		//{
		//	get
		//	{
		//		return detailCommand
		//		?? (detailCommand = new RelayCommand(
		//				async () =>
		//				{
		//					await GetTopDetail();
		//			        await Get2DImage();
							
		//				}));
		//	}
		//}
	}
}
