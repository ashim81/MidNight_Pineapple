using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StateTests
{
    private InternalStateMachine stateMachine;

    // A Test behaves as an ordinary method
    [Test]
    public void TestStateValues()
    {
        InternalStateMachine stateMachine = new InternalStateMachine();
        Assert.AreEqual(0,1);
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
