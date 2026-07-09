using CoralDating.Inventory;
using CoralDating.Runtime;
using UnityEngine;
using System;
using System.Threading.Tasks;

namespace CoralDating.Gifts
{
    public class GiftGivingManager : MonoBehaviour
    {
        [SerializeField] private GameObject giftMenuPanel;
        [SerializeField] private CoralDating.Inventory.InventorySystem inventorySystem;
        [SerializeField] private Transform contentParent;
        [SerializeField] private GiftButton giftButtonPrefab;
        [SerializeField] private InventoryDrawer inventoryDrawer;

        public bool IsGivingGift { get; private set; } = false;

        private string currentCoralID;

        public event Action OnGiftFinished;

        private TaskCompletionSource<GiftData> giftSelectionTask;

        // 👇 TEMPORARY TEST
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    OpenGiftMenu("brain_coral");
            //}
        }
        public async Task<GiftData> OpenGiftMenu(string coralID)
        {
            currentCoralID = coralID;

            IsGivingGift = true;

            inventoryDrawer.OpenInventory();

            PopulateGiftMenu();

            giftSelectionTask = new TaskCompletionSource<GiftData>();

            return await giftSelectionTask.Task;
        }

        public void OnGiftSelected(GiftData gift)
        {
            inventorySystem.RemoveGift(gift);

            PopulateGiftMenu();

            Debug.Log($"Player gave {gift.displayName} to {currentCoralID}");

            Debug.Log($"{gift.displayName} now has {inventorySystem.GetGiftCount(gift)} remaining.");

            inventoryDrawer.CloseInventory();

            IsGivingGift = false;

            giftSelectionTask?.SetResult(gift);
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

        private void OnEnable()
        {
            if (inventorySystem != null)
            {
                inventorySystem.OnInventoryChanged += PopulateGiftMenu;
            }
        }

        private void OnDisable()
        {
            if (inventorySystem != null)
            {
                inventorySystem.OnInventoryChanged -= PopulateGiftMenu;
            }
        }
    }
}