using UnityEngine;

public class FireSpell : MonoBehaviour
{
    public int fireballsPerShot = 2;   // 2 fireballs
    public float fireRate = .3f;        // every 0.3 seconds
    public float spreadAngle = 20f;    // spread between fireballs

    private float timer = 0f;

    public void TryCast(Transform enemy, Transform player)
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0f;
            ShootSpread(enemy, player);
        }
    }

    private void ShootSpread(Transform enemy, Transform player)
    {
        // Calculate base direction toward player
        Vector3 baseDir = (player.position - enemy.position).normalized;
        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

        // Fire 3 fireballs with spread
        // Example with 3: angles will be -20, 0, +20
        float startAngle = -spreadAngle * (fireballsPerShot - 1) / 2f;

        for (int i = 0; i < fireballsPerShot; i++)
        {
            float angle = startAngle + (spreadAngle * i);
            float finalAngle = (baseAngle + angle) * Mathf.Deg2Rad;

            // Get fireball from pool
            GameObject fb = FireballPool.Instance.Get(enemy.position);

            // Set spread direction
            Vector3 spreadDir = new Vector3(
                Mathf.Cos(finalAngle),
                Mathf.Sin(finalAngle),
                0f
            );

            fb.GetComponent<Fireball>()?.SetDirection(
                enemy.position + spreadDir
            );
        }
    }
}