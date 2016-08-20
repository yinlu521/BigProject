<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news_content.aspx.cs" Inherits="iNethinkCMS.Web.admin.news.news_content" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor_lang/zh-cn.js"></script>
    <script src="../../inc/artDialog/artDialog.js?skin=default" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../inc/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
    <script type="text/javascript">
    
        $(function () {
            var jDomain = "<%=iNethinkCMS.Web.UI.WebUI_Function.Fun_GetDomain()%>";
            $('#txtContents').xheditor({
                width: '100%',
                //height: '400',
                html5Upload: false,
                upMultiple: 10,
                upLinkUrl: '/inc/upload.aspx?immediate=1',
                upLinkExt: '<%=siteConfig.UpFileType%>',
                upImgUrl: '/inc/upload.aspx?immediate=1&filetype=img',
                upImgExt: 'jpg,jpeg,gif,png,bmp',
                upFlashUrl: '/inc/upload.aspx?immediate=1',
                upFlashExt: 'swf', upMediaUrl: '/inc/upload.aspx?immediate=1',
                upMediaExt: 'wmv,avi,wma,mp3,mid',
                cleanPaste: 2,

                <% if (bool.Parse(siteConfig.RemoteImgDown) == true)
                   { %>
                localUrlTest: new RegExp("^https?:\/\/[^\/]*?(" + jDomain + ")\/", "i"),
                //localUrlTest: /^https?:\/\/[^\/]*?(domain\.com)\//i,
                remoteImgSaveUrl: '/inc/upload_remote.aspx',
                remoteInsert: "txtIndexpicTemp",
                <% } %>
                onUpload: insertUpload
            });
        });
                
        //预留获取图片列表信息
        function insertUpload(arrMsg) {
            var i, msg;
            for (i = 0; i < arrMsg.length; i++) {
                msg = arrMsg[i];
                if (msg.filetype == "img") {
                    $("#txtIndexpicTemp").append('<option value="' + msg.baseurl + '">' + msg.baseurl + '</option>');
                }
            }
        }
        
    </script>
