using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void XButton_CloseUI()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
