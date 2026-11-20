using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool Enabled = false;
    public Sprite ActivatedSprite;
    public int LevelToLoad;
    public int LevelToUnload;
    
    private bool Activated = false;

    public void EnableDoor()
    {
        Enabled = true;
        GetComponent<SpriteRenderer>().sprite = ActivatedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Activated || !Enabled) return;
        
        if (other == Player.collider)
        {
            Activated = true;
            SceneManager.LoadSceneAsync("Level" + LevelToLoad, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Level" + LevelToUnload);
        }
    }
}
