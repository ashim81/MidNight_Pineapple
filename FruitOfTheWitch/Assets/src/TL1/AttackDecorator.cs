using UnityEngine;

public abstract class AttackDecorator : BaseBossAttack
{
    protected BaseBossAttack wrappedAttack;

    public AttackDecorator(BaseBossAttack attack)
    {
        wrappedAttack = attack;
    }

    public override void Initialize(Transform playerTransform, Rigidbody2D bossRb = null)
    {
        base.Initialize(playerTransform, bossRb);
        wrappedAttack.Initialize(playerTransform, bossRb);
    }

    public override void ExecuteAttack(Transform boss)
    {
        wrappedAttack.ExecuteAttack(boss);
    }

    public override void OnPhaseEnter()
    {
        wrappedAttack.OnPhaseEnter();
    }

    public override void OnPhaseExit()
    {
        wrappedAttack.OnPhaseExit();
    }
}

public class ExtraFireDecorator : AttackDecorator
{
    public ExtraFireDecorator(BaseBossAttack attack) : base(attack) { }

    public override void ExecuteAttack(Transform boss)
    {
        wrappedAttack.ExecuteAttack(boss);
        Debug.Log("Decorator: Extra Fire!");
        ShootFireballs(boss, 2, 30f);
    }
}