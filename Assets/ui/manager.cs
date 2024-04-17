
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class manager : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature _fullScreenPassRendererFeature;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject menu;
    int currentSceneIndex;
    public void Start()
    {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    string[] urls = new string[]
    {   "https://t.me/fateX6510",
        "https://t.me/filippgora",
        "https://t.me/+mswwKHfyTKM0MDky",
        "https://t.me/Shishi_kaka",
        "https://t.me/lewdmus",
        "https://t.me/lopata_i_hleb"
    };
    int i;
    public void Option()
    {
    menu.SetActive(false);
        options.SetActive(true);
    }
    public  void Menu()
    {
        options.SetActive(false);
        menu.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Play()
    {
        _fullScreenPassRendererFeature.SetActive(true);
        SceneManager.LoadScene(currentSceneIndex+1);
    }
    void Url()
    {
        Application.OpenURL(urls[i]);
    }
    public void OnButtonClick(Button button)
    {
        string buttonText = button.GetComponentInChildren<Text>().text;
        switch (buttonText)
        {
            case "альберт - @fateX6510 (Программист) (Лид)":
                i = 0;
                break;
            case "Филипп Горафонов - @filippgora (3D-художник) (Зам)":
                i = 1;
                break;
            case "Light_lost_in_darkness - @fire_four_real (https://t.me/fire_four_real) (2D-художник / Геймдизайнер)":
                i = 2;
                break;
            case "Костя Мотыка - @Shishi_kaka (3D-художник)":
                i = 3;
                break;
            case "Богдан - @lewdmus (https://t.me/lewdmus) (Sound-дизайнер)":
                i = 4;
                break;
            case "lopata":
                i = 5;
                break;
            default:
                break;
        }
        Url();
    }
}