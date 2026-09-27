using UnityEngine;

public class Objetivos : MonoBehaviour
{
    [Header("UI dos Objetivos")]
    public GameObject[] uiObjetivos;

    [Header("Objetos do Jogo (Cubos)")]
    public GameObject[] cubosObjetivos;

    [HideInInspector]
    public int objetivoAtual = 0;

    void Start()
    {
        AtualizarObjetivo();
    }

    public void CompletarObjetivo()
    {
        if (objetivoAtual < uiObjetivos.Length)
        {
            objetivoAtual++;
            AtualizarObjetivo();
        }
    }

    private void AtualizarObjetivo()
    {
        // Atualiza as UIs
        for (int i = 0; i < uiObjetivos.Length; i++)
        {
            if (uiObjetivos[i] != null)
            {
                uiObjetivos[i].SetActive(i == objetivoAtual);
            }
        }

        // Atualiza os Cubos
        for (int i = 0; i < cubosObjetivos.Length; i++)
        {
            if (cubosObjetivos[i] != null)
            {
                cubosObjetivos[i].SetActive(i == objetivoAtual);
            }
        }
    }
}