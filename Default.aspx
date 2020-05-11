<%@ Page Language="C#" Inherits="projbrowser.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Mijn Projecten</title>
 <meta name="viewport" content="width=device-width, initial-scale=1">
 <link rel="stylesheet" href="/client/w3.css"> 
 <link rel="stylesheet" href="/client/prism.css">
 <link rel="stylesheet" href="/client/materialdesignicons.css">
 <link rel="stylesheet" href="/client/spinner.css">
 <link rel="stylesheet" href="/client/bladeren.css">
 <script src="/client/jquery-3.5.0.min.js"></script>
 <script src="/client/prism.js"></script> 
 <script src="/client/blader.js"></script>  
 <script src="/client/editor.js"></script>   
</head>
<body>
	<form id="form1" runat="server">
  <div class="w3-container m">
   <div class="toolbar">
    <img src="/pix/logo.png" />
   </div>
   
   <div class="padwrapper">
    <div class="pad">/media/marcel/hele pad naar projectfolder</div>
    <div id="count"></div>
   </div>
   
   <div class="w3-row">
    
    <div class="w3-col treecol">     
     <div id="treeicons">  
      <i id="ModeFile" class="mdi mdi-text-box-outline active" title="Bestanden"></i>      
      <i id="ModePix" class="mdi mdi-image-outline" title="Afbeeldingen"></i>
      <i id="ModeScad" class="mdi mdi-vector-circle-variant" title="Openscadbestanden"></i>   
      <i id="ModeSTL" class="mdi mdi-cube-outline" title="STL-bestanden"></i>   
     </div>
     <div id="tree">
      <div class="" id="spinnerviewerT"><div class="spinner"><div class="cube1"></div><div class="cube2"></div></div></div>
      <a href="#"><i class="root"></i>Atari joystick (hoofdfolder</a>
      <a href="#"><i class="subfolder"></i>Details atarti (subfolder)</a>
      <a href="#"><i class="file"></i>Javascriptfile.js</a>
      <a href="#"><i class="scad"></i>ontwerp.scad</a>
      <a href="#"><i class="scad"></i>arduino.ino</a>
     </div>
    </div>
    <div class="w3-rest">
     <div id="bestandsnaam">dingetje.js</div>
     <div class="" id="bestandsviewer"></div>
     <div class="" id="spinnerviewer"><div class="spinner"><div class="cube1"></div><div class="cube2"></div></div></div>           
    </div>
   </div>
   <div class="br">&nbsp;</div>
   <div class="br2">&nbsp;</div>
  </div>
	</form>
</body>
</html>
