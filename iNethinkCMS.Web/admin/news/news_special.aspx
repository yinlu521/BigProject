<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news_special.aspx.cs" Inherits="iNethinkCMS.Web.admin.news.news_special" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor_lang/zh-cn.js"></script>
    <script src="../../inc/artDialog/artDialog.js?skin=default" type="text/javascript"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
    <script type="text/javascript">
        $(function () {
            var jDomain = "<%=iNethinkCMS.Web.UI.WebUI_Function.Fun_GetDomain()%>";
            $('#txtSpecialContent').xheditor({
                <% if (bool.Parse(siteConfig.RemoteImgDown) == true)
                   { %>
                localUrlTest: new RegExp("^https?:\/\/[^\/]*?(" + jDomain + ")\/", "i"),
                //localUrlTest: /^https?:\/\/[^\/]*?(domain\.com)\//i,
                remoteImgSaveUrl: '/inc/upload_remote.aspx',
                remoteInsert: "txtIndexpicTemp",
                <% } %>

                width: '100%',
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
    </script>
</head>
<body>
    <form id="form_news_special" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable" id="searchInfoID" runat="server">
            <table width="100%">
                <tr>
                    <td align="left">
                        <div class="oper">
                            <a href="?">专题管理</a><a href="?act=guide">添加专题</a>
                        </div>
                    </td>
                    <td align="right" width="300">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="SpecialName">专题名称</asp:ListItem>
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
                    <td>专题名称
                    </td>
                    <td width="140">启用状态
                    </td>
                    <td width="80">排序权重
                    </td>
                    <td width="120">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="5">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "SpecialName")%>
                            </td>
                            <td>
                                <%#Fun_GetDisplay(DataBinder.Eval(Container.DataItem, "Display"))%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "OrderNum")%>
                            </td>
                            <td>
                                <a href="?act=guide&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a>&nbsp;&nbsp;
                            <a href="javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>';}">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="pagelist" class="pages" style="text-align:center;" runat="server"></div>
        </div>
        <div class="main" id="guideID" runat="server">
            <ul class="switchs">
                <li id="s1">基本设置</li>
                <li id="s2" class="c">专题介绍</li>
                <li id="s3" class="c">SEO相关</li>
            </ul>
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>专题名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialName" runat="server" CssClass="int" MaxLength="200"
                                Width="250"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>跳转连接：</dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialUrl" runat="server" CssClass="int" MaxLength="200"
                                Width="250"></asp:TextBox></dd>
                        <dd class="t">可留空.如输入,则访问该专题自动跳转至指定页面</dd>
                    </dl>
                    <dl>
                        <dt>形象图： </dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialPic" runat="server" CssClass="int" MaxLength="120" Width="250"></asp:TextBox></dd>
                        <dd>
                            <iframe src="/inc/upload_base.aspx?rname=txtSpecialPic" scrolling="no" frameborder="0" height="25px"></iframe>
                        </dd>
                    </dl>
                    <dl style="border: none; display: none;" id="iUpInfo_txtSpecialPic">
                        <dt>&nbsp;</dt>
                        <dd id="iUpInfo_msg_txtSpecialPic"></dd>
                    </dl>
                    <dl>
                        <dt>模板路径：</dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialTemplate" runat="server" CssClass="int" MaxLength="200"
                                Width="250"></asp:TextBox></dd>
                        <dd class="t">相对于系统设置中模板路径</dd>
                    </dl>
                    <dl>
                        <dt>是否启用：</dt>
                        <dd>
                            <asp:CheckBox ID="txtDisplay" runat="server" />
                        </dd>
                    </dl>
                    <dl>
                        <dt>排序权重：</dt>
                        <dd>
                            <asp:TextBox ID="txtOrderNum" runat="server" CssClass="int" MaxLength="6"
                                Width="60"></asp:TextBox></dd>
                    </dl>
                </div>
                <div id="infos2" style="display: none">
                    <dl style="border: 0;">
                        <dt>专题介绍：</dt>
                        <dd style="line-height: 0; width: 89%">
                            <asp:TextBox ID="txtSpecialContent" runat="server" TextMode="MultiLine" Height="300"></asp:TextBox>
                        </dd>
                    </dl>
                </div>
                <div id="infos3" style="display: none">
                    <dl style="border: 0;">
                        <dt>专题标题：</dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialTitle" runat="server" CssClass="int" MaxLength="120" Width="300"></asp:TextBox>
                        </dd>
                        <dd class="t">{field:specialtitle}</dd>
                    </dl>
                    <dl>
                        <dt>专题关键字：</dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialKeyword" runat="server" CssClass="int" Height="30px" TextMode="MultiLine"
                                Width="500px" MaxLength="100"></asp:TextBox>
                        </dd>
                        <dd class="t">关键字以","分隔 {field:specialkeyword}</dd>
                    </dl>
                    <dl>
                        <dt>专题描述：</dt>
                        <dd>
                            <asp:TextBox ID="txtSpecialDescription" runat="server" CssClass="int" Height="30px"
                                TextMode="MultiLine" Width="500px" MaxLength="250"></asp:TextBox>
                        </dd>
                        <dd class="t">{field:specialdescription}</dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" /></dd>
                </dl>
            </div>
            <script type="text/javascript">
                js_showImg(<%=siteConfig.ImageSeconds%>,$('#txtSpecialPic').val());
            </script>
        </div>
    </form>
</body>
</html>
