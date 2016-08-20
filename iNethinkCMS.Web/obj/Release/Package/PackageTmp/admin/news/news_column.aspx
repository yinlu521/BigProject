<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news_column.aspx.cs" Inherits="iNethinkCMS.Web.admin.news.news_column" %>

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
            $('#txtContents').xheditor({
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
    <form id="form_column" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main" id="mainID" runat="server">
            <table width="100%" class="tablist">
                <tr class="trbg">
                    <td width="60">编号
                    </td>
                    <td>栏目名称
                    </td>
                    <td width="100">栏目类型
                    </td>
                    <td width="80">导航菜单
                    </td>
                    <td width="80">排序权重
                    </td>
                    <td width="240">栏目操作
                    </td>
                </tr>
                <%=BindDateList()%>
            </table>
        </div>
        <div class="main" id="guideID" runat="server">
            <ul class="switchs">
                <li id="s1">基本信息</li>
                <li id="s2" class="c">栏目介绍</li>
                <li id="s3" class="c">SEO设置</li>
            </ul>
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>上级栏目：</dt>
                        <dd>
                            <asp:DropDownList ID="txtFatherID" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="txtChildID" runat="server" />
                            <asp:HiddenField ID="txtDeepPath" runat="server" Value="1" />
                        </dd>
                    </dl>
                    <dl>
                        <dt>栏目名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtName" runat="server" CssClass="int" Width="220"
                                MaxLength="220"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>英文名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtEname" runat="server" CssClass="int" Width="220" MaxLength="220"></asp:TextBox>
                        </dd>
                        <dd class="t">栏目因为名称</dd>
                    </dl>
                    <dl>
                        <dt>栏目类型：</dt>
                        <dd>
                            <asp:RadioButtonList ID="txtMold" runat="server" RepeatColumns="2">
                                <asp:ListItem Selected="True" Value="1">新闻列表</asp:ListItem>
                                <asp:ListItem Value="2">单页显示</asp:ListItem>
                            </asp:RadioButtonList></dd>
                        <dd class="t">单页显示：一般用于不添加新闻信息,仅显示栏目介绍内容.多用于:关于我们/联系方式等</dd>
                    </dl>
                    <dl>
                        <dt>形象图： </dt>
                        <dd>
                            <asp:TextBox ID="txtPicture" runat="server" CssClass="int" MaxLength="120" Width="220"></asp:TextBox></dd>
                        <dd>
                            <iframe src="/inc/upload_base.aspx?rname=txtPicture" scrolling="no" frameborder="0"
                                height="25px"></iframe>
                        </dd>
                    </dl>
                    <dl style="border: none; display: none;" id="iUpInfo_txtPicture">
                        <dt>&nbsp;</dt>
                        <dd id="iUpInfo_msg_txtPicture"></dd>
                    </dl>
                    <dl>
                        <dt>外部链接：</dt>
                        <dd>
                            <asp:DropDownList ID="txtOutSideLink" runat="server">
                                <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                <asp:ListItem Value="1" Text="是"></asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>跳转地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtDomain" runat="server" CssClass="int" Width="220"
                                MaxLength="100"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>大类模板：</dt>
                        <dd>
                            <asp:TextBox ID="txtTemplatechannel" runat="server" CssClass="int"
                                MaxLength="100" Width="220"></asp:TextBox></dd>
                        <dd class="t">相对于系统设置中模板路径(如果该栏目含有子栏目,则使用)</dd>
                    </dl>
                    <dl>
                        <dt>小类模板：</dt>
                        <dd>
                            <asp:TextBox ID="txtTemplateclass" runat="server" CssClass="int"
                                Width="220" MaxLength="100"></asp:TextBox></dd>
                        <dd class="t">相对于系统设置中模板路径</dd>
                    </dl>
                    <dl>
                        <dt>内容页模板：</dt>
                        <dd>
                            <asp:TextBox ID="txtTemplateview" runat="server" CssClass="int"
                                Width="220" MaxLength="100"></asp:TextBox></dd>
                        <dd class="t">相对于系统设置中模板路径</dd>
                    </dl>
                    <dl>
                        <dt>导航菜单显示：</dt>
                        <dd>
                            <asp:CheckBox ID="txtDisplay" runat="server" Text="勾选则设定为显示" /></dd>
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
                        <dt>栏目介绍：</dt>
                        <dd style="line-height: 0; width: 89%">
                            <asp:TextBox ID="txtContents" runat="server" TextMode="MultiLine" Height="300"></asp:TextBox>
                        </dd>
                    </dl>
                </div>
                <div id="infos3" style="display: none">
                    <dl style="border: 0;">
                        <dt>栏目关键字：</dt>
                        <dd>
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="int" Height="30px"
                                TextMode="MultiLine" Width="500px" MaxLength="100"></asp:TextBox>
                        </dd>
                        <dd class="t">{field:keywords}</dd>
                    </dl>
                    <dl>
                        <dt>栏目描述：</dt>
                        <dd>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="int"
                                Height="30px" TextMode="MultiLine" Width="500px" MaxLength="250"></asp:TextBox></dd>
                        <dd
                            class="t">{field:description}</dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" /></dd>
                </dl>
            </div>
            <script type="text/javascript">
                js_showImg(<%=siteConfig.ImageSeconds%>,$('#txtPicture').val());
            </script>
        </div>
    </form>
</body>
</html>
