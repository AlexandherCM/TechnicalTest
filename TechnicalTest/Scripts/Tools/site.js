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
        if (result.isConfirmed) {
            api.redirectToAction('https://localhost:44373/Home/Index');
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