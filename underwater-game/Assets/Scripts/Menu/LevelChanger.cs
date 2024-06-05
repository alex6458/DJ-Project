using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    
    public Animator animator;
    private int levelToload;

    public void FadeToLevel()
    {
        levelToload = SceneManager.GetActiveScene().buildIndex + 1;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToload);
    }

}
