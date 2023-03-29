using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathUIBtn : MonoBehaviour
{
    [SerializeField] private HubWorldCart Cart;
    [SerializeField] private Button[] buttons;

    public void ButtonTra()
    {
        
        Cart.GoToTra();
        DeactivateButtons();
    }
    public void ButtonRot()
    {
        Cart.GoToRot();
        DeactivateButtons();
    }
    public void ButtonRef()
    {
        Cart.GoToRef();
        DeactivateButtons();
    }

    private void DeactivateButtons()
    {
        foreach (Button buttons in buttons)
        {
            buttons.interactable = false;
        }
    }
    private void ReactivateButtons()
    {
        for(int x = 0; x < buttons.Length; x++)
        {
            if (LevelTimer.ChambersComplete[x] != true)
            {
                buttons[x].interactable = true;
            }
        }

    }

    private void Awake()
    {
        ReactivateButtons();
    }

















    /*
    public void BtnCmd()
    {
        
            if (ForB == true)
            {
                StartCoroutine(MoveFromPath());
            }
            if (ForB == false)
            {
                StartCoroutine(MoveToPath());
            }
            ForB = !ForB;
        
        
    }

    private void FixedUpdate()
    {
        if (InRotation == true)
        {
            GetComponent<Button>().interactable = false;
        }
        else if (Cart.InMovement == false && InRotation == false && (Cart.ChosenPath == -1 || (DirectionArrows.CurrentCheckpoint == 0 && Cart.ChosenPath == TrackofChoice))) 
        {
            GetComponent<Button>().interactable = true;
        }
        else if (Cart.InMovement || DirectionArrows.CurrentCheckpoint != 0)
        {
            GetComponent<Button>().interactable = false;
        }
        
    }

    IEnumerator MoveFromPath()
    {
        
        Cart.AllAboard(TrackofChoice, 13.5f, 0);
        DirectionArrows.CurrentCheckpoint = -1;
        InRotation = true;
        float StartAngle = Cart.gameObject.transform.rotation.eulerAngles.y;
        float x = 0;
        Vector3 EndAngle = new Vector3(0, 180, 0);
        while (Cart.gameObject.transform.rotation.eulerAngles.y != EndAngle.y)
        {
            Debug.Log(Cart.gameObject.transform.rotation);
            Cart.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Lerp(StartAngle, EndAngle.y, x/10)));
            x += Time.deltaTime;
            yield return null;
        }
        InRotation = false;
    }

    IEnumerator MoveToPath()
    {
        InRotation = true;
        float StartAngle = Cart.gameObject.transform.rotation.eulerAngles.y;
        float x = 0;
        Vector3 EndAngle = new Vector3(0, (Cart.pathCreators[TrackofChoice].gameObject.transform.rotation).eulerAngles.y + 180, 0);
        //Debug.Log(EndAngle);
        float OldY = Cart.gameObject.transform.rotation.eulerAngles.y;
        float NewY = EndAngle.y;
        while (OldY > 360)
        {
            OldY -= 360;
        }
        while(NewY > 360)
        {
            NewY -= 360;
        }

        while (OldY != NewY)
        {
            OldY = Cart.gameObject.transform.rotation.eulerAngles.y;
            while (OldY > 360)
            {
                OldY -= 360;
            }
            //Debug.Log(OldY);
            //Debug.Log(NewY);
            Cart.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Lerp(StartAngle, EndAngle.y, x / 10))); 
            x += Time.deltaTime;
            yield return null;
        }
        InRotation = false;
        Cart.AllAboard(TrackofChoice, 0, 13.5f);
        DirectionArrows.CurrentCheckpoint = 0;
    }
    */

}
