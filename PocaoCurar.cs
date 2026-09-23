using UnityEngine;

public class PocaoCurar : MonoBehaviour
{
    [Header("Configurações da Poção")]
    [SerializeField] private KeyCode teclaUso = KeyCode.Mouse1; // Tecla 'H' para usar
    [SerializeField] private float quantidadeDeCura = 2000f;  // Quanto ela cura
    [SerializeField] private int quantidadeDePocoes = 50;    // Quantas poções o player tem

    [Header("Referências")]
    [SerializeField] private Vida scriptVida; // Referência ao outro script

    void Start()
    {
        // Caso você esqueça de arrastar no Inspector, tenta buscar automaticamente no mesmo GameObject
        if (scriptVida == null)
        {
            scriptVida = GetComponent<Vida>();
        }
    }

    void Update()
    {
        // Detecta o clique na tecla e se ainda possui poções
        if (Input.GetKeyDown(teclaUso) && quantidadeDePocoes > 0)
        {
            UsarPocao();
        }
    }

    void UsarPocao()
    {
        // 1. Consome a poção diminuindo o estoque
        quantidadeDePocoes--;

        // 2. Comunica com o script de vida e aplica a cura
        if (scriptVida != null)
        {
            scriptVida.Curar(quantidadeDeCura);
            Debug.Log("Poção consumida! Restam: " + quantidadeDePocoes);
        }
        else
        {
            Debug.LogError("O script da poção não encontrou a referência do script 'VidaDoJogador'!");
        }
    }
}
