using NUnit.Framework;
using UnityEngine;

public class EnemyAI_TL6_Tests
{
    private GameObject enemyObj;
    private GameObject playerObj;
    private EnemyAI_TL6 enemy;

    [SetUp]
    public void Setup()
    {
        enemyObj = new GameObject();
        playerObj = new GameObject();

        enemy = enemyObj.AddComponent<EnemyAI_TL6>();
        enemy.player = playerObj.transform;

        enemy.speed = 2f;
        enemy.chaseRange = 5f;
        enemy.attackRange = 1.5f;
        enemy.health = 50;

        enemyObj.transform.position = Vector3.zero;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(enemyObj);
        Object.DestroyImmediate(playerObj);
    }

    [Test]
    public void Test1_PlayerFar_NoMovement()
    {
        playerObj.transform.position = new Vector3(10, 0, 0);
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreEqual(before, enemyObj.transform.position);
    }

    [Test]
    public void Test2_PlayerInChaseRange_Moves()
    {
        playerObj.transform.position = new Vector3(3, 0, 0);
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreNotEqual(before, enemyObj.transform.position);
    }

    [Test]
    public void Test3_PlayerRight_MovesRight()
    {
        playerObj.transform.position = new Vector3(4, 0, 0);
        enemyObj.SendMessage("Update");
        Assert.Greater(enemyObj.transform.position.x, 0);
    }

    [Test]
    public void Test4_PlayerLeft_MovesLeft()
    {
        playerObj.transform.position = new Vector3(-4, 0, 0);
        enemyObj.SendMessage("Update");
        Assert.Less(enemyObj.transform.position.x, 0);
    }

    [Test]
    public void Test5_AttackRange_NoMovement()
    {
        playerObj.transform.position = new Vector3(1, 0, 0);
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreEqual(before, enemyObj.transform.position);
    }

    [Test]
    public void Test6_EnemyTakesDamage()
    {
        int before = enemy.health;
        enemy.TakeDamage(10);
        Assert.Less(enemy.health, before);
    }

    [Test]
    public void Test7_EnemyDiesAtZeroHealth()
    {
        enemy.TakeDamage(100);
        Assert.LessOrEqual(enemy.health, 0);
    }

    [Test]
    public void Test8_NoCrash_WhenPlayerNull()
    {
        enemy.player = null;
        enemyObj.SendMessage("Update");
        Assert.Pass();
    }

    [Test]
    public void Test9_DeadEnemy_DoesNotMove()
    {
        enemy.TakeDamage(100);
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreEqual(before, enemyObj.transform.position);
    }

    [Test]
    public void Test10_MultipleDamage_Accumulates()
    {
        enemy.TakeDamage(10);
        enemy.TakeDamage(15);
        Assert.AreEqual(25, enemy.health);
    }

    [Test]
    public void Test11_Health_NotNegative()
    {
        enemy.TakeDamage(1000);
        Assert.LessOrEqual(enemy.health, 0);
    }

    [Test]
    public void Test12_Chase_MultipleUpdates_Closer()
    {
        playerObj.transform.position = new Vector3(4, 0, 0);
        float before = Vector3.Distance(enemyObj.transform.position, playerObj.transform.position);
        enemyObj.SendMessage("Update");
        enemyObj.SendMessage("Update");
        float after = Vector3.Distance(enemyObj.transform.position, playerObj.transform.position);
        Assert.Less(after, before);
    }

    [Test]
    public void Test13_PlayerSamePosition_NoMovement()
    {
        playerObj.transform.position = enemyObj.transform.position;
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreEqual(before, enemyObj.transform.position);
    }

    [Test]
    public void Test14_AtExactChaseRange_Moves()
    {
        playerObj.transform.position = new Vector3(enemy.chaseRange, 0, 0);
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreNotEqual(before, enemyObj.transform.position);
    }

    [Test]
    public void Test15_AtExactAttackRange_NoMovement()
    {
        playerObj.transform.position = new Vector3(enemy.attackRange, 0, 0);
        Vector3 before = enemyObj.transform.position;
        enemyObj.SendMessage("Update");
        Assert.AreEqual(before, enemyObj.transform.position);
    }
}