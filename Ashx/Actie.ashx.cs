using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class Actie : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string Actie = context.Request.Params["actie"];
   string Bestand = context.Request.Params["file"];
   switch(Actie) {
    case "scad":
     StartScad(Bestand);
     break;
    case "nemo":
     StartNemo(Path.GetDirectoryName(Bestand));
     break;     
   }   
  }
  
   public void StartScad(string fname)  {  
   var proc = new Process {
    StartInfo = new ProcessStartInfo {
     FileName = Default.OpenScadExe,
     Arguments = "\""+ fname + "\"",
     UseShellExecute = false,
     RedirectStandardOutput = true,
     CreateNoWindow = false
    }
   };   
   proc.Start();       
  }
  
  public void StartNemo(string folder)  {  
   var proc = new Process {
    StartInfo = new ProcessStartInfo {
     FileName = "nemo",
     Arguments = "\""+ folder + "\"",
     UseShellExecute = false,
     RedirectStandardOutput = true,
     CreateNoWindow = false
    }
   };   
   proc.Start();       
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
