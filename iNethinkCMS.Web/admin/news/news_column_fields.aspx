<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news_column_fields.aspx.cs" Inherits="iNethinkCMS.Web.admin.news.news_column_fields" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_news_column_fields" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?cid=<%=vCID%>">自定字段管理</a><a href="?act=guide&cid=<%=vCID%>">添加自定字段</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="CustomFieldsName">字段名称</asp:ListItem>
                            <asp:ListItem Value="CustomFieldsKey">字段标识</asp:ListItem>
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
                    <td>字段名称
                    </td>
                    <td width="260">字段标识
                    </td>
                    <td width="130">字段类型
                    </td>
                    <td width="80">是否必填
                    </td>
                    <td width="80">是否使用
                    </td>
                    <td width="80">排序权重
                    </td>
                    <td width="140">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="9">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"CustomFieldsName")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"CustomFieldsKey")%>
                            </td>
                            <td>
                                <%#Fun_Switch_CustomFieldsType(DataBinder.Eval(Container.DataItem,"CustomFieldsType"))%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"CustomFieldsRequired").ToString() == "1" ? "是":"否"%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"Display").ToString() == "1" ? "使用":"<font color=\"#ff000\">不使用</font>"%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "OrderNum")%>
                            </td>
                            <td>
                                <a href="?act=guide&cid=<%=vCID%>&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a>&nbsp;&nbsp;
                            <a href="javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&cid=<%=vCID%>&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>';}">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="pagelist" class="pages" style="text-align:center;" runat="server"></div>
        </div>
        <div class="main" id="guideID" runat="server">
            <div class="info">
                <ul class="switchs">
                    <li id="s1">基本信息</li>
                    <li id="s2" class="c">选择字段所属栏目</li>
                </ul>
                <div id="infos1">
                    <dl style="border: 0;">

                        <dt>字段名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtCustomFieldsName" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>字段标识：</dt>
                        <dd>
                            <asp:TextBox ID="txtCustomFieldsKey" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                        <dd class="t">前台调用：{field/page/列表名:myfields_字段标识}</dd>
                    </dl>
                    <dl>
                        <dt>字段类型：</dt>
                        <dd>
                            <asp:TextBox ID="txtCustomFieldsType" runat="server" CssClass="int" Width="180" style="display:none;"></asp:TextBox>
                            <table width="100%" class="tablist1">
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_textfield" value="my_textfield" /><label for="my_textfield">文本域:</label>单行文本输入框,如:文章标题等
                                    </td>
                                </tr>
                                <tr>
                                    <td><input type="radio" name="myCustomFieldsType" id="my_datetext" value="my_datetext"/><label for="my_textfield">选择日期文本域:</label>单行文本输入框,带选择日期</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_textarea" value="my_textarea" /><label for="my_textarea">文本区域:</label>多行文本输入框,如:文章描述等
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_richtextarea" value="my_richtextarea" /><label for="my_richtextarea">富文本区域:</label>带有富文本编辑器的多行文本输入框,如:文章内容等。</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_checkbox" value="my_checkbox" /><label for="my_checkbox">复选框:</label>多项选择框</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_radio" value="my_radio" /><label for="my_radio">单选按钮：</label>单项选择框,如:性别选择</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_select" value="my_select" /><label for="my_select">选择(列表菜单):</label>下拉菜单选择框,如:栏目选择等</td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="radio" name="myCustomFieldsType" id="my_file" value="my_file" /><label for="my_file">文件域：</label>文件上传,如:图片、文档的上传</td>
                                </tr>
                            </table>
                        </dd>
                    </dl>
                    <dl>
                        <dt>字段初始值：</dt>
                        <dd>注:复选框、单选按钮、选择（列表菜单）的格式为"选项1=值1,选项2=值2",每个选项之间用半角英文逗号分割<br />
                            <asp:TextBox ID="txtCustomFieldsValue" runat="server" CssClass="int" Width="460px" Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </dd>

                    </dl>
                    <dl style="display:none;">
                        <dt>是否必填：</dt>
                        <dd>
                            <asp:CheckBox ID="txtCustomFieldsRequired" runat="server" Text="勾选则设定为必填" /></dd>
                    </dl>
                    <dl>
                        <dt>是否使用：</dt>
                        <dd>
                            <asp:CheckBox ID="txtDisplay" runat="server" Text="勾选则设定为使用" /></dd>
                    </dl>
                    <dl>
                        <dt>排序权重：</dt>
                        <dd>
                            <asp:TextBox ID="txtOrderNum" runat="server" CssClass="int" MaxLength="6" Width="80"></asp:TextBox></dd>
                    </dl>
                </div>
                <div id="infos2" style="display: none">
                    <dl style="border: 0;">
                        <dt>所属栏目：</dt>
                        <dd>
                            <asp:TextBox ID="txtCIDList" runat="server" CssClass="int" Width="180" style="display:none;"></asp:TextBox>
                            <input id="allcheck" type="button" value="全选" class="btnmini" onclick="alls('txtCheckBoxCID', 'allcheck');" /></dd>
                        <dd class="t">选择该自定义字段在添加内容时对应的哪些栏目可以使用(可多选)</dd>
                    </dl>
                    <dl>
                        <dt>&nbsp;</dt>
                        <dd>
                            <%=BindCheckBoxList()%>
                        </dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" OnClientClick="return do_checkfields();" /></dd>
                </dl>
            </div>
        </div>
        <script type="text/javascript">do_set_customfieldstype("<%=this.txtCustomFieldsType.Text%>");</script>
    </form>
</body>
</html>
