using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace projbrowser.Ashx {

 public class Pix : System.Web.IHttpHandler {

  public void ProcessRequest(HttpContext context) {
   string Pic=context.Request.Params["name"];   
   var base64= Convert.ToBase64String(File.ReadAllBytes(Pic));
   //   /media/marcel/4TB/Proj/Projecten/ProjBrowser/openscad  -o 'pic.png'  -D param1=\"charlie_back.stl\" convert.scad

  
   if(Pic.EndsWith(".png"))
    context.Response.Write("<img class='w3-image' src='data:image/png;base64,"+base64+"' />");
   else
    context.Response.Write("<img class='w3-image' src='data:image/jpg;base64,"+base64+"' />");
  }

  public bool IsReusable {
   get {
    return false;
   }
  }
 }
}
