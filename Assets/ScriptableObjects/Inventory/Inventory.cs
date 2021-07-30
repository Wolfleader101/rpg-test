using System;
using System.Collections.Generic;
using ScriptableObjects.Items;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject
    {
        public event Action<BaseItem, int> OnItemAdded;
        public event Action<BaseItem, int> OnItemRemoved;

        [SerializeField] private int maxCapacity = 36;
        public int MaxCapacity => maxCapacity;

        private Dictionary<BaseItem, int> _items;

        public void Init()
        {
            _items = new Dictionary<BaseItem, int>(maxCapacity);
        }

        public bool AddItem(BaseItem item, int itemCount)
        {
            if (!_items.ContainsKey(item))
            {
                if (_items.Count >= maxCapacity) return false;
                _items.Add(item, itemCount); 
                // going to have issues with the item count being different from the actual UI count
                // really I should be doing item countChecks here
                // and knowing if there are multiple stacks
                OnItemAdded?.Invoke(item, itemCount);
                return true;
            }

            //if (_items[item] >= item.MaxStackSize) return false;
            _items[item] += itemCount;
            OnItemAdded?.Invoke(item, itemCount);
            return true;
        }

        public void RemoveItem(BaseItem item, int count)
        {
            if (!_items.ContainsKey(item)) return;

            if (_items[item] - count <= 0)
            {
                _items.Remove(item);
                OnItemRemoved?.Invoke(item, count);
                return;
            }

            _items[item] -= count;
            OnItemRemoved?.Invoke(item, count);
            return;
        }
    }
}