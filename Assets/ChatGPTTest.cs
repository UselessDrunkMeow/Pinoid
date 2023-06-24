using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Transform startPoint; // The starting point
    public Transform endPoint; // The ending point
    public float speed = 5f; // The movement speed

    private float distance; // The distance between the start and end points
    private float startTime; // The time at which movement starts

    private void Start()
    {
        // Calculate the distance between the start and end points
        distance = Vector3.Distance(startPoint.position, endPoint.position);
    }

    private void Update()
    {
        // Calculate the current position based on the elapsed time
        float elapsedTime = Time.time - startTime;
        float t = Mathf.Clamp01(elapsedTime / distance);
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

        // Check if the object has reached the end point
        if (t >= 1f)
        {
            // Swap the start and end points
            Transform temp = startPoint;
            startPoint = endPoint;
            endPoint = temp;

            // Reset the start time
            startTime = Time.time;
        }
    }

    private void OnEnable()
    {
        // Set the initial start time
        startTime = Time.time;
    }
}