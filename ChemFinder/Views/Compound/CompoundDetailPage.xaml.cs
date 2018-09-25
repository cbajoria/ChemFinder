using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChemFinder
{
	public partial class CompoundDetailPage : ContentPage
	{

		CompoundDetailViewModel itemVm;
		public CompoundDetailPage(string Id)
		{
			InitializeComponent();


			itemVm = App.Locator.Compound;
			BindingContext = itemVm;
			itemVm.ChemId = Id;
		


		}


		protected override void OnAppearing()
		{
			
			var task = Task.Run(() => itemVm.GetData(itemVm.ChemId));
		

			if (task.Result)
			{
				setUpTopSection();

				CompoundDescription();
				GetOtherIdentifiers();
				GetDescription();
				GetMeShSynonyms();
				GetDepositorSuppliedSynonyms();
				//GetComputedProperties();
				//GetExperimentalProperties();

				GetDrugAndMedicationInformation();
			}
			if (itemVm.ChemicalAndPhysical.Count == 0)
			{
				ChemicalAndPhysicalSection.IsVisible = false;
			}
			if (itemVm.ExperimentalProperties.Count == 0)
			{
				ExperimentalPropertiesSection.IsVisible = false;
			}
				




		}


		void Handle_PinchUpdated(object sender, Xamarin.Forms.PinchGestureUpdatedEventArgs e)
		{
			Image img = (Image)sender;
			img.HeightRequest = 200;
			img.WidthRequest = 200;


		}

		private void setUpTopSection()
		{

			Grid grid = new Grid() { Padding=10, HorizontalOptions=LayoutOptions.FillAndExpand};
			int row = 0;
		
			foreach (ItemDetailInList item in itemVm.ItemDetailsInList)
			{

				grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
				grid.ColumnDefinitions.Add(new ColumnDefinition { Width =GridLength.Auto});
				grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto });

				Label label1 = new Label { TextColor = Color.Black, Text = item.Label,FontSize=12};
				Label label2 = new Label {HorizontalOptions=LayoutOptions.FillAndExpand, TextColor = Color.Black, Text = item.Value , FontSize = 12 };



				grid.Children.Add(label1,0,row);
				grid.Children.Add(label2,1,row);

				row++;
					//StackLayout row = new StackLayout
					//{
					//	Orientation = StackOrientation.Horizontal
					//};

					//row.Children.Add(new Label
					//{
					//	TextColor = Color.Black,
					//	Text = string.Format("{0}:",item.Label)
					//});

					//row.Children.Add(new Label
					//{
					//	TextColor = Color.Black,
					//	Text = item.Value,
					//});
					
			}
			TopSection.Children.Add(grid);
		}



		private void GetDescription()
		{

			if (itemVm.RecordDescription.Count != 0)
			{

				foreach (String item in itemVm.RecordDescription)
				{

					StackLayout row = new StackLayout
					{
						Orientation = StackOrientation.Horizontal,
						Padding = 10

					};

					row.Children.Add(new Label
					{
						TextColor = Color.Black,
						Text = string.Format("{0}:", item),
						FontSize = 12

					});


					Description.Children.Add(row);
				}
			}
			else {
				Description.IsVisible = false;
			}
		}






		private void CompoundDescription()
		{

			if (itemVm.ComputedDescriptor.Count != 0)
			{

				Grid grid = new Grid() { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand };
				int row = 0;

				foreach (ItemDetailInList item in itemVm.ComputedDescriptor)
				{

					grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

					Label label1 = new Label { TextColor = Color.Black, Text = item.Label, FontSize = 12 };
					Label label2 = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.Black, Text = item.Value, FontSize = 12 };



					grid.Children.Add(label1, 0, row);
					grid.Children.Add(label2, 1, row);

					row++;
				}
				CompoundDescriptor.Children.Add(grid);
			}
			else {
				CompoundDescriptorSection.IsVisible = false;
			}
		}
		private void GetOtherIdentifiers()
		{
			if (itemVm.OtherIdentifier.Count != 0)
			{
				Grid grid = new Grid() { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand };
				int row = 0;

				foreach (ItemDetailInList item in itemVm.OtherIdentifier)
				{

					grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

					Label label1 = new Label { TextColor = Color.Black, Text = item.Label, FontSize = 12 };
					Label label2 = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.Black, Text = item.Value, FontSize = 12 };



					grid.Children.Add(label1, 0, row);
					grid.Children.Add(label2, 1, row);

					row++;
				}
				OtherIdentifiers.Children.Add(grid);
			}
			else {
				OtherIdentifiersSection.IsVisible = false;
			}
		}


		private void GetMeShSynonyms()
		{

			if (itemVm.MeshSynonyms.Count != 0)
			{
				int num = 1;
				foreach (String item in itemVm.MeshSynonyms)
				{

					StackLayout row = new StackLayout
					{
						Orientation = StackOrientation.Horizontal,


					};
					row.Children.Add(new Label
					{
						TextColor = Color.Black,
						Text = string.Format("{0}.", num),
						FontSize = 12

					});

					row.Children.Add(new Label
					{
						TextColor = Color.Black,
						Text = string.Format("{0}", item),
						FontSize = 12

					});
					num++;
					MeShSynonyms.Children.Add(row);
				}
			}
			else {
				MeShSynonymsSection.IsVisible = false;
			}


		}

		private void GetDepositorSuppliedSynonyms()
		{

			if (itemVm.DepositorSuppliedSynonyms.Count != 0)
			{
				int num = 1;
				foreach (String item in itemVm.DepositorSuppliedSynonyms)
				{

					StackLayout row = new StackLayout
					{
						Orientation = StackOrientation.Horizontal,

					};
					row.Children.Add(new Label
					{
						TextColor = Color.Black,
						Text = string.Format("{0}.", num),
						FontSize = 12

					});
					row.Children.Add(new Label
					{
						TextColor = Color.Black,
						Text = string.Format("{0}", item),
						FontSize = 12

					});
					num++;
					DepositorSuppliedSynonyms.Children.Add(row);
				}
			}
			else {

				DepositorSuppliedSynonymsSection.IsVisible = false;
			}

		}
		//private void GetComputedProperties()
		//{

		//Grid grid = new Grid() { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand };
		//	int row = 0;

		//	foreach (ItemDetailInList item in itemVm.ChemicalAndPhysical)
		//	{

		//		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		//		grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
		//		grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

		//		Label label1 = new Label { TextColor = Color.Black, Text = item.Label, FontSize = 12 };
		//		Label label2 = new Label { TextColor = Color.Black, Text = string.Format("{0}:", item.Value), FontSize = 12 };



		//		grid.Children.Add(label1, 0, row);
		//		grid.Children.Add(label2, 1, row);

		//		row++;
		//	}
		//	ChemicalAndPhysical.Children.Add(grid);
		//}

		//private void GetExperimentalProperties()
		//{

		//	Grid grid = new Grid() { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand };
		//	int row = 0;

		//	foreach (ItemDetailInList item in itemVm.ExperimentalProperties)
		//	{

		//		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		//		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		//		//grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		//		//grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

		//		Label label1 = new Label { TextColor = Color.Black, Text = item.Label, FontSize = 12 };
		//		Label label2 = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.Black, Text = item.Value, FontSize = 12 };



		//		grid.Children.Add(label1, 0, 0+row);
		//		grid.Children.Add(label2, 0, 1+row);

		//		row++;
		//	}
		//	ExperimentalProperties.Children.Add(grid);
		//}



		private void GetDrugAndMedicationInformation()
		{
			if (itemVm.DrugAndMedicationInformation.Count != 0)
			{

				Grid grid = new Grid() { Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand };
				int row = 0;

				foreach (ItemDetailInList item in itemVm.DrugAndMedicationInformation)
				{

					grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
					grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

					//grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

					Label label1 = new Label { TextColor = Color.Black, Text = item.Label, FontSize = 12, FontAttributes = FontAttributes.Bold };
					Label label2 = new Label { TextColor = Color.Black, Text = item.Value, FontSize = 12 };



					grid.Children.Add(label1, 0, 0 + row);
					grid.Children.Add(label2, 0, 1 + row);

					row += 2;

				}
				DrugAndMedicationInformation.Children.Add(grid);
			}
			else {
				DrugAndMedicationInformationSection.IsVisible = false;
			}
		}


	}
}
