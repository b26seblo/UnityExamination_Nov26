using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI TopLeftText;
    public TextMeshProUGUI BottomLeftText;

    private float StartTime;

    private void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float remainingTime = Player.GameTimeLimit - (Time.time - StartTime);
        TopLeftText.text = 
            $@"Level {Player.CurrentLevel}
{Mathf.Floor(remainingTime / 60f)}:{Mathf.Floor(remainingTime % 60f)}";

        BottomLeftText.text =
            $@"Coins: {Player.instance.Coins}
Destroyed Objects: {Player.instance.DestroyedObjects}";
        
        if (remainingTime <= 0f)
        {
            int currentLevel = Player.CurrentLevel;
            Player.CurrentLevel = 1;
            Player.instance.Coins = 0;
            Player.instance.DestroyedObjects = 0;
            SceneManager.LoadSceneAsync("Level" + 1);
            SceneManager.UnloadSceneAsync("Level" + currentLevel);
            Player.instance.ResetPosition();
        }
    }
}
