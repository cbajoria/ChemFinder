/*
 Created by : Moni
 Created Date : 01/03/2017
 Purpose : To bind detail of Compounds
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
	public class CompoundDetailViewModel : ViewModelBase
	{
		public ICommand GetChemImageCommand { get; private set; }
		public CompoundDetailViewModel()
		{
			GetChemImageCommand = new Command<string>((item) => GetChemImage(item));

		}

		#region Properties
		private bool isLoad;
		public bool IsLoad
		{
			get { return isLoad; }
			set
			{
				isLoad = value;
				RaisePropertyChanged("IsLoading");
			}
		}

		ObservableCollection<string> meshSynonyms = new ObservableCollection<string>();
		public ObservableCollection<string> MeshSynonyms
		{
			get { return meshSynonyms; }
			set
			{
				meshSynonyms = value;
				RaisePropertyChanged("MeshSynonyms");
			}
		}

		ObservableCollection<string> depositorSuppliedSynonyms = new ObservableCollection<string>();
		public ObservableCollection<string> DepositorSuppliedSynonyms
		{
			get { return depositorSuppliedSynonyms; }
			set
			{
				depositorSuppliedSynonyms = value;
				RaisePropertyChanged("DepositorSuppliedSynonyms");
			}
		}

		ObservableCollection<ItemDetailInList> computedDescriptor = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> ComputedDescriptor
		{
			get { return computedDescriptor; }
			set
			{
				computedDescriptor = value;
				RaisePropertyChanged("ComputedDescriptor");
			}
		}
		ObservableCollection<ItemDetailInList> molecularFormula = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> MolecularFormula
		{
			get { return molecularFormula; }
			set
			{
				molecularFormula = value;
				RaisePropertyChanged("MolecularFormula");
			}
		}

		ObservableCollection<ItemDetailInList> otherIdentifier = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> OtherIdentifier
		{
			get { return otherIdentifier; }
			set
			{
				otherIdentifier = value;
				RaisePropertyChanged("OtherIdentifier");
			}
		}

		ObservableCollection<string> recordDescription = new ObservableCollection<string>();
		public ObservableCollection<string> RecordDescription
		{
			get { return recordDescription; }
			set
			{
				recordDescription = value;
				RaisePropertyChanged("RecordDescription");
			}
		}
		ObservableCollection<ItemDetailInList> chemicalAndPhysical = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> ChemicalAndPhysical
		{
			get { return chemicalAndPhysical; }
			set
			{
				chemicalAndPhysical = value;
				RaisePropertyChanged("ChemicalAndPhysical");
			}
		}

		ObservableCollection<ItemDetailInList> experimentalProperties = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> ExperimentalProperties
		{
			get { return experimentalProperties; }
			set
			{
				experimentalProperties = value;
				RaisePropertyChanged("ExperimentalProperties");
			}
		}

		ObservableCollection<ItemDetailInList> drugAndMedicationInformation = new ObservableCollection<ItemDetailInList>();

		public ObservableCollection<ItemDetailInList> DrugAndMedicationInformation
		{
			get { return drugAndMedicationInformation; }
			set
			{
				drugAndMedicationInformation = value;
				RaisePropertyChanged("DrugAndMedicationInformation");
			}
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
		#endregion

		#region Methods
		public async Task GetItemDetail(string searchId)
		{
			try
			{
				IsLoad = true;
				string searchValue = searchId;
				CompoundDetails objdetail = new CompoundDetails();
				var result = await objdetail.GetData(searchValue, GlobalHelper.Category);

				RecordDescription = new ObservableCollection<string>(result.Item1.Item1);
				ComputedDescriptor = new ObservableCollection<ItemDetailInList>(result.Item1.Item2);
				MolecularFormula = new ObservableCollection<ItemDetailInList>(result.Item1.Item3);
				OtherIdentifier = new ObservableCollection<ItemDetailInList>(result.Item1.Item4);
				MeshSynonyms = new ObservableCollection<string>(result.Item1.Item5);
				DepositorSuppliedSynonyms = new ObservableCollection<string>(result.Item1.Item6);
				ChemicalAndPhysical = new ObservableCollection<ItemDetailInList>(result.Item2.Item1);
				ExperimentalProperties = new ObservableCollection<ItemDetailInList>(result.Item2.Item2);
				DrugAndMedicationInformation = new ObservableCollection<ItemDetailInList>(result.Item3);
				IsLoad = false;
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}

		}
		private async Task GetTopDetail(string searchId)
		{
			try
			{
				IsLoad = true;

				string searchValue = searchId;
				ItemDetailsFactory objFactory = new ItemDetailsFactory();
				IItemDetails objItem = objFactory.CreateObj(GlobalHelper.Category);
				Tuple<ObservableCollection<ItemDetailInList>, string> tuple = await objItem.GetTopSectionData(searchValue, GlobalHelper.Category);
				ItemDetailsInList = tuple.Item1;
				ChemName = tuple.Item2;
				IsLoad = false;
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
		}
		public void Get2DImage(string searchId)
		{

			string searchValue = searchId;
			CollectionGenerator objgenerate = new CollectionGenerator();
			Imageurl2D = objgenerate.Get2DImage(searchValue, GlobalHelper.Category, false);

		}
		public async Task<bool> GetData(string searchId)
		{
			try
			{

				await GetTopDetail(searchId);
				Get2DImage(searchId);
				await GetItemDetail(searchId);

				return true;
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
				return false;

			}
		}
		void GetChemImage(string chemImageUrl)
		{
			Application.Current.MainPage.Navigation.PushPopupAsync(new ImagePopup(chemImageUrl), true);

		}

		#endregion

	}
}
