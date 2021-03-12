using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class Tree : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string Root=context.Request.Params["root"];
   if(string.IsNullOrEmpty(Root)) Root = Default.Rootfolder;
   List<string> F=Directory.GetFiles(Root).ToList();
   List<string> D=Directory.GetDirectories(Root).ToList();
   List<Default.Bestand> ret = new List<Default.Bestand>();
   D.Sort();
   F.Sort();
   if(Root!=Default.Rootfolder) {
    D.Insert(0,Root);
   }
   int tel=0;
   foreach(var d in D) {
    if(!Path.GetFileName(d).StartsWith(".")) {    
     Default.Bestand b =new Default.Bestand() {VolledigeNaam=d, 
      Display="<span class='folder'><i class='mdi mdi-folder-outline'></i>&nbsp;"+Path.GetFileName(d)+"</span>", IsFolder=true };
     ret.Add(b);
         
     if(b.VolledigeNaam==Root && tel==0) {
      b.IsUp=true;
      b.Display="<span class='folder'><i class='mdi mdi-folder-upload-outline'></i>&nbsp;"+Path.GetFileName(d)+"</span>";
      int lastSlash =  b.VolledigeNaam.LastIndexOf('/');
       b.VolledigeNaam = (lastSlash > -1) ?  b.VolledigeNaam.Substring(0, lastSlash) :  b.VolledigeNaam;     
     }      
     tel++;
     //if(Default.BelangrijkeFolders.Contains(Path.GetFileName(d)))
     // b.Display="<span class='belangrijk'>"+b.Display+"</span>";    
    }
   }
      
   foreach(var f in F) {
    if(!Path.GetFileName(f).StartsWith(".")) {
     Default.Bestand b = new Default.Bestand() {VolledigeNaam=f, Display=Path.GetFileName(f), IsFolder=false };     
     ret.Add(b);
    }
   }//"data-folder" : "data-file" %> data-naam='<%#Eval("VolledigeNaam") %>'><%#Eval("Display") %></a>
         
   string s="";
   foreach(var b in ret) {   
    string tClass = "";
    //if(!b.IsFolder) {
    // tClass=Default.Taal(Path.GetExtension(b.VolledigeNaam));
    // if(tClass=="") tClass="notclickable";
   // }
    s += string.Format("<a href='#' class='{3}' {2} data-name='{0}'>{1}</a>",b.VolledigeNaam, 
     b.Display, b.IsFolder? "data-folder":"data-file",
     tClass);
   }
   context.Response.Write( s );
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
