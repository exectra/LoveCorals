using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoralDating.Gifts;
using CoralDating.Runtime;

namespace CoralDating.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField]
        private List<InventoryEntry> inventory = new();

        public event Action<GiftData, int> OnGiftAdded;

        public event Action<GiftData, int> OnGiftRemoved;

        public event Action OnInventoryChanged;

        public bool AddGift(GiftData gift, int amount = 1)
        {
            // Validate input
            if (gift == null || amount <= 0)
            {
                Debug.LogWarning("Attempted to add an invalid gift.");
                return false;
            }

            // Check if the gift already exists
            InventoryEntry entry = FindEntry(gift);

            if (entry != null)
            {
                // Increase the existing quantity
                entry.Add(amount);
            }
            else
            {
                // Create a new inventory entry
                inventory.Add(new InventoryEntry(gift, amount));
            }

            // Notify listeners
            OnGiftAdded?.Invoke(gift, amount);
            OnInventoryChanged?.Invoke();

            return true;
        }

        public bool RemoveGift(GiftData gift, int amount = 1)
        {
            // Validate input
            if (gift == null || amount <= 0)
            {
                Debug.LogWarning("Attempted to remove an invalid gift.");
                return false;
            }

            // Find the inventory entry
            InventoryEntry entry = FindEntry(gift);

            if (entry == null)
            {
                return false;
            }

            // Try to remove the requested amount
            if (!entry.Remove(amount))
            {
                return false;
            }

            // Remove empty entries
            if (entry.Quantity <= 0)
            {
                inventory.Remove(entry);
            }

            // Notify listeners
            OnGiftRemoved?.Invoke(gift, amount);
            OnInventoryChanged?.Invoke();

            return true;
        }

        public bool HasGift(GiftData gift)
        {
            InventoryEntry entry = FindEntry(gift);

            return entry != null && entry.Quantity > 0;
        }

        public bool HasGift(string giftID)
        {
            if (string.IsNullOrWhiteSpace(giftID))
                return false;

            foreach (InventoryEntry entry in inventory)
            {
                if (entry.Gift.giftID == giftID)
                {
                    return entry.Quantity > 0;
                }
            }

            return false;
        }

        public int GetGiftCount(GiftData gift)
        {
            InventoryEntry entry = FindEntry(gift);

            return entry != null ? entry.Quantity : 0;
        }

        public int GetGiftCount(string giftID)
        {
            if (string.IsNullOrWhiteSpace(giftID))
                return 0;

            foreach (InventoryEntry entry in inventory)
            {
                if (entry.Gift.giftID == giftID)
                {
                    return entry.Quantity;
                }
            }

            return 0;
        }

        public IReadOnlyList<InventoryEntry> GetAllGifts()
        {
            return inventory;
        }

        public void Clear()
        {
            inventory.Clear();

            OnInventoryChanged?.Invoke();
        }

        private InventoryEntry FindEntry(GiftData gift)
        {
            if (gift == null)
                return null;

            foreach (InventoryEntry entry in inventory)
            {
                if (entry.Gift == gift)
                {
                    return entry;
                }
            }

            return null;
        }

        [ContextMenu("Test Add Gift")]
        private void TestAddGift()
        {
            Debug.Log("Assign a GiftData in the Inspector before testing.");
        }
    }
}
