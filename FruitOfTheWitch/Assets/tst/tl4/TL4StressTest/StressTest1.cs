using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stresstest1
{
    private Player player;
    private HealthBar healthBar;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Level1_WitchHouse");
        yield return null;

        player = Object.FindObjectOfType<Player>();
        healthBar = Object.FindObjectOfType<HealthBar>();

        Assert.IsNotNull(player, "Player not found in scene!");
        Assert.IsNotNull(healthBar, "HealthBar not found in scene!");
    }

    [UnityTest]
    public IEnumerator HealthBar_RapidUpdates_DoesNotDropFrames()
    {
        player.currentHealth = player.maxHealth;
        healthBar.SetMaxHealth(player.maxHealth);

        for (int i = 0; i < 1000; i++)
        {
            int randomHealth = Random.Range(0, 100);
            player.currentHealth = randomHealth;
            healthBar.SetHealth(player.currentHealth);

            Assert.Less(Time.deltaTime, 0.033f, $"Frame drop on update {i}! deltaTime was {Time.deltaTime}");
            yield return null;
        }
    }
}
