using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TestTools;

public class GenericTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GenericTest()
    {
        Assert.IsTrue(true);
    }

    [Test]
    public void TestKeyboardInput()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        Assert.IsNotNull(keyboard, "Keyboard device not found.");
    }

    [Test]
    public void TestWKeyPressed()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        Assert.IsNotNull(keyboard, "Keyboard device not found.");
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.W));
        InputSystem.Update();  
        Assert.IsTrue(keyboard.wKey.isPressed, "W key should be pressed.");
    }
    
    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator StateTestsWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
}
