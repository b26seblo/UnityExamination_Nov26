using System;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider == Player.collider)
        {
            Player.instance.transform.position += new Vector3(Time.fixedDeltaTime, 0f, 0f);
        }
    }
}
