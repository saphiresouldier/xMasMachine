using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * 
 * TODO: if at right when still letters left - level-- 
 * if string emptied - add new object + a little fast (gamemanager) 
 * 
 * */

public enum CoreLoopPhase
{
    PREWARM,
    CORELOOP,
    WON,
    LOST
}

public class GameCoreLoop : MonoBehaviour
{

    public int playerLives = 3;
    public float spawnIntervallSpeed = 0.001f; 

    public List<GameObject> ToyPrefabs;
    public StringManager stringManager;
    public float SpawnIntervall = 2.0f;    
    public Transform SpawnLocation;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        CreateToy();
        timer = 0.0f;
    }

    private void OnEnable()
    {
        //Don't tell anybody
        if(!SpawnLocation)
        {
            SpawnLocation = GameObject.Find("SpawnLocation").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > SpawnIntervall)
        {
            timer = 0.0f;

            CreateToy();
        }
    }

    public void CreateToy(int type = 0)
    {

        Debug.Log("current scene: " + SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "1-level") {
            int rand = (int)(Random.value * (ToyPrefabs.Count - 1));
            GameObject randPrefab = ToyPrefabs[rand];

            GameObject instance = Instantiate(randPrefab, SpawnLocation.position, SpawnLocation.rotation);
            TextMesh display = instance.GetComponentInChildren<TextMesh>();
            stringManager.AddToyString(display);
            SpawnIntervall = SpawnIntervall - spawnIntervallSpeed;
            spawnIntervallSpeed = spawnIntervallSpeed + 0.001f;

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hello" + other.gameObject.name + " collided");
    }

    public void OnGUI()
    {
#if UNITY_EDITOR
        GUILayout.Label("Current lives: " + playerLives);
        GUILayout.Label("Current toys: " +  stringManager.toysCollected);
#endif
    }


    public int getPlayerLives() {
        return playerLives;
    }

    public int PlayerLives
    {
        get
        {
            return playerLives;
        }

        set { playerLives = value; }

    }


}
