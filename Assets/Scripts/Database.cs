using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour
{
    [Header("Hat")]
    public Sprite[] hatArr;

    [Header("Hand")]
    public Sprite[] handArr;

    public Sprite[] returnHats()
    {
        return hatArr;
    }

    public Sprite[] returnHands()
    {
        return handArr;
    }
}
