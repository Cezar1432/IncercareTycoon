using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menuPanel;
    public Button openMenuButton;
    public Button closeMenuButton;

    [Header("Tab Buttons")]
    public Button equipmentTab;
    public Button stuffTab;
    public Button popularityTab;
    public Button spaceTab;

    [Header("Tab Panels")]
    public GameObject equipmentPanel;
    public GameObject stuffPanel;
    public GameObject popularityPanel;
    public GameObject spacePanel;

    [Header("Tab Colors (Optional)")]
    public Color activeTabColor = Color.white;
    public Color inactiveTabColor = Color.gray;

    public enum Tab
    {
        EQUIPMENT, STUFF, POPULARITY, SPACE
    }

    private List<GameObject> panels;
    private List<Button> tabButtons;

    private void Start()
    {
        // Initialize lists
        panels = new List<GameObject> { equipmentPanel, stuffPanel, popularityPanel, spacePanel };
        tabButtons = new List<Button> { equipmentTab, stuffTab, popularityTab, spaceTab };

        // When buttons are clicked, call these functions
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        equipmentTab.onClick.AddListener(() => ShowTab(Tab.EQUIPMENT));
        stuffTab.onClick.AddListener(() => ShowTab(Tab.STUFF));
        popularityTab.onClick.AddListener(() => ShowTab(Tab.POPULARITY));
        spaceTab.onClick.AddListener(() => ShowTab(Tab.SPACE));

        // Start with menu closed
        menuPanel.SetActive(false);

        // Show the first tab by default
        ShowTab(Tab.EQUIPMENT);
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    public void ShowTab(Tab tab)
    {
        // Hide all panels and reset all tab button colors
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        foreach (Button button in tabButtons)
        {
            button.GetComponent<Image>().color = inactiveTabColor;
        }

        // Show the selected panel and highlight its tab
        switch (tab)
        {
            case Tab.EQUIPMENT:
                equipmentPanel.SetActive(true);
                equipmentTab.GetComponent<Image>().color = activeTabColor;
                break;

            case Tab.STUFF:
                stuffPanel.SetActive(true);
                stuffTab.GetComponent<Image>().color = activeTabColor;
                break;

            case Tab.POPULARITY:
                popularityPanel.SetActive(true);
                popularityTab.GetComponent<Image>().color = activeTabColor;
                break;

            case Tab.SPACE:
                spacePanel.SetActive(true);
                spaceTab.GetComponent<Image>().color = activeTabColor;
                break;
        }
    }
}