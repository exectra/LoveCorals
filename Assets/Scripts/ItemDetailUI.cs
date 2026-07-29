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

        [Header("References")]
        [SerializeField] private GiftGivingManager giftGivingManager;
        [SerializeField] private InventoryDrawer inventoryDrawer;
        [SerializeField] private InventorySystem inventorySystem;

        private GiftData currentGift;

        public void Show(GiftData gift)
        {
            Debug.Log("ItemDetailUI: Show() called");
            currentGift = gift;

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
            if (currentGift == null)
                return;

            if (!giftGivingManager.IsGivingGift)
            {
                Debug.Log("Cannot give gifts outside of dialogue.");
                return;
            }

            giftGivingManager.OnGiftSelected(currentGift);
            Hide();
        }
        public void DiscardSelectedGift()
        {
            if (currentGift == null)
                return;

            bool removed = inventorySystem.RemoveGift(currentGift);

            if (!removed)
                return;

            Debug.Log($"Discarded one {currentGift.displayName}");

            Hide();
        }
    }
}