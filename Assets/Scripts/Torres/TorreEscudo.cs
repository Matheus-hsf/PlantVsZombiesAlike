using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreEscudo : MonoBehaviour, ITorre, IDanificavel
{
    [SerializeField]
    private TorreStatus myStatus;

    private int vidaAtual;

    [SerializeField]
    private GameObject particleDestroy;

    [SerializeField]
    private AudioClip torreDetroyAC;


    void Start()
    {
        vidaAtual = myStatus.Vida;
    }

    void FixedUpdate()
    {
        if(vidaAtual <= 0){
            SoundController.GetDJ().EmitirSom(torreDetroyAC, 0.2f);
            Instantiate(particleDestroy, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        } 
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
