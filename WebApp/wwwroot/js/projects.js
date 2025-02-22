﻿var dataTable;

$(document).ready(function () {

    loadDataTable();
});



function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/ProjectsManagement/GetAll"
        },
        "columns": [
            { "data": "title", "widht": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="w-75 btn-group" role="group">
                        <a href="/Admin/ProjectsManagement/Upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i>Update
                        </a>
                        <a onClick=Delete('/Admin/ProjectsManagement/Delete/${data}')
                           class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i>Delete
                        </a>
                    </div>
                    `
                },
                "widht": "15%"
            },
        ],
        "responsive": true,
        "autoWidth": false,   // Helps in adjusting column widths automatically
        "scrollX": true       // Adds horizontal scrolling if the table is wide for mobile
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You can't go back!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it'
    }).then((result) => {
        if (result.isConfirmed) {
            //We will have to make an ajax request to hit the endpoint for delete
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        //Reload datatable
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

