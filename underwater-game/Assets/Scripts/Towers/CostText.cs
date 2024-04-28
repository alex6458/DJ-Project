using UnityEngine;
using TMPro;

public class UpdateTowerCostText : MonoBehaviour
{
    // Reference to the TowerCost script
    public TowerCost towerCostScript;

    // Reference to the TextMeshPro object
    public TextMeshProUGUI costText;

    void Start()
    {
        // Ensure towerCostScript reference is set
        if (towerCostScript == null)
        {
            Debug.LogError("TowerCost script reference not set!");
            return;
        }

        // Ensure costText reference is set
        if (costText == null)
        {
            Debug.LogError("TextMeshPro reference not set!");
            return;
        }

        // Update the text initially
        UpdateText();
    }

    void UpdateText()
    {
        // Update TextMeshPro text to display tower cost
        costText.text = ("Wood: " + towerCostScript.woodCost + "\nStone: " + towerCostScript.stoneCost + "\nIron: " + towerCostScript.ironCost + "\nGold: " + towerCostScript.goldCost);
    }
}
