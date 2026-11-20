using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].point.y < transform.position.y - transform.lossyScale.y * 0.4f)
        {
            Player.instance.DestroyedObjects++;
            Destroy(this.gameObject);
        }
    }
}
