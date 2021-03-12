using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class Files : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string Root=context.Request.Params["root"];   
   if(string.IsNullOrEmpty(Root)) Root = Default.Rootfolder;
   string Type=context.Request.Params["type"],ret="";   
     
   if(Root==Default.Rootfolder) {
    context.Response.Write("");
    return;
   }   
   
   if(Type=="Scad") {
    List<string> F=Directory.GetFiles(Root,"*.scad",SearchOption.AllDirectories).ToList();       
    int offset = Convert.ToInt32(context.Request.Params["offset"]);
    F.Sort();           
    for(int i=offset; i<Math.Min(offset+Default.PageSize,F.Count); i++) {   
     var f=F[i];
     string f2=f.Replace("'","\\'");
     ret += string.Format("<div class='scad2' data-done='0' data-name='{0}'>{1}</div>\n",f2,Path.GetFileName(f2));          
    } 
    if(offset+Default.PageSize<F.Count) {
     // Er is nog meer!
     ret += string.Format("<div class='LaadMeer' data-root='{0}' data-type='Scad' data-offset='{1}'>Bezig met meer te laden...</div>",
      Root, offset+Default.PageSize
     );
    }  
   }
   else if(Type=="Stl") {
    List<string> F=Directory.GetFiles(Root,"*.stl",SearchOption.AllDirectories).ToList();       
    int offset = Convert.ToInt32(context.Request.Params["offset"]);
    F.Sort();           
    for(int i=offset; i<Math.Min(offset+Default.PageSize,F.Count); i++) {   
     var f=F[i];
     string f2=f.Replace("'","\\'");
     ret += string.Format("<div class='stl2' data-done='0' data-name='{0}'>{1}</div>\n",f2,Path.GetFileName(f2));          
    }   
    if(offset+Default.PageSize<F.Count) {
     // Er is nog meer!
     ret += string.Format("<div class='LaadMeer' data-root='{0}' data-type='Stl' data-offset='{1}'>Bezig met meer te laden...</div>",
      Root, offset+Default.PageSize
     );
    }
   }
   else if(Type=="Pix") {
    List<string> F= Directory.GetFiles(Root,"*.png",SearchOption.AllDirectories).ToList();  
    F.AddRange(Directory.GetFiles(Root,"*.jpg",SearchOption.AllDirectories));
    int offset = Convert.ToInt32(context.Request.Params["offset"]);
    F.Sort();           
    for(int i=offset; i<Math.Min(offset+Default.PageSize,F.Count); i++) {   
     var f=F[i];
     string f2=f.Replace("'","\\'");
     ret += string.Format("<div class='pix2' data-done='0' data-name='{0}'>{1}</div>\n",f2,Path.GetFileName(f2));          
    }
    if(offset+Default.PageSize<F.Count) {
     // Er is nog meer!
     ret += string.Format("<div class='LaadMeer' data-root='{0}' data-type='Pix' data-offset='{1}'>Bezig met meer te laden...</div>",
      Root, offset+Default.PageSize
     );
    }
   }
   else {
    List<string> F=Directory.GetFiles(Root).ToList();       
    F.Sort();
    for(int i=0; i<F.Count; i++) {   
     var f=F[i];
     string f2=f.Replace("'","\\'");
     ret += string.Format("<div class='' data-done='0' data-name='{0}'>{1}</div>\n",f2,Path.GetFileName(f2));          
    }
   }
   
   context.Response.Write(ret);
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
