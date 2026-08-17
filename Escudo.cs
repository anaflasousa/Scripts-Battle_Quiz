using UnityEngine;

public class EscudoPlayer : MonoBehaviour
{
    [Header("Estado da Defesa")]
    public bool estaDefendendo = false;

    // Opcional: arraste o GameObject 3D do seu escudo aqui no Inspector para sumir/aparecer
    [Header("Visual (Opcional)")]
    public GameObject objetoEscudo; 

    void Update()
    {
        // GetMouseButton(1) -> Botão DIREITO do mouse (retorna verdadeiro enquanto estiver SEGURANDO)
        if (Input.GetMouseButton(1))
        {
            estaDefendendo = true;

            // Ativa o modelo 3D do escudo na tela (se você colocou um objeto)
            if (objetoEscudo != null) objetoEscudo.SetActive(true);
        }
        else
        {
            estaDefendendo = false;

            // Esconde o escudo quando soltar o botão
            if (objetoEscudo != null) objetoEscudo.SetActive(false);
        }
    }
}
