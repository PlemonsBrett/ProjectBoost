using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    #region PARAMETERS

    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] private AudioClip crashSFX;
    [SerializeField] private AudioClip successSFX;

    # endregion

    #region CACHE

    // CACHE - e.g. references for readability or speed
    AudioSource audioSource;

    #endregion

    #region STATE

    // STATE - private instance (member) variables
    private bool isTransitioning = false;

    #endregion

    #region Event Functions

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    #endregion

    private void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
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
        isTransitioning = false;
    }

    private void StartCrashSequence()
    {
        // TODO: Add particle effects
        isTransitioning = true;
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        // TODO: Add particle effects
        isTransitioning = true;
        audioSource.PlayOneShot(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }
}