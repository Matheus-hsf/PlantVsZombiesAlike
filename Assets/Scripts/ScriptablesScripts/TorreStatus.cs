using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TorreStatus", menuName = "Scriptables/Torres", order = 1)]
public class TorreStatus : ScriptableObject
{
    public string Name;
    public int Dano;
    public float taxaDeDisparo;
    public int Vida;

    public float offSetY;

    [SerializeField]
    private int precoTorre;
    public int GetPrecoTorre(){
        return this.precoTorre;
    }

}
