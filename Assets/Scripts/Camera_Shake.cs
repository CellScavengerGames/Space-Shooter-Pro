using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator CamShake(float shakeDuration, float shakeStrength)
    {
        Vector3 originalPosition = transform.position;
        float timeElapsed = 0f;

        while (timeElapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeStrength;
            float y = Random.Range(-1f, 1f) * shakeStrength;

            transform.position = new Vector3(x, y, -10f);
            timeElapsed += Time.deltaTime;
            yield return 0;
        }

        transform.position = originalPosition;
    }
}
