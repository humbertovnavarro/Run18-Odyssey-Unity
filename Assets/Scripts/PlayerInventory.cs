using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kamaii
{
    public class PlayerInventory : MonoBehaviour
    {
        public int slots;
        public Dictionary<ItemID, int> items;
        bool CanAddItem(ItemID item)
        {
            if (!items.ContainsKey(item))
            {
                return true;
            }
            else if (items[item] > Items.GetMaxStack(item))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void AddItem(ItemID item)
        {
            if (CanAddItem(item))
            {
                items[item]++;
            }
        }
        void RemoveItem(ItemID item)
        {
            if (!items.ContainsKey(item))
            {
                items[item]--;
                if (items[item] <= 0)
                {
                    items.Remove(item);
                }
            }
        }
    }
}

