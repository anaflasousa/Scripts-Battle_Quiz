using UnityEngine;

public class Escudo : MonoBehaviour
{
    // Se o player nao estiver usando o escudo
    public bool estaDefendendo = false;

    public GameObject Escudo; 

    void Update()
    {
        //  Botão DIREITO do mouse (retorna verdadeiro enquanto estiver SEGURANDO)
        if (Input.GetMouseButton(1))
        {
            estaDefendendo = true;

            // Se o personagem tiver com um escudo, ele começa a funcionar
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
