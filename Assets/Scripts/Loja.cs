using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loja : MonoBehaviour
{

    [SerializeField]
    private GameObject uiInteracoes;
    public LayerMask hitLayers;
    private GameObject torreSelecionada;
    private bool comprando;

    [SerializeField]
    private Texture2D cursorHammer;

    void Start()
    {
        
    }


    private void Update() {
        Comprando();
    }


    private void Comprando(){
        if(comprando){
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
                {
                    if(Input.GetMouseButtonDown(0)){
                        if(hit.transform.tag == "Slot" && !hit.transform.GetComponent<Slot>().ocupado){
                            hit.transform.GetComponent<Slot>().SetSlot(true, torreSelecionada);
                            ExecutarCompra();
                        }
                    }
                }
                else{
                    if(Input.GetMouseButtonDown(0)){
                        CompraCancelada();
                    }
                }
        }
        
    }

    public void Comprar(GameObject torreComprada){
        if(ValidarCompra(torreComprada.GetComponent<ITorre>().GetPrecoTorre())){
            torreSelecionada = torreComprada;
            comprando = true;
            Cursor.SetCursor(cursorHammer,Vector2.zero, CursorMode.Auto);
            uiInteracoes.SetActive(false);
       }
    }

    private void ExecutarCompra(){
        PlayerInfo.OuroAtual -= torreSelecionada.GetComponent<ITorre>().GetPrecoTorre();
        PlayerInfo.attOuro = true;
        torreSelecionada = null;
        comprando = false;
        uiInteracoes.SetActive(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void CompraCancelada(){
        torreSelecionada = null;
        comprando = false;
        uiInteracoes.SetActive(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private bool ValidarCompra(int precoTorre){
        if(PlayerInfo.OuroAtual - precoTorre >= 0){
            return true;
        }
        else{
            return false;
        }
    }


    public void SetCursorIcon(Texture2D spriteTower){
        Cursor.SetCursor(spriteTower,Vector2.zero, CursorMode.Auto);
    }



}
