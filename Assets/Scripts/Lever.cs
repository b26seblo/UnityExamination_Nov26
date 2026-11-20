using System;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public Sprite ActivatedSprite;
    public UnityEvent OnActivated;

    private bool Activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Activated) return;
        
        if (other == Player.collider)
        {
            Activated = true;
            GetComponent<SpriteRenderer>().sprite = ActivatedSprite;
            OnActivated.Invoke();
        }
    }
}
