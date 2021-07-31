using ScriptableObjects.Items;
using UnityEngine;

namespace UI
{
    public class InventoryItem : MonoBehaviour
    {
        [HideInInspector] public BaseItem currentItem;
        [HideInInspector] public int itemCount;
    }
}