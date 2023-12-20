using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    [SerializeField] private SkinnedMeshRenderer spriteRenderer;

    private Material _originalMaterial;
    private Coroutine flashRoutine;

    private void Start()
    {
        _originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    
    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = _originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
