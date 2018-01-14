$(document).ready(function () {
    initList();



    $('#btnsubmit').click(function () {
        insert();
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
    var patience = GetInfo();
    $.ajax({
        url: "PostAPI.aspx",
        type: "post",
        cache: false,//是否要缓存
        async: true,//是否要异步
        data: {
            patient: patience,
        },
        dataType: "json",//返回类型为“文本格式”:"text"；JSON格式：json
        success: function (msg) {
            alert(msg)
        },
        error: function (a, b, c) {
            alert('failure')
        }
    })
};

function GetInfo() {
    var bid = $('#BID').val() == '' ? '0.0.0' : $('#BID').val();
    var sex = $('#Sex').val();
    var name = $('#BName').val();
    var hid = $('#HID').val();
    var intime = $('#intime').val();
    var leave = $('#leavetime').val();
    var day = $('#birthday').val();
    var time = $('#birthtime').val();
    var date = $('#weeks').val() * 7 + $('#days').val();

    var place = $('#place').val();
    var hospital = $('#HName').val();
    var midwife = $('#midwife').val();
    var babynum = $('#babynum').val();

    var babytime = $('#babytime').val();
    var babyweight = $('#babyweight').val();
    var babyheight = $('#babyheight').val();
    var patience = [bid, sex, name, hid, intime, leave, day, time, date, place, babynum, babytime, babyweight, babyheight];
    var hospital = [hospital, midwife];
    return patience;
}
