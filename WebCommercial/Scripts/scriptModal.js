$('#commandeDetailsModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var listArticles = button.data('listarticles');
    var noCom = button.data('nocom');
    var dateCom = button.data('datecom');
    var prixTotal = 0;

    var modal = $(this)
    
    var list = listArticles.split("/");
    list.forEach(function (arti) {
        if (arti.length < 1) { return; }

        arti = arti.replace(",", ".");
        var a = arti.split(";");
        var s = "<tr><td>" + a[0] + "</td><td>" + a[1] + "</td><td>" + a[2] + "</td><td>" + a[3] + "</td><td>" + Math.round(a[2] * a[3] * 100)/100 + "</td></tr>";
        prixTotal += a[2] * a[3];
        modal.find('.modal-body tbody').append(s);
    });

    prixTotal = Math.round(prixTotal * 100) / 100;

    dateCom = dateCom.replace(" ", " à ");

    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    
    modal.find('.modal-title').text('Détails de la commande N°' + noCom);
    modal.find('.modal-body #noCmde').text(noCom);
    modal.find('.modal-body #dateCmde').text(dateCom);
    modal.find('.modal-body #montantTotalCmde').text(prixTotal);
})