using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject EnemySpawn;
    public GameObject GameOverGO;
    public GameObject ScoreUITextGO;
    private AudioSource Startfx;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,
    }
    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
     GMState = GameManagerState.Opening;   
     Startfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch(GMState){
            case GameManagerState.Opening:
            GameOverGO.SetActive(false);
            playButton.SetActive(true);
            break;

            case GameManagerState.Gameplay:
            ScoreUITextGO.GetComponent<GameScore>().Score = 0;
            playButton.SetActive(false);
            playerShip.GetComponent<PlayerCONTROL>().Init();
            EnemySpawn.GetComponent<EnemySpawner>().StartEnemySpawn();
            break;

            case GameManagerState.Gameover:
            EnemySpawn.GetComponent<EnemySpawner>().StopEnemySpawn();
            GameOverGO.SetActive(true);
            Invoke("OpenState", 8f);
            break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGame()
    {
        Startfx.PlayOneShot(Startfx.clip);
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void OpenState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

}
