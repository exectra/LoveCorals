using CoralDating.Inventory;
using UnityEngine;

namespace CoralDating.Gifts
{
    public class GiftGivingManager : MonoBehaviour
    {
        [SerializeField] private GameObject giftMenuPanel;
        [SerializeField] private CoralDating.Inventory.InventorySystem inventorySystem;
        private string currentCoralID;
        public void OpenGiftMenu(string coralID)
        {
            currentCoralID = coralID;

            giftMenuPanel.SetActive(true);

            PopulateGiftMenu();
        }

        public void OnGiftSelected(GiftData gift)
        {
            inventorySystem.RemoveGift(gift);

            Debug.Log($"Gave {gift.displayName} to {currentCoralID}");

            giftMenuPanel.SetActive(false);
        }

        private void PopulateGiftMenu()
        {
            Debug.Log("Populate the UI with inventory gifts here.");
        }

        public void GiveGift(GiftData gift)
        {
            if (inventorySystem.RemoveGift(gift))
            {
                Debug.Log($"Gave {gift.displayName} to {currentCoralID}");

                giftMenuPanel.SetActive(false);

            }
        }
        public void CancelGift()
        {
            giftMenuPanel.SetActive(false);
        }
    }
}