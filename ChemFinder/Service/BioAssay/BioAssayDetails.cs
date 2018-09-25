/*
 Created by : Moni
 Created Date : 27/02/2017
 Purpose : To get detail of selected BioAssay based on aid
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public class BioAssayDetails : IItemDetails
	{
		public BioAssayDetails()
		{
		}

		public async Task<Tuple<List<ItemDetailInList>, string, string, List<ItemDetailInList>, List<ItemDetailInList>, List<ItemDetailInList>, string>> GetData(string searchValue, string searchCategory)
		{
			var searchInput = new PubSearchItem();
			searchInput.SearchValue = searchValue;
			searchInput.SearchCategory = searchCategory;
			//searchInput.OutputFormat = "JSON";
			var url = searchInput.SearchDetailUrlPathAndParameters;
			ServiceHelper objHelper = new ServiceHelper();
			string data = await objHelper.GetData(url);
			if (data != "")
			{
				//var jdata = JObject.Parse(data);
				var searchData = JsonConvert.DeserializeObject<BioAssayDetailList>(data);

				//var item = GetItemDetail(searchData);
				var item = GetItemDetail(searchData);
				return item;
			}
			else
			{
				return null;
			}
		}


		public async Task<Tuple<ObservableCollection<ItemDetailInList>, string>> GetTopSectionData(string searchValue, string searchCategory)
		{
			try
			{
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				//searchInput.OutputFormat = "JSON";
				var url = searchInput.SearchBioAssayDetailUrlPathAndParameters;
				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);
				if (data != "")
				{
					var searchData = JsonConvert.DeserializeObject<BioAssayList>(data);
					CollectionGenerator obj = new CollectionGenerator();
					var ItemDetailsInList = obj.GetBioAssayPropertyDetail(searchData, searchCategory);// obj.GetProperty(items, null, searchCategory);
					return ItemDetailsInList;
				}
				else {

					return new Tuple<ObservableCollection<ItemDetailInList>, string>(new ObservableCollection<ItemDetailInList>(), "");
				}
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
				return null;
			}
		}
		// Method to fetch different sections of selected bioAssay
		private Tuple<List<ItemDetailInList>,string,string,List<ItemDetailInList>,List<ItemDetailInList>,List<ItemDetailInList>,string> GetItemDetail(BioAssayDetailList item)
		{
			try
			{
				var generalInfoList = new List<ItemDetailInList>();
				string descriptionValue = "";
				string protocolValue = "";
				var bioAssayTargetList = new List<ItemDetailInList>();
				var categorizedCommentList = new List<ItemDetailInList>();
				var entrezCrosslinkList = new List<ItemDetailInList>();
				string primaryCitation = "";

				generalInfoList = GetGeneralInfo(item);

				descriptionValue = GetDescription(item);

				protocolValue = GetProtocol(item);

				bioAssayTargetList = GetBioAssayTarget(item);

				categorizedCommentList = GetCategorizedComments(item);

				entrezCrosslinkList = GetEntrezCrossLinkComments(item);

				primaryCitation = GetPrimaryCitation(item);

				return new Tuple<List<ItemDetailInList>, string, string, List<ItemDetailInList>, List<ItemDetailInList>, List<ItemDetailInList>, string>
					(generalInfoList,descriptionValue,protocolValue,bioAssayTargetList,categorizedCommentList,entrezCrosslinkList,primaryCitation);

			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private List<ItemDetailInList> GetGeneralInfo(BioAssayDetailList item)
		{
			try
			{
				var generalInfoList = new List<ItemDetailInList>();
				var generalInfo = item.Record.Section.Where(n => n.TOCHeading == "General Information").SelectMany(m => m.section).SelectMany(o => o.Information);
				foreach (var info in generalInfo)
				{
					ItemDetailInList obj = new ItemDetailInList();
					obj.Label = info.Name;
					obj.Value = info.StringValue;
					generalInfoList.Add(obj);

				}
				return generalInfoList;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private string GetDescription(BioAssayDetailList item)
		{
			try
			{
				string descriptionValue = "";
				var description = item.Record.Section.Where(n => n.TOCHeading == "Description").SelectMany(m => m.Information);
				foreach (var info in description)
				{
					descriptionValue = info.StringValue;

				}
				return descriptionValue;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private string GetProtocol(BioAssayDetailList item)
		{
			try
			{
				string protocolValue = "";
				var protocol = item.Record.Section.Where(n => n.TOCHeading == "Protocol").SelectMany(m => m.Information);
				foreach (var info in protocol)
				{
					protocolValue = info.StringValue;

				}
				return protocolValue;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private List<ItemDetailInList> GetBioAssayTarget(BioAssayDetailList item)
		{
			try
			{
				var bioAssayTargetList = new List<ItemDetailInList>();
				var bioAssayTarget = item.Record.Section.Where(n => n.TOCHeading == "BioAssay Target").SelectMany(m => m.section).SelectMany(o => o.Information);
				foreach (var info in bioAssayTarget)
				{
					ItemDetailInList obj = new ItemDetailInList();
					obj.Label = info.Name;
					obj.Value = info.StringValue;
					bioAssayTargetList.Add(obj);

				}
				return bioAssayTargetList;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private List<ItemDetailInList> GetCategorizedComments(BioAssayDetailList item)
		{
			try
			{
				var categorizedCommentList = new List<ItemDetailInList>();
				var categorizedComment = item.Record.Section.Where(n => n.TOCHeading == "Categorized Comment").SelectMany(m => m.section).SelectMany(o => o.Information);
				foreach (var info in categorizedComment)
				{
					ItemDetailInList obj = new ItemDetailInList();
					obj.Label = info.Name;
					obj.Value = info.StringValue;
					categorizedCommentList.Add(obj);

				}
				return categorizedCommentList;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private List<ItemDetailInList> GetEntrezCrossLinkComments(BioAssayDetailList item)
		{
			try
			{
				var entrezCrosslinkList = new List<ItemDetailInList>();
				var entrezCrosslink = item.Record.Section.Where(n => n.TOCHeading == "Entrez Crosslinks").SelectMany(m => m.section).SelectMany(o => o.Information);
				foreach (var info in entrezCrosslink)
				{
					ItemDetailInList obj = new ItemDetailInList();
					obj.Label = info.Name;
					obj.Value = info.StringValue;
					entrezCrosslinkList.Add(obj);

				}
				return entrezCrosslinkList;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private string GetPrimaryCitation(BioAssayDetailList item)
		{
			try
			{
				string primaryCitation = "";
				var primaryCitationInfo = item.Record.Section.Where(n => n.TOCHeading == "Entrez Crosslinks").SelectMany(m => m.section).Where(p => p.TOCHeading == "Primary Citation").SelectMany(q => q.Information);
				foreach (var info in primaryCitationInfo)
				{
					primaryCitation = info.StringValue;
				}

				return primaryCitation;
			}
			catch (Exception ex)
			{
				return null;
			}
		}


	}
}
