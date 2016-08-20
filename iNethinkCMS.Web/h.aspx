<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/h.aspx.cs" Inherits="iNethinkCMS.Web.h" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script>
        var x = document.getElementById("demo");
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            }
            else { x.innerHTML = "Geolocation is not supported by this browser."; }
        }
        function showPosition(position) {            
            setCookie("latitude", position.coords.latitude);
            setCookie("longitude", position.coords.longitude);
           
        }
        function setCookie(name, value) {
            var Days = 30;
            var exp = new Date();
            exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
            document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
        }
        getLocation();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div>
    
        </div>
    </form>
</body>
</html>
