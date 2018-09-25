using Xamarin.Forms;

namespace ChemFinder
{
	public partial class ChemFinderPage : ContentPage
	{
		public ChemFinderPage()
		{
			InitializeComponent();

			this.BindingContext = App.Locator.Search;
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			
		}
	}
}
