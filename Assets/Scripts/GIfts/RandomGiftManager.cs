using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoralDating.Inventory;

namespace CoralDating.Gifts
{
    public class RandomGiftManager : MonoBehaviour
    {
        [Header("Gift Database")]
        [SerializeField] private List<GiftData> availableGifts = new();
        [SerializeField] private CoralDating.Inventory.InventorySystem inventorySystem;

        [Header("Settings")]
        [SerializeField] private float minGiftInterval;
        [SerializeField] private float maxGiftInterval;

        [SerializeField] private GiftNotificationUI notificationUI;

        private void Start()
        {
            StartCoroutine(GiftRoutine());
        }

        private IEnumerator GiftRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(
                    Random.Range(minGiftInterval, maxGiftInterval));

                GiveRandomGift();
            }
        }

        private void GiveRandomGift()
        {
            if (availableGifts.Count == 0)
                return;

            GiftData gift = availableGifts[Random.Range(0, availableGifts.Count)];

            inventorySystem.AddGift(gift);

            notificationUI.ShowGift(gift);

            Debug.Log($"Received {gift.displayName}");
        }
    }
}