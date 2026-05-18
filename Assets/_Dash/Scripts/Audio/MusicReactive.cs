using UnityEngine;

public class MusicReactive : MonoBehaviour
{
    public float scaleMultiplier = 5f;
    public float baseScale = 1f;
    public bool reactToEmission = true;
    
    private Material mat;
    private Color baseEmission;

    void Start()
    {
        if (reactToEmission)
        {
            var renderer = GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                mat = renderer.material;
                baseEmission = mat.GetColor("_EmissionColor");
            }
        }
    }

    void Update()
    {
        if (AudioManager.Instance == null) return;

        float amp = AudioManager.Instance.GetAverageAmplitude();
        
        // Pulse Scale
        float s = baseScale + amp * scaleMultiplier;
        transform.localScale = new Vector3(transform.localScale.x, s, transform.localScale.z);

        // Pulse Emission
        if (reactToEmission && mat != null)
        {
            mat.SetColor("_EmissionColor", baseEmission * (1f + amp * scaleMultiplier * 2f));
        }
    }
}
