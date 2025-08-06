using UnityEngine;

public class pipepawnscript : MonoBehaviour
{
    [Header("Pipe Settings")]
    public GameObject pipe;
    public float spawnRate = 7f; // Changed from int to float for more control
    private float timer = 0;
    public float heightOffset = 2f; // Reduced from 10 - was way too random!

    [Header("Movement & Cleanup")]
    public float pipeSpeed = 3f; // Speed pipes move left
    public float destroyDistance = -15f; // Where to destroy pipes

    void Start()
    {
        // Don't spawn immediately - let player get ready
        timer = spawnRate * 0.5f; // Start halfway to first spawn
    }

    void Update()
    {
        // Spawn timer
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnpipe();
            timer = 0;
        }

        // Clean up old pipes
        CleanupOldPipes();
    }

    void spawnpipe()
    {
        // Much more reasonable spawn range
        float lowestpoint = transform.position.y - heightOffset;
        float highestpoint = transform.position.y + heightOffset;

        // Create the pipe
        GameObject newPipe = Instantiate(pipe,
            new Vector3(transform.position.x, Random.Range(lowestpoint, highestpoint), 0),
            transform.rotation);

        // Add movement to the pipe
        PipeMovement pipeMovement = newPipe.AddComponent<PipeMovement>();
        pipeMovement.speed = pipeSpeed;

        Debug.Log($"Spawned chill pipe at Y: {newPipe.transform.position.y:F1}");
    }

    void CleanupOldPipes()
    {
        // Find all pipes and destroy ones that are too far left
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipeObj in pipes)
        {
            if (pipeObj.transform.position.x < destroyDistance)
            {
                Destroy(pipeObj);
            }
        }
    }
}

// Simple component to make pipes move left
public class PipeMovement : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}