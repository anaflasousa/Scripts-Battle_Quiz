using UnityEngine;

public class lancadorPocoes : MonoBehaviour 
{
    [Header("Configurações da Poção")]
    [SerializeField] private KeyCode teclaUso = KeyCode.G; 
    [SerializeField] private int quantidadeDePocoes = 50;

    [Header("Arremesso")]
    [SerializeField] private GameObject pocaoPrefab; 
    [SerializeField] private Transform pontoLancamento;
    [SerializeField] private float forcaArremesso = 20f;

    [Header("Configurações de Mira")]
    [SerializeField] private LayerMask layersParaMirar; // Marque as layers do chão/cenário no Inspector
    private Camera cameraPrincipal;

    void Start()
    {
        // Pega a câmera principal do jogo para calcular o raio do mouse
        cameraPrincipal = Camera.main;
    }

    void Update() 
    {
        if (Input.GetKeyDown(teclaUso) && quantidadeDePocoes > 0) 
        {
            ArremessarPocao();
        }
    }

    void ArremessarPocao() 
    {
        if (pocaoPrefab == null || pontoLancamento == null) 
        {
            Debug.LogError("LancadorPocoes: Referências ausentes no Inspector!");
            return;
        }

        quantidadeDePocoes--; 

        // 1. Instancia a poção na posição do ponto de lançamento
        GameObject pocao = Instantiate(pocaoPrefab, pontoLancamento.position, pontoLancamento.rotation); 

        Rigidbody rb = pocao.GetComponent<Rigidbody>(); 
        if (rb != null) 
        {
            // 2. Calcula a direção da mira usando Raycast a partir do mouse
            Vector3 direcaoLancamento = CalcularDirecaoDaMira();

            // 3. Aplica a força física na direção calculada
            rb.AddForce(direcaoLancamento * forcaArremesso, ForceMode.Impulse);
        } 
        else 
        {
            Debug.LogError("LancadorPocoes: O prefab da poção precisa ter um Rigidbody!");
        }

        Debug.Log("Poção lançada! Restam: " + quantidadeDePocoes);
    }

    Vector3 CalcularDirecaoDaMira()
    {
        // Cria um raio que sai da câmera passando pela posição atual do mouse na tela
        Ray raio = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Se o raio atingir o chão ou qualquer obstáculo configurado
        if (Physics.Raycast(raio, out hit, 100f, layersParaMirar))
        {
            // Calcula o vetor que vai do ponto de lançamento até onde o mouse bateu no cenário
            Vector3 direcao = (hit.point - pontoLancamento.position).normalized;
            
            // Opcional: Descomente a linha abaixo se quiser que ela vá reta sem inclinar para cima/baixo
            // direcao.y = 0; direcao.Normalize();

            return direcao;
        }

        // Caso o mouse esteja apontando para o "vazio" (fim do mapa), joga para a frente do personagem
        return pontoLancamento.forward;
    }
}
