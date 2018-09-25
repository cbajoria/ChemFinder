using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ChemFinder
{
	public partial class ImagePopup : PopupPage
	{

	
		public ImagePopup(string ChemId)
		{
			InitializeComponent();
			CollectionGenerator objCollection = new CollectionGenerator();
			img.Source = objCollection.Get2DImage(ChemId, GlobalHelper.Category, false);

		}

		void Handle_ImageTapped(object sender, System.EventArgs e)
		{
			
			this.Navigation.PopPopupAsync(true);


			
		}

		void Handle_Tapped(object sender, System.EventArgs e)
		{
			this.Navigation.PopPopupAsync(true);
		}	

	
		protected override bool OnBackgroundClicked()
		{
			// Return default value - CloseWhenBackgroundIsClicked
			this.Navigation.PopPopupAsync(true);
			return true;
		}
	}
}
