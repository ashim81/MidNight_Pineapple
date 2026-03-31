using UnityEngine;

public class InternalStateMachine
{
    private StateObject[] statesList = 
    {
        new StateObject(5f, false),
        new StateObject(2f, true)
    };

    public enum State {
        Normal = 0,
        Sneaking = 1
    }
    public State currentState;

    public enum Command { Sneak, Unsneak }

    public InternalStateMachine()
    {
        currentState = State.Normal;
    }

    public float getMoveSpeed()
    {
        return statesList[(int)currentState].moveSpeed;
    }

    public bool isSneaky()
    {
        return statesList[(int)currentState].sneaky;
    }

    public void RunCommand(Command command)
    {
        switch (currentState)
        {
            case State.Normal:
                if (command == Command.Sneak)
                {
                    currentState = State.Sneaking;
                }
                break;
            case State.Sneaking:
                if (command == Command.Unsneak)
                {
                    currentState = State.Normal;
                }
                break;
        }
    }
}
