using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOrOffPanelFade : MonoBehaviour
{
    public GameObject panel;
    private void Awake()
    {
        panel.SetActive(true);
        
    }
}
