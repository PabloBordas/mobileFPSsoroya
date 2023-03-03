using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] respawnPoints;
    public int totalNumberOfEnemies;
    public int currentEnemies;
    public static GameController gameControllerInstance;

    void Awake()
    {
        gameControllerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(totalNumberOfEnemies > currentEnemies){
            CreateEnemy();
        }
    }

    void CreateEnemy(){
        Vector3 enemyRespawnPosition = respawnPoints[Random.Range(0,4)].transform.position;
        Instantiate(enemyPrefab, enemyRespawnPosition, enemyPrefab.transform.rotation);
        currentEnemies++;
    }

    void StartLevel(){
        for(int i = 0; i < totalNumberOfEnemies; i++){
            CreateEnemy();
        }
    }
}
