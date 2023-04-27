using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelect : MonoBehaviour
{
    public Image icon;
    private bool startSelect = false;
    
    
    void Start()
    {
        icon.color = Color.green;
        icon.gameObject.SetActive(false);
    }


    public void StartSelectTarget(Action<Monster> action)
    {
        
    }
    
    void Update()
    {
        if (!startSelect)
        {
            return;
        }
    }
}
