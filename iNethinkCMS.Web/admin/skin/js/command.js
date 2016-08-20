$(function () {
    $(".switchs li").click(function () {
        $(".switchs li").addClass("c");
        $(this).removeClass("c");
        $(".info div").css("display", "none");
        $("#info" + $(this).attr("id")).css("display", "block");
    });
});

function loadJS(url, callback, charset) {
    var script = document.createElement('script');
    script.onload = script.onreadystatechange = function () {
        if (script && script.readyState && /^(?!(?:loaded|complete)$)/.test(script.readyState)) return;
        script.onload = script.onreadystatechange = null;
        script.src = '';
        script.parentNode.removeChild(script);
        script = null;
        if (callback) callback();
    };
    script.charset = charset || document.charset || document.characterSet;
    script.src = url;
    try { document.getElementsByTagName("head")[0].appendChild(script); } catch (e) { }
}

function JumpFrame(url1, url2) {
    if (url1) { window.parent.left.location.href = url1; }
    if (url2) { window.parent.main.location.href = url2; }
}

function alls(divc, inputs) {
    if ($("#" + inputs).attr('value') == '全选') {
        $("input[name=" + divc + "]").attr('checked', true);
        $("#" + inputs).attr('value', '取消全选');
    } else {
        $("input[name=" + divc + "]").attr('checked', false);
        $("#" + inputs).attr('value', '全选');
    }
}

function alls_channelpower(inputs) {
    var channleNum = document.getElementById("txtUserChannelPower").getElementsByTagName("input").length;
    if ($("#" + inputs).attr('value') == '全选') {
        for (var i = 0; i < channleNum; i++) {
            document.getElementById("txtUserChannelPower_" + i).checked = true;
        }
        $("#" + inputs).attr('value', '取消全选');
    }
    else {
        for (var i = 0; i < channleNum; i++) {
            document.getElementById("txtUserChannelPower_" + i).checked = false;
        }
        $("#" + inputs).attr('value', '全选');
    }
}

function change_channelpower() {
    if ($("#txtUserChannelPowerAll").is(":checked") == true) {
        $("#channelpowerselect").css("display", "none");
    }
    else {
        $("#channelpowerselect").css("display", "block");
    }
}

function do_content(val) {
    var id = '';
    var runit = false;
    form_news_content.action = '?act=' + val;

    for (var i = 0; i < $("input[type='checkbox']").length; i++) {
        if ($("input[type='checkbox']")[i].checked) { runit = true; }
    }
    if (runit) {
        if (val == 'deletes') {
            if (confirm('您确定要删除这些记录吗?')) { form_news_content.submit(); }
        } else {
            form_news_content.submit();
        }
    } else {
        alert("请选择需要操作的信息!");
    }
}

function js_showImg(bySecond, bySelVal) {
    if (art.dialog.list['art_img'] != null) { art.dialog.list['art_img'].close(); }
    if (bySelVal != "" && bySelVal != null) {
        var timer;
        var i = bySecond;

        if (bySecond > 0) {
            art.dialog({
                id: 'art_img',
                title: false,
                content: '<div><div id="iTimeShow" class="con_timeshow"></div><img src=' + bySelVal + ' width=225></div>',
                time: i,
                init: function () {
                    var fn = function () {
                        $('#iTimeShow').html(i + '秒后关闭');
                        !i && this.close();
                        i--;
                    };
                    timer = setInterval(fn, 1000);
                    fn();
                },
                close: function () { clearInterval(timer); },
                left: '100%',
                top: 5,
                drag: false,
                resize: false,
                padding: "1px 1px 1px 1px"
            });
        } else {
            art.dialog({
                id: 'art_img',
                title: false,
                content: '<div><div id="iTimeShow" class="con_timeshow"></div><img src=' + bySelVal + ' width=225></div>',
                left: '100%',
                top: 5,
                drag: false,
                resize: false,
                padding: "1px 1px 1px 1px"
            });
        }
    }
}

function do_checkfields() {
    var runit = false;
    for (var i = 0; i < $("input[name='myCustomFieldsType']").length; i++) {
        if ($("input[name='myCustomFieldsType']")[i].checked) {
            runit = true;
        }
    }
    if (!runit) {
        alert("请选择字段类型!");
        return false;
    }

    //next	
    runit = false;
    var cidlist = '';
    for (var i = 0; i < $("input[name='txtCheckBoxCID']").length; i++) {
        if ($("input[name='txtCheckBoxCID']")[i].checked) {
            runit = true;
        }
    }

    if (!runit) {
        alert("请选择字段所属栏目!");
        return false;
    }

    $("input[name='myCustomFieldsType']").each(function () {
        if ($(this).attr("checked")) {
            $("#txtCustomFieldsType").val($(this).val());
        }
    });


    $("input[name='txtCheckBoxCID']").each(function () {
        if ($(this).attr("checked")) {
            cidlist += $(this).val() + ",";
        }
    });

    if (cidlist.length > 0) {
        cidlist = cidlist.substring(0, cidlist.length - 1)
    }

    $("#txtCIDList").val(cidlist);
    return true;
}

function do_set_customfieldstype(val) {
    $("input[name='myCustomFieldsType']").each(function () {
        if ($(this).val() == val) {
            $(this).attr("checked", true);
        }
    });
}

function do_showrewriteset() {
    var jsUrlmode = $("#txtUrlMode").val();
    $("#s3").css("display", "none");
    if (jsUrlmode == 1) {
        $("#s3").css("display", "block");
    }
}

function initEditor(val) {
    loadJS("/inc/xheditor/xheditor.js", function () {
        loadJS('/inc/xheditor/xheditor_lang/zh-cn.js');
        $('#' + val).xheditor({ tools: 'full', width: '100%', height: 250, forcePtag: false, cleanPaste: 2, html5Upload: false });
    });
}

function ajax_content_customfields(jsID) {
    var jsCid = $("#txtCid").val();

    if (jsCid == 0) {
        $("#s3").css("display", "none");
        $("#infos3").html("");
        return;
    }

    //$("#s3").css("display", "block");
    $("#infos3").html("<dl style='border: 0;'><dt>&nbsp;</dt><dd>正在分析该栏目的自定义字段，请稍等...</dd></dl>");

    $.ajax({
        url: "../../inc/ajax.aspx?act=getcustomfields&cid=" + jsCid + "&id=" + jsID + "&r=" + Math.random(),
        type: 'GET',
        cache: false,
        success: function (data) {
            $("#infos3").html(data);

            if (data.length == 0) {
                $("#s3").css("display", "none");
            } else {
                $("#s3").css("display", "block");
            }
        }
    });
}

function ajax_content_checktitle(jsID) {
    var jsTitle = $("#txtTitle").val();
    if (jsTitle.length > 0) {
        $.ajax({
            url: "../../inc/ajax.aspx?act=checktitle&id=" + jsID + "&title=" + jsTitle + "&r=" + Math.random(),
            type: 'GET',
            cache: false,
            success: function (data) {
                if (data.length > 0) {
                    alert(data);
                }
            }
        });
    }
}

function checkpost_content() {
    var chkok = true;
    if (chkok) { if ($("#txtCid").val() == "0") { alert('请选择所属栏目!'); chkok = false; } }
    if (chkok) { if ($("#txtTitle").val().length <= 0) { alert('请输入文章标题!'); chkok = false; } }
    if (chkok) { if ($("#txtContents").val().length <= 0) { alert('请输入内容!'); chkok = false; } }
    return chkok;
}