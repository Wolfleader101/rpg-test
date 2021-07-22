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

      public bool AddItem(BaseItem item)
      {
         if (!_items.ContainsKey(item))
         {
            if (_items.Count >= maxCapacity) return false;
            _items.Add(item, 1);
            OnItemAdded?.Invoke (item, _items[item]);
            return true;

         }

         if (_items[item] >= item.MaxStackSize) return false;
         ++_items[item];
         OnItemAdded?.Invoke (item, _items[item]);
         return true;

      }
   }
}
