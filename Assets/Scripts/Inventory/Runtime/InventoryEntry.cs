using System;
using UnityEngine;
using CoralDating.Gifts;

namespace CoralDating.Runtime
{
    [Serializable]
    public class InventoryEntry
    {
        [SerializeField]
        private GiftData gift;

        [SerializeField]
        private int quantity;

        public GiftData Gift => gift;
        public int Quantity => quantity;

        public InventoryEntry(GiftData gift, int quantity = 1)
        {
            this.gift = gift;
            this.quantity = Mathf.Max(0, quantity);
        }

        public void Add(int amount)
        {
            quantity += Mathf.Max(0, amount);
        }

        public bool Remove(int amount)
        {
            if (amount <= 0)
                return false;

            if (quantity < amount)
                return false;

            quantity -= amount;
            return true;
        }
    }
}