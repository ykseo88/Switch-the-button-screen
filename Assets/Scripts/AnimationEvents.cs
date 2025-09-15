using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject onTargetObj;
    [SerializeField] private ParticleSystem particle;
    
    public UIManager uiManager;

    private void OnObj()
    {
        onTargetObj.SetActive(true);
    }

    private void PlayParticle()
    {
        ParticleSystem tempParticle = Instantiate(particle, onTargetObj.transform.position, onTargetObj.transform.rotation);
        tempParticle.Play();
        uiManager.OnShake();
        uiManager.soundManager.OnSound(uiManager.hammerslam);
    }
}
