/// <reference path="D:\HY\Baby\BabyBirth\api/PostAPI.aspx" />
$(document).ready(function () {
    initList();



    $('#btnsubmit').click(function () {
        insert();
    })

    $('#btncancel').click(function () {
        if (confirm("你确定取消录入吗？")) {
            $('#content tr td input').val('');
        }
    })
})

function initList() {
    $.ajax({
        url: "GetInfoAPI.aspx",
        type: "Get",
        cache: false,//是否要缓存
        async: true,//是否要异步
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
                                    '<td>' + '<a>编辑</a> ' + ' <a >删除</a>' + '</td>' +
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

//转换时间戳
function getLocalTime(nS) {
    return new Date(nS).toLocaleString().replace(/:\d{1,2}$/, '-').substr(0, 9)
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
        url: '/api/PostAPI.aspx',
        type: "POST",
        cache: false,//是否要缓存
        async: true,//是否要异步
        //content-type:application/json,
        data: {
            patient: patience,
        },
        dataType: "text",//返回类型为“文本格式”:"text"；JSON格式：json
        success: function (msg) {
            alert(msg)
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

