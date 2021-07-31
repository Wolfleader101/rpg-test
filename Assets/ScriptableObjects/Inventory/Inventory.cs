using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<Dictionary<BaseItem, int>> _items;

        public void Init()
        {
            _items = new List<Dictionary<BaseItem, int>>(maxCapacity);
        }

        public bool AddItem(BaseItem item, int itemCount)
        {
            
            if (_items.Find(itemInDict => itemInDict.ContainsKey(item)) == null)
            {
                if (_items.Count >= maxCapacity) return false;
                while (itemCount > item.MaxStackSize)
                {
                    _items.Add(new Dictionary<BaseItem, int>()
                    {
                        {
                            item, item.MaxStackSize
                        }
                    });
                
                    OnItemAdded?.Invoke(item, itemCount);
                    
                    itemCount -= item.MaxStackSize;
                }
                
                _items.Add(new Dictionary<BaseItem, int>()
                {
                    {
                        item, itemCount
                    }
                });

                OnItemAdded?.Invoke(item, itemCount);
                return true;
            }

            foreach (var itemDict in _items.Where(queuedItem => queuedItem.ContainsKey(item)))
            {
                // if its already at max cap
                if (itemDict[item] >= item.MaxStackSize) continue;
                
                int maxIncrement = item.MaxStackSize - itemDict[item];
                int clamped = Mathf.Clamp(itemCount, 1, maxIncrement);
                
                itemDict[item] += clamped;
                OnItemAdded?.Invoke(item, clamped);

                itemCount -= clamped;
                if (itemCount != 0) continue;
                
                return true;

            }

            if (_items.Count >= maxCapacity) return false;
            
            while (itemCount > item.MaxStackSize)
            {
                _items.Add(new Dictionary<BaseItem, int>()
                {
                    {
                        item, item.MaxStackSize
                    }
                });
                
                OnItemAdded?.Invoke(item, itemCount);
                    
                itemCount -= item.MaxStackSize;
            }
                
            _items.Add(new Dictionary<BaseItem, int>()
            {
                {
                    item, itemCount
                }
            });

            OnItemAdded?.Invoke(item, itemCount);
            return true;
        }
        

        // public void RemoveItem(BaseItem item, int count)
        // {
        //     if (!_items.ContainsKey(item)) return;
        //
        //     if (_items[item] - count <= 0)
        //     {
        //         _items.Remove(item);
        //         OnItemRemoved?.Invoke(item, count);
        //         return;
        //     }
        //
        //     _items[item] -= count;
        //     OnItemRemoved?.Invoke(item, count);
        //     return;
        // }
    }
}