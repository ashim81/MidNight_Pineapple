using UnityEngine;

public class State
{
    protected float moveSpeed;
    protected float soundRadius;
    protected bool sneaky;
    protected bool running;
    protected string name;

    protected int staminaDelta;

    protected bool powered;

    public State()
    {
        this.moveSpeed = 0;
        this.soundRadius = 5f;
        this.sneaky = false;
        this.running = false;
        this.name = "generic";
        this.staminaDelta = 0;
        powered = false;
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

    public bool isRunning()
    {
        return running;
    }

    public string getName()
    {
        return name;
    }

    public int getStaminaCost()
    {
        return staminaDelta;
    }

    public bool isPowered()
    {
        return powered;
    }
}

public class NormalState : State
{
    public NormalState()
    {
        moveSpeed = 5f;
        soundRadius = 5f;
        sneaky = false;
        running = false;
        name = "normal";
        staminaDelta = 0;
        powered = false;
    }
}

public class ExhaustedState : State
{
    public ExhaustedState()
    {
        moveSpeed = 4f;
        soundRadius = 5f;
        sneaky = false;
        running = false;
        name = "exhausted";
        staminaDelta = 1;
        powered = false;
    }
}

public class SneakingState : State
{
    public SneakingState()
    {
        moveSpeed = 2f;
        soundRadius = 3f;
        sneaky = true;
        running = false;
        name = "sneaking";
        staminaDelta = 0;
        powered = false;
    }
}

public class RunningState : State
{
    public RunningState()
    {
        moveSpeed = 10f;
        soundRadius = 7f;
        sneaky = false;
        running = true;
        name = "running";
        staminaDelta = -3;
        powered = false;
    }
}

public class PoweredState : State
{
    public PoweredState()
    {
        moveSpeed = 7f;
        soundRadius = 5f;
        sneaky = false;
        running = false;
        name = "powered";
        staminaDelta = -6;
        powered = true;
    }
}