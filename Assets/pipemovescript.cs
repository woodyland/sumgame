using UnityEngine;

public class pipemovescript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadzone = -45;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed);

        if (transform.position.x < deadzone)
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
            
        }
    
}
}
