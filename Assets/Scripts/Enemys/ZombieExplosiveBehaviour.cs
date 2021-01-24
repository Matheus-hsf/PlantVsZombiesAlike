using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieExplosiveBehaviour : MonoBehaviour, IDanificavel
{
   public EnemyStatus myStatus;

    private float speed;
    private EnemyMovement myMov;

    private Animator anim;

    private GameObject target;

    private bool atacando;

    private int vidaAtual;
    private bool morto;

    [SerializeField]
    private GameObject miniZombies;

    [SerializeField]
    private Slot slotAtual;

    [SerializeField]
    private GameObject particleDie;


    [SerializeField]
    private GameObject particleHit;

    [SerializeField]
    private AudioClip morrendoAC;
    void Start()
    {
        vidaAtual = myStatus.Vida;
        anim = this.GetComponentInChildren<Animator>();
        speed = myStatus.speed;
        myMov = new EnemyMovement();
    }


    void FixedUpdate()
    {
        if(vidaAtual > 0){
            myMov.Andar(this.gameObject, speed);
            if(target == null && atacando){
                atacando = false;
                myMov.ResetarMovimentacao();
                anim.SetBool("Atack", false);
            }
        }
        else if(morto){
            morto = false;
            anim.SetTrigger("Die");
            Morri();
        }
    }

    public void ReceberDano(int dano){
        vidaAtual -= dano;
        
        Instantiate(particleHit,this.transform.position + Vector3.up * 2, this.transform.rotation);

        if(vidaAtual <= 0 && !morto){
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<Collider>().enabled = false;
            morto = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Torre"){
            atacando = true;
            myMov.Parar();
            anim.SetBool("Atack", true);
            anim.SetFloat("AtackSpeed", myStatus.atackSpeed);
            target = other.gameObject;
        }

        if(other.gameObject.tag == "Slot"){
            slotAtual = other.gameObject.GetComponent<Slot>(); 
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Finish"){
            PlayerInfo.GetPlayerInfoInstance().Perdeu();
        }
    }


    public void Morri(){

        SoundController.GetDJ().EmitirSom(morrendoAC, 0.15f);
        

        Instantiate(particleDie, this.transform.position + Vector3.up * 2, this.transform.rotation);

        //cima
        Slot posC = SlotsController.GetSlotsControllerInstance().GetSlotCima(slotAtual); 
        if(posC != null && !posC.ocupado){
            Instantiate(miniZombies, posC.GetMyPosition() , miniZombies.transform.rotation);
        }
        // baixo
        Slot posB = SlotsController.GetSlotsControllerInstance().GetSlotBaixo(slotAtual); 
        if(posB != null && !posB.ocupado){
            Instantiate(miniZombies, posB.GetMyPosition() , miniZombies.transform.rotation);
        }
        // frente
        Slot posF = SlotsController.GetSlotsControllerInstance().GetSlotFrente(slotAtual); 
        if(posF != null && !posF.ocupado){
            Instantiate(miniZombies, posF.GetMyPosition() , miniZombies.transform.rotation);
        }
        // tras
        Slot posT = SlotsController.GetSlotsControllerInstance().GetSlotAtras(slotAtual); 
        if(posT != null && !posT.ocupado){
            Instantiate(miniZombies, posT.GetMyPosition() , miniZombies.transform.rotation);
        }

        PlayerInfo.OuroAtual += myStatus.ouro;
        PlayerInfo.attOuro = true;
        Destroy(this.gameObject, 2);
    }

    private void OnDestroy() {
        if(EnemyController.GetEnemyControllerInstance() != null){
            EnemyController.GetEnemyControllerInstance().CheckEnemys();
        }
    }


}
