using NUnit.Framework;
using UnityEngine;

public class AdditionalEditTests
{
    // TEST 1 — GameObject is created
    [Test]
    public void Test01_GameObjectCreated()
    {
        GameObject obj = new GameObject();
        Assert.IsNotNull(obj);
        GameObject.DestroyImmediate(obj);
    }

    // TEST 2 — Rigidbody2D can be added
    [Test]
    public void Test02_AddRigidbody2D()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<Rigidbody2D>();
        Assert.IsNotNull(obj.GetComponent<Rigidbody2D>());
        GameObject.DestroyImmediate(obj);
    }

    // TEST 3 — Transform exists by default
    [Test]
    public void Test03_TransformExists()
    {
        GameObject obj = new GameObject();
        Assert.IsNotNull(obj.transform);
        GameObject.DestroyImmediate(obj);
    }

    // TEST 4 — Object name is assigned correctly
    [Test]
    public void Test04_ObjectName()
    {
        GameObject obj = new GameObject("Enemy");
        Assert.AreEqual("Enemy", obj.name);
        GameObject.DestroyImmediate(obj);
    }

    // TEST 5 — Tag can be assigned
    [Test]
    public void Test05_TagAssignment()
    {
        GameObject obj = new GameObject();
        obj.tag = "Untagged";
        Assert.AreEqual("Untagged", obj.tag);
        GameObject.DestroyImmediate(obj);
    }

    // TEST 6 — Multiple components can exist
    [Test]
    public void Test06_MultipleComponents()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<Rigidbody2D>();
        obj.AddComponent<BoxCollider2D>();

        Assert.IsNotNull(obj.GetComponent<Rigidbody2D>());
        Assert.IsNotNull(obj.GetComponent<BoxCollider2D>());

        GameObject.DestroyImmediate(obj);
    }

    // TEST 7 — Component removal works
    [Test]
    public void Test07_RemoveComponent()
    {
        GameObject obj = new GameObject();
        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();

        Object.DestroyImmediate(rb);

        Assert.IsNull(obj.GetComponent<Rigidbody2D>());
        GameObject.DestroyImmediate(obj);
    }

    // TEST 8 — Position can be changed
    [Test]
    public void Test08_PositionChange()
    {
        GameObject obj = new GameObject();
        obj.transform.position = new Vector3(5, 5, 0);

        Assert.AreEqual(new Vector3(5, 5, 0), obj.transform.position);

        GameObject.DestroyImmediate(obj);
    }

    // TEST 9 — Scale can be changed
    [Test]
    public void Test09_ScaleChange()
    {
        GameObject obj = new GameObject();
        obj.transform.localScale = new Vector3(2, 2, 2);

        Assert.AreEqual(new Vector3(2, 2, 2), obj.transform.localScale);

        GameObject.DestroyImmediate(obj);
    }

    // TEST 10 — Rigidbody2D default gravity scale
    [Test]
    public void Test10_RigidbodyDefaultGravity()
    {
        GameObject obj = new GameObject();
        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();

    Assert.AreEqual(1f, rb.gravityScale);

    GameObject.DestroyImmediate(obj);
}
}