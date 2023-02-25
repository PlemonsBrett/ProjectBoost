using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables

    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    #endregion

    #region Event Functions

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    #endregion

    # region Movement

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(Vector3.back);
        }
    }

    private void ApplyRotation(Vector3 vector)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(vector * (rotationThrust * Time.deltaTime));
        rb.freezeRotation = false; // Unfreezing rotation so the physics system can take over
    }

    # endregion
}