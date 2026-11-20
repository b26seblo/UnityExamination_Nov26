using System;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private float ActivationHeight;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        ActivationHeight = transform.position.y + (transform.lossyScale.y * boxCollider.size.y) * 0.4f;
    }

    private void Update()
    {
        boxCollider.enabled = (Player.instance.transform.position.y - Player.collider.size.y * 0.5f) >= ActivationHeight;
    }
}
