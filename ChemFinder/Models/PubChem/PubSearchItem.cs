using System;
namespace ChemFinder
{
	public class PubSearchItem:ISearchItem
	{
		public PubSearchItem(){
			SearchOperation= new PubSearchOperation();
			Start = "0";
			End = "50";

		}
		public string SearchValue
		{ get; set; }
		public string Start
		{ 
			get; set; 
		
		}
		public string End
		{ 
			get; set;
		}
		private string imageSize;
		public string ImageSize 
		{ 
			get
			{
				return imageSize;
			}
			set
			{
				imageSize = value;
			}
		}
		public PubSearchInput SearchInput;
		public PubSearchOperation SearchOperation;
		private string _outputFormat="JSON";
		public string OutputFormat { 
			get 
			{ 
				return _outputFormat;
			}
			set
			{	_outputFormat = value;
			} 
		 }
		public string SearchCategory { get; set; }

		#region Compound Urls
		public string SearchUrlWithPathAndParameters 
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/name" + @"/" + SearchValue;
				url = url + @"/property/" + this.SearchOperation.CompoundProperty;
				url = url + @"/" + _outputFormat + "?name_type=word&listkey_start="+Start+"&listkey_count="+End;
				return UrlHelperForItemList.BaseUrl + url;
			}
			
		}
		public string SearchUrlForDescription
		{ 
			get
			{ 
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/name" + @"/" + SearchValue;
				url = url + @"/description" ;
				url = url + @"/" + _outputFormat + "?name_type=word&listkey_start=" + Start + "&listkey_count=" + End;
				return UrlHelperForItemList.BaseUrl + url;
			}
		}
		public string SearchDetailUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/" + SearchValue;
				url = url + @"/" +_outputFormat;
				return UrlHelperForItemDetail.BaseUrl + url;
			}

		}
		public string SearchTopDetailUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/cid" + @"/" + SearchValue;
				url = url + @"/property/" + this.SearchOperation.CompoundDetailProperty;
				url = url + @"/" + _outputFormat;
				return UrlHelperForItemList.BaseUrl + url;
			}

		}
		public string SearchDescriptionByIdUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/cid" + @"/" + SearchValue;
				url = url + @"/description";
				url = url + @"/" + _outputFormat + "?name_type=word&listkey_start=" + Start + "&listkey_count=" + End ;
				return UrlHelperForItemList.BaseUrl + url;
			}

		}
		#endregion

		#region Substance Urls
		public string SearchForSubstanceListUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/name" + @"/" + SearchValue;
				url = url + @"/" + _outputFormat + "?name_type=word&listkey_start=" + Start + "&listkey_count=" + End;
				return UrlHelperForItemList.BaseUrl + url;
			}

		}
		public string SearchSubstanceDetailUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/sid" + @"/" + SearchValue;
				url = url + @"/" + _outputFormat;
				return UrlHelperForItemList.BaseUrl + url;
			}

		}
		#endregion

		#region BioAssay Urls
		public string SearchBioAssayListUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/sourceall" + @"/" + SearchValue;
				url = url + @"/" + _outputFormat + "?listkey_start = " + Start + " & listkey_count=" + End;
				return UrlHelperForItemList.BaseUrl + url;
			}

		}
		public string SearchBioAssayDetailUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategory();
				url = url + @"/aid" + @"/" + SearchValue;
				url = url + @"/" + _outputFormat;
				return UrlHelperForItemList.BaseUrl + url;
			}

		}
		#endregion

		#region Image url
		public string Search2dImageUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				string idName = GetIdName();
				url = url + idName + @"=" + SearchValue + "&t=" + ImageSize;
				return UrlHelperForItemDetail.ImageBaseUrl + url;
			}

		}
		#endregion

		#region AutoComplete Suggestion url
		public string SearchForSuggestionUrlPathAndParameters
		{
			get
			{
				string url = string.Empty;
				url = url + GetCategoryForSearch();
				url = url + SearchValue;
				return UrlHelperForSuggestion.BaseUrl + url;
			}

		}
		#endregion

		private string GetCategory()
		{
			string category = string.Empty;
			if (SearchCategory == CategoryEnum.Compound.ToString())
			{
				category = category + @"compound";
			}
			else if (SearchCategory == CategoryEnum.Substance.ToString())
			{
				category = category + @"substance";
			}
			else if (SearchCategory == CategoryEnum.BioAssay.ToString())
			{
				category = category + @"assay";
			}
			return category;
		}
		private string GetCategoryForSearch()
		{
			string category = string.Empty;
			if (SearchCategory == CategoryEnum.Compound.ToString())
			{
				category = category + UrlHelperForSuggestion.ForCompounds;
			}
			else if (SearchCategory == CategoryEnum.Substance.ToString())
			{
				category = category + UrlHelperForSuggestion.ForSubstances;
			}
			else if (SearchCategory == CategoryEnum.BioAssay.ToString())
			{
				category = category + UrlHelperForSuggestion.ForAssays;
			}
			return category;
		}
		private string GetIdName()
		{
			string idName = string.Empty;
			if (SearchCategory == CategoryEnum.Compound.ToString())
			{
				idName = "cid";
			}
			else if (SearchCategory == CategoryEnum.Substance.ToString())
			{
				idName = "sid";
			}
			else if (SearchCategory == CategoryEnum.BioAssay.ToString())
			{
				idName = "aid";
			}
			return idName;
		}

	}
}
