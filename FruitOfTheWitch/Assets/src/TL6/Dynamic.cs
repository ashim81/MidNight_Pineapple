using UnityEngine;

// Super class
class EnemyState
{
    // Virtual → dynamic binding
    public virtual void Execute()
    {
        Debug.Log("Base Execute");
    }

    // Non-virtual → static binding
    public void PrintType()
    {
        Debug.Log("EnemyState");
    }
}

// Subclass 1
class PatrolState : EnemyState
{
    public override void Execute()
    {
        Debug.Log("Patrol Execute");
    }

    public new void PrintType()
    {
        Debug.Log("PatrolState");
    }
}

// Subclass 2
class ChaseState : EnemyState
{
    public override void Execute()
    {
        Debug.Log("Chase Execute");
    }

    public new void PrintType()
    {
        Debug.Log("ChaseState");
    }
}

// Demo runner
public class BindingDemo : MonoBehaviour
{
    void Start()
    {
        // Static type = EnemyState
        // Dynamic type = PatrolState
        EnemyState state = new PatrolState();

        Debug.Log("First object: PatrolState");

        // Dynamic binding (virtual)
        state.Execute();

        // Static binding (non-virtual)
        state.PrintType();

        // Change dynamic type
        state = new ChaseState();

        Debug.Log("Second object: ChaseState");

        // Dynamic binding again
        state.Execute();

        // Static binding again
        state.PrintType();
    }
}