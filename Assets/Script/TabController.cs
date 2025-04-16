using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    public int currentTab = 0;
    // Start is called before the first frame update
    void Start()
    {
        ActivateTab(currentTab);
    }

    // Update is called once per frame
    public void ActivateTab(int tabNo)
    {
        currentTab = tabNo;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;


    }
#if UNITY_EDITOR
    void OnValidate()
    {
        if (pages != null && tabImages != null && currentTab < pages.Length)
        {
            ActivateTab(currentTab);
        }
    }
#endif

}
