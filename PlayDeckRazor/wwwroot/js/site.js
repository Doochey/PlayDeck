// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
document.addEventListener('alpine:init', () => {
    Alpine.data('gameData', () => ({
        gameTitle: '',
        gameId: '',
        deckId: '',
        imageUrl: '',
        playStatus: '',
        startDate: '',
        completeDate: '',
        rating: '',
        playTime: '',
        lastPlayed: ''
    }))
})



function ShowToast(operation, text) {
    document.getElementById('toast-operation').innerHTML = operation;
    document.getElementById('toast-operation-text').innerHTML = text;
    const toastLiveExample = document.getElementById('liveToast');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.show()
}

function SendDelete() {
    fetch('https://localhost:6610/index?handler=delete', {
        method: 'POST',
        body: new FormData(document.querySelector('#delete-modal-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#game-card-' + data).remove();
            ShowToast('Deletion', 'was removed from the database');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

async function GetGameDetails(id, {gameTitle, gameId, deckId, imageUrl, playStatus, startDate, completeDate, rating, playTime, lastPlayed}) {
    await fetch('https://localhost:6610/Games/Gateway/retrieve/'+ id, {
        method: 'GET'})
        .then(response => response.text())
        .then(data => {
            let jsonResponse = JSON.parse(data);
            gameTitle = jsonResponse['Title'];
            gameId: jsonResponse['gameId'];
            deckId: jsonResponse['deckId'];
            imageUrl: jsonResponse['imageUrl'];
            playStatus: jsonResponse['playStatus'];
            startDate: jsonResponse['startDate'];
            completeDate: jsonResponse['completeDate'];
            rating: jsonResponse['rating'];
            playTime: jsonResponse['playTime'];
            lastPlayed: jsonResponse['lastPlayed'];
            alert(gameTitle);
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

