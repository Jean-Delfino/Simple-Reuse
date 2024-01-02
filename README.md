This is a repository for easy to analyze and free to copy codes (since some of them are not mine to begin with). This repository is good to study or develop real world code, in the near future I plan to make this a real thing, but for now it’s mostly a group of nice to have codes.


## Interconnection

Some codes are free to use (no need for a Unity setup), while others need to set up some elements, for now the “**CameraController**” and “**InputManager**”.


## Camera


### CameraController

The CameraController is my most used script, not because it’s fantastic, or is the most well written code, it’s simple because I don’t need to use Camera.main, and it has several functions to me. It follows the rule of encapsulating native code into your own, so if Unity changes the way it uses Camera.main I need to change CameraController.


### DragMoveCamera

One camera type that works by dragging the screen of the device, it has limits for the dragging, and has a sensitivity control.


## Checkpoint

Simple checkpoint manager and checkpoint objects/triggers.


## CSV

The project has a CSV reader for general usage, the main functionality is for multiple languages in game, but could be also be used for general projects to separate the main writer with the main project, the only link between these two is a CSV with all the writing, this also can be used to mitigate bugs.

The most useful part of the CSV is the “VersatileText” component.


## GameObjectOperations

Here you find several operations in transform (rotation, position or scale) and Rigidbody, which is useful for code reference when developing some enemy or object behavior.


## InputManager

I did my own input manager, the reason is simple, I can do a much easier key mapping in the future if I need, and without changing Unity native InputManager at any given point.


## Patterns

Here are some design patterns for Unity.


## Pooling 

Here is a simple pooling idea, still incomplete if you consider all the ways of spawning in Unity, but it’s usable.


## Sound

Simple and useful sound manager, the idea is to make it more robust in the future.


## Utils

Several codes exist in the Utils, for diverse reasons, like, easy to access, no code duplication and even some performance. But because of this, you may lose some time just looking into each for a viable Util, here are some of the most useful ones:


### UtilCamera

This Util has several functions to work with the “CameraController” and some functions based on the camera types of “Camera”.


### UtilImage

This Util has a basic idea of how to deal with images, I used it in a simple card game project to make the layout, but games like “Bricks King” use similar ideas to build their map.


### UtilLanguage

This is a useful tool to find the computer language.


### UtilProbability

This util is one I plan to expand as time goes by, because it’s reassuring to have something that automatically makes some probability of a event for you, currently, this Util has the following probabilities functions:



* DefaultProcGeneration: Given a chance of every possible event, give you which event “Proc” (was generated randomly), but, this has a control method, which makes the most random “popular” events have less chances to be generated in the next “batch”. This makes the generation more balanced.


### UtilRandom

I use this Util in basically every project I work on, for several reasons, first: you have a centralized way of generating data, so, you don’t need to create a Random every time you need a random value, which is good for code maintenance (less code) and code performance (less object creations).


### UtilVersatileText

For now it’s only useful to find the computer language and setting GameVersatileTextController.


### UtilVisuals

This util was very useful in my first professional project, for the fact that it provides some function that can be used for UI classes.

This and “UtilMaterials” were used for code improvement in readability and maintenance. 
