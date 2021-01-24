using UnityEngine;

public class Slot : MonoBehaviour
{
    // posicao desse slot referente a todos no "grid" 
    public Vector2 positionGrid;
    
    // detectar se o slot já está ocupado
    public bool ocupado;
    
    // Torre que esta ocupando o slot 
    public GameObject torreAtual;
    
    // material individual de cada slot
    private Material myMaterial;

    // detecta se o "slot"(torre) esta em processo de mudança de posição
    private bool transiting;

    // referencia ao component MeshRenderer desse objeto
    private MeshRenderer mr;

    // Cores de estado do slot
    [SerializeField]
    private Color over, normal, full, transition;

    private void Awake() {
        mr = this.GetComponent<MeshRenderer>();
        myMaterial = mr.material;
        mr.material = myMaterial;
    }

    // define a torre desse slot
    public void SetSlot(bool setOcupado, GameObject setTorreAtual){
        ocupado = setOcupado;
        torreAtual = Instantiate(setTorreAtual, this.transform.position + (Vector3.up * setTorreAtual.GetComponent<ITorre>().GetOffSetY()) , setTorreAtual.transform.rotation);
    }

    // sobrecarga do SetSlot para poder passar a vida atual da torre, para a transição de torres
    public void SetSlot(bool setOcupado, GameObject setTorreAtual, int vidaAtual){
        ocupado = setOcupado;
        torreAtual = Instantiate(setTorreAtual, this.transform.position + (Vector3.up * setTorreAtual.GetComponent<ITorre>().GetOffSetY()) , setTorreAtual.transform.rotation);
        torreAtual.GetComponent<ITorre>().SetVidaAtual(vidaAtual);
    }


    private void OnMouseEnter() {
        if(!transiting){
            if(torreAtual == null){
                ocupado = false;
            }
            if(ocupado){
                mr.material.color = full;
            }
            else{
                mr.material.color = over;
            }
        }
    }

    private void OnMouseExit() {
        if(!transiting){
            mr.material.color = normal;
        }
    }
    public void OnTrasition(){
        mr.material.color = transition;
        transiting = true;
    }
    public void NormalColor(){
        mr.material.color = normal;
        transiting = false;
    }
    public Vector3 GetMyPosition(){
        return this.transform.position;
    }
    public void MoverTorre(Slot slotAlvo){
        if(torreAtual != null){
            slotAlvo.SetSlot(true, torreAtual, torreAtual.GetComponent<ITorre>().GetVida());
            this.ocupado = false;
            Destroy(torreAtual.gameObject);
            this.torreAtual = null;
        }
        
    }

}
