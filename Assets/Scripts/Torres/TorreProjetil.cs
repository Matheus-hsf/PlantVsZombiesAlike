using UnityEngine;

public class TorreProjetil : MonoBehaviour
{
    public int dano;
    public float speed;
    public bool go;

    public Vector3 direction;

    public bool piercing;
    void FixedUpdate()
    {
        if(go){
            this.transform.position += direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy"){
            other.GetComponent<IDanificavel>().ReceberDano(dano);
            if(!piercing){
                Destroy(this.gameObject);
            }
        }
    }


}
