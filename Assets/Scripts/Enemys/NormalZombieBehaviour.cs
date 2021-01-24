using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieBehaviour : MonoBehaviour, IDanificavel
{
    // Valores dos atributos do zombie
    public EnemyStatus myStatus;

    private float speed;
    
    // classe responsavel pelo metodo de movimentação dos inimigos
    private EnemyMovement myMov;

    private Animator anim;

    private GameObject target;

    private bool atacando;

    private int vidaAtual;

    private bool morto;

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
        // se a vida for maior que 0 ele anda e se encontrar uma torre ataca
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

    // metodo Referente a interface IDanificavel para todas as classes que recebem dano
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
        if(other.gameObject != null){
            if(other.gameObject.tag == "Torre"){
                atacando = true;
                myMov.Parar();
                anim.SetBool("Atack", true);
                anim.SetFloat("AtackSpeed", myStatus.atackSpeed);
                target = other.gameObject;
            }
        }
    }

    // Metodo para quando ele encostar na casa acionar o game over
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Finish"){
            PlayerInfo.GetPlayerInfoInstance().Perdeu();
        }
    }

    public void Morri(){
        // reproduz o som do zombie morrendo
        SoundController.GetDJ().EmitirSom(morrendoAC, 0.15f);

        // instancia as particulas dele morrendo
        Instantiate(particleDie, this.transform.position + Vector3.up * 2, this.transform.rotation);

        // aumenta o dinheiro do jogador
        PlayerInfo.OuroAtual += myStatus.ouro;
        PlayerInfo.attOuro = true;

        // se o zombie estiver com uma barreira ela será destruida
        if(this.transform.GetComponentInChildren<EnemyBarreira>()){
            Destroy(this.transform.GetChild(1).gameObject);
        }
        // destroi o zombie depois de 2 segundos, optado para não utilizar um destroy no fim da animação por meio do animation event
        Destroy(this.gameObject, 2);
    }


    // checa com o controller de inimigos se ainda restam inimigos
    private void OnDestroy() {
        if(EnemyController.GetEnemyControllerInstance() != null){
            EnemyController.GetEnemyControllerInstance().CheckEnemys();
        }  
    }
    
}
