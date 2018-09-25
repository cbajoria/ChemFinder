
using Foundation;
using Syncfusion.SfAutoComplete.XForms.iOS;
using UIKit;


//[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadAutoComplete), typeof(Telerik.XamarinForms.InputRenderer.iOS.AutoCompleteRenderer))]
namespace ChemFinder.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			//new Telerik.XamarinForms.InputRenderer.iOS.AutoCompleteRenderer();
			new SfAutoCompleteRenderer();


			global::Xamarin.Forms.Forms.Init();

			App.ScreenWidth = UIScreen.MainScreen.Bounds.Width*2;
			App.ScreenHeight = UIScreen.MainScreen.Bounds.Height*2;
			//Telerik.XamarinForms.Common.iOS.TelerikForms.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
