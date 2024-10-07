using System.Collections;
using Deform;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private GameObject otherCube;

    [SerializeField] private SkewDeformer skewDeformer;
    [SerializeField] private BendDeformer bendDeformer;
    [SerializeField] private CurveDisplaceDeformer curveDeformer;
    [SerializeField] private GameObject cameraHolder;

    private AudioSource crashSound;
    private void Awake() {
        crashSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision target) {
        if(target.gameObject == otherCube){
            crashParticles.Play();
            crashSound.Play();
            OjbectDeformer();
            StartCoroutine(CameraShake(cameraHolder, 0.5f, 0.9f));
        }else{
            //crashParticles.Pause();
        }
        
    }

    // private void PlayAudio(AudioClip audioClip,float volume){
        
    //     AudioSource.PlayClipAtPoint(audioClip, transform.position,volume);
    // }
    private void OjbectDeformer(){
        Deformable deformable = GetComponent<Deformable>();

        skewDeformer.Factor = Random.Range(-1f, 1f);
        bendDeformer.Angle = Random.Range(-60f, 60f);
        curveDeformer.Factor = Random.Range(-2f, 2f);

        deformable.AddDeformer(skewDeformer);
        deformable.AddDeformer(bendDeformer);
        deformable.AddDeformer(curveDeformer);
        
    }


    private IEnumerator CameraShake(GameObject cameraHolder, float duration, float magnitude)

    {
        Vector3 originalPos = cameraHolder.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude + originalPos.x;
            float y = Random.Range(-1f, 1f) * magnitude + originalPos.y;

            cameraHolder.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        cameraHolder.transform.localPosition = originalPos;
    }
}
