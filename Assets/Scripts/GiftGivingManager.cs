using CoralDating.Inventory;
using CoralDating.Runtime;
using UnityEngine;

namespace CoralDating.Gifts
{
    public class GiftGivingManager : MonoBehaviour
    {
        [SerializeField] private GameObject giftMenuPanel;
        [SerializeField] private CoralDating.Inventory.InventorySystem inventorySystem;
        [SerializeField] private Transform contentParent;
        [SerializeField] private GiftButton giftButtonPrefab;

        private string currentCoralID;

        // 👇 TEMPORARY TEST
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                OpenGiftMenu("brain_coral");
            }
        }

        public void OpenGiftMenu(string coralID)
        {
            Debug.Log("Opening Gift Menu");
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
            Debug.Log("PopulateGiftMenu called");

            Debug.Log($"Inventory reference: {inventorySystem.name}");
            Debug.Log($"Gift count: {inventorySystem.GetAllGifts().Count}");


            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            foreach (InventoryEntry entry in inventorySystem.GetAllGifts())
            {
                Debug.Log($"Creating button for {entry.Gift.displayName}");

                GiftButton button = Instantiate(
                    giftButtonPrefab,
                    contentParent);

                button.Setup(
                    entry.Gift,
                    entry.Quantity,
                    this);
            }
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