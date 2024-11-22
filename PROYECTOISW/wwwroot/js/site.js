// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Manejar el clic en los enlaces del menú lateral
    $('.sidebar ul li a').click(function (event) {
        event.preventDefault();
        var target = $(this).data('target');
        $('.formulario').hide(); // Ocultar todas las vistas parciales
        $(target).show(); // Mostrar la vista parcial seleccionada
    });

    // Manejar el envío del formulario de cambiar contraseña
    $('#formCambiarContrasena').submit(function (event) {
        event.preventDefault(); // Prevenir el envío normal del formulario
        var form = $(this);
        var url = form.attr('action'); // Obtener la URL de acción del formulario
        var data = form.serialize(); // Serializar los datos del formulario

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                $('#resultadoContrasena').html('<p>Contraseña cambiada exitosamente.</p>');
            },
            error: function () {
                $('#resultadoContrasena').html('<p>Hubo un error al cambiar la contraseña.</p>');
            }
        });
    });

    // Manejar el envío del formulario de cambiar teléfono
    $('#formCambiarTelefono').submit(function (event) {
        event.preventDefault(); // Prevenir el envío normal del formulario
        var form = $(this);
        var url = form.attr('action'); // Obtener la URL de acción del formulario
        var data = form.serialize(); // Serializar los datos del formulario

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                $('#resultadoTelefono').html('<p>Teléfono cambiado exitosamente.</p>');
            },
            error: function () {
                $('#resultadoTelefono').html('<p>Hubo un error al cambiar el teléfono.</p>');
            }
        });
    });
});
