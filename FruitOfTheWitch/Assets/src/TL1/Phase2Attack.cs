using UnityEngine;

public class Phase2Attack : BaseBossAttack
{
    private float fireRate = 3f;
    private float timer = 0f;
    private int fireballCount = 3;

    public override void OnPhaseEnter()
    {
        Debug.Log("Phase 2 — Boss is angry!");
    }

    public override void ExecuteAttack(Transform boss)
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            ShootFireballs(boss, fireballCount, 20f);
        }
    }

    public override void OnPhaseExit()
    {
        Debug.Log("Phase 2 ending!");
    }
}