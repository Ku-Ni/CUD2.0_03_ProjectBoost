using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour {

    [SerializeField]
    private AudioClip successfulAudio;

    private int currentLevel;
    private int maxLevel;

    private AudioSource audioSource;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        maxLevel = SceneManager.sceneCountInBuildSettings - 1;
        Debug.Log("Current Level: " + currentLevel + "\n Max Level: " + maxLevel);
        LoadNextLevel();
    }


    private void LoadNextLevel() {

        if (currentLevel == maxLevel) {
            // todo complete, currently remain level
        }
        else {
            currentLevel = currentLevel + 1;
        }

        Debug.Log("Loading level: " + currentLevel);
        SceneManager.LoadScene(currentLevel);
    }


    public void Crash() {
        currentLevel = 0;

        GameObject player = GameObject.FindGameObjectWithTag(Tags.Player.ToString());
        if (player == null) {
            throw new UnityException("Could not load player object tagged with " + Tags.Player.ToString());
        } else {
            player.SetActive(false);
        }
                   
        LoadNextLevel();
    }


    public void CompleteCurrentLevel() {
        audioSource.PlayOneShot(successfulAudio);
        Invoke("LoadNextLevel", successfulAudio.length);
    }
	
}
