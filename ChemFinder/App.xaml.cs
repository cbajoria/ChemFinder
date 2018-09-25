using Xamarin.Forms;

namespace ChemFinder
{
	public partial class App : Application
	{
		public static double ScreenWidth;
		public static double ScreenHeight;
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new SearchPage())
			{
				BarBackgroundColor = Color.FromHex("#3f235e"),
				BarTextColor = Color.White
				                          
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		private static ViewModelLocator _locator;

		public static ViewModelLocator Locator
		{
			get
			{
				return _locator ?? (_locator = new ViewModelLocator());
			}
		}
	}
}
