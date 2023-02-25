using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("OK");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("Picked up fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    private void StartCrashSequence()
    {
        // TODO: Add particle effects
        // TODO: Add SFX upon crash
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void StartNextLevelSequence()
    {
        // TODO: Add particle effects
        // TODO: Add SFX upon crash
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }
}