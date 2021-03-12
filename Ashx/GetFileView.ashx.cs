using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class GetFileView : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string Bestand=context.Request.Params["name"]; 
   string Taal = Default.Taal(Path.GetExtension(Bestand));
   string Inhoud;
   long Len=new FileInfo(Bestand).Length;
   if(Len>5*1024*1024) Inhoud="Bestand te groot ("+Math.Round(Len/1024M/1024M)+"MB). Max 5MB toegestaan.";
   else Inhoud=File.ReadAllText(Bestand);
   string ret="<pre data-name='"+Bestand+"' class='line-numbers'><code class='language-"+Taal+"'>"+
    Inhoud+
    "</code></pre>";
   context.Response.Write(ret);
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
