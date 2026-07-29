using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoralDating.Gifts
{
    [CreateAssetMenu(
    fileName = "Gift",
    menuName = "Coral Dating/Gifts/Gift")]
    public class GiftData : ScriptableObject
    {
        [Header("Identification")]
        public string giftID;

        public string displayName;

        [Header("Visuals")]
        public Sprite icon;

        [TextArea]
        public string description;

        [Header("Classification")]
        public GiftCategory category;
    }
}
