using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
    public string sceneName = "PlatformerScene";
    public GameObject loadingPanel;

    // If player touchs end goal.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PauseMenu.Instance.isLoading = true;
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

            if (PlayerPrefs.GetInt("Level") > PlayerPrefs.GetInt("Unlock")) // Unlocks current level in main menu.
            {
                PlayerPrefs.SetInt("Unlock", PlayerPrefs.GetInt("Level"));
            }

            if (PlayerPrefs.GetInt("Level") > 11) // Checks if a level can be loaded if not return to main menu.
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
