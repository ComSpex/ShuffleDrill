using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace ShuffleDrill {
	class Program {
		static void Main(string[] args) {
			Console.Title="Fisher-Yates shuffle";
			string[] a = HowManyPacks();// { "a","b","c","d","e","a","b","c","f","g","a" };
			Print(a);
			for(int i = 0;i<a.Length-2;++i) {
				int j=RandomInteger(i,a.Length);
				Exchange(ref a[i],ref a[j]);
				Print(a);
			}
			Print(a);
		}
		static string[] HowManyPacks() {
			Dictionary<string,int> packs = new Dictionary<string,int>();
			packs.Add("塩",6);
			packs.Add("醤油",4);
			packs.Add("豚骨",2);
			packs.Add("うどん",5);
			packs.Add("鶏",5);
			packs.Add("沖縄",3);
			packs.Add("そば",2);
			packs.Add("味噌",2);
			packs.Add("バリカタ",2);
			packs.Add("焼きチキン",1);
			packs.Add("函館",1);
			using(StringWriter sw = new StringWriter()) {
				int p = 0;
				foreach(KeyValuePair<string,int> pack in packs) {
					string name = pack.Key;
					int pieces = pack.Value;
					for(int i = 0;i<pieces;++i) {
						if(i>0) {
							sw.Write(",");
						}
						sw.Write("{0}",name);
					}
					++p;
					if(p<packs.Count) {
						sw.Write(",");
					}
				}
				string str = sw.ToString();
				string[] names = str.Split(',');
				return names;
			}
		}
		private static void Print(string[] a) {
			for(int i = 0;i<a.Length;++i) {
				if(i>0) {
					ConsoleWrite(",");
				}
				ConsoleWrite("{0}",a[i]);
			}
			ConsoleWriteLine("");
		}
		static void ConsoleWrite(string format,params object[] args) {
			string text = String.Format(format,args);
			Console.Write(text);
			Debug.Write(text);
		}
		static void ConsoleWriteLine(string format,params object[] args) {
			string text = String.Format(format,args);
			Console.WriteLine(text);
			Debug.WriteLine(text);
		}
		private static int RandomInteger(int i,int length) {
			Random r = new Random();
			int j = r.Next();
			while(!(i<=j&&j<length)) {
				j=r.Next();
			}
			return j;
		}
		private static void Exchange(ref string v1,ref string v2) {
			string v3 = v1;
			v1=v2;
			v2=v3;
		}
	}
}
