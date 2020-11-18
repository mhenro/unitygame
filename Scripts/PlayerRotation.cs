using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Joystick joystick;

    private float exposure = 0f;
    private float minExp = 0.4f;
    private float maxExp = 0.6f;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1f);
        RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(minExp, maxExp, exposure));
        exposure += 0.1f * Time.deltaTime;
        if (exposure > 1)
        {
            float tmpExp = maxExp;
            maxExp = minExp;
            minExp = tmpExp;
            exposure = 0;
        }
    }
}
