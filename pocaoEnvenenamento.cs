using UnityEngine;
using System.Collections;

public class ObjetoPocaoEnvenenamento : MonoBehaviour
{
    [Header("Configurações do Veneno")]
    [SerializeField] private float danoPorSegundo = 5f;
    [SerializeField] private float duracaoVeneno = 10f;
    [SerializeField] private float intervaloDano = 1f;

    [Header("Configurações se Errar o Alvo")]
    // ADICIONADO: Tempo em segundos que ela fica no chão antes de sumir se não acertar um Inimigo
    [SerializeField] private float tempoAteSumirNoChao = 4f; 

    private bool jaAtingiu = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (jaAtingiu) return;

        Transform objeto = collision.transform;

        // Sobe na hierarquia para achar o script de Vida caso bata em um colisor filho do Boss
        while (objeto != null)
        {
            if (objeto.CompareTag("Inimigo"))
            {
                Vida vidaBoss = objeto.GetComponent<Vida>();

                if (vidaBoss != null)
                {
                    jaAtingiu = true;

                    // AJUSTADO: Congela a física para a poção invisível não continuar caindo pelo mapa
                    Rigidbody rb = GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero; 
                        rb.isKinematic = true;            
                    }

                    // Desativa o colisor e o visual para dar o efeito de que a poção quebrou/sumiu
                    if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;
                    if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = false;
                    
                    // Desativa o visual de objetos filhos também (caso o modelo 3D tenha sub-objetos)
                    foreach (var r in GetComponentsInChildren<Renderer>()) r.enabled = false;

                    // Inicia o efeito de dano contínuo no Boss
                    StartCoroutine(Envenenar(vidaBoss));
                }
                return;
            }
            objeto = objeto.parent;
        }

        // ADICIONADO: Se bater no chão, parede ou qualquer obstáculo, espera o tempo configurado antes de sumir
        if (!jaAtingiu)
        {
            jaAtingiu = true; // Impede que repita o código se ela quicar no chão

            // Destrói o objeto após os segundos definidos no Inspector
            Destroy(gameObject, tempoAteSumirNoChao);
        }
    }

    IEnumerator Envenenar(Vida vidaBoss)
    {
        float tempoPassado = 0f;

        while (tempoPassado < duracaoVeneno)
        {
            // Se o Boss morrer antes do veneno acabar, encerra a corrotina
            if (vidaBoss == null) break;

            // AJUSTADO: Garante que a matemática do dano por segundo esteja correta se você mudar o intervalo
            vidaBoss.ReceberDano(danoPorSegundo * intervaloDano);
            
            yield return new WaitForSeconds(intervaloDano);
            tempoPassado += intervaloDano;
        }

        // Destrói o objeto da poção definitivamente após acabar o efeito do veneno
        Destroy(gameObject);
    }
}
