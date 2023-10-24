# About AR Memory Game

AR Memory Game is a game for two players in an augmented reality environment orientated towards both players' real desks.

## About the 'develop' branch
This repository's 'develop' branch contains the work-in-progress source code for the AR Memory Game.

The first stable version of the AR Memory Game was released on June 20th 2023. Further updates are soon to be released.

## Installation

To install the AR Memory Game the Git control system is needed. 

Having opened the Git Bash window type the following commands.

```bash
git remote add origin https://allankirui@bitbucket.org/allankirui/ar-memory-game.git
git pull
git checkout -f develop
```

## Connecting process 
At first, the user needs to authenticate him/herself by providing a username of choice. 
After successful authentication, the scene changes for “Lobby List”.

At the top, the provided player name is displayed. There are 2 buttons “Create lobby” (with plus), which show the popup for creating a lobby, and the Refresh lobby list searches for any available lobbies.

The player has the possibility to create the lobby (future host) to specify the lobby name, accessibility (public or private), players available (for now it is 2 players only), and game mode (for now it is ‘Classic’ only). 

## Gameplay
 
Having detected a flat surface the game starts. Both players see the same set of cards and have to find a pair of identical pictures in order to score a point. If the matching is successful a player can continue guessing, if not the second player takes a turn.
## Features

The game is designed essentially for the Android system for smartphones and tablets. There are two possibilities to play the game -in horizontal and vertical ways.

AR Memory Game detects the real surface and contains 3D graphics to make it look more realistic.

There will be a possibility for players to customize their own set of cards with unique graphics.

Thanks to intuitive UI the application is simple to use. 

## Documentation

The whole documentation process has been done using Confluence Workspace and Google Docs.

To see the documentation visit 
https://allankirui.atlassian.net/wiki/home 

https://docs.google.com/document/d/18H0Wjag5TFDHXO8vVvFvGL5LRLKxlVi-/edit

## Authors and acknowledgement

The group of five students of Gdańsk University of Technology who contributed to the project:

Allan Kirui -  the leader of the group - responsible for UI  

Aleksandra Wójcikowska - responsible for game logic, UI and animations

Agata Wysokińska - responsible for documentation, visual effects and graphics 

Michał Boczoń - responsible for game logic and UI

Michał Łubiński - responsible for game logic and UI


 