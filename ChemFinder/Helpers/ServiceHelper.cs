using System;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;

namespace ChemFinder
{
	public class ServiceHelper
	{
		public ServiceHelper()
		{
			
		}
		public async Task<string> GetData(string url)
		{
			try
			{
				using (var client = new HttpClient(new NativeMessageHandler()))
				{
					var message = new HttpRequestMessage(HttpMethod.Get, url);
					var result = await client.SendAsync(message, GlobalHelper.token);
					HttpResponseMessage http = result.EnsureSuccessStatusCode();
					using (HttpContent content = result.Content)
					{
						// ... Read the string.
						string data = await content.ReadAsStringAsync();
						return data;
					}
				}

			}
			catch (OperationCanceledException ex) { 
			return "";
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");

				return "";
			}
			//finally {
			//GlobalHelper.cts.Dispose();
			//}
		}
	}
}
