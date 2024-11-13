using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particle : MonoBehaviour
{
    public GameObject particleObj;
    private ParticleSystem particle;

    private void OnEnable()
    {
        EventBus.Subscribe("Sword", ParticleCreat);
    }

    void ParticleCreat()
    {
        GameObject go = Instantiate(particleObj);
        particle = go.GetComponent<ParticleSystem>();
        particle.Play();

        go.transform.position = GameManager.Instance.Player.controller.mousePos;

        Destroy(go, 1f);
    }
}
