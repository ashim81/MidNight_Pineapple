using UnityEngine;

public class InternalStateMachine
{
    private State[] statesList = 
    {
        new NormalState(),
        new RunningState(), // Sneaking
        new SneakingState() // Running
    };

    public enum StateEnum {
        Normal = 0,
        Sneaking = 2,
        Running = 1
    }
    private StateEnum currentState;

    public enum CommandEnum {ToggleSneak, ToggleRunning, StopRunning, Reset}
    public Command toggleSneakCommand;
    public Command toggleRunningCommand;
    public Command stopRunningCommand;
    public Command resetCommand;

    public InternalStateMachine()
    {
        currentState = StateEnum.Normal;
        toggleSneakCommand = new ToggleSneakCommand(this);
        toggleRunningCommand = new ToggleRunningCommand(this);
        stopRunningCommand = new StopRunningCommand(this);
        resetCommand = new ResetCommand(this);
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

    public string getName()
    {
        return statesList[(int)currentState].getName();
    }

    public void RunCommandOld(CommandEnum command)
    {
        switch (command)
        {
            case CommandEnum.ToggleSneak:
                if (currentState == StateEnum.Normal)
                {
                    currentState = StateEnum.Sneaking;
                }
                else if (currentState == StateEnum.Sneaking)
                {
                    currentState = StateEnum.Normal;
                }
                break;
            case CommandEnum.ToggleRunning:
                if (currentState == StateEnum.Normal)
                {
                    currentState = StateEnum.Running;
                } else if (currentState == StateEnum.Running)
                {
                    currentState = StateEnum.Normal;
                }
                break;
            case CommandEnum.StopRunning:
                if (currentState == StateEnum.Running)
                {
                    currentState = StateEnum.Normal;
                }  
                break;
        }
    }

    public void ForceState(StateEnum state)
    {
        currentState = state;
    }

    public StateEnum getCurrentStateEnum()
    {
        return currentState;
    }

    public State getCurrentState()
    {
        return statesList[(int)currentState];
    }
}
