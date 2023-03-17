using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggertoButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            button.onClick.Invoke();
        }
    }
}
