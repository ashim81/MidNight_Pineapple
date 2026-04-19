using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StateTests
{
    private InternalStateMachine stateMachine;

    // A Test behaves as an ordinary method
    [TestCase(5f, 5f, false, InternalStateMachine.StateEnum.Normal, "normal")]
    [TestCase(2f, 3f, true, InternalStateMachine.StateEnum.Sneaking, "sneaking")]
    [TestCase(10f, 7f, false, InternalStateMachine.StateEnum.Running, "running")]
    public void TestStateValues(float moveSpeed, float soundRadius, bool sneaky, InternalStateMachine.StateEnum state, string name)
    {
        stateMachine = new InternalStateMachine();
        stateMachine.ForceState(state);
        Assert.AreEqual(moveSpeed, stateMachine.getMoveSpeed());
        Assert.AreEqual(soundRadius, stateMachine.getSoundRadius());
        Assert.AreEqual(sneaky, stateMachine.isSneaky());
        Assert.AreEqual(name, stateMachine.getName());
    }

    [Test]
    public void TestCommandsStructuredCorrectly()
    {
        stateMachine = new InternalStateMachine();
        Command toggleSneakCommand = new ToggleSneakCommand(stateMachine);
        Assert.IsInstanceOf<Command>(toggleSneakCommand);
        Assert.AreEqual(stateMachine, toggleSneakCommand.GetStateMachine());
    }

    [Test]
    public void TestToggleSneakCommand()
    {
        stateMachine = new InternalStateMachine();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Normal);

        stateMachine.toggleSneakCommand.Execute();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Sneaking);

        stateMachine.toggleSneakCommand.Execute();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Normal);
    }

    [Test]
    public void TestToggleRunningCommand()
    {
        stateMachine = new InternalStateMachine();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Normal);

        stateMachine.toggleRunningCommand.Execute();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Running);

        stateMachine.toggleRunningCommand.Execute();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Normal);
    }

    [Test]
    public void TestStopRunningCommand()
    {
        stateMachine = new InternalStateMachine();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Normal);

        stateMachine.ForceState(InternalStateMachine.StateEnum.Running);

        stateMachine.stopRunningCommand.Execute();
        Assert.AreEqual(stateMachine.getCurrentStateEnum(), InternalStateMachine.StateEnum.Normal);
    }

    [TestCase(InternalStateMachine.StateEnum.Normal)]
    [TestCase(InternalStateMachine.StateEnum.Sneaking)]
    [TestCase(InternalStateMachine.StateEnum.Running)]
    public void TestResetCommand(InternalStateMachine.StateEnum state)
    {
        stateMachine = new InternalStateMachine();
        stateMachine.ForceState(state);
        Assert.AreEqual(state, stateMachine.getCurrentStateEnum());

        stateMachine.resetCommand.Execute();
        Assert.AreEqual(InternalStateMachine.StateEnum.Normal, stateMachine.getCurrentStateEnum());
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
