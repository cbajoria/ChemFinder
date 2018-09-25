namespace ChemFinder
{
	public class Count
	{
		public int heavy_atom { get; set; }
		public int atom_chiral { get; set; }
		public int atom_chiral_def { get; set; }
		public int atom_chiral_undef { get; set; }
		public int bond_chiral { get; set; }
		public int bond_chiral_def { get; set; }
		public int bond_chiral_undef { get; set; }
		public int isotope_atom { get; set; }
		public int covalent_unit { get; set; }
		public int tautomers { get; set; }
	}
}