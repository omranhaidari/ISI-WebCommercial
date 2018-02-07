
// ----- Global -----------
var articles;


// ----- Modal --------
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

$('#commandeArticleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var noCom = button.data('nocom');
    var typeAction = button.data('action');
    var noArt = button.data('noart');
    var quantite = button.data('quantite');
    var libelle, prix;

    var titre = button.data('titre');

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

            e.text = arti[1];
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
        modal.find('.modal-body #noArt').text(noArt);
        modal.find('.modal-body #libelleArt').text(libelle);
        modal.find('.modal-body #prixArt').text(prix);
        modal.find('.modal-body #quantiteArt').val(quantite);
        document.getElementById('quantiteArt').addEventListener("input", function (evt) {
            quantite = evt.target.value;
            updateTotal(quantite * prix);
        });
        updateTotal(); // Essaie de mettre à jour le montant total une première fois

        function updateTotal() {
            if (quantite != undefined && quantite != "") {
                if (prix != undefined && prix != "") {
                    modal.find('.modal-body #montantTotalArt').text(Math.round(prix.replace(",", ".") * quantite * 100) / 100);
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
        var url = "", content = "";
        switch (typeAction) {
            case "A":
                url = "/Commande/AjoutArticle";
                content = "id=" + noCom + "&noart=" + noArt + "&quantite=" + quantite + "&livree=F";
                break;
            case "M":
                url = "/Commande/ModifierArticle";
                content = "id=" + noCom + "&noart=" + noArt + "&quantite=" + quantite + "&livree=F";
                break;
            case "S":
                url = "/Commande/SupprimerArticle";
                content = "id=" + noCom + "&noart=" + noArt;
                break;
        }
        console.log(content);
        SendAjaxPOST(url, content, updateForm, function () {
            showErreur("Erreur lors de l'appel à  : '" + url + "'");
        });
    });
})


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