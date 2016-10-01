using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillZipMethod {
	class Program {
		static void Main(string[] args) {
			// Two source arrays.
			var array1 = new int[] { 1,2,3,4,5 };
			var array2 = new int[] { 6,7,8,9,10 };

			// Add elements at each position together.
			var zip = array1.Zip(array2,(a,b) => (a+b));

			// Look at results.
			foreach(var value in zip) {
				Console.WriteLine(value);
			}
			//Result => 7,9,11,13,15
		}
	}
}
