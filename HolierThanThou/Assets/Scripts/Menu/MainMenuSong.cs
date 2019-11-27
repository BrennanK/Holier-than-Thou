using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSong : MonoBehaviour
{
    private AudioManager audioManager;
    private string SceneBackgroundMusic;

    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        SceneBackgroundMusic = SceneManager.GetActiveScene().name;
        audioManager.Stop();
        audioManager.Play(SceneBackgroundMusic);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
