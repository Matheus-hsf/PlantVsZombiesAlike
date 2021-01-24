using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreArma : MonoBehaviour, ITorre, IDanificavel
{
    [SerializeField]
    private TorreStatus myStatus;

    private int vidaAtual;


    [SerializeField]
    private GameObject projetil;

    private float contador;
    private float cdDisparos;

    private bool alvosAFrente;

    private Animator anim;

    [SerializeField]
    private GameObject particleDestroy;

    [SerializeField]
    private AudioClip tiroAC, torreDetroyAC;



    void Awake()
    {
        anim = this.GetComponentInChildren<Animator>();
        vidaAtual = myStatus.Vida;
        cdDisparos = myStatus.taxaDeDisparo;
    }

    void FixedUpdate()
    {
        ChecarAlvos();
        TimerToShoot();
        if(vidaAtual <= 0){
            SoundController.GetDJ().EmitirSom(torreDetroyAC, 0.2f);
            Destroy(this.gameObject);
            Instantiate(particleDestroy, this.transform.position, this.transform.rotation);
        }
    }

    private void ChecarAlvos(){
        Ray castPoint = new Ray(this.transform.position, this.transform.forward * 2);
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

    private void TimerToShoot(){
        if(alvosAFrente){
            contador += Time.fixedDeltaTime;
            if(contador >= cdDisparos){
                contador = 0;
                StartCoroutine(MultiplosDisparos());
            }
        }
    }

    IEnumerator MultiplosDisparos(){
        WaitForSeconds cd = new WaitForSeconds(0.5f);
        yield return cd;
        Disparar();
        yield return cd;
        Disparar();
        yield return cd;
        Disparar();
    }

    private void Disparar(){
        SoundController.GetDJ().EmitirSom(tiroAC, 0.25f);
        anim.SetTrigger("Fire");
        GameObject projetilInstance = Instantiate(this.projetil, this.transform.position, this.projetil.transform.rotation);
        projetil.GetComponent<TorreProjetil>().dano = myStatus.Dano;
        projetil.GetComponent<TorreProjetil>().go = true;
    }
    public int GetPrecoTorre(){
        return myStatus.GetPrecoTorre();
    }
    
    public float GetOffSetY(){
        return myStatus.offSetY;
    }

    public void ReceberDano(int atackDamage){
        vidaAtual -= atackDamage;
    }

    public int GetVida(){
        return vidaAtual;
    }

    public void SetVidaAtual(int value){
        vidaAtual = value;
    }

}
