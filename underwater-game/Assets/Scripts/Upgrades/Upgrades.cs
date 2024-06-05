using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public Canvas upgradesCanvas;
    public Canvas PlayerUpgrade;
    public Canvas MageUpgrade;
    public Canvas ArcherUpgrade;
    public Mineral currentResources;
    public AudioSource audioSource;

    public Stats playerStats;

    // Enum for different stat types
    public enum StatType { PlayerHealth, PlayerSpeed, MageHealth, MageAttackSpeed, MageDamage, ArcherHealth, ArcherAttackSpeed, ArcherDamage }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllCanvases();
    }

    public void OpenUpgradesWindow()
    {
        if (AnyCanvasActive())
        {
            DeactivateAllCanvases();
        }
        else
        {
            if (upgradesCanvas != null)
            {
                upgradesCanvas.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Upgrades Canvas is not assigned.");
            }
        }
    }

    private bool AnyCanvasActive()
    {
        return (upgradesCanvas != null && upgradesCanvas.gameObject.activeSelf) ||
               (PlayerUpgrade != null && PlayerUpgrade.gameObject.activeSelf) ||
               (MageUpgrade != null && MageUpgrade.gameObject.activeSelf) ||
               (ArcherUpgrade != null && ArcherUpgrade.gameObject.activeSelf);
    }

    private void DeactivateAllCanvases()
    {
        if (upgradesCanvas != null)
        {
            upgradesCanvas.gameObject.SetActive(false);
        }
        if (PlayerUpgrade != null)
        {
            PlayerUpgrade.gameObject.SetActive(false);
        }
        if (MageUpgrade != null)
        {
            MageUpgrade.gameObject.SetActive(false);
        }
        if (ArcherUpgrade != null)
        {
            ArcherUpgrade.gameObject.SetActive(false);
        }
    }

    public void OpenPlayerWindow()
    {
        ToggleCanvas(upgradesCanvas);
        ToggleCanvas(PlayerUpgrade);
    }

    public void OpenArcherWindow()
    {
        ToggleCanvas(upgradesCanvas);
        ToggleCanvas(ArcherUpgrade);
    }

    public void OpenMageWindow()
    {
        ToggleCanvas(upgradesCanvas);
        ToggleCanvas(MageUpgrade);
    }

    private void ToggleCanvas(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("Canvas is not assigned.");
        }
    }

    public void UpgradeStat(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.PlayerHealth:
                if(currentResources.CheckResources(0f, 20f, 0f, 0f)){
                    playerStats.UpgradePlayerHealth(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.PlayerSpeed:
                if(currentResources.CheckResources(0f, 0f, 0f, 20f)){
                    playerStats.UpgradePlayerSpeed(amount);
                    audioSource.Play();
                }
                    
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.MageHealth:
                if (currentResources.CheckResources(0f, 20f, 0f, 0f)){
                    playerStats.UpgradeMageHealth(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.MageAttackSpeed:
                if (currentResources.CheckResources(0f, 0f, 0f, 20f)){
                    playerStats.UpgradeMageAttackSpeed(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.MageDamage:
                if (currentResources.CheckResources(0f, 0f, 30f, 0f)){
                    playerStats.UpgradeMageDamage(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.ArcherHealth:
                if (currentResources.CheckResources(0f, 20f, 0f, 0f)){
                    playerStats.UpgradeArcherHealth(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.ArcherAttackSpeed:
                if (currentResources.CheckResources(0f, 0f, 0f, 20f)){
                    playerStats.UpgradeArcherAttackSpeed(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            case StatType.ArcherDamage:
                if (currentResources.CheckResources(0f, 0f, 30f, 0f)){
                    playerStats.UpgradeArcherDamage(amount);
                    audioSource.Play();
                }
                else
                    Debug.Log("Not enough resources to upgrade.");

                break;
            default:
                Debug.LogWarning("Unknown stat type.");
                break;
        }
    }

    // Wrapper methods for UI buttons
    public void UpgradePlayerHealth()
    {
        UpgradeStat(StatType.PlayerHealth, 1f);
    }

    public void UpgradePlayerSpeed()
    {
        UpgradeStat(StatType.PlayerSpeed, 1f);
    }

    public void UpgradeMageHealth()
    {
        UpgradeStat(StatType.MageHealth, 1f);
    }

    public void UpgradeMageAttackSpeed()
    {
        UpgradeStat(StatType.MageAttackSpeed, 0.1f);
    }

    public void UpgradeMageDamage()
    {
        UpgradeStat(StatType.MageDamage, 50f);
    }

    public void UpgradeArcherHealth()
    {
        UpgradeStat(StatType.ArcherHealth, 1f);
    }

    public void UpgradeArcherAttackSpeed()
    {
        UpgradeStat(StatType.ArcherAttackSpeed, 0.1f);
    }

    public void UpgradeArcherDamage()
    {
        UpgradeStat(StatType.ArcherDamage, 0.2f);
    }
}
