using System.Collections;
using UnityEngine;

public class ANIMATOR : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject!");
            return;
        }

        StartCoroutine(CheckAudioAndTransition());
    }

    private IEnumerator CheckAudioAndTransition()
    {
        while (true)
        {
            bool isAudioPlaying = audioSource.isPlaying;
            anim.SetBool("IsTalking", isAudioPlaying);

            // Randomize TalkingIndex ONLY when audio is playing
            if (isAudioPlaying)
            {
                anim.SetInteger("TalkingIndex", Random.Range(0, 5));
                anim.SetFloat("New Float", Random.Range(0.75f, 1.15f));
                //yield return new WaitForSeconds(0.5f);
            }

            yield return null; // Wait for the next frame
        }
    }
}
