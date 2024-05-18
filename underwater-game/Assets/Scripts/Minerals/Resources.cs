using UnityEngine;

public class Mineral : MonoBehaviour
{
    // Define public variables for different types of resources
    public float wood = 0f;
    public float stone = 0f;
    public float iron = 0f;
    public float gold = 0f;
    public CurrentResources currentResourcesScript;


    public void Start()
    {
        if (currentResourcesScript == null)
            Debug.Log("currentResourcesScript is Null");
    }

    // Store Resources for the player
    public void StoreResources(float woodAmount, float stoneAmount, float ironAmount, float goldAmount)
    {
        wood += woodAmount;
        stone += stoneAmount;
        iron += ironAmount;
        gold += goldAmount;

        Debug.Log("Stored resources: Wood - " + wood + ", Stone - " + stone + ", Iron - " + iron + ", Gold - " + gold);

    }

    // Add resources to the base
    public void AddResources(float woodAmount, float stoneAmount, float ironAmount, float goldAmount)
    {
        wood += woodAmount;
        stone += stoneAmount;
        iron += ironAmount;
        gold += goldAmount;

        Debug.Log("Current resources: Wood - " + wood + ", Stone - " + stone + ", Iron - " + iron + ", Gold - " + gold);

        currentResourcesScript.UpdateText();

    }

    public void ResetResources()
    {
        wood = 0;
        stone = 0;
        iron = 0;
        gold = 0;

    }


    public bool CheckResources(float woodAmount, float stoneAmount, float ironAmount, float goldAmount)
    {

        if (woodAmount <= wood && stoneAmount <= stone && ironAmount <= iron && goldAmount <= gold)
        {
            wood = wood - woodAmount;
            stone = stone - stoneAmount;
            iron = iron - ironAmount;
            gold = gold - goldAmount;


            currentResourcesScript.UpdateText();
            Debug.Log("Current resources: Wood - " + wood + ", Stone - " + stone + ", Iron - " + iron + ", Gold - " + gold);

            return true;
        }

        return false;
    }

}
        
