using UnityEngine;
using TMPro;

public class CurrentResources : MonoBehaviour
{
    // Reference to the TowerCost script
    public Mineral ResourcesScript;

    // Reference to the TextMeshPro object
    public TextMeshProUGUI costText;

    void Start()
    {
        // Ensure towerCostScript reference is set
        if (ResourcesScript == null)
        {
            Debug.LogError("Resources script reference not set!");
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

    public void UpdateText()
    {
        // Update TextMeshPro text to display tower cost
        costText.text = ("Wood: " + ResourcesScript.currentWood + "            Stone: " + ResourcesScript.currentStone + "            Iron: " + ResourcesScript.currentIron + "            Gold: " + ResourcesScript.currentGold);
    }
}
