NoiseMaker Prefab
Author: Brandon Lunney
Version: 1.0

Description:
    This prefab allows you to drag and drop an audio generating interactable to your game.

Components:
    Circle Collider 2D
    Sprite Renderer
    NoiseMaker Script

Set Up:
    I. To place in scene, simply drag the prefab from the prefab folder into your scene.
    II. Circle Collider 2D
            1. In order for the trigger system to work correctly you must set the object you want to trigger the audio to have the tag "Player"
            2. Whenever a object with the "Player" tag touches the NoiseMaker it will send a signal to emit its sound.
    III. Sprite Renderer
            1. In order to customize the image to be something other than the default red audio icon, you must change the "Sprite" property.
            2. To do this, simply drag the image you would like from your Project Heirarchy to the "Sprite" input field.
    IV. NoiseMaker Script
            1. In the Inspecter you have access to the following "Sound Settings": Radius, Expansion Speed, Enemy Layer, Sound to Emit.
                - Radius: this is the radius of the the sound wave circle that will expand ourwards from the point of origin.
                - Expansion Speed: this is how fast you want the sound to "travel", or the speed at which the circle will move from a pixel out to the radius.
                - Enemy Layer: this will tell the system what enemy layer you want to be alerted to the sound.
                - Sound To Emit: this is the sound you want to play when the NoiseMaker is triggered. (Too add additional sounds, contact Nastia Inc.) 

Requirements
    Unity 2025.6.21f1 (or later)
    Nastia Inc. Proprietary Audio Engine +
