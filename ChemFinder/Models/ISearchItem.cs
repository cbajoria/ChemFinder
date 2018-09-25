using System;
namespace ChemFinder
{
	public interface ISearchItem
	{
		string SearchValue { get; set; }
		string SearchCategory { get; set; }
		string SearchUrlWithPathAndParameters { get; }
		string SearchDetailUrlPathAndParameters { get; }
		string SearchTopDetailUrlPathAndParameters{ get; }
		string Search2dImageUrlPathAndParameters { get; }
		string SearchDescriptionByIdUrlPathAndParameters { get; }
		string SearchForSuggestionUrlPathAndParameters { get; }
		string SearchForSubstanceListUrlPathAndParameters { get; }
		string SearchSubstanceDetailUrlPathAndParameters { get; }
		string SearchBioAssayListUrlPathAndParameters { get; }
		string SearchBioAssayDetailUrlPathAndParameters { get; }



	}
}
