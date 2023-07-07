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
        mode: '',

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



function ShowToast(operation, text) {
    document.getElementById('toast-operation').innerHTML = operation;
    document.getElementById('toast-operation-text').innerHTML = text;
    const toastLiveExample = document.getElementById('liveToast');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.show()
}

async function SendDelete() {
    await fetch('https://localhost:6610/index?handler=delete', {
        method: 'POST',
        body: new FormData(document.querySelector('#delete-modal-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#game-card-' + data).remove();
            ShowToast('Deletion', 'was removed from the database');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function SendEdit(id) {
    await fetch('https://localhost:6610/Games/Gateway/edit/', {
        method: 'POST',
        body: new FormData(document.querySelector('#edit-modal-form'))})
        .then(response => response.text())
        .then(data => {
            newCard = document.createElement('div');
            newCard.innerHTML = data;
            oldCard =  document.querySelector('#game-card-' + id);
            oldCard.parentNode.replaceChild(newCard, oldCard);
            ShowToast('Edit', 'was modified');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function SendAdd() {
    await fetch('https://localhost:6610/index?handler=delete', {
        method: 'POST',
        body: new FormData(document.querySelector('#delete-modal-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#game-card-' + data).remove();
            ShowToast('Deletion', 'was removed from the database');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}



