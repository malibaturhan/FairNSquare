using UnityEngine;

public class SquareCorners : MonoBehaviour
{

    [Header("***Settings***")]
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, 0.05f);
        //Gizmos.DrawLine(transform.position, direction);
    }
}
