using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundManager : MonoBehaviour
{
    //public MultiSound[] soundType;
    public MultiSound soundHeartBeat;
    public MultiSound soundBreath;

    public void defaultState()
    {
        soundHeartBeat.iCurrentClip = -1;
        soundBreath.iCurrentClip = -1;
    }
    public void sphereAppear()
    {
        soundHeartBeat.iCurrentClip = 3;
        soundBreath.iCurrentClip = 2;

        soundBreath.volumeLevel = 1;

        StartCoroutine(calmDown());
    }

    public void anxiousWithWater()
    {
        StartCoroutine(anxiousWithW());
    }

    IEnumerator anxiousWithW()
    {
        soundBreath.iCurrentClip = 4;
        soundBreath.volumeLevel = 1;

        soundHeartBeat.iCurrentClip = 2;
        soundBreath.volumeLevel = 0.5f;

        yield return new WaitForSeconds(7);

        soundBreath.iCurrentClip = 1;
        soundBreath.volumeLevel = 0.5f;

        yield return new WaitForSeconds(7);

        defaultState();
        soundBreath.volumeLevel = 1f;
    }


    IEnumerator calmDown()
    {
        yield return new WaitForSeconds(15);

        soundBreath.iCurrentClip = 0;
        soundHeartBeat.iCurrentClip = -1;

        soundHeartBeat.volumeLevel = 1;
        soundBreath.volumeLevel = 0.6f;
    }

    public void enterGate()
    {
        StopAllCoroutines();

        soundHeartBeat.iCurrentClip = 5;
        soundBreath.iCurrentClip = 6;
        soundBreath.volumeLevel = 0.2f;

        StartCoroutine(calmDown());

    }

    public void discoverText()
    {
        StopAllCoroutines();

        soundHeartBeat.iCurrentClip = 6;
        soundBreath.iCurrentClip = 4;
  
    }

    public void morecalmAfterText()
    {
        soundHeartBeat.iCurrentClip = 4;
        soundBreath.iCurrentClip = 2;
    }

    public void calmDownAfterText()
    {
        soundHeartBeat.iCurrentClip = 2;
        soundBreath.iCurrentClip = 0;

        soundBreath.volumeLevel = 0.1f;
    }
}
