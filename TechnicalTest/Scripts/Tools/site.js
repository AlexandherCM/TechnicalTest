// FUNCIONES DE SWEETALERT - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

function SweetAlert(AlertJS) {
    if (AlertJS == null)
        return;

    let type;

    if (AlertJS.Estado === 'info') {
        type = 'info';
    }
    else {
        type = AlertJS.Estado ? 'success' : 'error';
    }

    Swal.fire({
        title: AlertJS.Titulo,
        html: AlertJS.Html || AlertJS.Leyenda,
        icon: type,
        confirmButtonText: 'Aceptar'
    }).then((result) => {
        if (result.isConfirmed && AlertJS.Redirect) {
            api.redirectToAction(`${api.urlMVC}${AlertJS.Redirect}` );
        }
    });
}

//function SweetAlert(AlertJS) {
//    if (AlertJS == null)
//        return;

//    let type;
//    if (AlertJS.Estado === 'info')
//        type = 'info';
//    else
//        type = AlertJS.Estado ? 'success' : 'error';

//    Swal.fire({
//        title: AlertJS.Titulo,
//        text: AlertJS.Leyenda,
//        icon: type,
//        html: AlertJS.Html
//    });
//}