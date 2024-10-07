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
            { "data": "email", "widht": "15%" },
        ],
        "responsive": true,
        "autoWidth": false,   // Helps in adjusting column widths automatically
        "scrollX": true       // Adds horizontal scrolling if the table is wide for mobile

    });
}




