using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class Scad : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string In=context.Request.Params["name"];   
   string Plaatje=MakeWebName(Path.GetDirectoryName(In)+"/"+Path.GetFileNameWithoutExtension(In));   
   string Out=context.Server.MapPath("/Db")+"/"+Plaatje+".png";
   if(!File.Exists(Out) || ( File.Exists(Out) && File.GetLastWriteTime(Out)<File.GetLastWriteTime(In))) {
    // Indien plaatje nieuwer is dan Scadbestand dan is het plaatje goed  
    StartCommand(Default.OpenScadExe+" -o '"+Out+"' '"+In+"'");              
   }
  
   context.Response.Write("<img class='w3-image' src='Db/"+Plaatje.Replace(" ","+")+".png' />");
  }
  
  public string MakeWebName(string s) {
   return s.Replace("/","_");
  }
  
  public void StartCommand(string command)  {
   command = command.Replace("\"","\"\"");
   var proc = new Process {
    StartInfo = new ProcessStartInfo {
     FileName = "/bin/bash",
     Arguments = "-c \""+ command + "\"",
     UseShellExecute = false,
     RedirectStandardOutput = true,
     CreateNoWindow = true
    }
   };   
   proc.Start();     
   proc.WaitForExit();   
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
