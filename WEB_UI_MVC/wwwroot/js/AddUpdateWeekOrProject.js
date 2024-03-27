function generateWeekOptions() {
    // R�cup�rer l'ann�e actuelle
    const currentYear = new Date().getFullYear();

    // S�lectionner l'�l�ment de la liste d�roulante
    const weekNumberSelect = document.getElementById("weekNumber");

    // Boucler de W-1 � W-52
    for (let i = 1; i <= 52; i++) {
        const weekOption = document.createElement("option");
        const weekText = `W-${i}/${currentYear}`;
        weekOption.value = weekText;
        weekOption.text = weekText;
        weekNumberSelect.appendChild(weekOption);
    }
}

// Appeler la fonction au chargement de la page
window.onload = generateWeekOptions;

document.getElementById("projetSelect").addEventListener("change", function () {
    // R�cup�rer l'ID du projet s�lectionn�
    var projectId = this.options[this.selectedIndex].getAttribute("id");

    console.log("Projet s�lectionn� ID:", projectId);


    // Cr�er une requ�te XMLHttpRequest
    var xhr = new XMLHttpRequest();

    // D�finir l'URL de la requ�te
    xhr.open("GET", "https://localhost:51379/api/Project/" + projectId, true);

    // D�finir la fonction de rappel pour g�rer la r�ponse de la requ�te
    xhr.onload = function () {
        if (xhr.status >= 200 && xhr.status < 300) {
            // Convertir la r�ponse JSON en objet JavaScript
            var data = JSON.parse(xhr.responseText);

            // S�lectionner l'�l�ment de la liste d�roulante des quotations
            var quotationSelect = document.getElementById("quotationSelect");

            // Vider la liste d�roulante des quotations
            quotationSelect.innerHTML = "";

            // Parcourir les donn�es des quotations et les ajouter � la liste d�roulante
            data.projectQuotations.forEach(function (quotation) {
                var option = document.createElement("option");
                option.value = quotation.quotation; // Supposons que l'ID de la quotation est stock� dans une propri�t� "id"
                option.textContent = quotation.quotation; // Supposons que le nom de la quotation est stock� dans une propri�t� "quotation"
                quotationSelect.appendChild(option);
            });
        } else {
            console.error("La requ�te a �chou�");
        }
    };

    // Envoyer la requ�te
    xhr.send();
});