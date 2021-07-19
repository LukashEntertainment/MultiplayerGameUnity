using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 defaultPosition = transform.localPosition;

        float elasped = 0.0f;

        while (elasped < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, defaultPosition.z);

            elasped += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = defaultPosition;
    }
}
