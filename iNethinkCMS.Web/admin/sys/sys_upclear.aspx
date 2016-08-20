<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_upclear.aspx.cs" Inherits="iNethinkCMS.Web.admin.sys.sys_upclear" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script src="../../inc/artDialog/artDialog.js?skin=default" type="text/javascript"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
    <script type="text/javascript">
        function js_showImg_upclear(byType, byID) {
            if (byType == "show") {
                var imgUrl = $('#img_' + byID).html();
                art.dialog({
                    id: 'art_' + byID,
                    title: false,
                    cancel: false,
                    follow: document.getElementById('img_' + byID),
                    drag: false,
                    resize: false,
                    padding: "1px 1px 1px 1px",
                    content: '<img src=' + imgUrl + ' width=225>'
                });
            }
            else {
                if (art.dialog.list['art_' + byID] != null) {
                    art.dialog.list['art_' + byID].close();
                }
            }
        }

        function js_ajaxClearUpFile() {
            $('#Button_Back').css({ display: "none" });
            $('#ajaxClearUpFile').html("正在清理无效上传文件,请稍等...");
            $.ajax({
                url: "?act=clearupfile_do&time" + new Date().toString(),
                type: 'GET',
                success: function () {
                    $('#UpButton_Back').css({ display: "block" });
                    $('#ajaxClearUpFile').html("无效上传文件清理成功!共清理文件[<font color=#ff0000>" + arguments[0] + "</font>]个!");
                }
            });
        }

        function js_ajaxClearThumbFile() {
            $('#Button_Back').css({ display: "none" });
            $('#ajaxClearThumbFile').html("正在清理系统内的缩略图,请稍等...");
            $.ajax({
                url: "?act=clearthumbfile_do&time" + new Date().toString(),
                type: 'GET',
                success: function () {
                    $('#ThumbButton_Back').css({ display: "block" });
                    $('#ajaxClearThumbFile').html("缩略图文件清理成功!共清理文件[<font color=#ff0000>" + arguments[0] + "</font>]个!<br>下次用户访问相关页面时，会自动生成新的缩略图!");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form_sys_upclear" runat="server">
    <div class="maintop" id="navInfoID" runat="server">
    </div>
    <div class="main_toptable" id="navButtonID" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <div class="oper">
                        <a href="?act=clearupfile">清理上传文件</a> <a href="?act=clearthumbfile">清空缩略图</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="main" id="mainID" runat="server">
        <table width="100%" class="tablist">
            <tr class="trbg">
                <td width="80">
                    编号
                </td>
                <td width="130">
                    所属关系
                </td>
                <td>
                    路径
                </td>
                <td width="180">
                    上传时间
                </td>
                <td width="80">
                    使用状态
                </td>
            </tr>
            <tr id="iNoInfo" runat="server">
                <td colspan="7">
                    暂无任何数据
                </td>
            </tr>
            <asp:Repeater ID="Repeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem,"ID")%>
                        </td>
                        <td>
                            <%#Fun_Switch_UpType(DataBinder.Eval(Container.DataItem,"UpType"))%>
                        </td>
                        <td>
                            <div style="text-align: left;">
                                <a href="javascript:" onmousemove="js_showImg_upclear('show',<%#DataBinder.Eval(Container.DataItem,"ID")%>);"
                                    onmouseout="js_showImg_upclear('hide',<%#DataBinder.Eval(Container.DataItem,"ID")%>);"
                                    id="img_<%#DataBinder.Eval(Container.DataItem,"ID")%>">
                                    <%#DataBinder.Eval(Container.DataItem,"Dir")%></a></div>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem,"Time")%>
                        </td>
                        <td>
                            <%#Fun_Switch_FileValid(DataBinder.Eval(Container.DataItem, "UpType"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
      <div id="pagelist" class="pages" style="text-align:center;" runat="server"></div>
    </div>
    <div class="main" id="clearUpFileID" runat="server">
        <div class="info">
            <dl>
                <dt>&nbsp;</dt>
                <dd style="font-weight: bold; font-size: 14px;" id="ajaxClearUpFile">
                    <script type="text/javascript">
                        js_ajaxClearUpFile();
                    </script>
                </dd>
            </dl>
            <dl>
                <dt>&nbsp;</dt><dd>
                    <input type="button" name="UpButton_Back" value="返 回" id="UpButton_Back" class="btnbig"
                        style="display: none;" onclick="location.href='?';" /></dd></dl>
        </div>
    </div>
    <div class="main" id="clearThumbFileID" runat="server">
        <div class="info">
            <dl>
                <dt>&nbsp;</dt>
                <dd style="font-weight: bold; font-size: 14px;" id="ajaxClearThumbFile">
                    <script type="text/javascript">
                        js_ajaxClearThumbFile();
                    </script>
                </dd>
            </dl>
            <dl>
                <dt>&nbsp;</dt><dd>
                    <input type="button" name="ThumbButton_Back" value="返 回" id="ThumbButton_Back" class="btnbig"
                        style="display: none;" onclick="location.href='?';" /></dd></dl>
        </div>
    </div>
    </form>
</body>
</html>
