using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Para CORROTINAS
using TMPro;

public class Vida : MonoBehaviour
{
   //Variaveis usadas
   public float vidaMaxima = 10000f;
   private float vidaAtual;

   //Elementos da UI
   public Slider barraVida;
   public TMP_Text textoVida;

   //Cores
   private Renderer[] renderers;
   private Color[] coresOriginais;

   void Start()
   {
       vidaAtual = vidaMaxima;
       //Só mexe na barra se ela existir
       if (barraVida != null)
       {
           barraVida.maxValue = vidaMaxima;
           barraVida.value = vidaAtual;
       }
       //Desce/sobe a barrinha 
       AtualizarUI();

       //Pega as cores
       renderers = GetComponentsInChildren<Renderer>(); //Acha as partes visiveis
       coresOriginais = new Color[renderers.Length]; //Cria a lista para essas partes visiveis e coloca as cores
       for (int i = 0; i < renderers.Length; i++)
        {
           if (renderers[i] != null && renderers[i].material.HasProperty("_Color"))
            {
               coresOriginais[i] = renderers[i].material.color;
            }
        }
   }

   //Diminui a vida após receber dano
   public void ReceberDano(float dano)
   {
       vidaAtual -= dano;
       if (vidaAtual < 0)
       {
           vidaAtual = 0;
       }
       AtualizarUI();

       // Personagem pisca vermelho quando leva dano
       StartCoroutine(PiscarVermelho());

       Debug.Log("Vida de " + gameObject.name + ": " + vidaAtual);

       if (vidaAtual <= 0)
       {
           Debug.Log(gameObject.name + " Morreu!");
           Destroy(gameObject); // Remove do jogo
       }
   }

   IEnumerator PiscarVermelho()
   {
       //Fica Vermelho
       for (int i = 0; i < renderers.Length; i++)
       {
           if (renderers[i] != null && renderers[i].material.HasProperty("_Color"))
           {
               renderers[i].material.color = Color.red;
           }
       }

       //Tempo que fica vermelho
       yield return new WaitForSeconds(0.1f);

       //Volta para o normal
       for (int i = 0; i < renderers.Length; i++)
       {
           if (renderers[i] != null && renderers[i].material.HasProperty("_Color"))
           {
               renderers[i].material.color = coresOriginais[i];
           }
       }
   }

   void AtualizarUI()
   {
       //Confere se a barra de vida existe
       if (barraVida != null)
       {
           barraVida.value = vidaAtual;
       }

       //Confere se o numero de vida existe
       if (textoVida != null)
       {
           // AJUSTADO: Fechando corretamente as tags </color> do TextMeshPro
           textoVida.text = "<color=green>" + Mathf.RoundToInt(vidaAtual) + "</color><color=red>/" + Mathf.RoundToInt(vidaMaxima) + "</color>";
       }
   }

   public void Curar(float quantidadeCura)
    {
        vidaAtual += quantidadeCura;
        
        // Impede que a vida passe do limite máximo
        vidaAtual = Mathf.Clamp(vidaAtual, 0f, vidaMaxima);

        // AJUSTADO: Chama a atualização visual para a barra e o texto mudarem na tela!
        AtualizarUI();

        Debug.Log("Curado! Vida atual: " + vidaAtual);
    }
}
