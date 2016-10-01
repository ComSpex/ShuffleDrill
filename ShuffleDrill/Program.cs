using Microsoft.VisualBasic;
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
			Console.WriteLine("日本語あきまへんか？\nあきまへん。");
			Console.ForegroundColor=ConsoleColor.Yellow;
			Console.WriteLine("\nIt was because of the font in the command prompt which must be of Raster Fonts, instead of MS Gothic and as well as Language for non-Unicode programs must be Japanese (Japan) in Region Settings.\n");
			Console.ResetColor();
			Fisher_Yates_shuffle();
		}
		static void Fisher_Yates_shuffle() {
			Console.Title="Fisher-Yates shuffle as of 27SEP2016";
			string[] a = HowManyPacks();// { "a","b","c","d","e","a","b","c","f","g","a" };
			Print(a);
			for(int i = 0;i<a.Length-2;++i) {
				int j=RandomInteger(i,a.Length);
				Exchange(ref a[i],ref a[j]);
				Print(a);
			}
			Print(a);
		}
		static void TestEncoding() {
			Console.OutputEncoding=Encoding.Default;
			for(var i = 0;i<=1000;i++) {
				Console.Write(Strings.ChrW(i));
				if(i%50==0) { // break every 50 chars
					Console.WriteLine();
				}
			}
			Console.ReadKey();
		}
	static string[] HowManyPacks() {
			Dictionary<string,int> packs = new Dictionary<string,int>();
			packs.Add("塩",5);
			packs.Add("醤油",3);
			packs.Add("豚骨",2);
			packs.Add("うどん",6);
			packs.Add("鶏",4);
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
			CultureInfo ci = new CultureInfo("ja-JP");
			for(int i = 0;i<a.Length;++i) {
				if(i>0) {
					ConsoleWrite(",");
				}
				ConsoleWrite("{0}",String.Format(ci,a[i]));
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
				DoEvents();
				j=r.Next();
			}
			return j;
		}
		private static void DoEvents() {
			//System.Windows.Forms.Application.DoEvents();
		}
		private static void Exchange(ref string v1,ref string v2) {
			string v3 = v1;
			v1=v2;
			v2=v3;
		}
	}
}
