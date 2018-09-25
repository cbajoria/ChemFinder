/*
 Created by : Moni
 Created Date : 22/02/2017
 Purpose :  To get detail of selected compound based on cid
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ChemFinder
{
	public class CompoundDetails : IItemDetails
	{
		public CompoundDetails()
		{
			
		}
		public async Task<Tuple<ObservableCollection<ItemDetailInList>, string>> GetTopSectionData(string searchValue, string searchCategory)

		{
			try
			{
				var searchInput = new PubSearchItem();
				searchInput.SearchValue = searchValue;
				searchInput.SearchCategory = searchCategory;
				//searchInput.OutputFormat = "JSON";
				var url = searchInput.SearchTopDetailUrlPathAndParameters;

				ServiceHelper objHelper = new ServiceHelper();
				string data = await objHelper.GetData(url);
				//var searchData = JsonConvert.DeserializeObject<CompoundsCollection>(data);
				if (data != "")
				{
					var jdata = JObject.Parse(data);
					var items = (jdata["PropertyTable"]["Properties"]) as JArray;
					//Code to get description
					url = searchInput.SearchDescriptionByIdUrlPathAndParameters;
					string descriptions = await objHelper.GetData(url);
					var itemDescription = JObject.Parse(descriptions);
					CollectionGenerator obj = new CollectionGenerator();
					var ItemDetailsInList = obj.GetCompoundPropertyDetail(items, itemDescription, searchCategory);

					return ItemDetailsInList;
				}
				else {

					return new Tuple<ObservableCollection<ItemDetailInList>, string>(new ObservableCollection<ItemDetailInList>(),"");
				}

			}
			catch (Exception ex)
			{

				App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
				return null;
			}
		}


		public async Task<Tuple<Tuple<List<string>,List<ItemDetailInList>,List<ItemDetailInList>,List<ItemDetailInList>,List<string>,List<string>>, Tuple<List<ItemDetailInList>,List<ItemDetailInList>>,List<ItemDetailInList>>>                GetData(string searchValue, string searchCategory) 		{ 			try 			{ 				var searchInput = new PubSearchItem(); 				searchInput.SearchValue = searchValue; 				searchInput.SearchCategory = searchCategory; 				//searchInput.OutputFormat = "JSON"; 				var url = searchInput.SearchDetailUrlPathAndParameters; 				ServiceHelper objHelper = new ServiceHelper(); 				string data = await objHelper.GetData(url);
				if (data != "")
				{
					var jdata = JObject.Parse(data);
					var searchData = JsonConvert.DeserializeObject<CompoundDetailList>(data);

					var item = GetItemDetail(searchData);

					return item;
				}
				else
				{
					return null;
				}  			} 			catch (Exception ex) 			{ 				await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK"); 				return null; 			} 		} 		// Method to fetch different sections of selected compounds
		private Tuple<Tuple<List<string>,List<ItemDetailInList>,List<ItemDetailInList>,List<ItemDetailInList>,List<string>,List<string>>, Tuple<List<ItemDetailInList>,List<ItemDetailInList>>,List<ItemDetailInList>>
		GetItemDetail(CompoundDetailList items)
		{
			try
			{
				var recordDescription = new List<string>();
				var computedDescriptor = new List<ItemDetailInList>();
				var molecularFormula = new List<ItemDetailInList>();
				var otherIdentifier = new List<ItemDetailInList>();
				var meshSynonyms = new List<string>();
				var depositorSuppliedSynonyms = new List<string>();
				var chemicalAndPhysical = new List<ItemDetailInList>();
				var experimentalProperties = new List<ItemDetailInList>();
				var drugAndMedicationInformation = new List<ItemDetailInList>();

				//Name and Identifier Section
				var sections = items.Record.Section.Where(m => m.TOCHeading == "Names and Identifiers").SelectMany(n => n.section);
				if (sections != null)
				{

					recordDescription = GetRecordDescription(sections);

					computedDescriptor = GetComputedDescriptor(sections);

					molecularFormula = GetMolecularFormula(sections);

					otherIdentifier = GetOtherIdentifier(sections);

					meshSynonyms = GetSynonyms(sections).Item1;

					depositorSuppliedSynonyms = GetSynonyms(sections).Item2;
				}

				//Chemical Properties Section
				var chemicalPropertiesSection = items.Record.Section.Where(m => m.TOCHeading == "Chemical and Physical Properties");

				if (chemicalPropertiesSection != null)
				{
					chemicalAndPhysical = GetChemicalProperties(chemicalPropertiesSection).Item1;
					experimentalProperties = GetChemicalProperties(chemicalPropertiesSection).Item2;
				}

				//Drug and medication Section
				var drugAndMedicationSection = items.Record.Section.Where(m => m.TOCHeading == "Drug and Medication Information");
				if (drugAndMedicationSection != null)
				{
					drugAndMedicationInformation = GetDrugAndMedicationInformation(drugAndMedicationSection);
				}

				return  new Tuple<Tuple<List<string>, List<ItemDetailInList>, List<ItemDetailInList>, List<ItemDetailInList>, List<string>, List<string>>, Tuple<List<ItemDetailInList>, List<ItemDetailInList>>, List<ItemDetailInList>>
					(new Tuple<List<string>, List<ItemDetailInList>, List<ItemDetailInList>, List<ItemDetailInList>, List<string>, List<string>>
					 (recordDescription, computedDescriptor, molecularFormula,
					  otherIdentifier, meshSynonyms, depositorSuppliedSynonyms),
					 new Tuple<List<ItemDetailInList>, List<ItemDetailInList>>(chemicalAndPhysical, experimentalProperties), drugAndMedicationInformation);

			}

			catch (Exception ex)
			{
				
				return null;
			}
		}

		private List<string> GetRecordDescription(IEnumerable<Section2> sections)
		{
			try
			{
				var recordDescription = new List<string>();
			
				var record = sections.Where(m => m.TOCHeading == "Record Description").SelectMany(n => n.Information);
				// To remove <a></a> tags use below line
				var descr = record.Where(n=>!n.StringValue.Contains("<a")).Select(n => n.StringValue).ToList();
				//var descr = record.Select(n => n.StringValue).ToList();
				recordDescription.AddRange(descr);
				return recordDescription;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private List<ItemDetailInList> GetComputedDescriptor(IEnumerable<Section2> sections)
		{
			try
			{
				var computedDescriptor = new List<ItemDetailInList>();
				var computedDescr = sections.Where(m => m.TOCHeading == "Computed Descriptors").SelectMany(n => n.Section);
				var computedDescripterSection = computedDescr.SelectMany(n => n.Information);
				foreach (var info in computedDescripterSection)
				{
					ItemDetailInList obj = new ItemDetailInList();
					obj.Label = info.Name;
					obj.Value = info.StringValue;
					computedDescriptor.Add(obj);

				}
				return computedDescriptor;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private List<ItemDetailInList> GetMolecularFormula(IEnumerable<Section2> sections)
		{
			try
			{
				var molecularFormula = new List<ItemDetailInList>();
				var molecularF = sections.Where(m => m.TOCHeading == "Molecular Formula").SelectMany(n => n.Information);
				foreach (var minfo in molecularF)
				{
					ItemDetailInList objm = new ItemDetailInList();
					objm.Label = minfo.Name;
					objm.Value = minfo.StringValue;
					molecularFormula.Add(objm);
				}
				return molecularFormula;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private List<ItemDetailInList> GetOtherIdentifier(IEnumerable<Section2> sections)
		{
			try
			{
				var otherIdentifier = new List<ItemDetailInList>();
				//Other Identifier Section
				var otherId = sections.Where(m => m.TOCHeading == "Other Identifiers").SelectMany(o => o.Section).SelectMany(n => n.Information);
				foreach (var identifier in otherId)
				{
					ItemDetailInList objm = new ItemDetailInList();
					objm.Label = identifier.Name;
					objm.Value = identifier.StringValue;
					otherIdentifier.Add(objm);
				}

				return otherIdentifier;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private Tuple<List<string>,List<string>> GetSynonyms(IEnumerable<Section2> sections)
		{
			try
			{
				var meshSynonyms = new List<string>();
				var depositorSuppliedSynonyms = new List<string>();

				var synonymsSection = sections.Where(m => m.TOCHeading == "Synonyms").SelectMany(o => o.Section);
				if (synonymsSection != null)
				{
					meshSynonyms = sections.Where(m => m.TOCHeading == "Synonyms").SelectMany(o => o.Section).Where(m => m.TOCHeading == "MeSH Synonyms").SelectMany(n => n.Information).SelectMany(p => p.StringValueList).ToList();

					depositorSuppliedSynonyms = sections.Where(m => m.TOCHeading == "Synonyms").SelectMany(o => o.Section).Where(m => m.TOCHeading == "Depositor-Supplied Synonyms").SelectMany(n => n.Information).SelectMany(p => p.StringValueList).Take(30).ToList();
				}

				return new Tuple<List<string>, List<string>>(meshSynonyms,depositorSuppliedSynonyms);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private Tuple<List<ItemDetailInList>,List<ItemDetailInList>> GetChemicalProperties(IEnumerable<Section> chemicalPropertiesSection)
		{
			try
			{
				var chemicalAndPhysical = new List<ItemDetailInList>();
				var experimentalProperties = new List<ItemDetailInList>();

				var chemicalProp = chemicalPropertiesSection.SelectMany(n => n.section).Where(m => m.TOCHeading == "Computed Properties").SelectMany(m => m.Section).SelectMany(o => o.Information);

				foreach (var chem in chemicalProp)
				{
					ItemDetailInList objm = new ItemDetailInList();
					objm.Label = chem.Name;
					objm.Value = chem.NumValue != null ? chem.NumValue.ToString() : "";
					chemicalAndPhysical.Add(objm);
				}


				var expProp = chemicalPropertiesSection.SelectMany(n => n.section).Where(m => m.TOCHeading == "Experimental Properties").SelectMany(m => m.Section).SelectMany(o => o.Information);

				foreach (var exp in expProp)
				{
					ItemDetailInList objm = new ItemDetailInList();
					objm.Label = exp.Name;
					objm.Value = exp.NumValue != null ? exp.NumValue.ToString() : "";
					experimentalProperties.Add(objm);
				}

				return new Tuple<List<ItemDetailInList>, List<ItemDetailInList>>(chemicalAndPhysical,experimentalProperties);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private List<ItemDetailInList> GetDrugAndMedicationInformation(IEnumerable<Section> drugAndMedicationSection)
		{
			try
			{
				var drugAndMedicationInformation = new List<ItemDetailInList>();
				var drugProp = drugAndMedicationSection.SelectMany(n => n.section).Where(m => m.Information != null).SelectMany(o => o.Information);
				string name = "";
				foreach (var drug in drugProp)
				{
					ItemDetailInList objm = new ItemDetailInList();
					if (name == drug.Name)
					{
						objm.Label = "";
					}
					else
					{
						name = drug.Name;
						objm.Label = drug.Name;
						objm.Value = drug.StringValue != null ? drug.StringValue.ToString() : "";
					}
					if (objm.Value != null)
					{
						drugAndMedicationInformation.Add(objm);
					}
				}
				return drugAndMedicationInformation;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
