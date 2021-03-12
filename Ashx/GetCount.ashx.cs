using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class GetCount : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string Root=context.Request.Params["root"];
   if(string.IsNullOrEmpty(Root)) Root = Default.Rootfolder;
   string sn="";
   // Bepaal aantal bestanden in folders
   int n=Directory.GetFiles(Root,"*",SearchOption.AllDirectories).Length;     
   int nScad=Directory.GetFiles(Root,"*.scad",SearchOption.AllDirectories).Length;     
   int nSTL=Directory.GetFiles(Root,"*.stl",SearchOption.AllDirectories).Length;     
   int nPix=Directory.GetFiles(Root,"*.png*",SearchOption.AllDirectories).Length +
    Directory.GetFiles(Root,"*.jpg*",SearchOption.AllDirectories).Length;
      
   n-=nSTL-nScad-nPix;
   sn="<span title='"+nPix+" afbeeldingen' class='muis fnPix'>"+nPix+"</span>"+
    " <span title='"+nScad+" OpenScad-bestanden' class='muis fnScad'>"+nScad+"</span>"+
    " <span title='"+nSTL+" STL-bestanden' class='muis fnSTL'>"+nSTL+"</span>"+
    " <span title='"+n+" overige bestanden' class='muis'>"+n+"</span>";
   context.Response.Write( sn );
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
