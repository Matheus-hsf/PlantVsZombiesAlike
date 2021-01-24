using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static int OuroAtual;

    public Text txtOuro;

    public Slider progressBarWave;
    public static bool attOuro;

    private static PlayerInfo playerInfo;

   
    public GameObject uiLost;

    private void Awake() {
        GetPlayerInfoInstance();     
    }

    private void Start() {
        OuroAtual = OuroAtual != 0 ? OuroAtual : 300;
        txtOuro.text = "$" + OuroAtual.ToString();
        progressBarWave.value = 0;
        progressBarWave.maxValue = EnemyController.GetEnemyControllerInstance().GetMaxEnemys();

    }


    private void Update() {
        if(attOuro){
            attOuro = false;
            txtOuro.text = "$" + OuroAtual.ToString();
        }
    }


    public static PlayerInfo GetPlayerInfoInstance(){

        if(playerInfo == null){
            playerInfo = GameObject.FindObjectOfType<PlayerInfo>();

            if(playerInfo == null){
                GameObject container = new GameObject("PlayerInfo");
                playerInfo = container.AddComponent<PlayerInfo>();
            }
        }
        return playerInfo;
    }


    public void Perdeu(){
        uiLost.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetarCena(){
        Time.timeScale = 1;
        OuroAtual = 300;
        SceneManager.LoadScene("Main");
    }

}
