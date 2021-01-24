using UnityEngine;

public class SoundController : MonoBehaviour
{
    //referência da classe para utilização do padrão singleton
    private static SoundController dj;


    // component para reproduzir os sons
    [SerializeField]
    private AudioSource audioSource;


    // metodo para pegar a instancia da classe SoundController
    public static SoundController GetDJ(){
        if(dj == null){
            dj = GameObject.FindObjectOfType<SoundController>();

            if(dj == null){
                GameObject container = new GameObject("SoundController");
                dj = container.AddComponent<SoundController>();
            }
        }

        return dj;        
    }


    void Start()
    {
        // atribuindo um valor ao audiosource 
        audioSource = audioSource == null ? GameObject.FindObjectOfType<AudioSource>() : audioSource;
    }

    // metodo para reproduzir sons
    public void EmitirSom(AudioClip som, float volume){
        audioSource.PlayOneShot(som, volume);
    }

}
