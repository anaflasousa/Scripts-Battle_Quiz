using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory Objects/ Create New")]
public class Objects : ScriptableObject
{
  public string itemName;
  public Sprite itemSprite;
}
