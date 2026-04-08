using UnityEngine;

// Safegaurds
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class VisualDetector : MonoBehaviour
{
[Header("Cone Settings")]

    [SerializeField, Range(0, 360)] private float angle = 90f;
    [SerializeField] private float coneDistance = 5f;
    [SerializeField] private int trianglesInCone = 100;
    private Mesh cone;
    private Mesh fillMesh;

[Header("Detection Settings")]

    [SerializeField] private LayerMask obstacleLayer;
    private Transform player;

[Header("Alert Settings")]

    [SerializeField] private float fillSpeed = 5f;
    private float fillDistance = 0f;

// S T A T E  P A T T E R N : States
    private IEnemyState currentState;
    private static IdleState idleState = new IdleState();
    private static SuspiciousState suspiciousState = new SuspiciousState();
    private static AlertedState alertedState = new AlertedState();

// Find Player
    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        currentState = idleState;

    // To Fix Render Order
        MeshRenderer mainRenderer = GetComponent<MeshRenderer>();
        mainRenderer.sortingLayerName = "Foreground";

        if (playerObject != null) { player = playerObject.transform; }
    }

// Generate Cone Changes in Inspector
    void OnValidate()
    {
        GenerateConeMesh();
    }

// General Cone Setup
    void OnEnable()
    {
        GenerateConeMesh();
        GenerateFillMesh();
    }

    void Update()
    {
        currentState.UpdateState(this);
        GenerateConeMesh();
        UpdateFillMesh();
    }

// Create a Cone Based on Inspector Inputs
    void GenerateConeMesh()
    {
    // Create Mesh
        if (cone == null)
        {
            cone = new Mesh();
            cone.name = "Vision Cone";
            GetComponent<MeshFilter>().mesh = cone;
        }

    // Remove Previous Version 
        cone.Clear();

    // Create Setup for Triangles
        int vertexCount = trianglesInCone + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[trianglesInCone * 3];

    // Set Center Point to Zero
        vertices[0] = Vector3.zero;

    // Find Angle Covered Per Triangle & Angle Start
        float anglePerTriangle = angle / trianglesInCone;
        float startAngle = -angle / 2f;

        // Center point
        vertices[0] = Vector3.zero;

    // Build Array of Vertices Locations

        Vector3 localScale = transform.lossyScale;

        for (int i = 0; i <= trianglesInCone; i++)
        {
            float currentAngle = startAngle + anglePerTriangle * i;
            float radianConversion = currentAngle * Mathf.Deg2Rad;

        // (Y, X, Z) To Make Cone Point Up
            Vector3 point = new Vector3(Mathf.Sin(radianConversion), Mathf.Cos(radianConversion), 0);

        // Raycast to Check for Walls
            float worldConeDistance = coneDistance * Mathf.Abs(transform.lossyScale.x);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, point, worldConeDistance, obstacleLayer);

            float hitDistance;

            if (hit.collider != null) { hitDistance = hit.distance / Mathf.Abs(transform.lossyScale.x); } 
            
            else { hitDistance = coneDistance; }

            vertices[i + 1] = point * hitDistance;
        }

    // Build Triangles: Unity Mesh Wants int[0, 1, 2, 0, 2, 3, 0, 3, 4, 0, ...] where each 3rd is the center point and = 0
        for (int i = 0; i < trianglesInCone; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

    // Assign Array Data to Mesh
        cone.vertices = vertices;
        cone.triangles = triangles;
    }

// Create Fill Container and Mesh
    void GenerateFillMesh()
    {
    // Create Fill Mesh
        if (fillMesh == null) 
        {
            fillMesh = new Mesh();
            fillMesh.name = "Alert Fill";
        }

    // Create Child to Hold Fill
        GameObject fillHolder = new GameObject("FillHolder");
        fillHolder.transform.SetParent(transform, false);

    // Add Mesh Filter & Renderer
        MeshFilter alertMeshFilter = fillHolder.AddComponent<MeshFilter>();
        MeshRenderer alertMeshRenderer = fillHolder.AddComponent<MeshRenderer>();
        
    // Make Sure Cone if On Top
        alertMeshRenderer.sortingLayerName = "Foreground";

    // Create & Assign Color
        alertMeshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        alertMeshRenderer.material.color = new Color(1f, 0f, 0f, .3f);
    
    // Assign Mesh
        alertMeshFilter.mesh = fillMesh;

    // Create Setup for Triangles
        int vertexCount = trianglesInCone + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[trianglesInCone * 3];

    // Set All Vertices to Zero; DEFAULT STATE
        for (int i = 0; i < vertices.Length; i++) { vertices[i] = Vector3.zero; }

    // Build Triangles: Unity Mesh Wants int[0, 1, 2, 0, 2, 3, 0, 3, 4, 0, ...] where each 3rd is the center point and = 0    
        for (int i = 0; i < trianglesInCone; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
    // Assign Array Data to Mesh
        fillMesh.vertices = vertices;
        fillMesh.triangles = triangles;
    }

// Update the Visuals of Detection Wave
    void UpdateFillMesh()
    {
        if (!player || fillMesh == null) { return; }

    // Update Triangle Vertices for Fill
        Vector3[] vertices = fillMesh.vertices;
        vertices[0] = Vector3.zero;

    // Find Angle Covered Per Triangle & Angle Start
        float anglePerTriangle = angle / trianglesInCone;
        float startAngle = -angle / 2f;

    // Build Array of Vertices Locations
        Vector3 localScale = transform.lossyScale;

        for (int i = 0; i <= trianglesInCone; i++)
        {
            float currentAngle = startAngle + anglePerTriangle * i;
            float radianConversion = currentAngle * Mathf.Deg2Rad;

            Vector3 point = new Vector3(Mathf.Sin(radianConversion), Mathf.Cos(radianConversion), 0);

        // Raycast to Check for Walls
            float worldConeDistance = coneDistance * Mathf.Abs(transform.lossyScale.x);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, point, worldConeDistance, obstacleLayer);

            float localWallDist;

            if (hit.collider != null) { localWallDist = hit.distance / Mathf.Abs(transform.lossyScale.x); }

            else { localWallDist = coneDistance; }

            float clampDistance = Mathf.Min(fillDistance, localWallDist);

            vertices[i + 1] = point * clampDistance;
        }

    // Assign Array Data to Mesh
        fillMesh.vertices = vertices;
    }

