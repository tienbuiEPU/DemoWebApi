$(document).ready(function () {
    loadData();
});

//Load data function
function loadData() {
    $.ajax({
        method: "GET",
        url: "http://localhost:58555/api/Student",
        success: function (data) {
            console.log(data);
            var html = '';
            $.each(data, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.id + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.email + '</td>';
                html += '<td><a href="#" onclick="return edit(' + item.id + ');">Edit</a> | <a href="#" onclick="return deleteRow(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('#tbody').html(html);
        }
    });
}

//add record function

function add() {
    var name = $('#name').val();
    var email = $('#email').val();

    $.ajax({
        method: "POST",
        url: "http://localhost:58555/api/Student?name=" + name + "&email=" + email,
        success: function (data) {
            loadData();
            $('#myModal').modal('hide');
        }

    });
}

//delete record function
function deleteRow(id) {
    var ans = confirm("Are you sure delete this record");

    if (ans) {
        $.ajax({
            method: "DELETE",
            url: "http://localhost:58555/api/Student/" + id,
            success: function (data) {
                loadData();
            }
        });
    }
}

//edit record funtion
function edit(id) {
    $.ajax({
        method: "GET",
        url: "http://localhost:58555/api/Student/" + id,
        success: function (data) {
            $('#STDID').val(data.id);
            $('#name').val(data.name);
            $('#email').val(data.email);
            $('#myModal').modal('show');
            $('#btnAdd').hide();
            $('#btnUpdate').show();
        }
    });
}

//update record function
function update() {
    var name = $('#name').val();
    var email = $('#email').val();
    var id = $('#STDID').val();
    $.ajax({
        method: "POST",
        url: "http://localhost:58555/api/Student/" + id + "?name=" + name + "&email=" + email,
        data: {
            id: id,
            name: name,
            email: email
        },
        success: function (data) {
            loadData();
            $('#myModal').modal('hide');
        }
    });
}

//clear function
function clearText() {
    $('#STDID').val("");
    $('#name').val("");
    $('#email').val("");
    $('#btnAdd').show();
    $('#btnUpdate').hide();
}

//function search
function searchItem() {
    id = $('#search').val();
    $.ajax({
        method: "GET",
        url: "http://localhost:58555/api/Student/" + id,
        success: function (data) {
            if (data) {
                var html = '';
                html += '<tr>';
                html += '<td>' + data.id + '</td>';
                html += '<td>' + data.name + '</td>';
                html += '<td>' + data.email + '</td>';
                html += '<td><a href="#" onclick="return edit(' + data.id + ');">Edit</a> | <a href="#" onclick="return deleteRow(' + data.id + ')">Delete</a></td>';
                html += '</tr>';
                $('#tbody').html(html);
            }
        }
    });
}
