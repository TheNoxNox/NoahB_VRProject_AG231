using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door;

    public GameObject question;

    private bool _buttonPressed = false;
    private bool _keyPlaced = false;

    public void ButtonPressed()
    {
        _buttonPressed = true;

        if (_keyPlaced)
        {
            PoseQuestion();
        }
    }

    public void KeyPlaced()
    {
        _keyPlaced = true;

        if (_buttonPressed)
        {
            PoseQuestion();
        }
    }

    public void PoseQuestion()
    {
        question.SetActive(true);
    }

    public void QuestionAnswered()
    {
        question.SetActive(false);
        MoveDoor();
    }

    public void MoveDoor()
    {
        Destroy(door);
    }
}
