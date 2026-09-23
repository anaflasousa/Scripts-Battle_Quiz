using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Objects[] slots;
    public Image[] slotImages;
    public int[] slotAmount;

    private InterfaceController interfaceController;

    // Para o raycast para de pegar na armadura do player
    public LayerMask raycastLayer;

    void Start()
    {
        interfaceController = FindAnyObjectByType<InterfaceController>();
    }

    void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        if (Physics.Raycast(ray, out hit, 10f, raycastLayer))
        {
            Debug.Log("Raycast bateu em: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Object"))
            {
                interfaceController.itemText.text = "Aperte E para pegar o item: " + hit.transform.GetComponent<ObjectType>().objectType.name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i] == null || slots[i].name == hit.collider.GetComponent<ObjectType>().objectType.name)
                        {
                            slots[i] = hit.transform.GetComponent<ObjectType>().objectType;
                            slotAmount[i]++;
                            slotImages[i].sprite = slots[i].itemSprite;

                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            } else if(hit.collider.tag != "Object")
            {
                interfaceController.itemText.text = null;
            }
        }
    }
}
