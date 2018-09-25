using System.Collections.Generic;

namespace ChemFinder
{
	public class Descr
	{
		public Aid aid { get; set; }
		public AidSource aid_source { get; set; }
		public string name { get; set; }
		public List<string> description { get; set; }
		public List<string> protocol { get; set; }
		public List<string> comment { get; set; }
		public List<Xref1> xref { get; set; }
		public List<Result> results { get; set; }
		public int revision { get; set; }
		public List<Target> target { get; set; }
		public int activity_outcome_method { get; set; }
		public List<string> grant_number { get; set; }
		public int project_category { get; set; }
		public List<CategorizedComment> categorized_comment { get; set; }
	}
}