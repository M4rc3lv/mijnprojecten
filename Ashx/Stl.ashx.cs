using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class Stl : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string In=context.Request.Params["name"];
   
   if(!File.Exists(In)) {
    // Nodig voor Octopi STL Greasemonkeyscript
    context.Response.Write("ERROR");
    return;
   }
      
   string Plaatje=MakeWebName(Path.GetDirectoryName(In)+"/"+Path.GetFileNameWithoutExtension(In));   
   string Out=context.Server.MapPath("/Db")+"/"+Plaatje+".stl.png";      
   if(!File.Exists(Out) || ( File.Exists(Out) && File.GetLastWriteTime(Out)<File.GetLastWriteTime(In))) {
    // Indien plaatje ouder is dan STL-bestand dan opnieuw genereen
    string TempScadFile = context.Server.MapPath("/Db")+"/"+Plaatje+".scad";
    File.WriteAllText(TempScadFile,"import(\""+In+"\");");
    DoCommand(Default.OpenScadExe+" --colorscheme Tomorrow  -o '"+Out+"' '"+TempScadFile+"'");
   }
   
   //   /media/marcel/4TB/Proj/Projecten/ProjBrowser/openscad  -o 'pic.png'  -D param1=\"charlie_back.stl\" convert.scad

  
   context.Response.Write("<img class='w3-image' src='Db/"+Plaatje.Replace(" ","+")+".stl.png' />");
  }
  
  public string MakeWebName(string s) {
   return s.Replace("/","_");
  }
  
  public void DoCommand(string command)  {
   command = command.Replace("\"","\"\"");
   //command = command.Replace("(",@"\(");
   //command = command.Replace(")",@"\)");
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
