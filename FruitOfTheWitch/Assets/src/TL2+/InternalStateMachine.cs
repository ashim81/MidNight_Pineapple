using UnityEngine;

public class InternalStateMachine
{
    private State[] statesList = 
    {
        new NormalState(),
        new RunningState(), // Sneaking
        new SneakingState(), // Running
        new ExhaustedState(), // Exhausted
        new PoweredState() // Powered
    };

    public enum StateEnum {
        Normal = 0,
        Running = 1,
        Sneaking = 2,
        Exhausted = 3,
        Powered = 4
    }
    private StateEnum currentState;

    public enum CommandEnum {ToggleSneak, ToggleRunning, StopRunning, Reset}
    public Command toggleSneakCommand;
    public Command toggleRunningCommand;
    public Command stopRunningCommand;
    public Command startRunningCommand;
    public Command stopExhaustedCommand;   
    public Command powerUpCommand; 
    public Command resetCommand;

    public InternalStateMachine()
    {
        currentState = StateEnum.Normal;
        toggleSneakCommand = new ToggleSneakCommand(this);
        toggleRunningCommand = new ToggleRunningCommand(this);
        stopRunningCommand = new StopRunningCommand(this);
        startRunningCommand = new StartRunningCommand(this);
        stopExhaustedCommand = new StopExhaustedCommand(this);
        powerUpCommand = new PowerUpCommand(this);
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

    public bool isRunning()
    {
        return statesList[(int)currentState].isRunning();
    }

    public bool isPowered()
    {
        return statesList[(int)currentState].isPowered();
    }

    public string getName()
    {
        return statesList[(int)currentState].getName();
    }

    public int getStaminaCost()
    {
        return statesList[(int)currentState].getStaminaCost();
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

    public State getState(StateEnum state)
    {
        return statesList[(int)state];
    }
}
