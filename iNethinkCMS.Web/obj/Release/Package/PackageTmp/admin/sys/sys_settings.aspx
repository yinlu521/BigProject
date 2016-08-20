<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_settings.aspx.cs" Inherits="iNethinkCMS.Web.admin.sys_settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_sys_setting" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>

        <div class="main" id="mainID" runat="server">
            <ul class="switchs">
                <li id="s1">基本设置</li>
                <li id="s2" class="c">高级设置</li>
                <li id="s3" class="c">伪静态设置</li>
                <li id="s4" class="c">上传设置</li>
                <li id="s5" class="c">个性设定</li>
            </ul>
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>网站名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtWebName" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd
                            class="t">{sys:title}</dd>
                    </dl>
                    <dl style="display: none;">
                        <dt>安装路径：</dt>
                        <dd>
                            <asp:TextBox ID="txtInstallDir" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>访问模式：</dt>
                        <dd>
                            <asp:DropDownList ID="txtUrlMode" runat="server">
                                <asp:ListItem Value="0">动态浏览</asp:ListItem>
                                <asp:ListItem Value="1">伪装静态</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dd class="t">建议在网站制作并调试完成后,再开启伪装静态功能</dd>
                    </dl>
                    <dl>
                        <dt>模板缓存：</dt>
                        <dd>
                            <asp:DropDownList ID="txtTemplateCache" runat="server">
                                <asp:ListItem Value="true">开启</asp:ListItem>
                                <asp:ListItem Value="false">关闭</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dd class="t">网站模板制作完成后,强烈建议您开启</dd>
                    </dl>
                    <dl>
                        <dt>整站缓存：</dt>
                        <dd>
                            <asp:DropDownList ID="txtWebPageCache" runat="server">
                                <asp:ListItem Value="true">开启</asp:ListItem>
                                <asp:ListItem Value="false">关闭</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dd class="t">开启后可媲美静态页面速度.网站模板制作完成后,建议您开启</dd>
                    </dl>
                    <dl>
                        <dt>缓存标识：</dt>
                        <dd>
                            <asp:TextBox ID="txtCacheKey" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>缓存时间：</dt>
                        <dd>
                            <asp:TextBox ID="txtCacheTime" runat="server" CssClass="int" MaxLength="5" Width="60px"></asp:TextBox></dd>
                        <dd>秒</dd>
                    </dl>

                    <dl style="display: none;">
                        <dt>模板路径：</dt>
                        <dd>
                            <asp:TextBox ID="txtTemplateDir" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                    </dl>
                </div>

                <div id="infos2" style="display:none;">
                    <dl style="border: 0;">
                        <dt>首页模板：</dt>
                        <dd>
                            <asp:TextBox ID="txtIndexTemplateName" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd class="t">相对于模板文件夹中,网站首页模板文件所在路径</dd>
                    </dl>
                    <dl>
                        <dt>DEBUG模式：</dt>
                        <dd>
                            <asp:DropDownList ID="txtDebugMode" runat="server">
                                <asp:ListItem Value="true">开启</asp:ListItem>
                                <asp:ListItem Value="false">关闭</asp:ListItem>
                            </asp:DropDownList>
                            </dd>
                        <dd class="t">如运行中出现错误,可开启后查看错误详情.正式运行后请关闭提示,以提高系统安全性</dd>
                    </dl>
                </div>

                <div id="infos3" style="display: none;">
                    <dl style="border: 0;">
                        <dt>&nbsp;</dt>
                        <dd class="t">修改“栏目/专题/内容/留言”的后缀名后,请修改web.config中相应的RewriterConfig.</dd>

                    </dl>
                     <dl>
                        <dt>伪静态后缀名：</dt>
                        <dd>
                            <asp:TextBox ID="txtRewriteExtName" runat="server" CssClass="int" Width="100px"></asp:TextBox></dd>
                        <dd
                            class="t">请注意该后缀名(扩展名)必须从IIS映射到ASP.NET引擎!</dd>
                    </dl>
                    <dl>
                        <dt>栏目页前缀：</dt>
                        <dd>
                            <asp:TextBox ID="txtRewriteChannelPrefix" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd
                            class="t">初始为：channle</dd>
                    </dl>
                    <dl>
                        <dt>专题页前缀：</dt>
                        <dd>
                            <asp:TextBox ID="txtRewriteSpecialPrefix" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd
                            class="t">初始为:special</dd>
                    </dl>
                    <dl>
                        <dt>内容页前缀：</dt>
                        <dd>
                            <asp:TextBox ID="txtRewriteContentPrefix" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd
                            class="t">初始为:content</dd>
                    </dl>
                    <dl>
                        <dt>留言板前缀：</dt>
                        <dd>
                            <asp:TextBox ID="txtRewriteGuestbookPrefix" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd
                            class="t">初始为:guestbook</dd>
                    </dl>
                </div>

                <div id="infos4" style="display: none;">
                    <dl style="border: 0;">
                        <dt>远程抓图：</dt>
                        <dd>
                            <asp:DropDownList ID="txtRemoteImgDown" runat="server">
                                <asp:ListItem Value="true">开启</asp:ListItem>
                                <asp:ListItem Value="false">关闭</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dd class="t">如开启,则在新闻添加过程中,自动将远程的图片下载至本地</dd>
                    </dl>
                    <dl>
                        <dt>允许上传类型：</dt>
                        <dd>
                            <asp:TextBox ID="txtUpFileType" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd>
                        <dd
                            class="t">多个类型用","分割</dd>
                    </dl>
                    <dl>
                        <dt>允许最大上传：</dt>
                        <dd>
                            <asp:TextBox ID="txtUpFileMaxSize" runat="server" CssClass="int"
                                MaxLength="10" Width="60px"></asp:TextBox>
                            K</dd>
                    </dl>
                </div>
                <div id="infos5" style="display: none;">
                    <dl style="border: 0;">
                        <dt>分页列表数量：</dt>
                        <dd>
                            <asp:TextBox ID="txtPageListNum" runat="server" CssClass="int" Width="60px"></asp:TextBox></dd>
                        <dd class="t">系统管理中相关分页列表,每页显示的信息数量</dd>
                    </dl>
                    <dl>
                        <dt>标题标尺：</dt>
                        <dd>
                            <asp:DropDownList ID="txtDisplayTitleRule" runat="server">
                                <asp:ListItem Value="true">开启</asp:ListItem>
                                <asp:ListItem Value="false">关闭</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dd class="t">如开启,则在新闻添加中,标题与子标题输入框中显示标尺</dd>
                    </dl>
                    <dl>
                        <dt>图片显示时间：</dt>
                        <dd>
                            <asp:TextBox ID="txtImageSeconds" runat="server" CssClass="int"
                                MaxLength="5" Width="60px"></asp:TextBox>
                            秒</dd>
                        <dd class="t">内容添加/其它页面,选择相应的形象图后,该显示的图片在[设定秒]后关闭.设置为[0]不自动关闭</dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Submit_Sys_Setting" runat="server" Text="提 交"
                            CssClass="btnbig" OnClick="Submit_Sys_Setting_Click" /></dd>
                </dl>
            </div>
            <script type="text/javascript">
                do_showrewriteset();
            </script>
        </div>

        <div class="main" id="copyrightID" runat="server">
        </div>
    </form>
</body>
</html>
