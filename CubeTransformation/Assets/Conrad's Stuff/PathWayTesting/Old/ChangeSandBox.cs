using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSandBox : MonoBehaviour
{
    [SerializeField] Material SkyboxArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RenderSettings.skybox = SkyboxArea;
        }
    }
}
