using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;

public class PortalEnter : MonoBehaviour
{
    [SerializeField]
    Text inputText;
    [SerializeField]
    float wait;

    ProCamera2DTransitionsFX transition;

    private void OnEnable()
    {
        inputText = GameObject.FindGameObjectWithTag("InputText").GetComponent<Text>();
        transition = GameObject.FindObjectOfType<ProCamera2DTransitionsFX>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            inputText.text = ("Y");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (Input.GetButtonDown("YButton"))
            {
                StartCoroutine(Transition());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            inputText.text = (" ");
        }
    }

    IEnumerator Transition()
    {
        transition.TransitionExit();

        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene("EndOfDemo");
    }
}
