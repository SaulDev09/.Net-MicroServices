var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/getall" },
        order: [[0, 'desc']],
        "columns": [
            { data: 'orderHeaderId', "width": "5%" },
            { data: 'email', "width": "25%" },
            { data: 'name', "width": "20%" },
            { data: 'phones', "width": "10%" },
            { data: 'status', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'orderHeaderId',
                "width": "10%",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/order/orderDetail?orderId=${data}" class="btn btn-primary mx-2">
                        <i class="bi bi-pencil-square"></i>
                    </a>
                    </div>`
                },
            },

        ]
    });
}