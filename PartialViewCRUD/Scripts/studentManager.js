var pageNumber = 1;  
var pageSize = 5;
var search = ""; 
function EditClick() {
    $('.btn-edit').click(function () {
        var id = $(this).attr('data-id');  
        var name = $(this).attr('data-name');  
        var age = $(this).attr('data-age');  
        var birth = $(this).attr('data-birth');
        var IDClass = $(this).attr('data-class');  
        var address = $(this).attr('data-address');
        $('.add-or-edit input[name="id"]').val(id);  
        $('.add-or-edit input[name="name"]').val(name);  
        $('.add-or-edit input[name="age"]').val(age);  
        $('.add-or-edit input[name="address"]').val(address);
        $('.add-or-edit input[name="birth"]').val(birth);
        $('.add-or-edit #idclass').val(IDClass);
    });  
 
}
EditClick(); 

function GetListStudent() {
    
    $.ajax({
        url: "/StudentManager/ListStudent", 
        type: "POST",  
        dataType: "html", 
        data: {
            pageNumber: pageNumber,
            pageSize: pageSize,
            search: search
        },  
        beforeSend: function () {

        },  
        success: function (res) {
            $('.get-data').html(''); 
            $('.get-data').append(res);
            EditClick(); 
            Pagination(); 
        }, 
        error: function () {

        },
        complete: function () {

        }
    })
}
GetListStudent();
AddStudent(); 
Update(); 
function AddStudent() {
    $('.btn-createEdit').click(function () {
        var name = $('.add-or-edit input[name="name"]').val();
        var address = $('.add-or-edit input[name="address"]').val();
        var age = $('.add-or-edit input[name="age"]').val();
        var birth = $('.add-or-edit input[name="birth"]').val();
        var idClass = $('.add-or-edit #idclass').val();
        if (Check(name, address, age, birth, idClass) == true) {
            $.ajax({
                url: "/StudentManager/Create",
                type: "POST",
                dataType: "json",
                data: {
                    name: name,
                    address: address,
                    age: age,
                    birth: birth,
                    idClass: idClass
                },
                beforeSend: function () {

                },
                success: function (res) {
                    if (res) {
                        GetListStudent();
                        DefaultValueInput();
                        alert('Thêm thành công!');
                    } 
                },
                error: function () {

                },
                complete: function () {

                }
            })
        }
       
    })
    
}
function Update() {
    $('.btn-update').click(function () {
        var id = $('.add-or-edit input[name="id"]').val();  
        var name = $('.add-or-edit input[name="name"]').val();
        var address = $('.add-or-edit input[name="address"]').val();
        var age = $('.add-or-edit input[name="age"]').val();
        var birth = $('.add-or-edit input[name="birth"]').val();
        var idClass = $('.add-or-edit #idclass').val();

        $.ajax({
            url: "/StudentManager/Update",
            type: "POST",
            dataType: "json",
            data: {
                id: id,
                name: name,
                address: address,
                age: age,
                birth: birth,
                idClass: idClass
            },
            beforeSend: function () {

            },
            success: function (res) {
                if (res) {
                    GetListStudent();
                    DefaultValueInput();
                }
            },
            error: function () {

            },
            complete: function () {

            }
        })
    })
}
function DefaultValueInput() {
    $('.add-or-edit input[name="name"]').val('');
    $('.add-or-edit input[name="address"]').val('');
    $('.add-or-edit input[name="age"]').val('');
    $('.add-or-edit input[name="birth"]').val('');
    $('.add-or-edit #idclass').val('1');
}

function Pagination() {
    $('.pagination button').click(function () {
        pageNumber = $(this).attr('data-page');
        GetListStudent(); 
    });  
}
function Search() {
    $('input[name="searchName"]').keyup(function () {
        search = $(this).val();
        GetListStudent(); 
    });  
}
Search(); 

function Check(name, address, age, birth, idClass) {
    if (name.trim() == '' || address.trim() == '' || age.trim() == '' || birth.trim() == '' || idClass.trim() == '') {
        $('.error').html('');
        $('.add-or-edit input').each(function () {
            if ($(this).val().trim() == '') {
                var name = $(this).siblings('label').text();
                var row = `<p> ${name} không được để trống!</p>`; 
                $(this).siblings('.error').append(row); 
            }
        })
        return false;  
    }
    else return true; 
}
