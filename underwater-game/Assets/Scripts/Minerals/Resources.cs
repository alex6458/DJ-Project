using UnityEngine;

public class Mineral : MonoBehaviour
{
    // Define public variables for different types of resources
    private float wood = 0f;
    private float stone = 0f;
    private float iron = 0f;
    private float gold = 0f;
    public float currentWood = 0f;
    public float currentStone = 0f;
    public float currentIron = 0f;
    public float currentGold = 0f;
    public CurrentResources currentResourcesScript;


    public void Start()
    {
        if (currentResourcesScript == null)
            Debug.Log("currentResourcesScript is Null");
    }


    public void StoreResources(float woodAmount, float stoneAmount, float ironAmount, float goldAmount)
    {
        wood += woodAmount;
        stone += stoneAmount;
        iron += ironAmount;
        gold += goldAmount;

        Debug.Log("Stored resources: Wood - " + wood + ", Stone - " + stone + ", Iron - " + iron + ", Gold - " + gold);

        currentResourcesScript.UpdateText();

    }

    // Example method to add resources
    public void AddResources()
    {
        currentWood += wood;
        currentStone += stone;
        currentIron += iron;
        currentGold += gold;

        wood = 0;
        stone = 0;
        iron = 0;
        gold = 0;

        Debug.Log("Current resources: Wood - " + currentWood + ", Stone - " + currentStone + ", Iron - " + currentIron + ", Gold - " + currentGold);

        currentResourcesScript.UpdateText();

    }


    public bool CheckResources(float woodAmount, float stoneAmount, float ironAmount, float goldAmount)
    {

        if (woodAmount <= currentWood && stoneAmount <= currentStone && ironAmount <= currentIron && goldAmount <= currentGold)
        {
            currentWood = currentWood - woodAmount;
            currentStone = currentStone - stoneAmount;
            currentIron = currentIron - ironAmount;
            currentGold = currentGold - goldAmount;

            currentResourcesScript.UpdateText();

            return true;
        }

        return false;
    }

}
        
