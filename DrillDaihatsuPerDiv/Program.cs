using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Access;

namespace DrillDaihatsuPerDiv {
	class Program {
		static void Main(string[] args) {
			DaihatsuShaffle d = new DaihatsuShaffle();
			d.Test(Console.Out);
			string accdb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"PerDiv.accdb");
			PrintTimeStamp(accdb);
			DaihatsuShaffle.Report(Console.Out,"-----");
			Application app = new Application();
			OptimizeDatabase(accdb,app);
			PrintTimeStamp(accdb);
		}
		private static void PrintTimeStamp(string accdb) {
			FileInfo access = new FileInfo(accdb);
			//DaihatsuShaffle.Report(Console.Out,"{0:O}",access.LastAccessTime);
			//DaihatsuShaffle.Report(Console.Out,"{0:O}",access.CreationTime);
			DaihatsuShaffle.Report(Console.Out,"{0:O}",access.LastWriteTime);
		}

		public static void OptimizeDatabase(string accessFile,Application app) {
			string tempFile = Path.Combine(Path.GetDirectoryName(accessFile),Path.GetRandomFileName()+Path.GetExtension(accessFile));
			app.CompactRepair(accessFile,tempFile,false);
			app.Visible=false;
			FileInfo temp = new FileInfo(tempFile);
			temp.CopyTo(accessFile,true);
			temp.Delete();
		}
	}
	public class DaihatsuShaffle {
		public void CalcRatio() {
		}
		public void Test(TextWriter tw) {
			double a = 1500;
			double b = 284;
			double aout, bout;
			aout=bout=0;
			GetRatio(a,b,ref aout,ref bout);
			Report(tw,"Ratio of {2}:{3} is {0}:{1}.",aout,bout,a,b);
		}
		static public void Report(TextWriter tw,string format,params object[] args) {
			string text = String.Format(format,args);
			tw.WriteLine(text);
		}
		virtual protected void GetRatio(double a,double b,ref double aout,ref double bout,bool no_paren=false) {
			double SumA = a;
			double SumB = b;
			for(double aGCM = GCM(a,b);aGCM>1.0;aGCM=GCM(a,b)) {
				a/=aGCM;
				b/=aGCM;
			}
			b=Math.Max(0,b);
			a=Math.Max(0,a);
			double innerB, innerA;
			innerA=innerB=0;
			// a:bの比率
			if(SumA==0&&SumB==0) {
				aout=bout=0;
			}else if(SumA>SumB) {
				innerB=1;
				innerA=Div(a,b);
				if(b==0) {
					innerB=0;
					a=innerA=10;
				}
			}else {
				innerA=1;
				innerB=Div(b,a);
				if(a==0) {
					innerA=0;
					b=innerB=10;
				}
			}
			aout=innerA;// Convert.ToInt32(innerA);
			bout=innerB;// Convert.ToInt32(innerB);
		}
		private int Div(int a,int b) {
			return Convert.ToInt32(Div((double)a,(double)b));
		}
		private double Div(double a,double b) {
#if false
			return Math.Max(1,Math.Floor(a/b)+0.5);
#else
			return Math.Max(1,Math.Round(a/b,3));
#endif
		}
		private double GCM(double a,double b) {
			for(double i = Math.Min(a,b);i>2.0;i-=1.0) {
				if(Math.IEEERemainder(a,i)==0&&Math.IEEERemainder(b,i)==0) {
					return i;
				}
			}
			return 1;
		}
	}
}
