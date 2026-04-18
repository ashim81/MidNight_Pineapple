using UnityEngine;

public class InternalStateMachine
{
    private StateObject[] statesList = 
    {
        new NormalState(),
        new RunningState(), // Sneaking
        new StealthState() // Running
    };

    public enum State {
        Normal = 0,
        Sneaking = 1,
        Running = 2
    }
    public State currentState;

    public enum Command {ToggleSneak, ToggleRunning, StopRunning, Reset}

    public InternalStateMachine()
    {
        currentState = State.Normal;
    }

    public float getMoveSpeed()
    {
        return statesList[(int)currentState].getMoveSpeed();
    }
    
    public float getSoundRadius()
    {
        return statesList[(int)currentState].getSoundRadius();
    }

    public bool isSneaky()
    {
        return statesList[(int)currentState].isSneaky();
    }

    public void RunCommand(Command command)
    {
        switch (command)
        {
            case Command.ToggleSneak:
                if (currentState == State.Normal)
                {
                    currentState = State.Sneaking;
                }
                else if (currentState == State.Sneaking)
                {
                    currentState = State.Normal;
                }
                break;
            case Command.ToggleRunning:
                if (currentState == State.Normal)
                {
                    currentState = State.Running;
                } else if (currentState == State.Running)
                {
                    currentState = State.Normal;
                }
                break;
            case Command.StopRunning:
                if (currentState == State.Running)
                {
                    currentState = State.Normal;
                }  
                break;
        }
    }

    public void ForceState(State state)
    {
        currentState = state;
    }
}
