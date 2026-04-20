using UnityEngine;

public class Phase1Attack : BaseBossAttack
{
    private float fireRate = 4f;
    private float timer = 0f;
    private int fireballCount = 1;

    public override void OnPhaseEnter()
    {
        Debug.Log("Phase 1 — Boss is calm");
    }

    public override void ExecuteAttack(Transform boss)
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            ShootFireballs(boss, fireballCount, 0f);
        }
    }

    public override void OnPhaseExit()
    {
        Debug.Log("Phase 1 ending!");
    }
}