// Checks to See if Player is in Cone & Not Blocked Visually
    public bool CanSeePlayer()
    {
        if (!player) return false;

    // Scaled Parent Fix :'(
        float worldConeDistance = coneDistance * Mathf.Abs(transform.lossyScale.x);

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        
    // Player Outside Cone Length
        if (distanceToPlayer > worldConeDistance) { return false; }

    // Player Outside Cone Width    
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);

        if (angleToPlayer > angle / 2f) { return false; }

    // Player Behind Obstacle
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, worldConeDistance, obstacleLayer);
        
        if (hit.collider != null && hit.collider.transform != player) { return false; }
    
    // Player In View
        return true;
    }

// S T A T E  P A T T E R N : Setter
    public void SetState(IEnemyState newState) { currentState = newState; }

// S T A T E  P A T T E R N : Change State Functions
    public void GoToIdle()          { SetState(idleState); }
    
    public void GoToSuspicious()    { SetState(suspiciousState); }
    
    public void GoToAlerted()       { SetState(alertedState); }

// S T A T E  P A T T E R N : Helper Functions
    public Transform GetPlayer()    { return player; }

    public float DistanceToPlayer()
    {
        if (!player) return 0f;
        return Vector2.Distance(transform.position, player.position);
    }

    public void FillTowards(float targetWorldDistance)
    {
    // lossyScale Fix for Scaled Enemies Breaking Cone
        float scaleFactor = transform.lossyScale.x; 
        float scaledTarget = targetWorldDistance / scaleFactor;

        fillDistance = Mathf.MoveTowards(fillDistance, scaledTarget, (fillSpeed / scaleFactor) * Time.deltaTime);
    }

    public bool FillReached(float targetWorldDistance)
    {
        float scaleFactor = transform.lossyScale.x;
        return fillDistance >= (targetWorldDistance / scaleFactor);
    }

    //////////////////////// TL6: Enemy AI Interface /////////////////////
    
    public bool CheckIfAlerted() { return currentState is AlertedState; }
    
    //////////////////////////////////////////////////////////////////////

    // TEST HARNESS :[
                    // // T E S T  H A R N E S S : Non-Playmode Refresh
                    // public void RefreshForTest()
                    // {
                    //     GenerateConeMesh();
                    // }

                    // // T E S T  H A R N E S S :  
                    // public int GetVertexCount()
                    // {
                    //     MeshFilter meshFilter = GetComponent<MeshFilter>();

                    //     // Safety Check
                    //     if (meshFilter != null && meshFilter.sharedMesh != null)
                    //     {
                    //         return meshFilter.sharedMesh.vertexCount;
                    //     }
                        
                    //     else
                    //     {
                    //         return 0;
                    //     }
                    // }
}

// S T A T E  P A T T E R N : Interface

public interface IEnemyState { void UpdateState(VisualDetector enemy); }

// S T A T E  P A T T E R N : States

public class IdleState : IEnemyState
{
    public void UpdateState(VisualDetector enemy)
    {
        if (enemy.CanSeePlayer()) { enemy.GoToSuspicious(); }
        
        else { enemy.FillTowards(0f); }
    }
}

public class SuspiciousState : IEnemyState
{
    public void UpdateState(VisualDetector enemy)
    {
        if (!enemy.CanSeePlayer())
        {
            enemy.SendMessageUpwards("OnHearSound", (Vector2)enemy.GetPlayer().position);
            enemy.GoToIdle();
            return;
        }

        float playerDistance = enemy.DistanceToPlayer();
        enemy.FillTowards(playerDistance);

        if (enemy.FillReached(playerDistance)) { enemy.GoToAlerted(); }
    }
}

public class AlertedState : IEnemyState
{
    public void UpdateState(VisualDetector enemy)
    {
        if (!enemy.CanSeePlayer())
        {
            enemy.SendMessageUpwards("OnHearSound", (Vector2)enemy.GetPlayer().position);
            enemy.GoToSuspicious();
            return;
        }
        
        enemy.FillTowards(enemy.DistanceToPlayer());
    }
}