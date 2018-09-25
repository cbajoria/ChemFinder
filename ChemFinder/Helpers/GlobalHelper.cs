using System;
using System.Threading;

namespace ChemFinder
{
	public static class GlobalHelper
	{
		public static string Category = CategoryEnum.Compound.ToString();

		public static CancellationTokenSource cts = new CancellationTokenSource();
		public static CancellationToken token = cts.Token;
	}
}
