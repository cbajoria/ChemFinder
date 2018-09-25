using System.Collections.Generic;

namespace ChemFinder
{
	public class Information3
	{
		public int ReferenceNumber { get; set; }
		public string Name { get; set; }
		public string StringValue { get; set; }
		public string URL { get; set; }
		public List<string> StringValueList { get; set; }
		public double? NumValue { get; set; }
		public string ValueUnit { get; set; }
		public string BinaryValue { get; set; }
		public bool? BoolValue { get; set; }
		public string Description { get; set; }
		public List<string> Reference { get; set; }
		public List<double?> NumValueList { get; set; }
		public ExternalTable ExternalTable { get; set; }
	}
}