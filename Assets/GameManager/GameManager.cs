using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ============================================
// GAME MANAGER - Handles game state and income
// ============================================
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Money")]
    public float currentMoney = 10f;

    [Header("Cap Settings")]
    public int spaceLevel = 0;
    public int popularityLevel = 0;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI incomeText;

    private List<UpgradeButton> allUpgradeButtons = new List<UpgradeButton>();
    private float incomeTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        allUpgradeButtons.AddRange(FindObjectsOfType<UpgradeButton>());
        UpdateUI();
    }

    private void Update()
    {
        incomeTimer += Time.deltaTime;
        if (incomeTimer >= 1f)
        {
            float income = CalculateTotalIncome();
            currentMoney += income;
            UpdateUI();
            incomeTimer = 0f;
            SaveGame();
        }
    }

    // Calculate total income per second (SUM of all upgrades)
    public float CalculateTotalIncome()
    {
        float totalIncome = 0f;

        foreach (UpgradeButton button in allUpgradeButtons)
        {
            totalIncome += button.GetCurrentIncome();
        }

        return totalIncome;
    }

    // Each popularity level allows 5 stuff levels to count
    public int GetStuffCap()
    {
        return popularityLevel * 5;
    }

    // Each space level allows 5 popularity levels to count
    public int GetPopularityCap()
    {
        return spaceLevel * 5;
    }

    // Get total stuff levels across all stuff buttons
    public int GetTotalStuffLevels()
    {
        int total = 0;
        foreach (UpgradeButton btn in allUpgradeButtons)
        {
            if (btn.upgradeType == UpgradeButton.UpgradeType.Stuff)
                total += btn.currentLevel;
        }
        return total;
    }

    // Get total popularity levels across all popularity buttons
    public int GetTotalPopularityLevels()
    {
        int total = 0;
        foreach (UpgradeButton btn in allUpgradeButtons)
        {
            if (btn.upgradeType == UpgradeButton.UpgradeType.Popularity)
                total += btn.currentLevel;
        }
        return total;
    }

    public bool SpendMoney(float amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UpdateUI();
            SaveGame();
            return true;
        }
        return false;
    }

    public bool CanAfford(float amount)
    {
        return currentMoney >= amount;
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "$" + currentMoney.ToString("F2");

        if (incomeText != null)
            incomeText.text = "$" + CalculateTotalIncome().ToString("F2") + "/sec";
    }

    // ============================================
    // SAVE/LOAD SYSTEM
    // ============================================

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("Money", currentMoney);
        PlayerPrefs.SetInt("SpaceLevel", spaceLevel);
        PlayerPrefs.SetInt("PopularityLevel", popularityLevel);

        for (int i = 0; i < allUpgradeButtons.Count; i++)
        {
            PlayerPrefs.SetInt("Button_" + i + "_Level", allUpgradeButtons[i].currentLevel);
        }

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            currentMoney = PlayerPrefs.GetFloat("Money");
            spaceLevel = PlayerPrefs.GetInt("SpaceLevel");
            popularityLevel = PlayerPrefs.GetInt("PopularityLevel");
        }
    }

    public void ResetGame()
    {
        currentMoney = 10f;
        spaceLevel = 0;
        popularityLevel = 0;

        foreach (UpgradeButton button in allUpgradeButtons)
        {
            button.currentLevel = 0;
            button.RefreshDisplay();
        }

        SaveGame();
        UpdateUI();
        Debug.Log("Game Reset!");
    }

    public void RegisterButton(UpgradeButton button)
    {
        if (!allUpgradeButtons.Contains(button))
        {
            allUpgradeButtons.Add(button);
        }
    }
}
