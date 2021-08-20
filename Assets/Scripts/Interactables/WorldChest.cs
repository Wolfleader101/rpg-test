using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using UnityEngine;

namespace Interactables
{
    public class WorldChest : MonoBehaviour, IInteractable
    {
        private Inventory _inventory;

        private void Start()
        {
            _inventory = ScriptableObject.CreateInstance<Inventory>();

            GenerateItems();
        }

        private void GenerateItems()
        {
            // generate items in inventory based on chest rarity
        }
    }
}