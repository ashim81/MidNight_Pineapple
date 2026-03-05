using UnityEngine;

// Safegaurds
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class VisualDetector : MonoBehaviour
{
// Inspector Variables:

[Header("Cone Settings")]

    [Range(0, 360)] public float angle = 90f;
    public float coneDistance = 5f;

    private int trianglesInCone = 30;
    private Mesh cone;
    private Mesh fillMesh;

[Header("Detection Settings")]

    public LayerMask obstacleLayer;
    private Transform player;

[Header("Alert Settings")]

    public float fillSpeed = 5f;
    private float fillDistance = 0f;

[Header("Enemy State")]

    public EnemyState state = EnemyState.Idle;
    public enum EnemyState 
    { 
        Idle, 
        Suspicious, 
        Alerted 
    }

// Find Player
    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

// Generate Cone Changes from Inspector
    void OnValidate()
    {
        GenerateConeMesh();
    }

// Generate Cone On "Play"
    void OnEnable()
    {
        GenerateConeMesh();
        GenerateFillMesh();
    }

    void Update()
    {
        UpdateEnemyState();
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
        for (int i = 0; i <= trianglesInCone; i++)
        {
            float currentAngle = startAngle + anglePerTriangle * i;
            float radianConversion = currentAngle * Mathf.Deg2Rad;

        // (Y, X, Z) To Make Cone Point Up
            Vector3 point = new Vector3(Mathf.Sin(radianConversion), Mathf.Cos(radianConversion), 0);

        // Raycast to Check for Walls
            float vertexDistance;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, point, coneDistance, obstacleLayer);

            if (hit.collider != null) 
                { vertexDistance = hit.distance; } 
            
            else 
                { vertexDistance = coneDistance; }

            vertices[i + 1] = point * vertexDistance;
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
        MeshRenderer altertMeshRenderer = fillHolder.AddComponent<MeshRenderer>();
        
    // Create & Assign Color
        altertMeshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        altertMeshRenderer.material.color = new Color(1f, 0f, 0f, .3f);
    
    // Assign Mesh
        alertMeshFilter.mesh = fillMesh;

    // Create Setup for Triangles
        int vertexCount = trianglesInCone + 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[trianglesInCone * 3];

    // Set All Vertices to Zero; DEFAULT STATE
        for (int i = 0; i < vertices.Length; i++) 
        {
            vertices[i] = Vector3.zero;
        }

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
        for (int i = 0; i <= trianglesInCone; i++)
        {
            float currentAngle = startAngle + anglePerTriangle * i;
            float radianConversion = currentAngle * Mathf.Deg2Rad;

            Vector3 point = new Vector3(Mathf.Sin(radianConversion), Mathf.Cos(radianConversion), 0);

        // Raycast to Check for Walls
            float wallDistance;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, point, coneDistance, obstacleLayer);

            if (hit.collider != null) 
                { wallDistance = hit.distance; } 
            
            else 
                { wallDistance = coneDistance; }

            float clampDistance = Mathf.Min(fillDistance, wallDistance);

            vertices[i + 1] = point * clampDistance;
        }

    // Assign Array Data to Mesh
        fillMesh.vertices = vertices;
    }

// Update Ememy State Based on Detection Wave
    void UpdateEnemyState()
    {
        if (!player) return;

        bool canSee = CanSeePlayer();
        float playerDistance;

        if (canSee)
        {
            playerDistance = Vector2.Distance(transform.position, player.position);
            state = EnemyState.Suspicious;
        }

        else
        {
            playerDistance = 0f;
            state = EnemyState.Idle;
        }

        fillDistance = Mathf.MoveTowards( fillDistance, playerDistance, fillSpeed * Time.deltaTime );

        if (fillDistance >= playerDistance && canSee)
        {
            state = EnemyState.Alerted;
        }
    }

// Checks to See if Player is in Cone & Not Blocked Visually
    bool CanSeePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        
    // Player Outside Cone Length
        if (distanceToPlayer > coneDistance) 
        {
            return false;
        }

    // Player Outside Cone Width    
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);

        if (angleToPlayer > angle / 2f) 
        {
            return false;
        }

    // Player Behind Obstacle
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, coneDistance, obstacleLayer);
        
        if (hit.collider != null && hit.collider.transform != player) 
        {
            return false;
        }
    
    // Player In View
        return true;
    }
}