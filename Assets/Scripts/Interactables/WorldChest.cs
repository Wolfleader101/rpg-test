using System;
using System.Collections;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using ScriptableObjects.Items.Armour;
using ScriptableObjects.LootTables;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Interactables
{

    public enum ChestType
    {
        ShowUI,
        EmptyChest,
        DropItems,
        
    }
    public class WorldChest : MonoBehaviour
    {
        [SerializeField] private BaseLootTable lootTable;
        [SerializeField] private ChestType chestType = ChestType.EmptyChest;
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

            OpenChestEffect();

            InventoryManager inventoryManager = player.GetComponent<InventoryManager>();

            switch (chestType)
            {
                case ChestType.ShowUI:
                    break;
                case ChestType.EmptyChest:
                    EmptyChest(inventoryManager);
                    EmptyChestEffect();
                    break;
                case ChestType.DropItems:
                    break;
            }
       
            
        }

        private void OpenChestEffect()
        {
            if (gameObject.transform.Find("Open").gameObject.activeSelf == false)
            {
                gameObject.transform.Find("Open").gameObject.SetActive(true);
                gameObject.transform.Find("Closed").gameObject.SetActive(false);
            }
        }
        

        private void EmptyChest(InventoryManager inventoryManager)
        {
            while (_inventory.Items.Count != 0)
            {
                var item = _inventory.Items[0];
                foreach (var dict in item)
                {
                    inventoryManager.AddItem(dict.Key, dict.Value);
                }
                _inventory.Items.Remove(item);
            }
        }
        
        private void EmptyChestEffect()
        {
            if(gameObject.transform.Find("Item").gameObject.activeSelf == false) return;
            if (_inventory.Items.Count == 0)
            {
                StartCoroutine(EmptyChestEffectRoutine());
            }
        }
        
        private IEnumerator EmptyChestEffectRoutine()
        {
            yield return new WaitForSeconds(.2f);
            gameObject.transform.Find("Item").gameObject.SetActive(false);
        }

        private void ItemRemoved(BaseItem item, int itemCount)
        {

        }


    }
}