/// <reference path="D:\HY\Baby\BabyBirth\api/PostAPI.aspx" />
$(document).ready(function () {
    initList()
    $('#place').on('change', function () {
        getHospital()
    })

    $('#HName').on('change', function () {
        getMidwife($(this).val())
    })
    $('#btnsubmit').on('click', function () {

        if (confirm("你确定录入吗？")) {
            insert();
        }
    })

    $('#btncancel').on('click', function () {
        if (confirm("你确定清空录入信息吗？")) {
            $('#content tr td input').val('');
        }
    })
    $('span.delete').on("click", function () {
        var index = $(this).closest('tr').index()
        Delete(index)

    })
    $('span.edit').on('click', function () {
        //用来传递当前选中行
        var index = $(this).closest('tr').index()
        $('#btnsubmit').text("修改")
        editInfo(index)
    })
})
//initial information when open
function initList() {
    $.ajax({
        url: "/api/Getlistapi.ashx",
        type: "Get",
        cache: false,//是否要缓存
        async: false,//是否要异步
        data: {

        },
        dataType: "json",//返回类型为“文本格式”:"text"；JSON格式：json
        success: function (list) {
            var html = "";
            if (list) {
                for (var i = 0; i < list.length; i++) {
                    var item = list[i];
                    var rectime = item.Recordtime.replace("/Date(", "").replace(")/", "");
                    var intime = item.Birthday.replace("/Date(", "").replace(")/", "");
                    var day = item.Birthday.replace("/Date(", "").replace(")/", "");

                    rectime = getLocalTime(parseInt(rectime))
                    intime = getLocalTime(parseInt(intime))
                    day = getLocalTime(parseInt(day))

                    html += '<tr>' +
                                    '<td>' + rectime + '</td>' +
                                    '<td>' + item.BName + '</td>' +
                                    '<td>' + item.Sex + '</td>' +
                                    '<td>' + item.BID + '</td>' +
                                    '<td>' + item.Hid + '</td>' +
                                    '<td>' + intime + '</td>' +
                                    '<td>' + day + '</td>' +
                                    '<td>' + item.HName + '</td>' +
                                    '<td>' + '<span class="edit blue" style="cursor:pointer">编辑</span> ' + ' <span class="delete blue" style="cursor:pointer" >删除</span>' + '</td>' +
                            ' </tr>'
                }
            };
            //empty:清空
            $("#information tbody").empty().append(html);
        },
        error: function (a, b, c) {

        }
    });

}

//转换时间戳,截取日期部分
function getLocalTime(nS) {
    var date = new Date(nS);
    var y = 1900 + date.getYear();
    var m = "0" + (date.getMonth() + 1);
    var d = "0" + date.getDate();
    return y + "-" + m.substring(m.length - 2, m.length) + "-" + d.substring(d.length - 2, d.length);
}
//转换时间戳,截取时间部分
function getTime(t) {
    return new Date(t).toTimeString().substr(0, 8)
}
//Get Hospital 
function getHospital() {
    if ($('#place').val() == 'hospital') {
        $.ajax({
            url: '/api/GetHospitalapi.ashx',
            type: 'get',
            cache: false,
            async: false,
            dataType: 'json',
            data: {

            },
            success: function (list) {
                var hname = ""
                if (list) {
                    //first option
                    hname += '<option value="' + list[0].HName + '">' + list[0].HName + '</option>'
                    for (var i = 0; i < list.length; i++) {
                        var item = list[i];
                        setCookie('midwife_' + item.HName + i, item.Midwife)


                        for (var j = i + 1; j < list.length - i; j++) {
                            if (i != 0 && list[i].HName == list[i - 1].HName) {
                                break
                            }
                            if (list[i].HName != list[j].HName && list[j].HName != list[j - 1].HName) {
                                hname += '<option value="' + list[j].HName + '">' + list[j].HName + '</option>'
                            }

                        }

                    }
                    $('#HName').empty().append(hname)
                }
            },
            error: function (a, b, c) {

            }
        })
    } else {
        $("#HName").append('<option>--</option>')
    }
}
//Get midwife
function getMidwife(hname) {
    var html = ''
    for (var i = 0; i < 20; i++) {
        var name = 'midwife_' + hname + i
        var mid = getCookie(name)
        if (mid) {
            html += '<option value="' + mid + '">' + mid + '</option>'
        }
    }
    $('#midwife').empty().append(html)

}




