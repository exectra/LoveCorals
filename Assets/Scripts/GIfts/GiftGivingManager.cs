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
        [SerializeField] private ItemDetailsUI itemDetailsUI;

        public string LastGiftID { get; private set; }
        public bool IsGivingGift { get; private set; } = false;

        private string currentCoralID;

        public event Action OnGiftFinished;

        private TaskCompletionSource<GiftData> giftSelectionTask;

        private void Update()
        {

        }
        public async Task<GiftData> OpenGiftMenu(string coralID)
        {
            currentCoralID = coralID;

            BeginGiftSelection();

            inventoryDrawer.OpenInventory();

            PopulateGiftMenu();

            giftSelectionTask = new TaskCompletionSource<GiftData>();

            return await giftSelectionTask.Task;
        }

        public void OnGiftSelected(GiftData gift)
        {
            LastGiftID = gift.giftID;

            inventorySystem.RemoveGift(gift);

            PopulateGiftMenu();

            EndGiftSelection();

            Debug.Log($"Player gave {gift.displayName} to {currentCoralID}");

            Debug.Log($"{gift.displayName} now has {inventorySystem.GetGiftCount(gift)} remaining.");

            inventoryDrawer.CloseInventory();



            giftSelectionTask?.SetResult(gift);
        }

        private void PopulateGiftMenu()
        {
            Debug.Log("PopulateGiftMenu called");

            Debug.Log("itemDetailsUI = " + itemDetailsUI);

            //Debug.Log($"Inventory reference: {inventorySystem.name}");
            //Debug.Log($"Gift count: {inventorySystem.GetAllGifts().Count}");

            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            foreach (InventoryEntry entry in inventorySystem.GetAllGifts())
            {
                //Debug.Log($"Creating button for {entry.Gift.displayName}");

                GiftButton button = Instantiate(
                    giftButtonPrefab,
                    contentParent);

                button.Setup(
                    entry.Gift,
                    entry.Quantity,
                    this,
                    itemDetailsUI);
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

        private void BeginGiftSelection()
        {
            IsGivingGift = true;
            Debug.Log("Gift selection started");
        }

        private void EndGiftSelection()
        {
            IsGivingGift = false;
            Debug.Log("Gift selection ended");
        }
    }
}