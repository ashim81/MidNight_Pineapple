using UnityEngine;

public class StateObject
{
    public float moveSpeed;
    public bool sneaky;
    public float soundRadius;

    public StateObject(float moveSpeed, bool sneaky, float soundRadius)
    {
        this.moveSpeed = moveSpeed;
        this.sneaky = sneaky;
        this.soundRadius = soundRadius;

    }
    public StateObject()
    {
        this.moveSpeed = 0;
        this.sneaky = false;
        this.soundRadius = 5f;
    }

    public void setValues(float moveSpeed, bool sneaky, float soundRadius)
    {
        this.moveSpeed = moveSpeed;
        this.sneaky = sneaky;
        this.soundRadius = soundRadius;
    }
}
