using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Security.Cryptography;

public class SoundEmitter : MonoBehaviour
{
    public ENAudioTrack audioTrack;

    public Material trueStateMaterial;
    public Material falseStateMaterial;

    public GameObject particlePrefab;
    private Vector3 particleSpawnPosition;
    public GameObject particlePrefabWhileActive;
    public GameObject[] particleSystemLocation;
    private List<GameObject> spawnedPS  = new List<GameObject>();

    private MeshRenderer meshRenderer;
    private AudioSource audioPlayer;

    private FrequencyBandAnalyser frequencyBandAnalyser;

    private Collider myCollider;
    private bool toggleState = false;
    private HashSet<Collider> overlappingColliders = new HashSet<Collider>();


    void Start()
    {
        myCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        frequencyBandAnalyser = GetComponent<FrequencyBandAnalyser>();
        audioPlayer = GetComponent<AudioSource>();

        if (myCollider == null)
        {
            return;

        }

    }

    void OnTriggerEnter(Collider other)
    {
        particleSpawnPosition = other.transform.position;
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
        if (particlePrefab != null)
        {
            GameObject particleInstance = Instantiate(particlePrefab, particleSpawnPosition, Quaternion.identity);
            Destroy(particleInstance, 1.25f);
        }

        if (inState == toggleState)
        {
            return;
        }

        toggleState = inState;


        if (toggleState)
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = trueStateMaterial;
            }
            if (audioPlayer != null)
            {
                audioPlayer.mute = false;
            }
            foreach (GameObject psl in particleSystemLocation)
            {
                if (particlePrefabWhileActive == null) { return;}
                GameObject particleInstance = Instantiate(particlePrefabWhileActive, psl.transform.position, psl.transform.rotation, psl.transform.parent);

                // Modify scale of particles depending on scale of car
                Vector3 carGrabberScale = psl.transform.parent.transform.parent.transform.parent.transform.localScale;
                float scaleChange = 0.5f / carGrabberScale.x;
                particleInstance.transform.localScale = new Vector3(particleInstance.transform.localScale.x / scaleChange, particleInstance.transform.localScale.y / scaleChange, particleInstance.transform.localScale.z / scaleChange);

                FFTSetParticleProp FFTParticlePropScript = particleInstance.GetComponent<FFTSetParticleProp>();
                if (FFTParticlePropScript != null)
                {
                    FFTParticlePropScript._FFT = frequencyBandAnalyser;
                    FFTModifyPulse PulseScript = GetComponent<FFTModifyPulse>();
                    if (PulseScript != null)
                    {
                        FFTParticlePropScript._FrequencyBandIndex = PulseScript._FrequencyBandIndex;
                    }
                }
                else
                {
                    print("Particle Prop does not exist");
                }

                spawnedPS.Add(particleInstance);
            }
        }
        else
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = falseStateMaterial;
            }
            if (audioPlayer != null)
            {
                audioPlayer.mute = true;
            }
            foreach (GameObject ps in spawnedPS)
            {
                Destroy(ps, 1.0f);
            }
            spawnedPS.Clear();
        }

    }
}
