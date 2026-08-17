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
       coresOriginais = new Color[renderers.Length]; //Cria a lista para essas partes visis=veis e coloca as cores
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
       // Busca o script de escudo no jogador (no próprio objeto, nos filhos ou no pai)
       EscudoPlayer escudo = GetComponent<EscudoPlayer>();
       if (escudo == null) escudo = GetComponentInChildren<EscudoPlayer>();
       if (escudo == null) escudo = GetComponentInParent<EscudoPlayer>();

       // Se o escudo existir e o jogador estiver defendendo, bloqueia o dano
       if (escudo != null && escudo.estaDefendendo)
       {
           Debug.Log("Dano bloqueado");
           return; // O 'return' para a função aqui, impedindo de perder vida ou piscar vermelho
       }

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
           textoVida.text = "<color=green>" + Mathf.RoundToInt(vidaAtual) + "<color=red>/" + Mathf.RoundToInt(vidaMaxima);
       }
   }
}
