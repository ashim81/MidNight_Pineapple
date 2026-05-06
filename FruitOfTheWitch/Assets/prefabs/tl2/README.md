HiddenlevelEntry

Author: Abdullah
Version: 1.0

Description
This prefab represents the hidden level entry trigger used in Level1_Alternative. Its purpose is to detect when the player enters the trigger area and send the player to the hidden potion mini-game scene, HiddenLevel.

Components
1. HiddenLevelEntry Script
Controls the prefab’s main behavior. When the player enters the trigger, this script loads the hidden scene. This is the core gameplay component of the prefab.

2. BoxCollider2D
Provides the trigger area for detecting the player.
This collider must have Is Trigger enabled so the player can activate the hidden level without physical collision stopping movement.

3. Rigidbody2D
Supports trigger interaction in Unity 2D physics.
This component is needed so the trigger event works reliably with the player object.

4. Transform
Controls the position and scale of the hidden entry object in the scene.
This should be placed in the correct hidden access location inside Level1_Alternative.

Setup Instructions
Drag the HiddenlevelEntry prefab into Level1_Alternative.
2. Make sure the object has:
	HiddenLevelEntry script
	BoxCollider2D
	Rigidbody2D
3. In the BoxCollider2D, check Is Trigger.
4. Make sure the player object uses the Player tag.
5. In the script, confirm the hidden scene name is set to HiddenLevel.
6. Add both Level1_Alternative and HiddenLevel to Build Settings.
7. Test in Play Mode by moving the player into the trigger and confirming the hidden level loads.

Expected Behavior
The prefab waits in the main level for the player to reach it.
When the player enters the trigger, the hidden level scene loads.
After the mini-game is completed, the return flow sends the player back to Level1_Alternative.

Dependencies
Player object with the Player tag
Hidden scene named HiddenLevel
Main scene named Level1_Alternative

Requirements
Unity 2022.3.42f1 or later