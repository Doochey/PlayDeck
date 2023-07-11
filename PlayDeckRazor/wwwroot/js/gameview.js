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