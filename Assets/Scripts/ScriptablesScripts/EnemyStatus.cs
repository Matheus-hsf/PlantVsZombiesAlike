using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptables/Enemy", order = 2)]
public class EnemyStatus : ScriptableObject
{
    public string Nome;
    public int Vida;
    public float speed;
    public int dano;
    public float atackSpeed;
    public float offSetY;

    public int ouro;

}
