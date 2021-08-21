using System;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using ScriptableObjects.Items.Armour;
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
            if (gameObject.transform.Find("Open").gameObject.activeSelf == false)
            {
                gameObject.transform.Find("Open").gameObject.SetActive(true);
                gameObject.transform.Find("Closed").gameObject.SetActive(false);
            }
            
            if (_inventory.Items.Count == 0)
            {
                gameObject.transform.Find("Item").gameObject.SetActive(false);
            }
            
            var inventoryManager = player.GetComponent<InventoryManager>();


            while (_inventory.Items.Count != 0)
            {
                var item = _inventory.Items[0];
                foreach (var dict in item)
                {
                    inventoryManager.AddItem(dict.Key, dict.Value);
                }
                _inventory.Items.Remove(item);
            }


            if (_inventory.Items.Count == 0)
            {
                gameObject.transform.Find("Item").gameObject.SetActive(false);
            }
        }

        private void ItemRemoved(BaseItem item, int itemCount)
        {
            
        }
    }
}