$(function(){

 var MODE="",CURRROOT="";
  
 SetMode(localStorage.getItem('MODE')? localStorage.getItem('MODE'): "ModeFiles");
 LoadTree(localStorage.getItem('CURRROOT')? localStorage.getItem('CURRROOT'): "");
 
 if(MODE=="ModeFile" && localStorage.getItem('LASTFILE')) {
  SetMode("ModeFile");
  ShowFile(localStorage.getItem('LASTFILE'));
 }
 
 var OSTOOLS="<div class='ostools'><i title='Kopieer bestandnaam' class='CmdCopy mdi mdi-content-copy'></i>&nbsp;"+
  "<i title='Open folder in Nemo'  class='CmdNemo mdi mdi-file-cabinet'></i>&nbsp;"+
  "<i title='Bewerk in OpenScad'  class='CmdScad mdi mdi-pencil'></i>"+
  "</div>";
   
 
 function SetMode(M) { 
  $("i").removeClass("active");
  $("#"+M).addClass("active");
  MODE=M;
  LoadFilesPane();
  localStorage.setItem('MODE', M);
  
  if($("#ModeScad").hasClass("active")) $("#ModeScad").addClass("fnScad"); else $("#ModeScad").removeClass("fnScad");
  if($("#ModeFile").hasClass("active")) $("#ModeScad").addClass("fnFile"); else $("#ModeFile").removeClass("fnFile");
  if($("#ModeSTL").hasClass("active")) $("#ModeSTL").addClass("fnSTL"); else $("#ModeSTL").removeClass("fnSTL");
  if($("#ModePix").hasClass("active")) $("#ModePix").addClass("fnPix"); else $("#ModePix").removeClass("fnPix");  
 }
 
 $("#treeicons i").click(function(){
  SetMode($(this).attr("id"));
 });
 
 $("#tree").on("click","a[data-folder='']",function(){     
  LoadTree($(this).attr("data-name"));
 }); 
 
 // Acties op 1 bestand  
 $(".w3-rest").on("click",".CmdCopy",function(){
  if($(this).parent().parent(".scad2").length)
   navigator.clipboard.writeText($(this).parents(".scad2").attr("data-name"));
  else if($(this).parent().parent(".stl2").length)
   navigator.clipboard.writeText($(this).parents(".stl2").attr("data-name"));
  else if($(this).parent().parent(".pix2").length)
   navigator.clipboard.writeText($(this).parents(".pix2").attr("data-name"));
 }).on("click",".CmdNemo",function() { 
   if($(this).parent().parent(".scad2").length) {
    $(this).load("/Ashx/Actie.ashx?actie=nemo&file="+encodeURIComponent($(this).parent().parent(".scad2").attr("data-name")) );
   }
   else if($(this).parent().parent(".stl2").length) {    
    $(this).load("/Ashx/Actie.ashx?actie=nemo&file="+encodeURIComponent($(this).parent().parent(".stl2").attr("data-name")) );
   }
   else if($(this).parent().parent(".pix2").length) {    
    $(this).load("/Ashx/Actie.ashx?actie=nemo&file="+encodeURIComponent($(this).parent().parent(".pix2").attr("data-name")) );
   }
 }).on("click",".CmdScad",function(){
   if($(this).parents(".scad2"))
    $(this).load("/Ashx/Actie.ashx?actie=scad&file="+encodeURIComponent($(this).parents(".scad2").attr("data-name")) );   
 });
 
 function LoadTree(root) {  
  CURRROOT=root;
  localStorage.setItem('CURRROOT', root);
  $(".pad").text(CURRROOT);
  $("#spinnerviewer").show();
  $("#spinnerviewerT").show();
  $("#tree").load("/Ashx/Tree.ashx?root="+encodeURIComponent(root),function(){
   $("#spinnerviewerT").hide();
   LoadFilesPane();       
  }); 
  $("#toolbar2").text(root); 
  $("#count").load("/Ashx/GetCount.ashx?root="+encodeURIComponent(root)); 
 } 
 
 function ShowFile(TheFile) {
  var fname=TheFile;
  SetMode("ModeFile");
  $("#bestandsnaam").html("Laden "+fname);    
  $("#bestandsviewer").load("/Ashx/GetFileView.ashx?name="+encodeURIComponent(fname),function(){
   var f=$(this).children("pre").attr("data-name");
   f = f.substring(f.lastIndexOf('/')+1);
   // = f.substring(0,f.lastIndexOf('.'));
   $("#bestandsnaam").html("<span class=fnFile><i class='mdi mdi-text-box-outline'></i>&nbsp;"+f+"</span>");
   localStorage.setItem('LASTFILE', $(this).children("pre").attr("data-name"));
   Prism.highlightAll(true);
  });   
 }
 
 $("#tree").on("click","a[data-file]",function(){
  ShowFile($(this).attr("data-name"));
 });
 
 $(window).on('DOMContentLoaded load resize scroll', function(){
  if($(".LaadMeer").length>0) {
   var bounding = $(".LaadMeer")[0].getBoundingClientRect();
    var hT = $('.LaadMeer').offset().top,
       hH = $('.LaadMeer').outerHeight(),
       wH = $(window).height(),
       wS = $(this).scrollTop();
    //console.log((hT-wH) , wS);
   if (wS > (hT+hH-wH)){   
    var fname = $(".LaadMeer").attr("data-root");
    var type = $(".LaadMeer").attr("data-type");
    var offset = $(".LaadMeer").attr("data-offset");
    $(".LaadMeer").remove();//removeClass("LaadMeer");
    $.get({context:{loader:type},url:"/Ashx/Files.ashx?offset="+offset+"&type="+type+"&root="+encodeURIComponent(fname)}).done(function(data){     
     var Loader = this.loader;      
     $("#bestandsviewer").append(data);
     $("div[data-done='0']").each(function(){        
      $(this).load("/Ashx/"+Loader+".ashx?&name="+encodeURIComponent($(this).attr("data-name")),function(){      
       $(this).attr("data-done","1").append(OSTOOLS);
       var f=$(this).attr("data-name");
       f = f.substring(f.lastIndexOf('/')+1);
       f = f.substring(0,f.lastIndexOf('.'));
       $(this).html($(this).html()+"<div class=cpt>"+f+"</div>");         
       //Filter1($(".cpt",this));
      });     
     });
    });
   }
  }
 });
  
 function LoadFilesPane() {
  $("#bestandsnaam").html("Laden...");
  $("#bestandsviewer").html("");
  switch(MODE) {
   case "ModePix":
    $("#bestandsnaam").html("<span class=fnPix><i class='mdi mdi-image-outline'></i>&nbsp;Afbeeldingen</span>");
    $("#bestandsviewer").load("/Ashx/Files.ashx?offset=0&type=Pix&root="+encodeURIComponent(CURRROOT),function(){
     $("div[data-done='0']").each(function(){      
      $(this).load("/Ashx/Pix.ashx?&name="+encodeURIComponent($(this).attr("data-name")),function(){
       $(this).attr("data-done","1").append(OSTOOLS);
       var f=$(this).attr("data-name");
       f = f.substring(f.lastIndexOf('/')+1);
       f = f.substring(0,f.lastIndexOf('.'));
       $(this).html($(this).html()+"<div class=cpt>"+f+"</div>");         
       //Filter1($(".cpt",this));
      });     
     });       
     $("#bestandsviewer").show();
     $("#spinnerviewer").hide();
    });
    break;
   case "ModeSTL":
    $("#bestandsnaam").html("<span class=fnSTL><i class='mdi mdi-cube-outline'></i>&nbsp;STL-bestanden</span>");
    $("#bestandsviewer").load("/Ashx/Files.ashx?offset=0&type=Stl&root="+encodeURIComponent(CURRROOT),function(){
     $("div[data-done='0']").each(function(){
      $(this).load("/Ashx/Stl.ashx?name="+encodeURIComponent($(this).attr("data-name")),function(){
       $(this).attr("data-done","1").append(OSTOOLS);
       var f=$(this).attr("data-name");
       f = f.substring(f.lastIndexOf('/')+1);
       f = f.substring(0,f.lastIndexOf('.'));
       $(this).html($(this).html()+"<div class=cpt>"+f+"</div>");         
       //Filter1($(".cpt",this));
      });     
     });       
     $("#bestandsviewer").show();
     $("#spinnerviewer").hide();
    });
    break;
   case "ModeScad":
    $("#bestandsnaam").html("<span class=fnScad><i class='mdi mdi-vector-circle-variant'></i>&nbsp;Openscadbestanden</span>");
    $("#bestandsviewer").load("/Ashx/Files.ashx?offset=0&type=Scad&root="+encodeURIComponent(CURRROOT),function(){
     $("div[data-done='0']").each(function(){
      $(this).load("/Ashx/Scad.ashx?name="+encodeURIComponent($(this).attr("data-name")),function(){
       $(this).attr("data-done","1").append(OSTOOLS);
       var f=$(this).attr("data-name");
       f = f.substring(f.lastIndexOf('/')+1);
       f = f.substring(0,f.lastIndexOf('.'));
       $(this).html($(this).html()+"<div class=cpt>"+f+"</div>");         
       //Filter1($(".cpt",this));
      });     
     });       
     $("#bestandsviewer").show();
     $("#spinnerviewer").hide();
    });
    break;
   default:
    localStorage.removeItem('LASTFILE');
    $("#bestandsnaam").text("");
    $("#bestandsviewer").show().html("<b>Selecteer in de boomstructuur een bestand of klik op een folder</b>");
    $("#spinnerviewer").hide();
    break;
  }//Switch(MODE)
 }
 
 // UX
 $(".w3-row").on("mouseenter",".scad2",function(){ $(".ostools",$(this)).show(); }).
  on("mouseleave",".scad2",function(){ $(".ostools",$(this)).hide(400); });
  
 $(".w3-row").on("mouseenter",".stl2",function(){ 
  $(".ostools",$(this)).show(); 
  $(".ostools .CmdScad").hide();
 }).
  on("mouseleave",".stl2",function(){ $(".ostools",$(this)).hide(400); });
  
 $(".w3-row").on("mouseenter",".pix2",function(){ 
  $(".ostools",$(this)).show(); 
  $(".ostools .CmdScad").hide();  
 }).
  on("mouseleave",".pix2",function(){ $(".ostools",$(this)).hide(400); });
 
 
 function isElementInViewport (el) {
  if (typeof jQuery === "function" && el instanceof jQuery) {
   el = el[0];
  }
  var rect = el.getBoundingClientRect();
  console.log(rect);
  console.log($(el).text());
  return (
   rect.top >= 0 && rect.left >= 0 &&
   rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) && /* or $(window).height() */
   rect.right <= (window.innerWidth || document.documentElement.clientWidth) /* or $(window).width() */
  );
 }
 
});
