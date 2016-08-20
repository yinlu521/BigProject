<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_adlist.aspx.cs" Inherits="iNethinkCMS.Web.admin.extend.extend_adlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?">广告列表</a><a href="?act=guide">添加广告</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="Title">名称</asp:ListItem>
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
                    <td width="80">编号</td>
                    <td width="160">广告位</td>
                    <td width="160">广告标题</td>
                    <td width="200">广告图片</td>
                    <td width="200">广告链接</td>
                    <td>排序权重</td>
                    <td>创建时间</td>
                    <td width="160">操作</td>
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
                            <td><%#GetAdGroup(Convert.ToInt32(Eval("parentId").ToString()))%></td>
                            <td><%#Eval("Title") %>
                            <td><img src="<%#Eval("IndexPic") %>" width="200" height="80"/></td>
                            </td>
                            <td><%#Eval("Linkurl") %></td>
                            <td><%#Eval("OrderNum") %></td>
                            <td><%#Eval("addtime") %></td>
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
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>广告位：</dt>
                        <dd>
                            <asp:DropDownList ID="dropGroup" runat="server" Width="150">
                               
                            </asp:DropDownList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>标题名称：</dt>
                        <dd><asp:TextBox id="txtTitle" runat="server"  Width="300"  CssClass="int"></asp:TextBox></dd>
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
                        <dt>链接地址：</dt>
                        <dd><asp:TextBox id="txtLinkUrl" runat="server" Width="300" CssClass="int"></asp:TextBox></dd>
                        <dd class="t">示例： http://www.baidu.com/index.aspx </dd>
                    </dl>
                    <dl>
                        <dt>排序权重：</dt>
                        <dd><asp:TextBox id="txtOrderNum" runat="server" CssClass="int"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>备注说明：</dt>
                        <dd><asp:TextBox id="txtRemark" runat="server" TextMode="MultiLine" Width="500" Height="60" CssClass="int"></asp:TextBox></dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" /></dd>
                </dl>
            </div>
        </div>
    </form>
</body>
</html>
