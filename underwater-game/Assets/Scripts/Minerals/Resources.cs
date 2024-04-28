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

    // Example method to add resources
    public void AddResources(float woodAmount, float stoneAmount, float ironAmount, float goldAmount)
    {
        wood += woodAmount;
        stone += stoneAmount;
        iron += ironAmount;
        gold += goldAmount;

        Debug.Log("Current resources: Wood - " + wood + ", Stone - " + stone + ", Iron - " + iron + ", Gold - " + gold);

        currentResourcesScript.UpdateText();

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

            return true;
        }

        return false;
    }

}
        