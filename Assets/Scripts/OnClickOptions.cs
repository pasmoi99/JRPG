using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickOptions : MonoBehaviour
{
    // Start is called before the first frame update
  public void OnClickOptions()
  {
            Options.gameObject.SetActive(true);
            Options.alpha = 0;
            Options.DoFadde(1, 0.2f);
  }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Options.gameObject.activeInHierarchy)
        {
            MeshColliderCookingOptions.DOFade(0, 0.2f).OnComplete( () => { Options.gameObject.SetActive(false); });
        }
    }
}
