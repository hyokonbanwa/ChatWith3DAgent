using System.Collections;
using UnityEngine;
using VRM;

public sealed class AutoBlink : MonoBehaviour
{
    [SerializeField]
    FaceController faceController;

    string currentStringKey;
    [SerializeField, Min(0)]
    float blinkDuration = 0.1f;
    [SerializeField, Min(0)]
    float blinkInterval = 3;
    bool blinking = false;
    WaitForSeconds blinkDurationWait;
    WaitForSeconds blinkIntervalWait;

    float blinkMax = 1.0f;
    public bool IsActive { get; set; } = true;
    void Start()
    {
        blinkDurationWait = new WaitForSeconds(blinkDuration);
        blinkIntervalWait = new WaitForSeconds(blinkInterval);
    }
    void Update()
    {
        currentStringKey = faceController.GetPreset();
        //Debug.Log(currentKey);
        if (!IsActive)
        {
            return;
        }
        Blink();
        blinkInterval = Random.Range(blinkInterval * 0.5f, blinkInterval * 1.5f);
    }

    public void SetBlinkMax(float blinkMax)
    {
        this.blinkMax = blinkMax;
    }
    public void Blink()
    {
        if (blinking)
        {
            return;
        }
        StartCoroutine(nameof(BlinkTimer));
    }
    IEnumerator BlinkTimer()
    {
        blinking = true;
        //float blinkMax = currentStringKey == "Neutral" ? 1f : 0.8f;
        faceController.ApplyFace(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), 0, blinkMax, blinkDuration);
        yield return blinkDurationWait;
        yield return null;
        faceController.ApplyFace(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), blinkMax, 0, blinkDuration);
        yield return blinkDurationWait;
        yield return blinkIntervalWait;
        blinking = false;
    }
}

