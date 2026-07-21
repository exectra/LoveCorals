using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoralDating.Gifts
{
    public class GiftNotificationUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        [SerializeField] private Image giftImage;

        [SerializeField] private TMP_Text giftName;

        public void ShowGift(GiftData gift)
        {
            giftImage.sprite = gift.icon;
            giftName.text = gift.displayName;

            panel.SetActive(true);

            Time.timeScale = 0f;
        }

        public void Close()
        {
            panel.SetActive(false);

            Time.timeScale = 1f;
        }
    }
}