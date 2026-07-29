using CoralDating.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoralDating.Gifts
{
    public class GiftButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI giftName;
        [SerializeField] private TextMeshProUGUI quantity;
        [SerializeField] private Button button;

        private GiftData gift;
        private GiftGivingManager giftGivingManager;
        private ItemDetailsUI detailsUI;

        private void Awake()
        {
            if (detailsUI == null)
            {
                detailsUI = FindFirstObjectByType<ItemDetailsUI>();
            }
        }

        public void Setup(
            GiftData giftData,
            int amount,
            GiftGivingManager manager, 
            ItemDetailsUI detailsUI)
        {
            this.gift = giftData;
            this.giftGivingManager = manager;
            this.detailsUI = detailsUI;

            Debug.Log("Setup called for " + gift.displayName);

            icon.sprite = gift.icon;
            giftName.text = gift.displayName;
            quantity.text = $"x{amount}";

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            //Debug.Log("GiftButton OnClicked");

            //if (detailsUI == null)
            //{
            //    Debug.LogError("detailsUI is NULL!");
            //    return;
            //}

            //Debug.Log("detailsUI found: " + detailsUI.name);

            //detailsUI.Show(gift,this);
            Debug.Log("GiftButton OnClicked");

            Debug.Log("detailsUI reference = " + detailsUI);

            if (detailsUI == null)
            {
                Debug.LogError("detailsUI is NULL!");
                return;
            }

            Debug.Log("Calling Show()...");

            detailsUI.Show(gift, this);

            Debug.Log("Returned from Show()");
        }

        public void UpdateQuantity(int amount)
        {
            quantity.text = $"x{amount}";
        }
    }
}