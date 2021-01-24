using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarreira : MonoBehaviour, IDanificavel
{
    public int vidaAtual;
    void FixedUpdate()
    {
        if(vidaAtual <= 0){
            Destroy(this.gameObject);
        }
    }

    public void ReceberDano(int dano){
        vidaAtual -= dano;
    }

}
