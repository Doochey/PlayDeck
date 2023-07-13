// Changes rating of game being viewed, replaces the Game Info Panel on the page
async function changeRating(formID, gameID) {
    await fetch('https://localhost:6610/Games/Gateway/ratingchange/', {
        method: 'POST',
        body: new FormData(document.querySelector('#star-form-' + formID))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#info-panel').remove();
            newPanel = document.createElement('div');
            newPanel.innerHTML = data;
            newPanel = newPanel.getElementsByTagName('div')[0];
            display =  document.querySelector("#game-info-container");
            display ? display.prepend(newPanel) : null;
            GetGameDetails(gameID);
            ShowToast('Rating Changed', 'rating was changed.');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

// Changes PlayStatus of game being viewed, replaces the Game Info Panel on the page
async function changePlayStatus(gameID) {
    await fetch('https://localhost:6610/Games/Gateway/playstatuschange/', {
        method: 'POST',
        body: new FormData(document.querySelector('#playstatus-form'))})
        .then(response => response.text())
        .then(data => {
            document.querySelector('#info-panel').remove();
            newPanel = document.createElement('div');
            newPanel.innerHTML = data;
            newPanel = newPanel.getElementsByTagName('div')[0];
            display =  document.querySelector("#game-info-container");
            display ? display.prepend(newPanel) : null;
            GetGameDetails(gameID);
            ShowToast('Play Status Changed', 'play status was changed.');
        })
        .catch(errorMsg => { console.log(errorMsg); });
}

// Posts game details from modal form, removes and replaces the game info panel using response
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

// Toggles Favourite status of the game being viewed, replaces the Game Info Panel on the page
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