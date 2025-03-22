using UnityEngine;

public class A_DynamicShift : MonoBehaviour
{
    public AudioSource bgmSource;

    public enum PlayerState
    {
        Calm,
        Normal,
        Tense
    }

    public PlayerState currentState = PlayerState.Normal;

    // Define target pitch values for each state
    public float calmPitch = 0.8f;
    public float normalPitch = 1.0f;
    public float tensePitch = 1.2f;

    // How quickly the pitch transitions between states
    public float pitchSmoothSpeed = 2.0f;

    void Update()
    {
        float targetPitch = normalPitch;

        // Choose target pitch based on the player's state
        switch (currentState)
        {
            case PlayerState.Calm:
                targetPitch = calmPitch;
                break;
            case PlayerState.Normal:
                targetPitch = normalPitch;
                break;
            case PlayerState.Tense:
                targetPitch = tensePitch;
                break;
        }

        // Smoothly transition to the target pitch
        bgmSource.pitch = Mathf.Lerp(bgmSource.pitch, targetPitch, Time.deltaTime * pitchSmoothSpeed);
    }
}