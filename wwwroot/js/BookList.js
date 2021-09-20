
var datatable;
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {

    //l'API DataTable a été Ajouté avec la reference JavaScript voir Le fichier Layout

   //on indique qu'il faudreait recuperer le fameux tableau qui a pour identifiant (DT_load)

    datatable = $('#DT_load').DataTable(
        {
            "ajax": {

                "url": "/api/Book",
                //type pour indiquer le type de la requêtte 
                "type": "GET",
                //datatype pour indiquer les type de données
                "datatype": "json",
            },
            //a l'interieur d'un tableau il est impossible d'utiliser un ensemble de clé et valeur sauf si vous introduisé la notion d'Objet qui est les {}
            //nous specifions a ce niveau toute les columns(on ne parle pa des valeurs) que nous devrons afficher c'est a dire une Liste de columns 
            "columns": [
                { "data": "name", "width": "20%" },
                { "data": "author", "width": "20%" },
                { "data": "isbn", "width": "20%" },

                //cette dernière columns concerne  les Actions (voir HTML)
                {
                    "data": "id",

                    "render": function (data) {


                        return `<div class="text-center">
                               
                          <a href="/BookList/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width=70%;'>
                                Edit
                        </a>
                         &nbsp;
                         <button  class='btn btn-danger text-white' style='cursor:pointer; width=70%;' onclick=Delete('api/Book?id='+${data})> 
                                            Delete 
                        </buttton>
                       </div>`;

                    }, "width": "40%",
                    
                }
            ],
            "language": {
                "emptyTable": "no data found"
            },
            "width": "100%"

        });

 
}

function Delete(url) {

    //utilisation de sweet-Alert
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {

                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }

                }
            });
        }
    });

}