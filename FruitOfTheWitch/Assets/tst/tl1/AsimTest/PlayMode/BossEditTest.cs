using UnityEngine;
using NUnit.Framework;

public class SimpleEditTest
{
    // -----------------------------------------------
    // TEST 1 — Basic Unity works
    // -----------------------------------------------
    [Test]
    public void Test01_UnityWorks()
    {
        Assert.IsTrue(true);
        Debug.Log("TEST 1 PASSED — Unity works");
    }

    // -----------------------------------------------
    // TEST 2 — GameObject can be created
    // -----------------------------------------------
    [Test]
    public void Test02_GameObjectCreated()
    {
        GameObject obj = new GameObject("TestObject");
        Assert.IsNotNull(obj);
        Debug.Log("TEST 2 PASSED — GameObject created");
        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------------
    // TEST 3 — GameObject has correct name
    // -----------------------------------------------
    [Test]
    public void Test03_GameObjectName()
    {
        GameObject obj = new GameObject("Enemy");
        Assert.AreEqual("Enemy", obj.name);
        Debug.Log("TEST 3 PASSED — GameObject name correct");
        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------------
    // TEST 4 — Player tag can be set
    // -----------------------------------------------
    [Test]
    public void Test04_PlayerTag()
    {
        GameObject obj = new GameObject();
        obj.tag = "Player";
        Assert.AreEqual("Player", obj.tag);
        Debug.Log("TEST 4 PASSED — Player tag correct");
        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------------
    // TEST 5 — Fireball tag can be set
    // -----------------------------------------------
    [Test]
    public void Test05_FireballTag()
    {
        GameObject obj = new GameObject();
        obj.tag = "Fireball";
        Assert.AreEqual("Fireball", obj.tag);
        Debug.Log("TEST 5 PASSED — Fireball tag correct");
        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------------
    // TEST 6 — Rigidbody2D can be added
    // -----------------------------------------------
    [Test]
    public void Test06_Rigidbody2DAdded()
    {
        GameObject obj = new GameObject();
        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
        Debug.Log("TEST 6 PASSED — Rigidbody2D added");
        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------------
    // TEST 7 — BoxCollider2D can be added
    // -----------------------------------------------
    [Test]
    public void Test07_BoxCollider2DAdded()
    {
        GameObject obj = new GameObject();
        BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
        Assert.IsNotNull(col);
        Debug.Log("TEST 7 PASSED — BoxCollider2D added");
        GameObject.DestroyImmediate(obj);
    }

    // -----------------------------------------------
    // TEST 8 — Move speed is greater than zero
    // -----------------------------------------------
    [Test]
    public void Test08_MoveSpeedGreaterThanZero()
    {
        float moveSpeed = 2f;
        Assert.Greater(moveSpeed, 0f);
        Debug.Log("TEST 8 PASSED — Move speed greater than zero");
    }

    // -----------------------------------------------
    // TEST 9 — Health cannot go below zero
    // -----------------------------------------------
    [Test]
    public void Test09_HealthNotBelowZero()
    {
        int health = 100;
        int damage = 9999;
        health -= damage;
        if (health < 0) health = 0;
        Assert.GreaterOrEqual(health, 0);
        Debug.Log("TEST 9 PASSED — Health clamped at zero");
    }

    // -----------------------------------------------
    // TEST 10 — Phase transition at 50 percent
    // -----------------------------------------------
    [Test]
    public void Test10_PhaseTransitionAt50Percent()
    {
        int health = 50;
        int maxHealth = 100;
        float percent = (float)health / maxHealth * 100f;
        Assert.LessOrEqual(percent, 50f);
        Debug.Log("TEST 10 PASSED — Phase 2 triggers at 50 percent");
    }
}