using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("Configurações do Inventário")]
    public Objects[] slots;
    public Image[] imagensDosSlots;
    public int[] quantidadeNosSlots;

    [Header("Mão do Personagem")]
    public Transform pontoDaMao; // Arraste o osso/objeto da mão do personagem aqui
    private GameObject itemEquipadoAtual; // Armazena o objeto 3D atualmente na mão
    private int indiceSlotEquipado = -1; // Guarda o índice do slot equipado (-1 = nenhum)

    private InterfaceController controladorInterface;

    [Header("Configuração de Raycast")]
    public LayerMask camadaDoRaycast; // Layer para ignorar a armadura/corpo do jogador

    void Start()
    {
        controladorInterface = FindAnyObjectByType<InterfaceController>();
    }

    void Update()
    {
        ProcessarRaycast();
        ProcessarTeclasAtalho();
    }

    void ProcessarRaycast()
    {
        Ray raio = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, 10f, camadaDoRaycast))
        {
            if (hit.collider.CompareTag("Object"))
            {
                ObjectType tipoObjeto = hit.transform.GetComponent<ObjectType>();
                if (tipoObjeto != null && tipoObjeto.objectType != null)
                {
                    controladorInterface.itemText.text = "Aperte E para pegar o item: " + tipoObjeto.objectType.itemName;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        for (int i = 0; i < slots.Length; i++)
                        {
                            if (slots[i] == null || slots[i] == tipoObjeto.objectType)
                            {
                                slots[i] = tipoObjeto.objectType;
                                quantidadeNosSlots[i]++;
                                imagensDosSlots[i].sprite = slots[i].itemSprite;

                                Destroy(hit.transform.gameObject);
                                controladorInterface.itemText.text = "";
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                controladorInterface.itemText.text = "";
            }
        }
        else
        {
            controladorInterface.itemText.text = "";
        }
    }

    // Seleciona e equipa o item correspondente ao pressionar as teclas numéricas (1 a 9)
    void ProcessarTeclasAtalho()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Aceita tanto a fileira superior quanto o Numpad
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
            {
                EquiparItem(i);
                break;
            }
        }
    }

    public void EquiparItem(int indice)
    {
        if (indice < 0 || indice >= slots.Length) return;

        Debug.Log("Tentou equipar o slot: " + indice);

        if (indiceSlotEquipado == indice)
        {
            Debug.Log("Desequipando item porque já estava selecionado.");
            DesequiparItem();
            return;
        }

        if (slots[indice] == null)
        {
            Debug.Log("Slot " + indice + " está vazio!");
            DesequiparItem();
            return;
        }

        DesequiparItem();

        Objects itemParaEquipar = slots[indice];

        if (itemParaEquipar.itemPrefab == null)
        {
            Debug.LogError("O item " + itemParaEquipar.itemName + " NÃO tem um Item Prefab atribuído no ScriptableObject!");
            return;
        }

        if (pontoDaMao == null)
        {
            Debug.LogError("O campo Ponto Da Mao no InventoryController está VAZIO no Inspector!");
            return;
        }

        itemEquipadoAtual = Instantiate(itemParaEquipar.itemPrefab, pontoDaMao);
        itemEquipadoAtual.transform.localPosition = Vector3.zero;
        itemEquipadoAtual.transform.localRotation = Quaternion.identity;

        Collider colisor = itemEquipadoAtual.GetComponent<Collider>();
        if (colisor != null) colisor.enabled = false;

        Rigidbody corpoRigido = itemEquipadoAtual.GetComponent<Rigidbody>();
        if (corpoRigido != null) corpoRigido.isKinematic = true;

        indiceSlotEquipado = indice;
        Debug.Log("Item " + itemParaEquipar.itemName + " equipado com sucesso na mão!");
    }

    public void DesequiparItem()
    {
        if (itemEquipadoAtual != null)
        {
            Destroy(itemEquipadoAtual);
        }
        indiceSlotEquipado = -1;
    }
}