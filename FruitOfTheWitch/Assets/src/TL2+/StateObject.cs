using UnityEngine;

public class StateObject
{
    public float moveSpeed;
    public bool sneaky;

    public StateObject(float moveSpeed, bool sneaky)
    {
        this.moveSpeed = moveSpeed;
        this.sneaky = sneaky;
    }
    public StateObject()
    {
        this.moveSpeed = 0;
        this.sneaky = false;
    }

    public void setValues(float moveSpeed, bool sneaky)
    {
        this.moveSpeed = moveSpeed;
        this.sneaky = sneaky;
    }
}
