var dataTable;

$(document).ready(function () {

    loadDataTable();
});



function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/ClientInformations/GetAll"
        },
        "columns": [
            { "data": "name", "widht": "15%" },
            { "data": "companyName", "widht": "15%" },
            { "data": "phoneNumber", "widht": "15%" },
            { "data": "email", "widht": "15%" },
            { "data": "question", "widht": "15%" },
            { "data": "howDidYouFindUs", "widht": "15%" },
        ],
    });
}




