using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class SoundEmitter : MonoBehaviour
{
    public MusicTimelineController timelineController;
    public ENAudioTrack audioTrack;

    public GameObject prefabToSpawn;
    Vector3 spawnPosition = new Vector3(0f, 0f, 0f);

    public Material trueStateMaterial;
    public Material falseStateMaterial;

    private MeshRenderer meshRenderer;

    public FrequencyBandAnalyser frequencyBandAnalyser;
    private GameObject spawnedPrefab;

    private Collider myCollider;
    private bool toggleState = false;
    private HashSet<Collider> overlappingColliders = new HashSet<Collider>();


    void Start()
    {
        myCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (myCollider == null)
        {
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!overlappingColliders.Contains(other))
        {
            overlappingColliders.Add(other);
            SetToggleState(!toggleState);
            spawnPosition = other.transform.position;
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

        if (toggleState)
        {
            Quaternion spawnRotation = Quaternion.identity;

            if (spawnedPrefab == null)
            {
                spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
                FFTSetMaterialColour fftSetMaterialColour = spawnedPrefab.GetComponent<FFTSetMaterialColour>();
                if (fftSetMaterialColour != null)
                {
                    fftSetMaterialColour._FFT = frequencyBandAnalyser;
                }
            }
            if (meshRenderer != null)
            {
                meshRenderer.material = trueStateMaterial;
            }
        }
        else
        {
            if (spawnedPrefab != null)
            {
                Destroy(spawnedPrefab);
                spawnedPrefab = null;
            }
            if (meshRenderer != null)
            {
                meshRenderer.material = falseStateMaterial;
            }
        }

        if (timelineController != null)
        {
            timelineController.RequestTrackState(audioTrack, toggleState);
        }
        

    }
}
