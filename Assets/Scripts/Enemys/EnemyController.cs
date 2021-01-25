using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject[] enemyList;


    [SerializeField]
    private int plusEnemysPerWave;

    [SerializeField]
    private int nmrEnemysFirstWave;

    [SerializeField]
    private int nmrWaves;

    private int waveAtual;
   
    [SerializeField]
    private int timeEntreWaves;

    [SerializeField]
    private float timeToStart;

    private static bool ultimoSpawn;

    [SerializeField]
    private GameObject particleSpawn;

    private static EnemyController enemyControllerInstance; 

    private static bool applicationQuiting = false;

    public GameObject uiWin;



    private void Awake() {
        GetEnemyControllerInstance();
        StartCoroutine(ComecarWave());
    }

    IEnumerator ComecarWave(){
        yield return new WaitForSeconds(timeToStart);
        StartCoroutine(SpawnHorda(nmrEnemysFirstWave));
    }

    IEnumerator SpawnHorda(int nmrEnemys){
        
        while(nmrEnemys > 0){
            int rngEnemy = Random.Range(0,enemyList.Length);
            int rngSpawn = Random.Range(0,spawnPoints.Length);
            Instantiate(enemyList[rngEnemy], spawnPoints[rngSpawn].transform.position, enemyList[rngEnemy].transform.rotation);
            Instantiate(particleSpawn, spawnPoints[rngSpawn].transform.position, particleSpawn.transform.transform.rotation);
            PlayerInfo.GetPlayerInfoInstance().progressBarWave.value += 1;
            float cdSpawn = Random.Range(1, 2.5f);
            nmrEnemys --;
            yield return new WaitForSeconds(cdSpawn);
        }

        waveAtual++;

        if(waveAtual < nmrWaves){
            yield return new WaitForSeconds(timeEntreWaves);
            StartCoroutine(SpawnHorda(nmrEnemysFirstWave + (waveAtual * plusEnemysPerWave)));
        }
        else{
            ultimoSpawn = true;
            CheckEnemys();
        }
        
    }

    public static EnemyController GetEnemyControllerInstance(){
        
        if(applicationQuiting){
            return null;
        }
       
        if(enemyControllerInstance == null){
            enemyControllerInstance = GameObject.FindObjectOfType<EnemyController>();

            if(enemyControllerInstance == null){
                GameObject container = new GameObject("EnemyController");
                container.AddComponent<EnemyController>();
            }
        }

        return enemyControllerInstance;

    }

   
    public void CheckEnemys(){       
        if(ultimoSpawn){
            if(GameObject.FindGameObjectWithTag("Enemy") == null && uiWin != null){
                uiWin.SetActive(true);
            }
        }
    }

    public int GetMaxEnemys(){
        int x = nmrEnemysFirstWave;
        for (int i = 1; i <= nmrWaves; i++)
        {
            x += i * plusEnemysPerWave;
        }
        return x;
    }

    private void OnDestroy() {
        applicationQuiting = true;
    }

    private void OnEnable() {
        applicationQuiting = false;
    }
  

}
