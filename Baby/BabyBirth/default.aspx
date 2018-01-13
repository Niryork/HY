<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BabyBirth._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>出生信息登记</title>
    <link href="css/common.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/laydate/laydate.js"></script>
    <script src="js/PostToBack.js"></script>
</head>
<body>
    <form id="info" runat="server">
        <div class="container border">
            <div style="height: 20px; vertical-align: central; border-bottom: 1px solid black"><b class="pleft10">》当前功能:出生信息登记</b></div>
            <table class="pleft10" style="width: 98%;">
                <tr>
                    <td class="header">
                        <b>======-出生信息登记操作====================================</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="content">
                            <tr>
                                <td style="width: 60px;">出生证号:</td>
                                <td style="width: 130px;">
                                    <input id="BID" type="text" /></td>
                                <td style="width: 60px;">姓&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;名:</td>
                                <td style="width: 130px;">
                                    <input id="BName" type="text" /></td>
                                <td style="width: 60px;">性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 别:</td>
                                <td colspan="3" style="width: 200px;">
                                    <select id="Sex">
                                        <option value="man">男</option>
                                        <option value="man">女</option>
                                        <option value="man">不详</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>住&nbsp; 院 号:</td>
                                <td>
                                    <input id="HID" type="text" /></td>
                                <td>住院日期:</td>
                                <td>
                                    <input type="text" id="intime" class="time" placeholder="  -  -  " />
                                </td>
                                <td>出院日期:</td>
                                <td colspan="3">
                                    <input id="leavetime" type="text" class="time" style="width: 80px;" placeholder="  -  -  " />
                                </td>
                            </tr>
                            <tr>
                                <td>出生日期:</td>
                                <td>
                                    <input id="birthday" type="text" class="time" placeholder="  -  -  " />
                                </td>
                                <td>出生时间:</td>
                                <td>
                                    <input id="birthtime" type="text" /></td>
                                <td>出生孕周:</td>
                                <td colspan="3">
                                    <input id="weeks" type="text" style="width: 40px;" />周<input id="days" type="text" style="width: 35px;" />天
                                </td>
                            </tr>
                            <tr>
                                <td>分娩地点:</td>
                                <td>
                                    <select id="place">
                                        <option value="man">医院</option>
                                        <option value="man">在家</option>
                                        <option value="man">其他</option>
                                    </select>
                                </td>
                                <td>接生医院:</td>
                                <td>
                                    <select id="HName">
                                        <option value="yb">福建省妇幼保健院</option>
                                        <option value="slyy">福建省立医院</option>
                                    </select>
                                </td>
                                <td>接 生 员:</td>
                                <td colspan="3">
                                    <select id="midwife">
                                        <option value="--">--</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>胎&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 数:</td>
                                <td class="auto-style6">
                                    <select id="babynum">
                                        <option value="one">单胎</option>
                                        <option value="twin">双胎</option>
                                        <option value="more">多胎</option>
                                    </select>
                                </td>
                                <td>胎&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 次:</td>
                                <td>
                                    <input id="babytime" type="text" style="width: 35px" />
                                </td>
                                <td>出生体重</td>
                                <td>
                                    <input id="babyweight" type="text" style="width: 45px" />kg&nbsp;
                                </td>
                                <td style="width: 60px;">出生身长:</td>
                                <td>
                                    <input id="babyheight" type="text" style="width: 45px" />cm
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <div class="tr fl pright10" style="width: 68%;">
                                <input id="btnsubmit" type="button" value="提交" />
                            </div>
                            <div class="tl fl pleft10">
                                <input id="btncancel" type="button" value="取消" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="tr">分页预留</div>
                    </td>
                </tr>
                <tr>
                    <td class="header"><b>======-出生信息查询====================================</b></td>
                </tr>
                <tr>
                    <td>
                        <div class="vc">
                            <span>
                                <select id="searchbar">
                                    <option value="BID">出生证号</option>
                                    <option value="BName">姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 名</option>
                                    <option value="HID">住&nbsp; 院 号</option>
                                </select>
                            </span>
                            <span>
                                <input type="text" id="search" />&nbsp;&nbsp;
                            </span>
                            <span>登记日期: </span>
                            <input id="begintime" type="text" class="time" /><span> 到 </span>
                            <span>
                                <input id="endtime" type="text" class="time" /></span>
                            <%--<asp:Button ID="btnSearch" runat="server" Text=" 查询 " BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="mleft10" />--%>
                            <span><input id="btnsearch" type="button" value="查询" /></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <div class="fl" style="width:100%">
                            <table style="width:100%">
                                <thead>
                                    <tr>
                                        <th>登记日期</th>
                                        <th>姓 名</th>
                                        <th>性 别</th>
                                        <th>出生证号</th>
                                        <th>住院号</th>
                                        <th>住院日期</th>
                                        <th>出生日期</th>
                                        <th>接生医院</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody id="List">
                                    <tr>
                                        <td>1</td>
                                        <td>2</td>
                                        <td>3</td>
                                        <td>4</td>
                                        <td>5</td>
                                        <td>6</td>
                                        <td>7</td>
                                        <td>8</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
<script>
    //日期选择器
    lay('.time').each(function () {
        laydate.render({
            elem: this,
            trigger: 'click'
        });
    });
    //时间选择器
    laydate.render({
        elem: '#birthtime',
        type: 'time'
    });

</script>

</html>
