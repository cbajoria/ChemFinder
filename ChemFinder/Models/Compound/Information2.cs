using System.Collections.Generic;

namespace ChemFinder
{
	public class Information2
	{
		public int ReferenceNumber { get; set; }
		public string Name { get; set; }
		public string StringValue { get; set; }
		public string DateValue { get; set; }
		public string Description { get; set; }
		public List<string> Reference { get; set; }
		public List<string> StringValueList { get; set; }
		public bool? BoolValue { get; set; }
		public string URL { get; set; }
		public int? NumValue { get; set; }
	}
}