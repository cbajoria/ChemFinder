
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace ChemFinder
{
	public interface IItems
	{
		//Task<T> GetData(string url);
		 Task<ObservableCollection<ItemDetailList>> GetData(string searchValue, string searchCategory);
	}
}
