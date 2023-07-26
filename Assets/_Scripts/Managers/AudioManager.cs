using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip onCorrect;
    [SerializeField] private AudioClip onWrong;

    Vector3 camPoint;
    public bool Sound
    {
        get
        {

            return PlayerPrefs.GetInt("Sound", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("Sound", (value) ? 1 : 0);
            UpdateToggles();

        }
    }
   
    public Toggle[] toggles;

    public void ToggleHaptic()
    {
        Sound = !Sound;
    }

    public void OnSelect()
    {
        if (!Sound)
            return;
        AudioSource.PlayClipAtPoint(onCorrect, camPoint);
    }
    public void OnWrong()
    {
        if (!Sound)
            return;
        AudioSource.PlayClipAtPoint(onWrong, camPoint);
    }
  
    void Start()
    {
        camPoint = Camera.main.transform.position;
        UpdateToggles();

    }

    private void UpdateToggles()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.SetIsOnWithoutNotify(Sound);
        }
       
    }
}
