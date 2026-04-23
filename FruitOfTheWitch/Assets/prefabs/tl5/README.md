AudioEngine Prefab
Author: NastiaKossiak
Version: 1.0

Description:
    This prefab handles the game's audio and volume control. It stores all the audio clips and provides the methods for playing them.
    This prefab should be present in every scene where audio is needed!

Components:
    AudioEngine Script
    Separate Audio Sources:
        SFX
        Music
        Ambience
        
Set Up:
    1. To place in scene, simply drag the prefab from the prefab folder into your scene.

    2. Make sure the scene contains an AudioListener, otherwise there won't be any sound.

    3. Assign all needed audio clips 
        (For example, "Collectible: Coin") in the inspector, by clicking on the dropdown menu and selecting the corresponding clips.

    4. If you want to implement the engine directly to your script, you can simply drag and drop the prefab into a component in your script that allows Audio.

    5. For music and ambience, the audio needs to be initialized once the scene starts, so each source must be assigned.

Requirements
    Unity 2025.6.21f1 (or later)
    AudioListener and AudioEngine should be present in each scene