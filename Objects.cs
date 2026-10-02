using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory Objects/ Create New")]
public class Objects : ScriptableObject
{
  public string itemName; // Nome do item
  public Sprite itemSprite; // Imagem Sprite do item
  public GameObject itemPrefab; // Prefab do item
}
