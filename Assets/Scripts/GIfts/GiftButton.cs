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

        public void Setup(
            GiftData giftData,
            int amount,
            GiftGivingManager manager)
        {
            gift = giftData;
            giftGivingManager = manager;

            icon.sprite = gift.icon;
            giftName.text = gift.displayName;
            quantity.text = $"x{amount}";

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            if (!giftGivingManager.IsGivingGift)
            {
                Debug.Log("Inventory is only for viewing.");
                return;
            }

            giftGivingManager.OnGiftSelected(gift);
        }
    }
}