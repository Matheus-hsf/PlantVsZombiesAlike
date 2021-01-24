using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int dano;

    private void Start() {
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Torre"){
            other.GetComponent<IDanificavel>().ReceberDano(dano);
        }  
    }

}
