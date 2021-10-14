using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private  AudioSource audioSource;

    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip death;

    private void OnEnable()
    {
        Bird.OnJump += PlayJumpSound;
        Bird.OnPipeCollision += PlayDeathSound;
    }

    private void OnDisable()
    {
        Bird.OnJump -= PlayJumpSound;
        Bird.OnPipeCollision -= PlayDeathSound;
    }

    private void PlayJumpSound()
    {
        if (jump == null) return;

        audioSource.PlayOneShot(jump);
    }
    
    private void PlayDeathSound()
    {
        if (death == null) return;

        audioSource.PlayOneShot(death);
        death = null;
    }
}
