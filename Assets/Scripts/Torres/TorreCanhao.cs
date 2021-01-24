using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreCanhao : MonoBehaviour, ITorre, IDanificavel
{

    [SerializeField]
    private TorreStatus myStatus;

    private int vidaAtual;

    [SerializeField]
    private GameObject projetil;

    private float contador;
    private float cdDisparos;

    private bool alvosAFrente;

    [SerializeField]
    private GameObject particleDestroy;

    [SerializeField]
    private AudioClip tiroAC, torreDetroyAC;

    void Awake()
    {
        vidaAtual = myStatus.Vida;
        cdDisparos = myStatus.taxaDeDisparo;
    }

    void FixedUpdate()
    {
        ChecarAlvos();
        TimerToShoot();
        ChecarVivo();
    }

    // checa se a vida da torre ainda é maior que 0
    private void ChecarVivo(){
        if(vidaAtual <= 0){
            SoundController.GetDJ().EmitirSom(torreDetroyAC, 0.2f);
            Instantiate(particleDestroy, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // checa se tem algum alvo a frente da torre
    private void ChecarAlvos(){
        Ray castPoint = new Ray(this.transform.GetChild(2).position, this.transform.GetChild(2).transform.forward * 2);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.layer == 8 && !alvosAFrente){
                alvosAFrente = true;
            }
        }
        else if(hit.transform == null || hit.transform.gameObject.layer != 8){
            alvosAFrente = false;
        }

        Debug.DrawRay(castPoint.origin, castPoint.direction * 50, Color.green);
    }

    // Metodo que conta o tempo entre os disparos da torre
    private void TimerToShoot(){
        if(alvosAFrente){
            contador += Time.fixedDeltaTime;
            if(contador >= cdDisparos){
                contador = 0;
                Disparar();
            }
        }
    }

    // metodo que dispara o tiro
    private void Disparar(){
        SoundController.GetDJ().EmitirSom(tiroAC, 0.2f);
        GameObject projetilInstance = Instantiate(this.projetil, this.transform.GetChild(2).transform.position, this.projetil.transform.rotation);
        projetil.GetComponent<TorreProjetil>().dano = myStatus.Dano;
        projetil.GetComponent<TorreProjetil>().go = true;
    }

    // retorna o preco da torre
    public int GetPrecoTorre(){
        return myStatus.GetPrecoTorre();
    }
    
    // retorna o offsetY da torre para ela ser instanciada na altura certa
    public float GetOffSetY(){
        return myStatus.offSetY;
    }

    // metodo que diminui a vida da torre
    public void ReceberDano(int atackDamage){
        vidaAtual -= atackDamage;
    }
    
    // metodo para saber a vida atual da torre
    public int GetVida(){
        return vidaAtual;
    }

    // metodo para definir a vida atual da torre
    public void SetVidaAtual(int value){
        vidaAtual = value;
    }

}
