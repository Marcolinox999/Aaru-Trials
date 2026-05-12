using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    public ParticleSystem desiredParticle;

    private void PlayVFX( ParticleSystem desiredParticle)
    {
        desiredParticle.Play(this.desiredParticle);
    }
}
