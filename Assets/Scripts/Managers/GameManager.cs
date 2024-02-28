using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    static GameManager instance;

    [SerializeField] PlayerController playerPrefab;
    public PlayerController PlayerInstance => playerInstance;
    PlayerController playerInstance = null;
    Transform currentCheckpoint;

    //Lives and Score
    [SerializeField] int maxLives = 5;
    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (lives > value)
                Respawn();

            _lives = value;

            //we've increased past our max lives so we should just be set to our max lives
            if (lives > maxLives)
                lives = maxLives;

            if (lives < 0)
                GameOver();

            //if (TestMode) Debug.Log("Lives has been set to: " + _lives.ToString());
        }
    }

    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            //if (score < value)
            //invalid setting so throw error = possibly return out of function before setting varible

            _score = value;

            //if (TestMode) Debug.Log("Score has been set to: " + _score.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        _lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int buildIndex = (SceneManager.GetActiveScene().name == "Level") ? 0 : 1;
            SceneManager.LoadScene(buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateCheckpoint(GameObject.FindGameObjectWithTag("Test").transform);
        }    
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        Debug.Log("checkpoint updated");
        currentCheckpoint = updatedCheckpoint;
    }

    void Respawn()
    {
        Debug.Log("Respawn Called");
        playerInstance.transform.position = currentCheckpoint.position;
    }

    void GameOver()
    {
        //gameover logic would go here
        Debug.Log("GameOver Called");
    }
}
