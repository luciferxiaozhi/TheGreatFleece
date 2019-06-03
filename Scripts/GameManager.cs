using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; // singleton instance
    private bool backgroundMusicPlayed;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL!!");
            }
            return _instance;
        }
    }

    public bool hasCard { get; set; }
    public PlayableDirector introCutscene;

    private void Awake()
    {
        _instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            introCutscene.time = 62.0f;
            AudioManager.Instance.PlayBackgroundMusic();
            backgroundMusicPlayed = true;
        }

        if (!backgroundMusicPlayed && introCutscene.time - 62.0f <= 0.01f)
        {
            AudioManager.Instance.PlayBackgroundMusic();
            backgroundMusicPlayed = true;
        }
    }
}
