function generateWeekOptions() {
    // Récupérer l'année actuelle
    const currentYear = new Date().getFullYear();

    // Sélectionner l'élément de la liste déroulante
    const weekNumberSelect = document.getElementById("weekNumber");

    // Boucler de W-1 à W-52
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
    // Récupérer l'ID du projet sélectionné
    var projectId = this.options[this.selectedIndex].getAttribute("id");

    console.log("Projet sélectionné ID:", projectId);


    // Créer une requête XMLHttpRequest
    var xhr = new XMLHttpRequest();

    // Définir l'URL de la requête
    xhr.open("GET", "https://localhost:51379/api/Project/" + projectId, true);

    // Définir la fonction de rappel pour gérer la réponse de la requête
    xhr.onload = function () {
        if (xhr.status >= 200 && xhr.status < 300) {
            // Convertir la réponse JSON en objet JavaScript
            var data = JSON.parse(xhr.responseText);

            // Sélectionner l'élément de la liste déroulante des quotations
            var quotationSelect = document.getElementById("quotationSelect");

            // Vider la liste déroulante des quotations
            quotationSelect.innerHTML = "";

            // Parcourir les données des quotations et les ajouter à la liste déroulante
            data.projectQuotations.forEach(function (quotation) {
                var option = document.createElement("option");
                option.value = quotation.quotation; // Supposons que l'ID de la quotation est stocké dans une propriété "id"
                option.textContent = quotation.quotation; // Supposons que le nom de la quotation est stocké dans une propriété "quotation"
                quotationSelect.appendChild(option);
            });
        } else {
            console.error("La requête a échoué");
        }
    };

    // Envoyer la requête
    xhr.send();
});