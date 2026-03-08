using System.Collections;
using UnityEngine;
[RequireComponent (typeof(FloatBehaviour))]

public class PiranaPlant : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5;
    private FloatBehaviour _fB;
    private bool _emerged = false;
    private float _targetPosition;
    private void Awake()
    {
        _fB = GetComponent<FloatBehaviour>();
        _targetPosition = transform.position.y;
    }
    private void Start()
    {
        InvokeRepeating("AppearDissapear", 5, 8);
    }
    private void Update()
    {
        if(_emerged && transform.position.y <= _targetPosition)
        {
            _fB.FloatTo(Vector3.up);
        }else if(!_emerged && transform.position.y >= _targetPosition)
        {
            _fB.FloatTo(Vector3.down);
        }
        if (_emerged)
            _fB.RotateTowards(GameManager.gm.player.transform.position);
    }
    private void AppearDissapear()
    {
        GameManager.gm.PlayAudio(SoundManager.AudioClips.Tube);
        if (_emerged)
        {
            _emerged = false;
            _targetPosition -= moveDistance;
        }
        else
        {
            _emerged = true;
            _targetPosition += moveDistance;
            StartCoroutine(PlaySound());
        }
    }
    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameManager.gm.PlayAudio(SoundManager.AudioClips.Piranha);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
