using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerClickHandler
{
    public bool pressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressButton();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // pressed = true;
        PressButton();
        Debug.Log($"On Pointer Click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // pressed = false;
    }

    private void PressButton()
    {
        pressed = true;
        StartCoroutine(PressDelayCoroutine());
    }

    private IEnumerator PressDelayCoroutine()
    {
        yield return null;
        pressed = false;
    }
}
