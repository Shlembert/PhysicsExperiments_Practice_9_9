using System.Collections.Generic;
using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;
    private int _index = 0;

    public void NextCam()
    {
        foreach (var cam in cameras) cam.SetActive(false);
        if (_index == cameras.Count - 1) _index = 0;
        else _index++;
        cameras[_index].SetActive(true);
    }

    public void PrevCam()
    {
        foreach (var cam in cameras) cam.SetActive(false);
        if (_index == 0) _index = cameras.Count - 1;
        else _index--;
        cameras[_index].SetActive(true);
    }
}
