using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [Header("Upgrade Type")]
    public UpgradeType upgradeType;

    [Header("Upgrade Settings")]
    public string upgradeName = "Upgrade";
    public float baseCost = 100f;
    public float costMultiplier = 2.5f; // Cost increases by this each level (should be > 2)
    public float baseIncome = 1f; // Income at level 1
    public float incomeMultiplier = 1.1f; // Each level multiplies previous level's income

    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI incomeText; // Shows "+$5.50/sec"
    public TextMeshProUGUI capWarningText;
    public UnityEngine.UI.Button buyButton;

    public int currentLevel = 0;
    private float currentCost;
    private int buttonIndex = -1;

    public enum UpgradeType
    {
        Equipment,
        Stuff,
        Popularity,
        Space
    }

    private void Start()
    {
        GameManager.Instance.RegisterButton(this);

        UpgradeButton[] allButtons = FindObjectsOfType<UpgradeButton>();
        for (int i = 0; i < allButtons.Length; i++)
        {
            if (allButtons[i] == this)
            {
                buttonIndex = i;
                break;
            }
        }

        if (buttonIndex >= 0 && PlayerPrefs.HasKey("Button_" + buttonIndex + "_Level"))
        {
            currentLevel = PlayerPrefs.GetInt("Button_" + buttonIndex + "_Level");
        }

        RefreshDisplay();
        buyButton.onClick.AddListener(TryPurchase);
    }
    void updateUI()
    {
        levelText.text = currentLevel.ToString();
        costText.text = currentCost.ToString();

    }

    private void Update()
    {
        bool canAfford = GameManager.Instance.CanAfford(currentCost);
        buyButton.interactable = canAfford;
        UpdateCapWarning();
        updateUI();
    }

    // Calculate income for THIS button based on its levels
    public float GetCurrentIncome()
    {
        if (currentLevel == 0) return 0f;

        // Calculate how many levels actually count
        int effectiveLevels = GetEffectiveLevels();

        // Sum up income for each level
        // Level 1: baseIncome
        // Level 2: baseIncome * multiplier
        // Level 3: baseIncome * multiplier^2
        // Total = baseIncome * (1 + multiplier + multiplier^2 + ... + multiplier^(n-1))

        float totalIncome = 0f;
        for (int i = 0; i < effectiveLevels; i++)
        {
            totalIncome += baseIncome * Mathf.Pow(incomeMultiplier, i);
        }

        return totalIncome;
    }

    // Get how many levels actually count (considering caps)
    private int GetEffectiveLevels()
    {
        if (upgradeType == UpgradeType.Equipment || upgradeType == UpgradeType.Space)
        {
            // No cap
            return currentLevel;
        }
        else if (upgradeType == UpgradeType.Stuff)
        {
            // Limited by popularity
            int cap = GameManager.Instance.GetStuffCap();
            int total = GameManager.Instance.GetTotalStuffLevels();

            // Calculate levels before this button
            int levelsBefore = 0;
            foreach (UpgradeButton btn in FindObjectsOfType<UpgradeButton>())
            {
                if (btn.upgradeType == UpgradeType.Stuff && btn != this && btn.buttonIndex < buttonIndex)
                {
                    levelsBefore += btn.currentLevel;
                }
            }

            int available = Mathf.Max(0, cap - levelsBefore);
            return Mathf.Min(currentLevel, available);
        }
        else if (upgradeType == UpgradeType.Popularity)
        {
            // Limited by space
            int cap = GameManager.Instance.GetPopularityCap();
            int total = GameManager.Instance.GetTotalPopularityLevels();

            int levelsBefore = 0;
            foreach (UpgradeButton btn in FindObjectsOfType<UpgradeButton>())
            {
                if (btn.upgradeType == UpgradeType.Popularity && btn != this && btn.buttonIndex < buttonIndex)
                {
                    levelsBefore += btn.currentLevel;
                }
            }

            int available = Mathf.Max(0, cap - levelsBefore);
            return Mathf.Min(currentLevel, available);
        }

        return currentLevel;
    }

    public void TryPurchase()
    {
        if (GameManager.Instance.SpendMoney(currentCost))
        {
            currentLevel++;

            if (upgradeType == UpgradeType.Space)
            {
                GameManager.Instance.spaceLevel++;
            }
            else if (upgradeType == UpgradeType.Popularity)
            {
                GameManager.Instance.popularityLevel++;
            }

            RefreshDisplay();
            Debug.Log("Purchased " + upgradeName + " - Level " + currentLevel);
        }
    }

    public void RefreshDisplay()
    {
        currentCost = baseCost * Mathf.Pow(costMultiplier, currentLevel);

        if (nameText != null)
            nameText.text = upgradeName;

        if (costText != null)
            costText.text = "$" + currentCost.ToString("F0");

        if (levelText != null)
            levelText.text = "Lvl " + currentLevel;

        if (incomeText != null)
        {
            float income = GetCurrentIncome();
            incomeText.text = "+$" + income.ToString("F2") + "/sec";
        }
    }

    private void UpdateCapWarning()
    {
        if (capWarningText == null) return;

        capWarningText.text = "";
        int effectiveLevels = GetEffectiveLevels();

        if (upgradeType == UpgradeType.Stuff)
        {
            if (effectiveLevels < currentLevel)
            {
                capWarningText.text = "Limited by Popularity! (" + effectiveLevels + "/" + currentLevel + " active)";
                capWarningText.color = Color.yellow;
            }
        }
        else if (upgradeType == UpgradeType.Popularity)
        {
            if (effectiveLevels < currentLevel)
            {
                capWarningText.text = "Limited by Space! (" + effectiveLevels + "/" + currentLevel + " active)";
                capWarningText.color = Color.yellow;
            }
        }
    }
}