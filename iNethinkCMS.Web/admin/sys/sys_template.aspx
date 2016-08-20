<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_template.aspx.cs" Inherits="iNethinkCMS.Web.admin.sys.sys_template" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_sys_template" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main" id="mainID" runat="server">
            <ul class="switchs">
                <li id="s1">本地可用模板</li>
                <li id="s2" class="c">模板制作&使用说明</li>
            </ul>
            <div class="info">
                <div id="infos1">
                    <table width="100%" class="tablist">
                        <tr class="trbg">
                            <td width="140">缩略图
                            </td>
                            <td width="260">模板信息
                            </td>
                            <td>注意事项/其它信息
                            </td>
                            <td width="80">启用状态
                            </td>
                            <td width="120">管理
                            </td>
                        </tr>
                        <tr id="iNoInfo" runat="server">
                            <td colspan="7">暂无任何数据
                            </td>
                        </tr>
                        <asp:Repeater ID="Repeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <a href="<%#DataBinder.Eval(Container.DataItem,"SkinThumbnail")%>" target="_blank">
                                            <img src="<%#DataBinder.Eval(Container.DataItem,"SkinThumbnail")%>" width="100" height="100" />
                                        </a></td>
                                    <td>
                                        <ul style="text-align: left; line-height: 18px;"><%#DataBinder.Eval(Container.DataItem,"SkinInfo")%></ul>
                                    </td>
                                    <td>
                                        <ul style="text-align: left; line-height: 18px;">
                                            <%#DataBinder.Eval(Container.DataItem,"Announcements")%>
                                        </ul>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem,"SkinState")=="1"?"<font color=\"#ff0000\">已启用</font>":"未启用"%>
                                    </td>
                                    <td>
                                        <ul style="line-height: 20px;"><%#DataBinder.Eval(Container.DataItem,"SkinManage")%></ul>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div id="infos2" style="display: none; border: 0; line-height: 220%; padding-left: 10px;">
                    【<strong><font color="#ff0000">重要约定</font></strong>】
                    <br />
                    &nbsp;&nbsp;所有模板文件均放置于系统的“template”目录下，每个目录对应一个模板风格包，完整的模板风格包应当包含：模板文件、基础数据（数据脚本、config.xml）文件、模板缩略图文件。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1）数据脚本文件sqlscript.sql，（当前版本需要您手工创建）是为了方便安装模板后能完整展示模板细节。<font color="#ff0000">注：文件编码必须为UTF-8</font>；<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;导出表：iNethinkCMS_Channel, iNethinkCMS_Channel_CustomFields, iNethinkCMS_Custom_Pages, iNethinkCMS_Custom_Tags, iNethinkCMS_Special中的数据脚本即可。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2）config.xml文件是为了分析该模板的相关信息。进行发布模板操作后，系统会自动创建，您也可根据实际情况进行修改；<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3）模板缩略图文件（手工创建）是为了显示该模板缩略图，默认命名为：thumb.jpg。<br />
                    <br />
                    【<strong>模板安装设置</strong>】<br />
                    <strong>模板安装</strong>：下载模板包后，将模板包拷贝至系统template目录下，后台刷新模板管理，即可看到新加的模板。<br />
                    <strong>启用模板</strong>：点击“启用模板”，将置换此模板为默认模板，如果存在数据脚本文件，则会将此模板目录中的数据脚本导入现在的数据库中,执行完成后会将sqlscript.sql更名为sqlscript.sqlbak。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="#ff0000">！请注意，此操作会删除并新增系统数据库的相关信息，操作前请对数据库进行备份操作。</font><br />
                    <strong>发布模板</strong>：点击“发布模板”，将把当前模板信息生成基础数据文件，此操作不会导出涉及系统隐私数据，请注意此处备份并非完整系统数据备份，不能替代系统数据备份功能。<br />
                    <strong>删除模板</strong>：将系统“template”目录下的对应模板文件夹完整删除即可。<br />
                    <br />
                    【<strong>模板的发布与分享</strong>】<br />
                    <strong>模板包命名</strong>：模板包只能由英文、数字、下划线组合，如果需要发布模板包，请尽量将模板包复杂命名，避免与其他用户的模板包文件夹冲突。<br />
                    <strong>模板包目录</strong>：模板文件放于根目录template，其中每个含有“config.xml”文件的文件夹为一套模板。<br />
                    <strong>发布模板</strong>：执行发布模板操作之后，模板风格目录即可作为一个完整模板包进行发布和分享。

                </div>
            </div>
        </div>
    </form>
</body>
</html>
