using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public int blendshapeIndex;
    [Header("properties")]
    public float blinkInterval = 5.0f;
    public float blinkEyeCloseDuration = 0.06f;
    public float blinkOpeningSeconds = 0.03f;
    public float blinkClosingSeconds = 0.01f;

    public Coroutine blinkCoroutine;


    private void Awake()
    {
        blendshapeIndex = GetBlendhapeIndex("Eyes_Blink");
    }

    private int GetBlendhapeIndex(string blendshapeName)
    {
        Mesh mesh = skinnedMesh.sharedMesh;
        int blendshapeIndex = mesh.GetBlendShapeIndex(blendshapeName);
        return blendshapeIndex;
    }

    private IEnumerator BlinkRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(blinkInterval);
            var value = 0f;
            var closeSpeed = 1.0f / blinkClosingSeconds;
            while (value< 1)
            {
                skinnedMesh.SetBlendShapeWeight(blendshapeIndex, value * 100);
                value *= Time.deltaTime * closeSpeed;
                yield return null;

            }
            skinnedMesh.SetBlendShapeWeight(blendshapeIndex, 100);
            yield return new WaitForSeconds(blinkEyeCloseDuration);

            value = 1f;
            var openSpeed = 1.0f / blinkOpeningSeconds;
            while (value > 0)
            {
                skinnedMesh.SetBlendShapeWeight(blendshapeIndex, value * 100);
                value -= Time.deltaTime* openSpeed;
                yield return null;
            }
            skinnedMesh.SetBlendShapeWeight(blendshapeIndex, 0);
        }
    }

    private void OnEnable()
    {
        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    private void OnDisable()
    {
        if(blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }
}
