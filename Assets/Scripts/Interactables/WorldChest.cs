using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using ScriptableObjects.LootTables;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Interactables
{
    public class WorldChest : MonoBehaviour
    {
        [SerializeField] private BaseLootTable lootTable;
        private Inventory _inventory;

        private void Start()
        {
            _inventory = ScriptableObject.CreateInstance<Inventory>();
            _inventory.Init();
            
            foreach (var tableItem in lootTable.items)
            {
                if(tableItem.Item == null) return;
                
                float randNum = Random.Range(0, 1);
                if (randNum <= tableItem.DropChance)
                {
                    _inventory.AddItem(tableItem.Item, Random.Range(tableItem.MinDrop, tableItem.MaxDrop));
                }
                
            }

            _inventory.OnItemRemoved += ItemRemoved;

        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            var colliderGameObject = collision.gameObject;
            if (colliderGameObject.CompareTag("Player") == false) return;
           
            OpenChest(colliderGameObject);

        }
        
        private void OpenChest(GameObject player)
        {
            var inventoryManager = player.GetComponent<InventoryManager>();
            
            foreach (var item in _inventory.Items)
            {
                foreach (var dict in item)
                {
                    inventoryManager.AddItem(dict.Key, dict.Value);
                }
            }
        }

        private void ItemRemoved(BaseItem item, int itemCount)
        {
            
        }
    }
}