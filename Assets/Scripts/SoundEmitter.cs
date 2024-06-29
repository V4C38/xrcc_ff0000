using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class ColliderOverlapHandler : MonoBehaviour
{
    public TrackMuteController timelineController;
    public ENAudioTrack audioTrack;

    private AudioSource audioSource;
    private Collider myCollider;
    private bool toggleState = false;
    private HashSet<Collider> overlappingColliders = new HashSet<Collider>();


    void Start()
    {
        myCollider = GetComponent<Collider>();
        ConfigureAudioSource();

        if (myCollider == null)
        {
            return;
        }

        if (audioSource == null)
        {
            return;
        }
    }

    void ConfigureAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            return;
        }
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!overlappingColliders.Contains(other))
        {
            overlappingColliders.Add(other);
            SetToggleState(!toggleState);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (overlappingColliders.Contains(other))
        {
            overlappingColliders.Remove(other);
        }
    }

    private void SetToggleState(bool inState)
    {
        if (inState == toggleState)
        {
            return;
        }

        toggleState = inState;

        timelineController.RequestTrackState(audioTrack, toggleState);

    }
}
