using System.Collections.Generic;

namespace ChemFinder
{
	public class ExternalTable
	{
		public string TableName { get; set; }
		public int FullTableNumRows { get; set; }
		public List<string> ColumnName { get; set; }
		public List<PreviewRow> PreviewRow { get; set; }
	}
}