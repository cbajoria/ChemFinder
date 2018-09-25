using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ChemFinder
{
	public class CollectionGenerator
	{
		public CollectionGenerator()
		{
		}

		ObservableCollection<ItemDetailList> ItemDetailsInList;
		ObservableCollection<ItemDetailInList> ItemDetails;
		// Function to create collection of label and value from Json to bind with listview
		public ObservableCollection<ItemDetailList> GetProperty(JArray items, JObject title, string category)
		{

			try
			{
				ItemDetailsInList = new ObservableCollection<ItemDetailList>();
				List<string> listOfProperties = new List<string>();
				listOfProperties = PropertyListHelper.GetPropertyList(category);
				foreach (var item in items)
				{
					ItemDetailList i = new ItemDetailList();
					string chemName = "";
					foreach (string val in listOfProperties)
					{


						if (item[val] != null)
						{
							ItemDetailInList idl = new ItemDetailInList();
							idl.Label = val;
							idl.Value = item[val].ToString();
							i.Add(idl);

							if (val == PropertyListHelper.IdName)
							{
								i.ChemId = item[val].ToString();

							}
							if (title != null)
							{
								if (val.Contains(PropertyListHelper.IdName))
								{
									chemName = GetTitle(val, title, item);
									if (chemName != "")
									{
										i.ChemName = chemName;
									}
								}
							}
						}
					}

					i.Imageurl2D = Get2DImage(i.ChemId, category, true);
					ItemDetailsInList.Add(i);
				}

				return ItemDetailsInList;
			}
			catch (Exception ex)
			{
				return null;
			}
		}



		public Tuple<ObservableCollection<ItemDetailInList> ,string> GetCompoundPropertyDetail(JArray items, JObject title, string category)
		{
			ItemDetails = new ObservableCollection<ItemDetailInList>();
			List<string> listOfProperties = new List<string>();
			listOfProperties = PropertyListHelper.GetPropertyList(category);
			//int count = items.Count;
			string chemName = "";
			foreach (var item in items)
			{
				
				foreach (string val in listOfProperties)
				{


					if (item[val] != null)
					{
						ItemDetailInList idl = new ItemDetailInList();
						idl.Label = val;
						idl.Value = item[val].ToString();
						ItemDetails.Add(idl);
					}
					if (title != null)
					{
						if (val.Contains(PropertyListHelper.IdName))
						{
							chemName = GetTitle(val, title, item);
						}
					}

				}
			}
			return new Tuple<ObservableCollection<ItemDetailInList>, string>(ItemDetails,chemName);
		}

		// Function to fetch the title of item on the basis of CID
		public string GetTitle(string val, JObject title, JToken item)
		{
			string chemTitle = "";
			//if (val.Contains(PropertyListHelper.IdName))
			//{
				//var titleItem = title.Where(n => title["CID"] == item[val]).Select(n=>title["Title"]).FirstOrDefault();

				JObject match = title["InformationList"]["Information"].Values<JObject>().Where(m => m[PropertyListHelper.IdName].Value<string>() == item[val].ToString()).FirstOrDefault();
				chemTitle = match["Title"].ToString();

			//}
			return chemTitle;
		}
		// Function to generate url for 2D image
		public string Get2DImage(string searchValue, string searchCategory, bool small)
		{
			try
			{
				string url = "";
				if (searchCategory == CategoryEnum.BioAssay.ToString())
				{
					url = "";
				}
				else
				{
					var searchInput = new PubSearchItem();
					searchInput.SearchValue = searchValue;
					searchInput.SearchCategory = searchCategory;
					if (small)
					{
						searchInput.ImageSize = "s";
					}
					else
					{
						searchInput.ImageSize = "l";
					}
					//searchInput.OutputFormat = "JSON";
					url = searchInput.Search2dImageUrlPathAndParameters;
				}

				return url;

			}
			catch (Exception ex)
			{
				 App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
				return null;
			}
		}
		//  Function to create collection of label and value for substance
		public ObservableCollection<ItemDetailList> CreateSubstanceNameValueList(SubstanceList substancelst, string searchCategory)
		{

			ItemDetailsInList = new ObservableCollection<ItemDetailList>();
			CollectionGenerator objCollection = new CollectionGenerator();
			foreach (PCSubstance pcSubstance in substancelst.PC_Substances)
			{
				ItemDetailList i = new ItemDetailList();
				ItemDetailInList idl = new ItemDetailInList();
				idl.Label = "SID";
				idl.Value = pcSubstance.sid.id.ToString();
				i.Add(idl);

				ItemDetailInList idl1 = new ItemDetailInList();
				idl1.Label = "Source";
				idl1.Value = pcSubstance.source.db.name + "," + pcSubstance.source.db.source_id.str;
				i.Add(idl1);

				i.ChemId = pcSubstance.sid.id.ToString();
				string chemName = "";
				if (pcSubstance.synonyms != null)
				{
					foreach (string synonym in pcSubstance.synonyms)
					{
						chemName += synonym.ToString() + ";";
					}
				}
				chemName = chemName.Length > 0 ? chemName.Substring(0, chemName.Length - 1) : chemName;
				i.ChemName = chemName;

				ItemDetailInList idl3 = new ItemDetailInList();
				var cid = pcSubstance.compound.Where(n => n.id.id != null).Select(n => n.id.id.cid).FirstOrDefault();
				idl3.Label = "CID";
				idl3.Value = cid.ToString();
				i.Add(idl3);

				i.Imageurl2D = objCollection.Get2DImage(i.ChemId, searchCategory, true);

				ItemDetailsInList.Add(i);

			}
			return ItemDetailsInList;
		}


		public Tuple<ObservableCollection<ItemDetailInList>,string> GetSubstancePropertyDetail(SubstanceList substancelst, string searchCategory)
		{
			string ChemName = "";
			ItemDetails = new ObservableCollection<ItemDetailInList>();
			CollectionGenerator objCollection = new CollectionGenerator();
			foreach (PCSubstance pcSubstance in substancelst.PC_Substances)
			{
				//ItemDetailList i = new ItemDetailList();
				ItemDetailInList idl = new ItemDetailInList();
				idl.Label = "SID";
				idl.Value = pcSubstance.sid.id.ToString();
				ItemDetails.Add(idl);

				ItemDetailInList idl1 = new ItemDetailInList();
				idl1.Label = "Source";
				idl1.Value = pcSubstance.source.db.name + "," + pcSubstance.source.db.source_id.str;
				ItemDetails.Add(idl1);

				//i.ChemId = pcSubstance.sid.id.ToString();
				string chemName = "";
				if (pcSubstance.synonyms != null)
				{
					foreach (string synonym in pcSubstance.synonyms)
					{
						chemName += synonym.ToString() + ";";
					}
				}
				chemName = chemName.Length > 0 ? chemName.Substring(0, chemName.Length - 1) : chemName;
				ChemName = chemName;

				ItemDetailInList idl3 = new ItemDetailInList();
				var cid = pcSubstance.compound.Where(n => n.id.id != null).Select(n => n.id.id.cid).FirstOrDefault();
				idl3.Label = "CID";
				idl3.Value = cid.ToString();
				ItemDetails.Add(idl3);

				//i.Imageurl2D = objCollection.Get2DImage(i.ChemId, searchCategory, true);

				//ItemDetailsInList.Add(i);

			}
			return new Tuple<ObservableCollection<ItemDetailInList>, string>(ItemDetails,ChemName);
		}



		//  Function to create collection of label and value for BioAssay
		public ObservableCollection<ItemDetailList> CreateBioAssayNameValueList(BioAssayList bioAssayList, string searchCategory)
		{
			ItemDetailsInList = new ObservableCollection<ItemDetailList>();
			CollectionGenerator objCollection = new CollectionGenerator();
			foreach (PCAssayContainer pcAssay in bioAssayList.PC_AssayContainer)
			{
				ItemDetailList i = new ItemDetailList();
				ItemDetailInList idl = new ItemDetailInList();
				idl.Label = "AID";
				idl.Value = pcAssay.assay.descr.aid.id.ToString();
				i.Add(idl);


				if (pcAssay.assay.descr.target!=null)
				{
					ItemDetailInList idl1 = new ItemDetailInList();
					idl1.Label = "Protein Targets";
					string targetName = "";
					int count = pcAssay.assay.descr.target.Count;
					foreach (var target in pcAssay.assay.descr.target)
					{
						targetName += target.name + "; ";
					}

					targetName = targetName.Length > 0 ? targetName.Substring(0, targetName.Length - 2) : targetName;
					idl1.Value = targetName;
					i.Add(idl1);

					ItemDetailInList idl3 = new ItemDetailInList();

					idl3.Label = "Total targets";
					idl3.Value = count.ToString();
					i.Add(idl3);
				}


				i.ChemId = pcAssay.assay.descr.aid.id.ToString();
				string chemName = "";

				chemName = pcAssay.assay.descr.name.ToString();
				i.ChemName = chemName;



				ItemDetailInList idl4 = new ItemDetailInList();

				idl4.Label = "Name";
				idl4.Value = pcAssay.assay.descr.name;
				i.Add(idl4);

				ItemDetailInList idl5 = new ItemDetailInList();

				idl5.Label = "Description";
				idl5.Value = pcAssay.assay.descr.description.Count > 0 ? pcAssay.assay.descr.description[0].ToString() : "";
				i.Add(idl5);

				ItemDetailInList idl6 = new ItemDetailInList();

				idl6.Label = "Protocol";
				idl6.Value = pcAssay.assay.descr.protocol.Count>0? pcAssay.assay.descr.protocol[0].ToString():"";
				i.Add(idl6);

				// image not required for BioAssays
				i.Imageurl2D = "";
				i.IsImageVisible = false;

				ItemDetailsInList.Add(i);

			}
			return ItemDetailsInList;
		}



		public Tuple<ObservableCollection<ItemDetailInList>, string> GetBioAssayPropertyDetail(BioAssayList bioAssayList, string searchCategory)
		{
			string chemName = "";
			ItemDetails = new ObservableCollection<ItemDetailInList>();
			CollectionGenerator objCollection = new CollectionGenerator();
			foreach (PCAssayContainer pcAssay in bioAssayList.PC_AssayContainer)
			{
				//ItemDetailList i = new ItemDetailList();
				ItemDetailInList idl = new ItemDetailInList();
				idl.Label = "AID";
				idl.Value = pcAssay.assay.descr.aid.id.ToString();
			ItemDetails.Add(idl);


				if (pcAssay.assay.descr.target != null)
				{
					ItemDetailInList idl1 = new ItemDetailInList();
					idl1.Label = "Protein Targets";
					string targetName = "";
					int count = pcAssay.assay.descr.target.Count;
					foreach (var target in pcAssay.assay.descr.target)
					{
						targetName += target.name + "; ";
					}

					targetName = targetName.Length > 0 ? targetName.Substring(0, targetName.Length - 2) : targetName;
					idl1.Value = targetName;
					ItemDetails.Add(idl1);

					ItemDetailInList idl3 = new ItemDetailInList();

					idl3.Label = "Total targets";
					idl3.Value = count.ToString();
					ItemDetails.Add(idl3);
				}


				//i.ChemId = pcAssay.assay.descr.aid.id.ToString();


				chemName = pcAssay.assay.descr.name.ToString();
				//i.ChemName = chemName;



				ItemDetailInList idl4 = new ItemDetailInList();

				idl4.Label = "Name";
				idl4.Value = pcAssay.assay.descr.name;
				ItemDetails.Add(idl4);

				ItemDetailInList idl5 = new ItemDetailInList();

				idl5.Label = "Description";
				idl5.Value = pcAssay.assay.descr.description.Count > 0 ? pcAssay.assay.descr.description[0].ToString() : "";
				ItemDetails.Add(idl5);

				ItemDetailInList idl6 = new ItemDetailInList();

				idl6.Label = "Protocol";
				idl6.Value = pcAssay.assay.descr.protocol.Count > 0 ? pcAssay.assay.descr.protocol[0].ToString() : "";
				ItemDetails.Add(idl6);

				// image not required for BioAssays
				//i.Imageurl2D = "";
				//i.IsImageVisible = false;

				//ItemDetailsInList.Add(i);

			}
			return new Tuple<ObservableCollection<ItemDetailInList>, string>(ItemDetails,chemName);
		}
	}
}
