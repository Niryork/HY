$(function () {
    initList();
});
function initList(){
    $.ajax({
        url: "default.aspx",
        method: "Get",
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
                    html += '<tr>' +
                                    '<td>' + item.Recordtime + '</td>' +
                                    '<td>' + item.BName + '</td>' +
                                    '<td>' + item.Sex + '</td>' +
                                    '<td>' + item.BID + '</td>' +
                                    '<td>' + item.Hid + '</td>' +
                                    '<td>' + item.Intime + '</td>' +
                                    '<td>' + item.Birthday + '</td>' +
                                    '<td>' + item.HName + '</td>' +
                                    '<td>' + '<a>Edit</a>'  +'</td>'+
                            ' </tr>'
                }
            };
            //'<td><a class="upt">编辑</a> <a class="delete"  data-bid="' + item.BID + '">删除</a> </td>' +

            //<a href="stuedit.html?sno=' + item.sno + '&name=' + escape("冰冰") + '">修改</a>
            //empty:清空
            $("table tbody tr").empty().append(html);
        },
        error: function (a, b, c) {

        }
    });
}