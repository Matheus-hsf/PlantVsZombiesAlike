using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsController : MonoBehaviour
{

    // layers que o raycast irá detectar
    public LayerMask hitLayers;

    // lista dos slots disponiveis na tela
    [SerializeField]
    public List<Slot> AllSlots = new List<Slot>();

    // objeto referente a interface do jogador
    [SerializeField]
    private GameObject uiPlayer;

    // bool que indica que uma torre está mudando de lugar
    private bool mudarTorre;

    // referencia do "slot"(torre) que ira ser trocada de lugar
    private Slot slotParaMudanca;



    // texturas do mouse quando interagir para a mudanca de posicao das torres
    [SerializeField]
    private Texture2D openHand, closedHand;

    //referência da classe para utilização do padrão singleton
    private static SlotsController slotsControllerInstance;


    private void Start() {
        GetSlotsControllerInstance();
    }

    private void Update() {
        EscolhendoSlotParaMudanca();
    }
    

    // metodo para mover a torre
    public void MoverTorre(){
        Cursor.SetCursor(openHand, Vector2.zero, CursorMode.Auto);
        uiPlayer.SetActive(false);
        mudarTorre = true;
    }

    // retorna o slot acima do slot passado no parametro
    public Slot GetSlotCima(Slot slot){
        Slot nullSlot = null;
        foreach(Slot x in AllSlots){
            if(x.positionGrid.x == slot.positionGrid.x && x.positionGrid.y == slot.positionGrid.y+1){
                return x;
            }
        }
        return nullSlot;
    }

    // retorna o slot abaixo do slot passado no parametro
    public Slot GetSlotBaixo(Slot slot){
        Slot nullSlot = null;
        foreach(Slot x in AllSlots){
            if(x.positionGrid.x == slot.positionGrid.x && x.positionGrid.y == slot.positionGrid.y-1){
                return x;
            }
        }
        return nullSlot;
    }
    
    // retorna o slot a frente do slot passado no parametro
    public Slot GetSlotFrente(Slot slot){
        Slot nullSlot = null;
        foreach(Slot x in AllSlots){
            if(x.positionGrid.x == slot.positionGrid.x-1 && x.positionGrid.y == slot.positionGrid.y){
                return x;
            }
        }
        return nullSlot;
    }
    
    // retorna o slot a atras do slot passado no parametro
    public Slot GetSlotAtras(Slot slot){
        Slot nullSlot = null;
        foreach(Slot x in AllSlots){
            if(x.positionGrid.x == slot.positionGrid.x+1 && x.positionGrid.y == slot.positionGrid.y){
                return x;
            }
        }
        return nullSlot;
    }
    
    // Detecta quando o mouse esta em cima de algum slot
    private void EscolhendoSlotParaMudanca(){
        if(mudarTorre){
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
                {
                    if(Input.GetMouseButtonDown(0) && hit.transform.tag == "Slot"){
                        if(mudarTorre && hit.transform.GetComponent<Slot>().ocupado && slotParaMudanca == null){
                            slotParaMudanca = hit.transform.GetComponent<Slot>();
                            slotParaMudanca.OnTrasition();
                            Cursor.SetCursor(closedHand, Vector2.zero, CursorMode.Auto);
                        }
                        else if(mudarTorre && slotParaMudanca != null && slotParaMudanca != hit.transform.GetComponent<Slot>() && !hit.transform.GetComponent<Slot>().ocupado ){
                            slotParaMudanca.MoverTorre(hit.transform.GetComponent<Slot>());
                            mudarTorre = false;
                            slotParaMudanca.NormalColor();
                            slotParaMudanca = null;
                            uiPlayer.SetActive(true);
                            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        }
                    }
                }
                else{
                    if(Input.GetMouseButtonDown(0)){
                        if(slotParaMudanca != null){
                            slotParaMudanca.NormalColor();
                        }
                        mudarTorre = false;
                        slotParaMudanca = null;
                        uiPlayer.SetActive(true);
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    }
                }
        }
        
    }

    // Retorna a instancia dessa classe
    public static SlotsController GetSlotsControllerInstance(){
        if(slotsControllerInstance == null){
            slotsControllerInstance = GameObject.FindObjectOfType<SlotsController>();

            if(slotsControllerInstance == null){
                GameObject container = new GameObject("SlotController");
                slotsControllerInstance = container.AddComponent<SlotsController>();
            }
        }
        return slotsControllerInstance;
    }
    
}
