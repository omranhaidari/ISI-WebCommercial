
// ----- Global -----------
var articles;


// ----- Modal --------
$('#confirmationMessageModal').on('show.bs.modal', function (event) {
    // Fonction pas utilisée
    var button = $(event.relatedTarget); // Button that triggered the modal
    var titre = button.data('titre');
    var message = button.data('message');
    var action = button.data('action');

    var modal = $(this);

    modal.find('.modal-title').text(titre);
    modal.find('.modal-body #confirmationMessageText').text(message);
    modal.find('.modal-body #confirmationMessageValider').text(action);
});

$('#commandeDetailsModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var listArticles = button.data('listarticles');
    var noCom = button.data('nocom');
    var dateCom = button.data('datecom');
    var prixTotal = 0;

    var modal = $(this)

    modal.find('.modal-body tbody').html("");

    var list = listArticles.split("/");
    list.forEach(function (arti) {
        if (arti.length < 1) { return; }

        arti = arti.replace(",", ".");
        var a = arti.split(";");
        var s = "<tr><td>" + a[0] + "</td><td>" + a[1] + "</td><td>" + a[2] + "</td><td>" + a[3] + "</td><td>" + Math.round(a[2] * a[3] * 100) / 100 + "</td></tr>";
        prixTotal += a[2] * a[3];
        modal.find('.modal-body tbody').append(s);
    });

    prixTotal = Math.round(prixTotal * 100) / 100;

    dateCom = dateCom.split(" ")[0];

    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.

    modal.find('.modal-title').text('Détails de la commande N°' + noCom);
    modal.find('.modal-body #noCmde').text(noCom);
    modal.find('.modal-body #dateCmde').text(dateCom);
    modal.find('.modal-body #montantTotalCmde').text(prixTotal);
});

$('#commandeArticleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var noCom = button.data('nocom');
    var typeAction = button.data('action');
    var noArt = button.data('noart');
    var quantite = button.data('quantite');
    var libelle, prix;

    var titre = button.data('titre');

    var form = document.getElementById("GestionArticleForm");

    document.getElementById("articleCommandeModalValidation").textContent = titre;

    var modal = $(this);

    var listOptions = document.getElementById('ListArticle').getElementsByTagName("option");
    if (articles == undefined || articles.size == 0) {
        articles = new Map();

        for (var i = 0; i < listOptions.length; i++) {
            var e = listOptions.item(i);
            var arti = e.text.split("/;/");

            articles.set(arti[0], {
                noArticle: arti[0],
                libelle: arti[1],
                prix: arti[2]
            });

            e.text = "(N°" + arti[0] + ") " + arti[1];
            if (noArt !== "" && noArt == arti[0]) {
                e.selected = true;
                libelle = arti[1];
                prix = arti[2];
            }
        }
    }

    // ================ Fin initialisation des variables =================
    //                      Tout le code va dessous

    document.getElementById('ListArticle').addEventListener("input", function (evt) {
        noArt = evt.target.value;
        libelle = articles.get(noArt).libelle;
        prix = articles.get(noArt).prix;
        updateForm();
    });

    updateForm();

    function updateForm() {
        modal.find('.modal-title').text(titre);
        modal.find('.modal-body #NoArt').val(noArt);
        modal.find('.modal-body #Libelle').val(libelle);
        modal.find('.modal-body #PrixArt').val(prix ? prix.replace(",", ".") : prix);
        modal.find('.modal-body #QuantiteArt').val(quantite);
        document.getElementById('QuantiteArt').addEventListener("input", function (evt) {
            quantite = evt.target.value;
            updateTotal(quantite * prix);
        });
        updateTotal(); // Essaie de mettre à jour le montant total une première fois

        function updateTotal() {
            if (quantite != undefined && quantite != "") {
                if (prix != undefined && prix != "") {
                    modal.find('.modal-body #MontantTotalArt').val(Math.round(prix.replace(",", ".") * quantite * 100) / 100);
                }
            } else {
                quantite = 1;
                updateForm();
            }
        }
    }

    function showErreur(msg) {
        modal.find('.modal-body #erreurField').text(msg);
    }


    document.getElementById('articleCommandeModalValidation').addEventListener("click", function (evt) {
        var url = "";
        switch (typeAction) {
            case "A":
                url = "/Commande/AjouterArticle";
                break;
            case "M":
                url = "/Commande/ModifierArticle";
                break;
            case "S":
                url = "/Commande/SupprimerArticle";
                break;
        }
        document.getElementById("NoCommandeArt").value = noCom;
        form.action = url;
        form.submit();
    });
});


// ---------- Requests ----------
function SendAjaxPOST(url, content, callback, error) {
    var xhr = null;

    if (window.XMLHttpRequest) {
        //for new browsers    
        xhr = new XMLHttpRequest();
    }
    else {
        //for old ones    
        alert("Erreur. Votre navigateur ne supporte pas les requêtes AJAX.")
    }

    if (xhr != null) {
        xhr.onreadystatechange = function () {
            console.log(xhr);
            console.log(content);
            if (xhr.readyState == 4 && xhr.status == 200) {
                console.log("Calling callback");
                callback(JSON.parse(xhr.responseText));
            } else {
                console.log("Calling error");
                error();
            }
        }

        console.log(url);

        //Pass the value to a web page on server as query string using XMLHttpObject.    
        xhr.open("POST", url, true);
        //var parameters = "userName=XXX";
        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        //xhr.setRequestHeader('Content-Type', 'application/json; charset=utf-8');
        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest'); // Utilisé par les filtres ASP pour restreindre l'utilisation de méthodes de contrôleurs
        xhr.send(content);
        //xhr.send("id=manasm&lname=mohaptra");
    }
}