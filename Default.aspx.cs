using System;
using System.Web;
using System.Web.UI;

namespace projbrowser {

 public partial class Default : System.Web.UI.Page {
 
  public static readonly string Rootfolder = "/media/marcel/4TB/Proj";
  public static readonly string OpenScadExe = "/media/marcel/4TB/Proj/Projecten/ProjBrowser/openscad";
  public static readonly int PageSize=10;
  
  public static readonly string[] Talen= {
   "cpp|h","cpp",
   "ino|scad","clike",
   "js|ts","js",
   "css","css",
   "cs","cs",   
   "html|xml|svg|ssml|config","html",  
   "asax|master|aspx","aspnet",
   "make","cmake",
   "sh|powershell","powershell",
   "wiki","wiki",
   "vb","vb",
   "sql","sql",
   "txt","txt",
   "sass","sass",
   "csproj|sln|txt","txt",
   "scss","scss",
   "rb","rb","rust","rust","py","py",
   "yaml|yml","yml",
   "kotlin","kotlin",
   "md","md",
   "perl","perl",
   "git","git",
   "php","php",
   "go","go",
   "graphql","graphql",
   "java","java",
   "json","json",
   "jsonp","jsonp",
  };
   
  public class Bestand {
   public string VolledigeNaam {get;set; }
   public string Display {get;set; }
   public bool IsFolder {get;set; }
   public bool IsUp {get;set; }
  }
  
  public static string Taal(string extensie) {
   if(extensie.StartsWith(".") && extensie.Length>=2) extensie=extensie.Substring(1);
   for(int i=0; i<Talen.Length; i+=2) {
    string[] t = Talen[i].Split(new char[]{'|' },StringSplitOptions.RemoveEmptyEntries);
    for(int j=0; j<t.Length; j++)
     if(t[j]==extensie.ToLower()) return Talen[i+1];
   }
   return "";
  }

 }
}
