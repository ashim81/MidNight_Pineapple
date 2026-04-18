using UnityEngine;

public class StateObject
{
    protected float moveSpeed;
    protected float soundRadius;
    protected bool sneaky;

    public StateObject(float moveSpeed, bool sneaky, float soundRadius)
    {
        this.moveSpeed = moveSpeed;
        this.soundRadius = soundRadius;
        this.sneaky = sneaky;
    }
    public StateObject()
    {
        this.moveSpeed = 0;
        this.soundRadius = 5f;
        this.sneaky = false;
    }

    public void setValues(float moveSpeed, bool sneaky, float soundRadius)
    {
        this.moveSpeed = moveSpeed;
        this.soundRadius = soundRadius;
        this.sneaky = sneaky;
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
}

public class NormalState : StateObject
{
    public NormalState()
    {
        moveSpeed = 5f;
        soundRadius = 5f;
        sneaky = false;
    }
}

public class StealthState : StateObject
{
    public StealthState()
    {
        moveSpeed = 2f;
        soundRadius = 3f;
        sneaky = true;
    }
}

public class RunningState : StateObject
{
    public RunningState()
    {
        moveSpeed = 10f;
        soundRadius = 7f;
        sneaky = false;
    }
}