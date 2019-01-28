using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Audio;

public class MainGameAudioController : SoundController
{
    private void Start()
    {
        GetSource("Music").Play("MainTheme");
        GetSource("Ambience").Play("City");
        Bonus.OnPickUpFail += MissTounts;
        Bonus.OnPickUpSuccess += PickupTount;
        GarbageContainer.OnKickFail += MissTounts;
        GarbageContainer.OnKickSuccess += TrashHit;
        GarbageContainer.OnKickSuccess += HitTounts;
        Invoke("MyHomeTounts", 2f);
    }

    private void OnDestroy()
    {
        Bonus.OnPickUpFail -= MissTounts;
        Bonus.OnPickUpSuccess -= PickupTount;
        GarbageContainer.OnKickFail -= MissTounts;
        GarbageContainer.OnKickSuccess -= TrashHit;
        GarbageContainer.OnKickSuccess -= HitTounts;
    }

    public void PickupTount(bool success)
    {
        if(success)
        {
            DrinkTount();
        }
        else
        {
            NiceTounts();
        }
    }

    public void DrinkTount()
    {
        GetSource("DrinkTounts").PlayRandomSound();
    }

    public void Steps()
    {
        GetSource("Steps").PlayRandomSound();
    }
    public void TrashHit()
    {
        GetSource("TrashHit").PlayRandomSound();
    }
    public void BelchTounts()
    {
        GetSource("BelchTounts").PlayRandomSound();
    }

    public void EatTounts()
    {
        GetSource("EatTounts").PlayRandomSound();
    }

    public void HitTounts()
    {
        GetSource("HitTounts").PlayRandomSound();
    }

    public void MissTounts()
    {
        GetSource("MissTounts").PlayRandomSound();
    }

    public void MyHomeTounts()
    {
        GetSource("MyHomeTount").PlayRandomSound();
    }

    public void NiceTounts()
    {
        GetSource("NiceTounts").PlayRandomSound();
    }
    public void StopTounts()
    {
        GetSource("StopTounts").PlayRandomSound();
    }
}