</head>
<body>
    <form id="form_news_content" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:DropDownList ID="sCID" runat="server" OnSelectedIndexChanged="Do_ExtSearch"
                            AutoPostBack="True">
                            <asp:ListItem Value="" Text="所有栏目"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                    <asp:DropDownList ID="sSID" runat="server" OnSelectedIndexChanged="Do_ExtSearch"
                        AutoPostBack="True">
                        <asp:ListItem Value="" Text="所有专题"></asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;
                    <asp:DropDownList ID="sDisplay" runat="server" OnSelectedIndexChanged="Do_ExtSearch"
                        AutoPostBack="True">
                        <asp:ListItem Text="发布情况" Value=""></asp:ListItem>
                        <asp:ListItem Text="已发布" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未发布" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;
                    <asp:DropDownList ID="sCommend" runat="server" OnSelectedIndexChanged="Do_ExtSearch"
                        AutoPostBack="True">
                        <asp:ListItem Text="推荐情况" Value=""></asp:ListItem>
                        <asp:ListItem Text="推荐" Value="1"></asp:ListItem>
                        <asp:ListItem Text="非推荐" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;
                    <asp:DropDownList ID="sIsComment" runat="server" OnSelectedIndexChanged="Do_ExtSearch"
                        AutoPostBack="True">
                        <asp:ListItem Text="评论情况" Value=""></asp:ListItem>
                        <asp:ListItem Text="允许" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不允许" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td align="right" width="300">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="Title">标题</asp:ListItem>
                            <asp:ListItem Value="SubTitle">子标题</asp:ListItem>
                            <asp:ListItem Value="Author">作者</asp:ListItem>
                            <asp:ListItem Value="Source">来源</asp:ListItem>
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
                    <td width="60">选择
                    </td>
                    <td width="100">栏目
                    </td>
                    <td>标题
                    </td>
                    <td width="80">状态
                    </td>
                    <td width="140">修改时间
                    </td>
                    <td width="80">浏览次数
                    </td>
                    <td width="120">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="8">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td>
                                <input id="ids" name="ids" type="checkbox" value='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                            </td>
                            <td>
                                <%#Fun_ChannleInfo(DataBinder.Eval(Container.DataItem, "CID"))%>
                            </td>
                            <td class="tdleft">
                                <%#Fun_SpecialInfo(DataBinder.Eval(Container.DataItem, "SID"))%>
                                <a href="/content.aspx?id=<%#DataBinder.Eval(Container.DataItem,"ID")%>" target="_blank">
                                    <%#DataBinder.Eval(Container.DataItem,"Title")%></a>
                                <%#Fun_ExpanInfo(DataBinder.Eval(Container.DataItem, "Commend"), DataBinder.Eval(Container.DataItem, "IsComment"))%>
                            </td>
                            <td>
                                <%#Fun_DisplayInfo(DataBinder.Eval(Container.DataItem, "Display"))%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "Modifytime")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "Views")%>
                            </td>
                            <td>
                                <a href="?act=guide&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a>&nbsp;&nbsp;
                            <a href="javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>';}">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" height="30" valign="bottom">
                        <input id="allcheck" type="button" value="全选" class="btnmini" onclick="alls('ids','allcheck');" />&nbsp;
                    <input type="button" value="审核" class="btnmini" onclick="do_content('audit');" />
                        &nbsp;<input type="button" value="删除" class="btnmini" onclick="do_content('deletes');" />
                        &nbsp;
                    <asp:DropDownList ID="moveCID" runat="server">
                        <asp:ListItem Value="0" Text="请选择移动至栏目"></asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;<input type="button" value="移动" class="btnmini" onclick="do_content('move');" />
                    </td>
                </tr>
            </table>
            <div id="pagelist" class="pages" style="text-align:center;" runat="server"></div>
        </div>
        <div class="main" id="guideID" runat="server">
            <ul class="switchs">
                <li id="s1">基本信息</li>
                <li id="s2" class="c">扩展属性</li>
                <li id="s3" class="c" style="display: none;">自定义字段</li>
            </ul>
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>所属栏目：</dt>
                        <dd>
                            <asp:DropDownList ID="txtCid" runat="server">
                                <asp:ListItem Value="0" Text="请选择所属栏目"></asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>标题：</dt>
                        <dd>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="int" MaxLength="300"
                                Width="380"></asp:TextBox></dd>
                        <dd>
                            <asp:CheckBox ID="txtCommend" runat="server" />推荐信息</dd>
                        <dd>
                            <asp:CheckBox ID="txtIsComment" runat="server" />允许评论</dd>
                        <dd>
                            <asp:CheckBox ID="txtDisplay" runat="server" />发布</dd>
                    </dl>
                    <dl>
                        <dt>子标题：</dt>
                        <dd>
                            <asp:TextBox ID="txtSubTitle" runat="server" CssClass="int" MaxLength="120" Width="380"></asp:TextBox></dd>
                        <dd
                            class="t">可留空</dd>
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
                        <dt>描述/简介：</dt>
                        <dd>
                            <input id="txtAutoGetDescription" name="txtAutoGetDescription" type="checkbox" runat="server"
                                onclick="do_txtAutoGetDescription_show();" />自动编辑描述<br />
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="int" MaxLength="500" TextMode="MultiLine"
                                Rows="3" Width="500"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>内容：</dt>
                        <dd style="line-height: 0; width: 89%">
                            <asp:TextBox ID="txtContents" runat="server" TextMode="MultiLine"
                                Height="430"></asp:TextBox></dd>
                    </dl>
                </div>
                <div id="infos2" style="display: none">
                    <dl style="border: 0;">
                        <dt>所属专题：</dt>
                        <dd>
                            <asp:DropDownList ID="txtSid" runat="server">
                                <asp:ListItem Value="0" Text="请选择所属专题"></asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>标题颜色：</dt>
                        <dd>
                            <asp:DropDownList ID="txtTitle_Color" runat="server">
                                <asp:ListItem Value="" Text="默认"></asp:ListItem>
                                <asp:ListItem Value="color:#CC0000;" Text="红色" style="color: #CC0000;"></asp:ListItem>
                                <asp:ListItem Value="color:#0000FF;" Text="蓝色" style="color: #0000FF;"></asp:ListItem>
                                <asp:ListItem Value="color:#009900;" Text="绿色" style="color: #009900;"></asp:ListItem>
                                <asp:ListItem Value="color:#996600;" Text="咖啡色" style="color: #996600;"></asp:ListItem>
                                <asp:ListItem Value="color:#FF6600;" Text="橘黄色" style="color: #FF6600;"></asp:ListItem>
                                <asp:ListItem Value="color:#8142BF;" Text="紫色" style="color: #8142BF;"></asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dd>
                            <strong>标题样式：</strong></dd>
                        <dd>
                            <asp:DropDownList ID="txtTitle_Style" runat="server">
                                <asp:ListItem Value="" Text="默认"></asp:ListItem>
                                <asp:ListItem Value="font-weight:bold;" Text="粗体"></asp:ListItem>
                                <asp:ListItem Value="font-style:italic;" Text="斜体"></asp:ListItem>
                                <asp:ListItem Value="text-decoration:underline;" Text="下划线"></asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>作者：</dt>
                        <dd>
                            <asp:TextBox ID="txtAuthor" runat="server" CssClass="int" MaxLength="100" Width="80"></asp:TextBox>
                        </dd>
                        <dd>
                            <strong>来源：</strong></dd>
                        <dd>
                            <asp:TextBox ID="txtSource" runat="server" CssClass="int" MaxLength="100" Width="140"></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>关键字：</dt>
                        <dd>
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="int" MaxLength="100" Width="240"></asp:TextBox>
                        </dd>
                        <dd class="t">显示于页面 meta keywords</dd>
                    </dl>
                    <dl>
                        <dt>跳转地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtJumpurl" runat="server" CssClass="int" MaxLength="200" Width="240"></asp:TextBox>
                        </dd>
                        <dd class="t">填写后,访问本内容时直接跳转自此地址</dd>
                    </dl>
                    <dl>
                        <dt>排序权重：</dt>
                        <dd>
                            <asp:TextBox ID="txtOrderNum" runat="server" CssClass="int" MaxLength="6" Width="80"></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>访问次数：</dt>
                        <dd>
                            <asp:TextBox ID="txtViews" runat="server" CssClass="int" MaxLength="6" Width="80"></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>修改时间：</dt>
                        <dd>
                            <asp:TextBox ID="txtModifytime" runat="server" CssClass="int" ReadOnly="True"></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>创建时间：</dt>
                        <dd>
                            <asp:TextBox ID="txtCreatetime" runat="server" CssClass="int"></asp:TextBox>
                        </dd>
                    </dl>
                </div>
                <div id="infos3" style="display: none;">
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" OnClientClick="return checkpost_content();" /></dd>
                </dl>
            </div>
            <script type="text/javascript">
                //描述信息是否显示
                function do_txtAutoGetDescription_show() {
                    if ($("#txtAutoGetDescription").is(":checked") == true) {
                        $("#txtDescription").css({"display":"none"});
                    } else {
                        $("#txtDescription").css({"display":"block"});
                    }
                }
                do_txtAutoGetDescription_show();
                js_showImg(<%=siteConfig.ImageSeconds%>,$('#txtIndexpic').val());
                ajax_content_customfields(<%=vID%>);
            </script>
        </div>
    </form>
</body>
</html>
