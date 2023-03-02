using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class D_MeniFunctionality : MonoBehaviour
{
    public Animator settingsAnimator;
    public Animator creditsAnimator;
    public int mainGameSceneManagerIndex;

    bool settingsOpen;
    bool creditsOpen;

    public GameObject[] menuItems;

    public Image[] buttons;

    public Color offColour;
    public Color onColour;

    void Start()
    {
        changeSettingsMenu(0);
    }

    public void playGame()
    {
        SceneManager.LoadScene(mainGameSceneManagerIndex);
    }

    public void openSettings()
    {
        if (settingsOpen)
        {
            settingsOpen = false;
            settingsAnimator.SetBool("settingsOpen", false);
        }
        else
        {
            settingsOpen = true;
            settingsAnimator.SetBool("settingsOpen", true);
        }
    }

    public void openCredits()
    {
        if (creditsOpen)
        {
            creditsOpen = false;
            creditsAnimator.SetBool("creditsOpen", false);
        }
        else
        {
            creditsOpen = true;
            creditsAnimator.SetBool("creditsOpen", true);
        }
    }

    public void changeSettingsMenu(int whichMenu)
    {
        //loop through each menu item and btton and set it to off
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItems[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = offColour;
        }

        //then the one selected is on
        menuItems[whichMenu].SetActive(true);
        buttons[whichMenu].color = onColour;
    }
}
