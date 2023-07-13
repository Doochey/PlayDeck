<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Doochey/PlayDeck">
    <img src="PlayDeckRazor/wwwroot/Resources/Icon large.png" alt="Logo" width="100" height="150">
  </a>

<h3 align="center">Play Deck</h3>

  <p align="center">
    A locally hosted web app to manage and review your video game collection
    <br />
    <a href="https://github.com/Doochey/PlayDeck/issues">Report Bug</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Hopeful Features</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![Product Name Screen Shot][product-screenshot]



<p align="right">(<a href="#readme-top">back to top</a>)</p>

PlayDeck allows you to use a web based ui to add your games to an offline locally hosted database.
Manage and store details such as:
* Start, Complete & Last Played dates
* Playtime 
* Complete status
* Star Rating

Add each game to one of four Decks: 
* Currently Playing
* Complete 
* My Collection
* Wishlist

### Built With

* [![Bootstrap][Bootstrap.com]][Bootstrap-url]
* [![Alpine][Alpine.js]][Alpine-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

Visit the [Releases](https://github.com/Doochey/PlayDeck/releases) tab and download the latest release, usually titled something like v1.0.0 or v1.0.0-beta

### Prerequisites
* Install [SQL Server Express 2022](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) and follow the instructions on the page to install LocalDB

### Installation

1. Extract the contents of the rar to the folder you would like to use as the base for the application
2. Double click to run the PlayDeckRazor.exe
3. You should see a console window open, after a short time it should look like this ![image](https://github.com/Doochey/PlayDeck/assets/22661442/1ea6d664-3fca-41fd-ab14-d513020076b2)


5. Open your browser and navigate to https://localhost:6610/
6. Unless you are using a Demo version you should see this: ![image](https://github.com/Doochey/PlayDeck/assets/22661442/e18ee891-f7c8-4f5a-9333-fa9b8caca0a8)


7. You are now ready to use PlayDeck! Begin by adding games to any of the four decks by clicking the + buttons. When you're done, close the console and browser window, 
   when you want to view or edit your decks again, simply run the PlayDeckRazor.exe and navigate to https://localhost:6610/ like you did the first time.


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Adding a new game:

Click +
![add game gif](https://github.com/Doochey/PlayDeck/assets/22661442/2a80749b-8f6b-4d31-a5c5-382ac0a5d0d4)


Enter details and click 'Create'
![image](https://github.com/Doochey/PlayDeck/assets/22661442/5ef67f1e-0e0a-4e70-90fe-ddeaac9e3c31)


Deleting a game from the deck view:

Hover over the card and click the 'X' button
![delete gif](https://github.com/Doochey/PlayDeck/assets/22661442/44b2e66d-52c3-4361-b9f0-68d484a7474a)


Deleting a game from the game details view:

Click the delete button
![delete gif 2](https://github.com/Doochey/PlayDeck/assets/22661442/13fd28fa-5208-4741-baea-5cc05c0622c3)


To edit any details about a game, such as which deck the game appears in, either:
* Click the buttons on the game details page, this is only available for the Title,Star Rating, Favourite, and Complete Trophy
* Click the 'Edit Details' button on the game details page
* Hover over the card in the deck view and click the pencil button

![image](https://github.com/Doochey/PlayDeck/assets/22661442/9a29c9d7-8b64-4e82-97e3-36ef5de8153d)



<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- ROADMAP -->
## Hopeful Features

- [ ] Allow the creation of custom decks beyond the defaults
- [x] Add a wishlist deck to the defaults
- [x] Add the ability to tag games as favourites
- [ ] Controls on the sides of deck displays to let you browse games in the deck
- [ ] Launch app as a background service that runs on startup
- [ ] Accessibility & Responsive pass
- [ ] Fill in game info from online database through api call

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Connor - [@PlayDeckDev](https://twitter.com/PlayDeckDev) - playdeckdev@gmail.com

Project Link: [https://github.com/Doochey/PlayDeck](https://github.com/Doochey/PlayDeck)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[license-shield]: https://img.shields.io/github/license/doochey/PlayDeck.svg?style=for-the-badge
[license-url]: https://github.com/Doochey/PlayDeck/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/connordouch
[product-screenshot]: https://github.com/Doochey/PlayDeck/assets/22661442/9e592298-21f7-4810-9f16-3f66bc16a847
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[Alpine.js]: https://img.shields.io/badge/Alpine.js-blue?style=for-the-badge&logo=Alpine.js&logoColor=white
[Alpine-url]: https://alpinejs.dev