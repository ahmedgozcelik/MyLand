using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] RectTransform joystickOutline;
    [SerializeField] RectTransform joystickButton;
    [SerializeField] float moveFactor;

    private Vector3 tapPosition;
    private Vector3 move;
    private bool canControlJoystick;

    void Start()
    {
        HideJoystick();
    }

    void Update()
    {
        if (canControlJoystick)
        {
            ControlJoystick();
        }
    }
    /// <summary>
    /// Joystick'in alanýna yani ekrana dokununca çaðrýlacak fonksiyon
    /// </summary>
    public void TappedOnJoystickZone()
    {
        tapPosition = Input.mousePosition; // Ekrana dokunulan yerin pozisyonunu al
        joystickOutline.position = tapPosition;
        ShowJoystick();
    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControlJoystick = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControlJoystick = false;
        move = Vector3.zero;
    }
    /// <summary>
    /// Joystick ile karakteri kontrol eden fonksiyon
    /// </summary>
    public void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - tapPosition;

        float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
        float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;

        float joystickOutlineHalfWidth = joystickOutline.rect.width / 2;
        float newWidth = joystickOutlineHalfWidth * canvasYScale;

        moveMagnitude = Mathf.Min(moveMagnitude, newWidth);

        //float moveMagnitude = direction.magnitude * moveFactor / Screen.width; //Hareket ettiðimiz yönün þiddeti = Ýçerdeki topun hareket hýzý
        //moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width/2);

        move = direction.normalized * moveMagnitude;

        Vector3 targetPos = tapPosition + move;

        joystickButton.position = targetPos;

        if (Input.GetMouseButtonUp(0)) //Ekrana bastýðýmýzda çalýþacak kod
        {
            HideJoystick();
        }
    }

    public Vector3 GetMovePosition()
    {
        return move / 1.75f;
    }
}
