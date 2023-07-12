// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
document.addEventListener('alpine:init', () => {
    Alpine.store('gameData', {
        gameTitle: '',
        gameId: '',
        deckId: '',
        imageUrl: '',
        playStatus: '',
        startDate: '',
        completeDate: '',
        rating: '',
        playTime: '',
        lastPlayed: '',
        mode: 'add',
        deckTitle: '',

        setInfo(id, title) {this.gameId = id; this.gameTitle=title;},
        setInfoFull(id, title, deckId, imageUrl, playStatus, startDate, completeDate, rating, playTime, lastPlayed) {
            this.gameId = id ?? null;
            this.gameTitle = title ?? null;
            this.deckId = deckId ?? null;
            this.imageUrl = imageUrl ?? null;
            this.playStatus = playStatus ?? null;
            this.startDate = startDate ?? null;
            this.completeDate = completeDate ?? null;
            this.rating = rating ?? null;
            this.playTime = playTime ?? null;
            this.lastPlayed = lastPlayed ?? null;
        }
    })
})

async function GetGameDetails(id) {
    await fetch('https://localhost:6610/Games/Gateway/retrieve/'+ id, {
        method: 'GET'})
        .then(response => response.text())
        .then(data => {
            let jsonResponse = JSON.parse(data);
            Alpine.store('gameData').setInfoFull(jsonResponse['ID'], jsonResponse['Title'], jsonResponse['DeckID'], jsonResponse['ImageURL'],
                jsonResponse['PlayStatus'], jsonResponse['StartDate']?.split("T")[0], jsonResponse['CompleteDate']?.split("T")[0], jsonResponse['Rating'],
                jsonResponse['PlayTime'], jsonResponse['LastPlayed']?.split("T")[0]);
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

function ResetGameDetails() {
    Alpine.store('gameData').setInfoFull(null,null, null, null, null, null, null, null, null, null);
}



function ShowToast(operation, text) {
    document.getElementById('toast-operation').innerHTML = operation;
    document.getElementById('toast-operation-text').innerHTML = text;
    const toastLiveExample = document.getElementById('liveToast');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.show()
}

async function SendDelete(gameView) {
    await fetch('https://localhost:6610/Games/Gateway/delete/', {
        method: 'POST',
        body: new FormData(document.querySelector('#delete-modal-form'))})
        .then(response => response.text())
        .then(data => {
            if (gameView) {
                window.location.replace("https://localhost:6610/");
            } else {
                document.querySelector('#game-card-' + data).remove();
                ShowToast('Deletion', 'was removed from the database.');  
            }
        })
        .catch(errorMsg => { console.log(errorMsg); });
}


async function SendEdit(id) {
    await fetch('https://localhost:6610/Games/Gateway/edit/', {
        method: 'POST',
        body: new FormData(document.querySelector('#edit-modal-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#game-card-' + id).remove();
            newCard = document.createElement('div');
            newCard.innerHTML = data;
            newCard = newCard.getElementsByTagName('li')[0];
            const targetDisplay = newCard.getAttribute('targetDeckDisplay');
            deckCardsDisplay =  document.querySelector("ul[deckDisplay=" + CSS.escape(targetDisplay) + "]");
            deckCardsDisplay ? deckCardsDisplay.prepend(newCard) : null;
            ShowToast('Edit', 'was modified.');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function SendEditGameView(id) {
    await fetch('https://localhost:6610/Games/Gateway/editGameView/', {
        method: 'POST',
        body: new FormData(document.querySelector('#edit-modal-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#info-panel').remove();
            newPanel = document.createElement('div');
            newPanel.innerHTML = data;
            newPanel = newPanel.getElementsByTagName('div')[0];
            display =  document.querySelector("#game-info-container");
            display ? display.prepend(newPanel) : null;
            ShowToast('Edit', 'was modified.');
            GetGameDetails(id);
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function SendAdd(deckTitle) {
    await fetch('https://localhost:6610/Games/Gateway/add/', {
        method: 'POST',
        body: new FormData(document.querySelector('#edit-modal-form'))})
        .then(response => response.text())
        .then(data => {
            newCard = document.createElement('div');
            newCard.innerHTML = data;
            deckCardsDisplay =  document.getElementById(deckTitle + '-cards');
            deckCardsDisplay.prepend(newCard);
            document.querySelector('#edit-modal-form').reset();
            let gameID = newCard.getElementsByTagName('li')[0].getAttribute('id').split('-')[2];
            GetGameDetails(gameID);
            ShowToast('Add', 'was added to ' + deckTitle + ' deck.');
        })
        .catch(errorMsg => { console.log(errorMsg); });
    
}

function checkValid(el) {
    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });
    let form = $( "#edit-modal-form" );
    form.validate()
    if (form.valid()) {
        bootstrap.Modal.getInstance($('#delete-modal')).toggle();
    }
    return form.valid();
}

function addValidation() {
    let form = $( "#edit-modal-form" );
    if (!form.data("validator")) {
        form.removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($("form"));
    }
}

async function toggleFavGameView(gameID) {
    await fetch('https://localhost:6610/Games/Gateway/togglefavgameview/', {
        method: 'POST',
        body: new FormData(document.querySelector('#fav-change-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#info-panel').remove();
            newPanel = document.createElement('div');
            newPanel.innerHTML = data;
            newPanel = newPanel.getElementsByTagName('div')[0];
            display =  document.querySelector("#game-info-container");
            display ? display.prepend(newPanel) : null;
            ShowToast('Edit', 'was modified.');
            GetGameDetails(gameID);
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function exportGames() {
    await fetch('https://localhost:6610/Games/Gateway/export/', {
        method: 'POST',
        body: new FormData(document.querySelector('#export-form'))})
        .then(response => response.text())
        .then(data => {
            console.log('exported')
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function importGames() {
    await fetch('https://localhost:6610/Games/Gateway/import/', {
        method: 'POST',
        body: new FormData(document.querySelector('#import-form'))})
        .then(response => response.text())
        .then(data => {
            location.reload();
            console.log('imported');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}





