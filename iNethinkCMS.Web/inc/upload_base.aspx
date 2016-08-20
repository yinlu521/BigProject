<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_base.aspx.cs" Inherits="iNethinkCMS.Web.inc.upload_base" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../admin/skin/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function LoadFile(filePath, fileType) {
            var byInfo = false;
            var strPath = filePath.toLowerCase();
            var strExt = strPath.substr(strPath.length - 4, 4).replace(/./, ",") + ",";
            var strType = "," + fileType.toLowerCase() + ",";
            if (strType.indexOf(strExt) == -1) {
                byInfo = false;
            } else {
                byInfo = true;
            }
            return byInfo;
        }

        function do_fileselect(val) {
            var vFilePath = $('#FileUpload').val()
            if (vFilePath != "") {
                parent.$('#iUpInfo_msg_' + val).html("已选择文件：" + $('#FileUpload').val());
                parent.$('#iUpInfo_' + val).css({ display: "block" });
                $('#Button_up').attr("disabled", false);
            } else {
                parent.$('#iUpInfo_msg_' + val).html("");
                parent.$('#iUpInfo_' + val).css({ display: "none" });
                $('#Button_up').attr("disabled", true);
            }
        }
        function do_isup(val) {
            if ($('#FileUpload').val() == "") {
                alert("请选择需要上传的文件!");
                return false;
            }

            var vFileExt = "<%=siteConfig.UpFileType%>";
            if (LoadFile($('#FileUpload').val(), vFileExt) == false) {
                alert("仅允许上传：" + vFileExt);
                return false;
            }

            parent.$('#iUpInfo_msg_' + val).html("<img src='/admin/skin/images/loading.gif'>&nbsp;正在上传文件,请稍等...");
            parent.$('#iUpInfo_' + val).css({ display: "block" });
        }
    </script>
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        .upbutton {
            background: url("../admin/skin/images/pic.gif") no-repeat;
            width: 80px;
            text-align: center;
            color: rgb(255, 255, 255);
            height: 25px;
            margin-right: 10px;
            border: none;
        }

        .fileshows {
            position: absolute;
            left: 202px;
            top: 52px;
            height: 26px;
            cursor: pointer;
            filter: Alpha(opacity=0);
            -moz-opacity: 0;
            opacity: 0;
        }
    </style>
</head>
<body>
    <form id="form_upload_base" runat="server">
        <div>
            <input type="button" onmousemove="FileUpload.style.pixelLeft=event.clientX-75;FileUpload.style.pixelTop=this.offsetTop;"
                value="文件浏览" onclick="FileUpload.click();" class="upbutton" />
            <asp:FileUpload ID="FileUpload" runat="server" Width="80" class="fileshows" hidefocus
                BorderStyle="None" />
            <asp:Button ID="Button_up" runat="server" Text="文件上传" CssClass="upbutton" OnClick="Button_up_Click" />
        </div>
    </form>
</body>
</html>
