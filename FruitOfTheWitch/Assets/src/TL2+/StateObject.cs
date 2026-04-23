using UnityEngine;

public class State
{
    protected float moveSpeed;
    protected float soundRadius;
    protected bool sneaky;
    protected string name;

    public State()
    {
        this.moveSpeed = 0;
        this.soundRadius = 5f;
        this.sneaky = false;
        this.name = "generic";
    }

    public float getMoveSpeed()
    {
        return  moveSpeed;
    }

    public float getSoundRadius()
    {
        return soundRadius;
    }

    public bool isSneaky()
    {
        return sneaky;
    }

    public string getName()
    {
        return name;
    }
}

public class NormalState : State
{
    public NormalState()
    {
        moveSpeed = 5f;
        soundRadius = 5f;
        sneaky = false;
        name = "normal";
    }
}

public class SneakingState : State
{
    public SneakingState()
    {
        moveSpeed = 2f;
        soundRadius = 3f;
        sneaky = true;
        name = "sneaking";
    }
}

public class RunningState : State
{
    public RunningState()
    {
        moveSpeed = 10f;
        soundRadius = 7f;
        sneaky = false;
        name = "running";
    }
}