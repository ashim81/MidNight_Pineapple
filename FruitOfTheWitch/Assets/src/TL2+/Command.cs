using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


/** This is using the command design pattern. */
public class Command
{
    protected InternalStateMachine stateMachine;
    public Command(InternalStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void Execute()
    {
        // Base implementation does nothing
    }

    public virtual void Unexecute()
    {
        Execute();
    }

    public virtual bool isValid()
    {
        return false;
    }

    public InternalStateMachine GetStateMachine()
    {
        return stateMachine;
    }
}

public class ToggleSneakCommand : Command
{
    public ToggleSneakCommand(InternalStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override bool isValid()
    {
        return stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Normal
            || stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Sneaking;
    }
    public override void Execute()
    {
        if (stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Normal)
        {
            stateMachine.ForceState(InternalStateMachine.StateEnum.Sneaking);
        }
        else if (stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Sneaking)
        {
            stateMachine.ForceState(InternalStateMachine.StateEnum.Normal);
        }
    }
}

public class ToggleRunningCommand : Command
{
    public ToggleRunningCommand(InternalStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override bool isValid()
    {
        return stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Normal
            || stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Running;
    }
    public override void Execute()
    {
        if (stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Normal)
        {
            stateMachine.ForceState(InternalStateMachine.StateEnum.Running);
        }
        else if (stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Running)
        {
            stateMachine.ForceState(InternalStateMachine.StateEnum.Normal);
        }
    }
}

public class StopRunningCommand : Command
{
    public StopRunningCommand(InternalStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override bool isValid()
    {
        return stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Running;
    }
    public override void Execute()
    {
        if (stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Running)
        {
            stateMachine.ForceState(InternalStateMachine.StateEnum.Normal);
        }
    }

    public override void Unexecute()
    {
        if (stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Normal)
        {
            stateMachine.ForceState(InternalStateMachine.StateEnum.Running);
        }
    }
}

public class ResetCommand : Command
{
    public ResetCommand(InternalStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override bool isValid()
    {
        return true;
    }
    public override void Execute()
    {
        stateMachine.ForceState(InternalStateMachine.StateEnum.Normal);
    }
}