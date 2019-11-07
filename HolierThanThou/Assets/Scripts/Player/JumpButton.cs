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
        PressButton();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

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
