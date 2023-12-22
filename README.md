This is a repository for easy to analyze and free to copy codes (since some of them are not mine to begin with). This repository is good to study or develop real world code, in a near future I plain to make this a real thing, but for now is mostly a group of nice to have codes.


## Interconnection

Some codes are free to use (no need for a Unity setup), while other need to set some elements, for now the “**CameraController**” and “**InputManager**”.


## CameraController

The CameraController is my most used script, not because it’s fantastic, or is the most well written code, it’s simple because I don’t need to use Camera.main, and it have several functions to me. It follow the rule of encapsulating native code into your own, so if Unity changes the way it uses Camera.main I need to change CameraController.


## InputManager

I did my own input manager, the reason is simple, I can do a much easier key mapping in the future is I need, and without changing Unity native InputManager at any point given.


## CSV

The project has a CSV reader for general usage, the main functionality is for multiple languages in game, but could be also be used for general projects to disconnect the main writer with the main project, the only link between this two is a CSV with all the writing, this also can be used to mitigate bugs.


## Utils

Several amount of codes exists in the Utils, for diverse reasons, like, easy to access, no code duplication and even some performance. But because of this, you may lose some time just looking into each viable Util, here are some of the most useful ones:


### UtilCamera

This Util has several function to work with the “CameraController”.


### UtilImages

This Util you can use to have a basic idea to how to deal with images, I used it in a simple card game project to make the layout, but games like “Bricks King” use similar ideas to build their map.


### UtilProbability

This util is one I plan to increase as time goes by, because it’s reassuring to have something that automatically makes some probability of a event for you, currently, this Util has the following probabilities functions:



* DefaultProcGeneration: Given a chance of every possible event, give you which event “Proc” (was generated randomly), but, this has a control method, which makes the most random “popular” events have less chances to be generated in the next “batch”. This, makes the generation more balanced.


### UtilRandom

I use this Util in basically every project I work, for several reasons, first: you have a centralized way of generating data, so, you don’t need to create a Random every time you need a random value, which is good for code maintenance (less code) and code performance (less object creation).


### UtilVisuals

This util was very useful in my first professional project, for the fact that it provides some function that can be used for UI classes.

This and “UtilMaterials” where used for code improvement in readability and maintenance. 
