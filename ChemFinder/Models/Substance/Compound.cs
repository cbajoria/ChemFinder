using System.Collections.Generic;

namespace ChemFinder
{
	public class Compound
	{
		public Id id { get; set; }
		public Atoms atoms { get; set; }
		public Bonds bonds { get; set; }
		public List<Coord> coords { get; set; }
		public int charge { get; set; }
		public Count count { get; set; }
		public List<Prop> props { get; set; }
		public List<Stereo> stereo { get; set; }
	}
}