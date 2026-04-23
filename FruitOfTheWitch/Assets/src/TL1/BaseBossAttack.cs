using UnityEngine;

public class BaseBossAttack : MonoBehaviour
{
    protected Transform player;

    public virtual void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }

    public virtual void ExecuteAttack(Transform boss) { }
    public virtual void OnPhaseEnter() { }
    public virtual void OnPhaseExit() { }

    protected void ShootFireballs(Transform boss, int count, float spread)
    {
        Vector3 baseDir = (player.position - boss.position).normalized;
        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;
        float startAngle = -spread * (count - 1) / 2f;

        for (int i = 0; i < count; i++)
        {
            float angle = (baseAngle + startAngle + (spread * i)) * Mathf.Deg2Rad;
            GameObject fb = FireballPool.Instance.Get(boss.position);
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
            fb.GetComponent<Fireball>()?.SetDirection(boss.position + dir);
        }
    }
}