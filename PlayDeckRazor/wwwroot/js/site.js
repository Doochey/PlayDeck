﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
        favourite: '',

        setInfo(id, title) {this.gameId = id; this.gameTitle=title;},
        setInfoFull(id, title, deckId, imageUrl, playStatus, startDate, completeDate, rating, playTime, lastPlayed, favourite) {
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
            this.favourite = favourite ?? false;
        }
    })
})

// Gets all game data associated with the provided id, populates alpine store with this data
async function GetGameDetails(id) {
    await fetch('https://localhost:6610/Games/Gateway/retrieve/'+ id, {
        method: 'GET'})
        .then(response => response.text())
        .then(data => {
            let jsonResponse = JSON.parse(data);
            Alpine.store('gameData').setInfoFull(jsonResponse['ID'], jsonResponse['Title'], jsonResponse['DeckID'], jsonResponse['ImageURL'],
                jsonResponse['PlayStatus'], jsonResponse['StartDate']?.split("T")[0], jsonResponse['CompleteDate']?.split("T")[0], jsonResponse['Rating'],
                jsonResponse['PlayTime'], jsonResponse['LastPlayed']?.split("T")[0], jsonResponse['Favourite']);
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

// Sets all game data properties to null, ignores mode which is used for changing modal contents
function ResetGameDetails() {
    Alpine.store('gameData').setInfoFull(null,null, null, null, null, null, null, null, null, null, null);
}



function ShowToast(operation, text) {
    document.getElementById('toast-operation').innerHTML = operation;
    document.getElementById('toast-operation-text').innerHTML = text;
    const toastLiveExample = document.getElementById('liveToast');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.show()
}

// Posts game id from modal of game to be deleted, then removes any game card of the game, 
// Optional param true if called from the GameView, sends user to homepage as game entry is deleted
async function SendDelete(gameView) {
    await fetch('https://localhost:6610/Games/Gateway/delete/', {
        method: 'POST',
        body: new FormData(document.querySelector('#delete-modal-form'))})
        .then(response => response.text())
        .then(data => {
            if (gameView) {
                // Entry is now deleted, send user to homepage to prevent bad requests
                window.location.replace("https://localhost:6610/");
            } else {
                // If game is tagged favourite there will be multiple elements with same id
                while (document.querySelector('#game-card-' + data) != null) {
                    document.querySelector('#game-card-' + data).remove();
                }
                ShowToast('Deletion', 'was removed from the database.');  
            }
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

// Posts game details from modal form, removes and replaces any game cards of the associated game using response
async function SendEdit(id) {
    await fetch('https://localhost:6610/Games/Gateway/edit/', {
        method: 'POST',
        body: new FormData(document.querySelector('#edit-modal-form'))})
        .then(response => response.text())
        .then(data => {
            GetGameDetails(id).then(result => {
                // If game is tagged favourite there will be multiple elements with same id
                while (document.querySelector('#game-card-' + id) != null) {
                    document.querySelector('#game-card-' + id).remove();
                }
                newCard = document.createElement('div');
                newCard.innerHTML = data;
                newCard = newCard.getElementsByTagName('li')[0];
                newCard.parentElement.remove(); 
                const targetDisplay = newCard.getAttribute('targetDeckDisplay');
                deckCardsDisplay =  document.querySelector("ul[deckDisplay=" + CSS.escape(targetDisplay) + "]");
                deckCardsDisplay ? deckCardsDisplay.prepend(newCard) : null;
                if (Alpine.store('gameData').favourite) {
                    document.querySelector("ul[deckDisplay='4']").prepend(newCard.cloneNode(true));
                }
                ShowToast('Edit', 'was modified.');
            })
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

// Posts game details from modal form, adds a new game card of the associated game using response
async function SendAdd(deckTitle) {
    await fetch('https://localhost:6610/Games/Gateway/add/', {
        method: 'POST',
        body: new FormData(document.querySelector('#edit-modal-form'))})
        .then(response => response.text())
        .then(data => {
            newCard = document.createElement('div');
            newCard.innerHTML = data;
            newCard = newCard.getElementsByTagName('li')[0];
            newCard.parentElement.remove();
            deckCardsDisplay =  document.getElementById(deckTitle + '-cards');
            deckCardsDisplay.prepend(newCard);
            document.querySelector('#edit-modal-form').reset();
            let gameID = newCard.getAttribute('id').split('-')[2];
            GetGameDetails(gameID);
            ShowToast('Add', 'was added to ' + deckTitle + ' deck.');
        })
        .catch(errorMsg => { console.log(errorMsg); });
    
}

// Checks input fields of modal follow validation rules
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

// Enables validation on modal as it breaks when swapping the contents
function addValidation() {
    let form = $( "#edit-modal-form" );
    if (!form.data("validator")) {
        form.removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($("form"));
    }
}

// Causes game data to be exported to CSV
async function exportGames() {
    await fetch('https://localhost:6610/Games/Gateway/export/', {
        method: 'POST',
        body: new FormData(document.querySelector('#export-form'))})
        .then(response => response.text())
        .then(data => {
            console.log('PlayDeck: Game Data Exported')
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

// Causes game data to be imported from CSV, reloads page
async function importGames() {
    await fetch('https://localhost:6610/Games/Gateway/import/', {
        method: 'POST',
        body: new FormData(document.querySelector('#import-form'))})
        .then(response => response.text())
        .then(data => {
            location.reload();
            console.log('PlayDeck: Game Data Imported');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}





