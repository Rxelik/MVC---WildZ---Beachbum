using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TPM : MonoBehaviour
{
   public TextMeshProUGUI childText;
   public TextMeshProUGUI paranetText;
    void Update()
    {
        childText.text = paranetText.text;
    }
}
