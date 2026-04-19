using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestMovement
{
    [SetUp]
    public void Setup()
    {
        // Load the test scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/TL2Plus_TestCase");
    }
    
    [UnityTest]
    public IEnumerator TestPlayerInitialization()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Assert.IsNotNull(player, "Player object not found in the scene.");
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestSneakBehavior()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        Assert.IsNotNull(keyboard, "Keyboard device not found.");

        GameObject player = GameObject.FindWithTag("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        Assert.IsNotNull(controller, "PlayerController component not found on the player object.");

        // Simulate crouch input
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.C));
        InputSystem.Update();  
        yield return null;

        Assert.AreEqual(InternalStateMachine.StateEnum.Sneaking, controller.getStateMachine().getCurrentStateEnum(), "Player should be in Sneaking state after crouch input.");
        
        // unpress keys
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        yield return null;
        // Simulate crouch input again to toggle back

        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.C));
        InputSystem.Update();
        yield return null;
        Assert.AreEqual(InternalStateMachine.StateEnum.Normal, controller.getStateMachine().getCurrentStateEnum(), "Player should be back in Normal state after toggling crouch input.");
        // unpress keys
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestRunBehavior()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        Assert.IsNotNull(keyboard, "Keyboard device not found.");

        GameObject player = GameObject.FindWithTag("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        Assert.IsNotNull(controller, "PlayerController component not found on the player object.");

        // Simulate sprint input
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.LeftShift));
        InputSystem.Update();  
        yield return null;

        Assert.AreEqual(InternalStateMachine.StateEnum.Running, controller.getStateMachine().getCurrentStateEnum(), "Player should be in Running state after sprint input.");
        
        // unpress keys
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        yield return null;
        // Simulate sprint input again to toggle back

        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.LeftShift));
        InputSystem.Update();
        yield return null;
        Assert.AreEqual(InternalStateMachine.StateEnum.Normal, controller.getStateMachine().getCurrentStateEnum(), "Player should be back in Normal state after toggling sprint input.");
        // unpress keys
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestExhaustionPreventsRunning()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        Assert.IsNotNull(keyboard, "Keyboard device not found.");

        GameObject player = GameObject.FindWithTag("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        Assert.IsNotNull(controller, "PlayerController component not found on the player object.");

        // Press sprint and w to enter running state
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.LeftShift | Key.W));
        InputSystem.Update();  
        yield return null;

        // Unpress keys
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        InputSystem.Update();  
        yield return null;

        // wait until exhausted
        while (!controller.isExhausted())
        {
            yield return null;
        }

        // Wait 3 more frames to ensure exhaustion state is fully applied
        yield return null;
        yield return null;
        yield return null;
        
        // Simulate sprint input
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.LeftShift));
        InputSystem.Update();  
        yield return null;

        Assert.AreEqual(InternalStateMachine.StateEnum.Normal, controller.getStateMachine().getCurrentStateEnum(), "Player should remain in Normal state when exhausted and sprint input is given.");
        
        // unpress keys
        InputSystem.QueueStateEvent(keyboard, new KeyboardState());
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestHealthReduction()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        Assert.IsNotNull(controller, "PlayerController component not found on the player object.");

        int initialHealth = controller.getHealth();

        // Simulate taking damage
        controller.TakeDamage(20);
        yield return null;

        Assert.AreEqual(initialHealth - 20, controller.getHealth(), "Player health should be reduced by the damage amount.");
    }

    [UnityTest]
    public IEnumerator TestRespawn()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        // move player to faraway location
        player.transform.position = new Vector3(1000, 1000, 1000);
        Assert.IsNotNull(controller, "PlayerController component not found on the player object.");

        // Simulate taking damage and respawning
        controller.TakeDamage(150); // Ensure health drops to 0
        yield return null;

        Assert.AreEqual(100, controller.getHealth(), "Player health should be restored to 100 after respawning.");
        Assert.AreEqual(InternalStateMachine.StateEnum.Normal, controller.getStateMachine().getCurrentStateEnum(), "Player should be in Normal state after respawning.");
        Assert.AreEqual(Vector3.zero, player.transform.position, "Player should be moved to the respawn point after respawning.");
    }

    [UnityTest]
    public IEnumerator TestNoiseMaker()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        NoiseMaker noiseMaker = player.GetComponent<NoiseMaker>();
        Assert.IsNotNull(controller, "PlayerController component not found on the player object.");
        Assert.IsNotNull(noiseMaker, "NoiseMaker component not found on the player object.");

        yield return null;

        float targetNormalSoundRadius = controller.getStateMachine().getState(InternalStateMachine.StateEnum.Normal).getSoundRadius();
        float targetSneakingSoundRadius = controller.getStateMachine().getState(InternalStateMachine.StateEnum.Sneaking).getSoundRadius();
        float targetRunningSoundRadius = controller.getStateMachine().getState(InternalStateMachine.StateEnum.Running).getSoundRadius();

        controller.ForceState(InternalStateMachine.StateEnum.Normal);
        yield return null;
        Assert.AreEqual(targetNormalSoundRadius, noiseMaker.getRadius(), "NoiseMaker sound radius should match the current state sound radius (Normal).");  
        yield return null;

        controller.ForceState(InternalStateMachine.StateEnum.Sneaking);
        yield return null;
        Assert.AreEqual(targetSneakingSoundRadius, noiseMaker.getRadius(), "NoiseMaker sound radius should match the current state sound radius (Sneaking).");
        yield return null;

        controller.ForceState(InternalStateMachine.StateEnum.Running);
        yield return null;
        Assert.AreEqual(targetRunningSoundRadius, noiseMaker.getRadius(), "NoiseMaker sound radius should match the current state sound radius (Running).");
        yield return null;
    }

    [TearDown]
     public void Teardown()
     {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Scenes/TL2Plus_TestCase");
     }
}
