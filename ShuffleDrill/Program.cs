using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShuffleDrill {
	class Program {
		public static string ラーメンリスト {
			get {
				string home = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string path = Path.Combine(home,"ラーメンリスト.txt");
				return path;
			}
		}
		static void Main(string[] args) {
			Console.WriteLine("日本語あきまへんか？\nあきまへん。");
			Console.ForegroundColor=ConsoleColor.Yellow;
			Console.WriteLine("\nIt was because of the font in the command prompt which must be of Raster Fonts, instead of MS Gothic and as well as Language for non-Unicode programs must be Japanese (Japan) in Region Settings.\n");
			Console.ResetColor();
			string[] a = HowManyPacks();// { "a","b","c","d","e","a","b","c","f","g","a" };
			Print(a);
			//Fisher_Yates_shuffle(a);
			Knuth_shuffle(a);
			WriteToFile(a);
			Print(a);
		}
		static void Fisher_Yates_shuffle(string[] a) {
			Console.Title="Fisher-Yates shuffles as of 27SEP2016";
			for(int i = 0;i<a.Length-2;++i) {
				int j = RandomInteger(i,a.Length);
				Exchange(ref a[i],ref a[j]);
				Print(a);
			}
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
		static void Knuth_shuffle(string[] a) {
			Console.Title="Knuth shuffles as of 01OCT2016";
			for(int i = 0;i<=a.Length-2;i++) {
				int j = uniform(a.Length-i);
				swap(ref a[i],ref a[i+j]);
				Print(a);
			}
		}
		private static void swap(ref string v1,ref string v2) {
			Exchange(ref v1,ref v2);
		}
		private static int uniform(int v) {
			Random r = new Random();
			int m = r.Next();
			while(!(0<=m&&m<=v-1)){
				m=r.Next();
			}
			return m;
		}
		private static void WriteToFile(string[] a) {
			Dictionary<string,int> packs = new Dictionary<string,int>();
			foreach(string name in a) {
				if(!packs.ContainsKey(name)) {
					packs.Add(name,1);
				}else {
					++packs[name];
				}
			}
			using(StreamWriter sw = new StreamWriter(ラーメンリスト)) {
				sw.AutoFlush=true;
				foreach(KeyValuePair<string,int> pack in packs) {
					sw.WriteLine("{0},{1}",pack.Key,pack.Value);
				}
			}
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
		static bool LoadFromFile(Dictionary<string,int> packs) {
			FileInfo file = new FileInfo(ラーメンリスト);
			if(file.Exists) {
				packs.Clear();
				using(StreamReader sr = file.OpenText()) {
					while(!sr.EndOfStream) {
						string line = sr.ReadLine();
						if(String.IsNullOrEmpty(line.Trim())) {
							continue;
						}
						string[] pair = line.Split(',');
						if(pair.Length==2) {
							try {
								packs.Add(pair[0].Trim(),int.Parse(pair[1]));
							}catch(FormatException ex) {
								MessageBox.Show(ex.Message,Console.Title,MessageBoxButtons.OK,MessageBoxIcon.Stop);
							}
						}else if(Regex.IsMatch(line,"^[0-9]+")) {
							packs.Add(line.Trim(),1);
						}
					}
				}
				return true;
			}
			return false;
		}
		static string[] HowManyPacks() {
			Dictionary<string,int> packs = new Dictionary<string,int>();
			if(!LoadFromFile(packs)) {
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
			}
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