//insert data to database
function insert() {
    var patience = new Patient();
    patience.BID = $('#BID').val() == '' ? '1000002' : $('#BID').val();
    patience.MID = $('#midwife').val() == '--' ? '10001' : '10000';
    patience.BName = $('#BName').val();
    patience.Sex = $('#Sex').val();
    patience.Hid = $('#HID').val();
    patience.Intime = $('#intime').val();
    patience.Leavetime = $('#leavetime').val();
    patience.Birthday = $('#birthday').val();
    patience.Birthtime = $('#birthtime').val();
    patience.PregnantWeeks = parseInt($('#weeks').val()) * 7 + parseInt($('#days').val());
    patience.BirthPlace = $('#place').val();
    patience.BabyNum = $('#babynum').val();
    patience.BabyTime = $('#babytime').val();
    patience.BabyWeight = $('#babyweight').val();
    patience.BabyHeight = $('#babyheight').val();
    patience.Recordtime = Datetime_Now()
    patience = JSON.stringify(patience);



    $.ajax({
        url: '/api/Informationapi.ashx',
        type: "POST",
        cache: false,//是否要缓存
        async: false,//是否要异步
        //content-type:application/json,
        data: {
            patient: patience,
        },
        dataType: "text",//返回类型为“文本格式”:"text"；JSON格式：json
        success: function (msg) {
            alert(msg)
            initList()
            $('#content tr td input').val('');
        },
        error: function (a, b, c) {
            alert('failure')
        }
    })
};

//Patient model
function Patient() {
    this.BID;
    this.MID;
    this.BName;
    this.Sex;
    this.Hid;
    this.Intime;
    this.Leavetime;
    this.Birthday;
    this.Birthtime;
    this.BirthPlace
    this.PregnantWeeks;
    this.BabyNum;
    this.BabyTime;
    this.BabyWeight;
    this.BabyHeight;
    this.Recordtime
}
//Get current date
function Datetime_Now() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
    return currentdate;
}

function Delete(index) {
    if (!confirm('是否确认删除')) {
        return
    }
    var bid = $("#information tbody tr").eq(index).children('td').eq(3).text();
    $.ajax({
        url: '/api/DeleteByBIDapi.ashx',
        type: 'post',
        cache: false,
        async: false,
        dataType: 'text',
        data: {
            BID: bid
        },
        success: function (msg) {
            if (msg == 'Hello World') {
                alert(msg)
                initList();
            } else {
                window.alert(msg)
            }
        },
        error: function (a, b, c) {

        }

    })
}

//update information
function editInfo(index) {
    var bid = $("#information tbody tr").eq(index).children('td').eq(3).text()
    $.ajax({
        url: '/api/UptByBIDapi.ashx',
        type: 'get',
        cache: false,
        async: false,
        dataType: 'json',
        data: {
            BID: bid
        },
        success: function (info) {
            var intime = info.Intime.replace("/Date(", "").replace(")/", "")
            var leave = info.Leavetime.replace("/Date(", "").replace(")/", "")
            var bday = info.Birthday.replace("/Date(", "").replace(")/", "")
            var btime = info.Birthtime.replace("/Date(", "").replace(")/", "")

            leave = getLocalTime(parseInt(leave))
            intime = getLocalTime(parseInt(intime))
            bday = getLocalTime(parseInt(bday))
            btime = getTime(parseInt(btime))

            var day = parseInt(info.PregnantWeeks / 7)
            var time = parseInt(info.PregnantWeeks) % 7
            $('#BID').val(info.BID)
            $("#weeks").val(day)
            $('#days').val(time)
            $('#birthtime').val(btime)
            //sex
            $('#BName').val(info.BName)
            if (info.Sex == "xy") {
                $('#Sex option[text="男"]').attr("selected", true)
            } else if (sex == "xx") {
                $('#Sex option[text="女"]').attr("selected", true)
            } else {
                $('#Sex option[text="不详"]').attr("selected", true)
            }
            $('#babynum option[value=' + info.BabyNum + ']').attr("selected", true)
            //$("#select_id option[text='jQuery']").attr("selected", true);   //设置Select的Text值为jQuery的项选中
            $('#place').val(info.BirthPlace)
            $('#BID').val(info.BID)
            $('#HID').val(info.Hid)
            $("#intime").val(intime)
            $('#leavetime').val(leave)
            $('#midwife').append('<option value=' + info.Midwife + ' selected="selected">' + info.Midwife + '</option>')
            $('#HName option[value=' + info.HName + ']').attr("selected", true)
            $("#birthday").val(bday)
            $('#babytime').val(info.BabyTime)
            $('#BabyWeight').val(info.BabyWeight)
            $('#BabyHeight').val(info.BabyHeight)
        },
        error: function (a, b, c) {

        }

    })

}

function setCookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}
