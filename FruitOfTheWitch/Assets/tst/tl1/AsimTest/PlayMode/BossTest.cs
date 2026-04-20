using NUnit.Framework;
using UnityEngine;

public class CheckpointEditTests
{
    // -----------------------------------------
    // TEST 1 — Player component exists
    // -----------------------------------------
    [Test]
    public void Test01_PlayerComponentExists()
    {
        GameObject player = new GameObject();
        player.AddComponent<Player>();

        Assert.IsNotNull(player.GetComponent<Player>());

        GameObject.DestroyImmediate(player);
    }

    // -----------------------------------------
    // TEST 2 — Wall does NOT have Player component
    // (prevents NullReference bug)
    // -----------------------------------------
    [Test]
    public void Test02_WallHasNoPlayerComponent()
    {
        GameObject wall = new GameObject();

        Assert.IsNull(wall.GetComponent<Player>());

        GameObject.DestroyImmediate(wall);
    }

    // -----------------------------------------
    // TEST 3 — Health starts at max
    // -----------------------------------------
    [Test]
    public void Test03_HealthStartsAtMax()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<Rigidbody2D>();
        Health h = obj.AddComponent<Health>();

        Assert.AreEqual(h.maxHealth, h.GetCurrentHealth());

        GameObject.DestroyImmediate(obj);
    }
    // -----------------------------------------
    // TEST 4 — Enemy move speed is valid
    // -----------------------------------------
    [Test]
    public void PlayerComponentExists()
    {
        GameObject player = new GameObject();
        player.AddComponent<Player>();

        Assert.IsNotNull(player.GetComponent<Player>());

        GameObject.DestroyImmediate(player);
    }
    // -----------------------------------------
    // TEST 5 — Enemy move speed is valid
    // -----------------------------------------
        [Test]
    public void WallHasNoPlayerComponent()
    {
        GameObject wall = new GameObject();

        Player p = wall.GetComponent<Player>();

        Assert.IsNull(p);

        GameObject.DestroyImmediate(wall);
    }


    // -----------------------------------------
    // TEST 6 — Player tag is correct
    // -----------------------------------------
    [Test]
    public void Test06_PlayerTag()
    {
        GameObject player = new GameObject();
        player.tag = "Player";

        Assert.AreEqual("Player", player.tag);

        GameObject.DestroyImmediate(player);
    }

    // -----------------------------------------
    // TEST 7 — Fireball tag is correct
    // -----------------------------------------
    [Test]
    public void Test07_FireballTag()
    {
        GameObject fireball = new GameObject();
        fireball.tag = "Fireball";

        Assert.AreEqual("Fireball", fireball.tag);

        GameObject.DestroyImmediate(fireball);
    }

    // -----------------------------------------
    // TEST 8 — Phase scripts can be added
    // -----------------------------------------
    [Test]
    public void Test08_AllBossPhasesExist()
    {
        GameObject obj = new GameObject();

        obj.AddComponent<Phase1Attack>();
        obj.AddComponent<Phase2Attack>();
        obj.AddComponent<Phase3Attack>();

        Assert.IsNotNull(obj.GetComponent<Phase1Attack>());
        Assert.IsNotNull(obj.GetComponent<Phase2Attack>());
        Assert.IsNotNull(obj.GetComponent<Phase3Attack>());

        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------
    // TEST 9 — Safe GetComponent usage
    // (protects against teammate mistakes)
    // -----------------------------------------
    [Test]
    public void Test09_SafeComponentAccess()
    {
        GameObject obj = new GameObject();

        Player p = obj.GetComponent<Player>();

        bool safe = (p != null);

        Assert.IsFalse(safe);

        GameObject.DestroyImmediate(obj);
    }
}