using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleDrill {
	public class JapaneseProvider:EncodingProvider {
		public JapaneseProvider() : base() {
		}
		public override Encoding GetEncoding(int codepage) {
			return Encoding.GetEncoding(codepage);
		}
		public override Encoding GetEncoding(string name) {
			Encoding jp;
			return Encoding.GetEncoding(name);
		}
	}
}
