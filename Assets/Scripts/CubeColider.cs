
using UnityEngine;

public class CubeColider : MonoBehaviour
{
    [SerializeField] private float forceDirection = 10f;
    private Rigidbody cubeRigidbody;    // Start is called before the first frame update
    void Start()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space) )
        {
            //Touch touch = Input.GetTouch(0);
            cubeRigidbody.AddForce(Vector3.left * forceDirection);
        }
        // Check if there are any touches
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);
        
            // Check if the touch just began
            cubeRigidbody.AddForce(Vector3.left * forceDirection);
            
        }
    }
}
