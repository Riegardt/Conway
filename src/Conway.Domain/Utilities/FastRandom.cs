using Conway.Domain.Interfaces;
using System;

namespace Conway.Domain.Utilities
{
	public class FastRandom : Random, IFastRandom
	{
		private uint bits;

		public FastRandom() : base() { }

		public bool NextBoolean()
		{
			bits >>= 1;
			if (bits <= 1)
			{
				bits = CallNext();
			}

			return (bits & 1) == 0;
		}

		internal virtual uint CallNext() => (uint)~Next();
	}
}
