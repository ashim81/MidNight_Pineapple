Simple HUD
Author: Lainey Brekken
4/5 Stars
Version: 1.0
Price: $0.99
[![Simple HUD Prefab](https://img.youtube.com/vi/VIDEO_ID/0.jpg)](https://youtu.be/u9cPH6BQ_LU)


Description
    The Simple HUD Prefab provides a quick and clean way to add a functional user interface for your Unity game! It includes a fully set up HealthBar and Stamina Bar, in the top left corner with a custom design!

Components
    Canvas UI
    UI Slider
    HealthBar Script
    Stamina Script

Set Up
    1. Drag the Simple HUD prefab into your Unity Hierarchy.
    2. In your player script, create variables for health and stamina:
        private int health = 100;
        private int stamina = 100; 
    3. Add references to the HUD components in your player script:
        public HealthBar healthBar
        public StaminaBar staminabar
    4. Assign references in the inspector
        Select your player in the hierarchy
        Drag corresponding HUD objects(from Simple HUD Prefab) into script fields.
    5. Call the provided functions to update the UI(or add your Own!):
        healthBar.SetHealth(health);
        staminaBar.SetStamina(stamina);
    6. Enjoy the Simple HUD!

Requirements
    Unity 2025.6.21f1 or later
