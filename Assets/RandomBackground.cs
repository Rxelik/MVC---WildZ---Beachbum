using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour
{
    public Sprite[] sprites;
    void Start()
    {
      int bla =  Random.Range(0, 3);

      if (bla >=2)
          GetComponent<Image>().sprite = sprites[0];
      else
          GetComponent<Image>().sprite = sprites[1];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
