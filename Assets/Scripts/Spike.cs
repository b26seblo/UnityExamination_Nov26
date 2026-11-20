using UnityEngine;

public class Spike : MonoBehaviour
{
    public float TimeOffset = 0f;
    public float UpDownSpeed = 1f;
    public float DownHeight = 1f;
    
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
        TimeOffset *= Mathf.PI;
    }

    void Update()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y - (Mathf.Abs(Mathf.Sin(TimeOffset + Time.time * UpDownSpeed)) * DownHeight), startPosition.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Player.collider)
            Player.instance.ResetPosition();
    }
}
