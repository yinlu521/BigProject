<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="custom_pages.aspx.cs" Inherits="iNethinkCMS.Web.admin.custom.custom_pages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor_lang/zh-cn.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#txtHtml').xheditor({
                width: '100%',
                height: '400',
                html5Upload: false,
                upMultiple: 10,
                upLinkUrl: '/inc/upload.aspx?immediate=1',
                upLinkExt: '<%=siteConfig.UpFileType%>',
                upImgUrl: '/inc/upload.aspx?immediate=1&filetype=img',
                upImgExt: 'jpg,jpeg,gif,png,bmp',
                upFlashUrl: '/inc/upload.aspx?immediate=1',
                upFlashExt: 'swf',
                upMediaUrl: '/inc/upload.aspx?immediate=1',
                upMediaExt: 'wmv,avi,wma,mp3,mid'
            });
        });

        function js_ajaxMakeAllPage() {
            $('#Button_Back').css({ display: "none" });
            $('#ajaxMakeAllPage').html("正在生成自定义页面,请稍等...");
            $.ajax({
                url: "?act=makeallpage_do&time" + new Date().toString(),
                type: 'GET',
                success: function () {
                    var tmpInfo = arguments[0].split("|&&|");
                    $('#ajaxMakeAllPage').html("自定义页面生成完成!<br>需生成自定义页面[<font color=#ff0000>" + tmpInfo[0] + "</font>]个，成功生成[<font color=#ff0000>" + tmpInfo[1] + "</font>]个!");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form_custom_pages" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable" id="searchInfoID" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?">自定页面管理</a><a href="?act=guide">添加自定页面</a><a href="?act=makeallpage">生成全部页面</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="Title">页面标题</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="sKey" runat="server"></asp:TextBox>
                        <asp:Button ID="Button_Search" runat="server" Text="搜索" CssClass="btnmini" OnClick="Button_Search_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="main" id="mainID" runat="server">
            <table width="100%" class="tablist">
                <tr class="trbg">
                    <td width="80">编号
                    </td>
                    <td width="240">页面标题
                    </td>
                    <td>页面路径
                    </td>
                    <td width="180">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="4">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "Title")%>
                            </td>
                            <td>
                                <a href="<%#DataBinder.Eval(Container.DataItem, "Dir")%>" target="_blank">
                                    <%#DataBinder.Eval(Container.DataItem, "Dir")%></a>
                            </td>
                            <td>
                                <a href="?act=makepage&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">页面生成</a>&nbsp;&nbsp;
                            <a href="?act=guide&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a>&nbsp;&nbsp;
                            <a href="javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>';}">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="pagelist" class="pages" style="text-align: center;" runat="server"></div>
        </div>
        <div class="main" id="guideID" runat="server">
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>页面名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                        <dd
                            class="t">{field:pagetitle}</dd>
                    </dl>
                    <dl>
                        <dt>模板路径：</dt>
                        <dd>
                            <asp:TextBox ID="txtTemplatePath" runat="server" CssClass="int" MaxLength="200" Width="250px"></asp:TextBox></dd>
                        <dd
                            class="t">如果输入模板路径,则根据模板的内容创建.例:custompages/example.html(<font color="red">相对于系统设置中的模板路径</font>)</dd>
                    </dl>
                    <dl>
                        <dt>保存路径：</dt>
                        <dd>
                            <asp:TextBox ID="txtDir" runat="server" CssClass="int" MaxLength="200" Width="250px"></asp:TextBox></dd>
                        <dd
                            class="t">保存后将不可更改.例:/about/company.html</dd>
                    </dl>
                    <dl>
                        <dt>关键字：</dt>
                        <dd>
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="int" Width="250px"
                                MaxLength="100"></asp:TextBox></dd>
                        <dd class="t">{field:pagekeywords}</dd>
                    </dl>
                    <dl>
                        <dt>描 述：</dt>
                        <dd>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="int" TextMode="MultiLine"
                                Width="500" MaxLength="200"></asp:TextBox></dd>
                        <dd class="t">{field:pagedescription}</dd>
                    </dl>
                    <dl>
                        <dt>&nbsp;</dt>
                        <dd class="t">当使用模板创建时,内容标签为:{field:pagehtml}.注:页面内容中支持任何标签</dd>
                    </dl>
                    <dl style="border: 0;">
                        <dt>页面内容：</dt>
                        <dd style="line-height: 0; width: 89%">
                            <asp:TextBox ID="txtHtml" runat="server" TextMode="MultiLine"></asp:TextBox></dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" /></dd>
                </dl>
            </div>
        </div>


        <div class="main" id="MakeAllPageID" runat="server">
            <div class="info">
                <dl>
                    <dt>&nbsp;</dt>
                    <dd style="font-weight: bold; font-size: 14px;" id="ajaxMakeAllPage">
                        <script type="text/javascript">
                            js_ajaxMakeAllPage();
                        </script>
                    </dd>
                </dl>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <input type="button" name="UpButton_Back" value="返 回" id="UpButton_Back" class="btnbig"
                            style="display: none;" onclick="location.href = '?';" /></dd>
                </dl>
            </div>
        </div>
    </form>
</body>
</html>
