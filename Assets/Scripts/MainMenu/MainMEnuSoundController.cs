using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Audio;
public class MainMEnuSoundController : SoundController
{
    public void PlayMainTheme()
    {
        GetSource("IntroAmbience").FadeOut(5);
        GetSource("IntroMusic").Play("Intro");
    }
}
