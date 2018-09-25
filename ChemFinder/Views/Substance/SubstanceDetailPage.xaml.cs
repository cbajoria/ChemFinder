using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChemFinder
{
	public partial class SubstanceDetailPage : ContentPage
	{
		
		public SubstanceDetailPage(string Id)
		{
			InitializeComponent();


			var itemVm = App.Locator.Substance;
			BindingContext = itemVm;
			itemVm.ChemId = Id;
			itemVm.GetData(Id);


		}


		void Handle_PinchUpdated(object sender, Xamarin.Forms.PinchGestureUpdatedEventArgs e)
		{
			Image img = (Image)sender;
			img.HeightRequest =200;
			img.WidthRequest = 200;
			   

		}
	}
}
