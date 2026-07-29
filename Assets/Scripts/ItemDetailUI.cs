using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CoralDating.Gifts;
using CoralDating.Inventory;

namespace CoralDating.Inventory
{
    public class ItemDetailsUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;

        [Header("Buttons")]
        [SerializeField] private Button useButton;
        [SerializeField] private Button discardButton;
        private GiftButton currentButton;

        [Header("References")]
        [SerializeField] private GiftGivingManager giftGivingManager;
        [SerializeField] private InventoryDrawer inventoryDrawer;
        [SerializeField] private InventorySystem inventorySystem;

        private GiftData currentGift;

        public void Show(GiftData gift, GiftButton button)
        {
            Debug.Log("Show called. IsGivingGift = " + giftGivingManager.IsGivingGift);
            currentGift = gift;

            currentButton = button;

            itemName.text = gift.displayName;
            description.text = gift.description;
            icon.sprite = gift.icon;

            panel.SetActive(true);

            inventoryDrawer.RaiseInventory();

            useButton.interactable = giftGivingManager.IsGivingGift;
            discardButton.interactable = true;
        }

        public void Hide()
        {
            panel.SetActive(false);
            currentGift = null;

            inventoryDrawer.LowerInventory();
        }

        public void UseSelectedGift()
        {
            Debug.Log("UseSelectedGift called");
            if (currentGift == null)
                return;
            Debug.Log("IsGivingGift = " + giftGivingManager.IsGivingGift);
            if (!giftGivingManager.IsGivingGift)
            {
                Debug.Log("Cannot give gifts outside of dialogue.");
                return;
            }

            giftGivingManager.OnGiftSelected(currentGift);
            panel.SetActive(false);
            currentGift = null;
        }
        public void DiscardSelectedGift()
        {
            Debug.Log("DiscardSelectedGift called");
            if (currentGift == null)
            {
                Debug.Log("Current gift is null");
                return;
            }

            bool removed = inventorySystem.RemoveGift(currentGift);

            Debug.Log($"Removed: {removed}");
            Debug.Log($"Remaining: {inventorySystem.GetGiftCount(currentGift)}");

            if (!removed)
                return;

            int remaining = inventorySystem.GetGiftCount(currentGift);

            Debug.Log($"Discarded one {currentGift.displayName}");

            if (remaining <= 0)
            {
                Hide();
                return;
            }

            currentButton.UpdateQuantity(remaining);
        }
    }
}