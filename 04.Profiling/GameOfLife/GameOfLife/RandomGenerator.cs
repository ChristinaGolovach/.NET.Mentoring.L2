using System;

namespace GameOfLife
{
	public class RandomGenerator : Random
	{
		private uint _boolBits;

		public RandomGenerator() : base() { }
		public RandomGenerator(int seed) : base(seed) { }

		public bool NextBoolean()
		{
			_boolBits >>= 1;
			if (_boolBits <= 1) _boolBits = (uint)~this.Next();
			return (_boolBits & 1) == 0;
		}
	}
}
