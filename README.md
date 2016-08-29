# MinesweeperBot
Bot written in C# to play the Windows XP version of the game.

Hello, this is my first project that I have put on GitHub. As such, I have no idea how I should be doing things, so go easy on me.

The bot is currently able to make all the moves necessary to solve the game on all difficulties; including guesses when required.

Code used to manipulate the mouse as well as the windows, were done using the windows32 api and were taken from various sources from 
the internet. Everything else I have written on my own.

NOTE: This has only been tested on my own laptop, which has a resolution of 1366*768. I have no idea if the resolutions for the game
will be different on other computers/screens and whether or not the colours will be the same. As such, it is possible that it will not 
work on other computers. If this is the case, I am hoping to address this once I have fully completed the program. 

Also, the Windows XP version of the game was taken from http://www.minesweeper.info/downloads/WinmineXP.html . I do not know if this
is the same version as that on actual XP systems.

#RECORDS:

Beginner: 1 second

Intermediate: 5 seconds

Expert: 12 seconds

#TODO:

Add way for program to detect whether the game is in progress, won, or lost.
  As such, use that to loop and restart games until a game is won.

Improve guessing algorithm so it always makes a guess if there are no
guaranteed moves.

Add a console menu to start bot as well as a way to stop it.

Add a way to record win rates

Clean up code

#Stuff to do after initial completion:

Improve algorithm so it considers a bigger range of tiles and therefore
have the ability to consider non-obvious moves before having to guess




