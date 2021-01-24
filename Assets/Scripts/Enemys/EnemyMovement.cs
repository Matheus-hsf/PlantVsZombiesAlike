using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    protected float speedInicial;
    private bool parado;

    

    public EnemyMovement(){
        
    }

    public void SetSpeedInicial(float value){
        speedInicial = value;
    }
    public void Parar(){
        parado = true;
    }

    public void ResetarMovimentacao(){
        parado = false;
    }

    public void Andar(GameObject obj, float speed){
        if(!parado){
            obj.transform.position +=  Vector3.right * speed/100;
        }
    } 

}
