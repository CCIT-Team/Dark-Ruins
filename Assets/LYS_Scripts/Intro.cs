using System;
using System.Collections;
using System.Collections.Generic;
using CutSceneEngine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField]
    float _introWait=3;


    [Header("Player Settings")]
    [SerializeField] GameObject _player;           // 플레이어 오브젝트
    [SerializeField] PlayerController_KSM _cont;   // 플레이어 컨트롤러
    [SerializeField] float _playerMoveDist = 5f;    // 플레이어 이동 거리
    [SerializeField] float _playerMoveTime = 2f;    // 플레이어 이동 시간

    [Header("Camera Settings")]
    [SerializeField] Transform _mainCamera;        // 움직일 카메라 (Main Camera)
    [SerializeField] float _camMoveDist = 3f;       // 카메라 이동 거리
    [SerializeField] float _camMoveTime = 2f;       // 카메라 이동 시간
    [SerializeField] float _clampMin=-20;
    [SerializeField] float _clampMax=10;

    [Header("Fade Settings")]
    [SerializeField] Image _fadeImage;
    [SerializeField] float _fadeDuration = 2f;
    [SerializeField] float _fadeDelay = 6f;

    [SerializeField]
    string _sceneName;
    [SerializeField]
    float _rotation;

    CutSceneInterpreter interp;

    void load(TextAsset asset)
    {
        interp.LoadCutSceneScript(asset.text);
    }

    void Start()
    {
        interp = gameObject.GetOrAddComponent<CutSceneInterpreter>();
        StartCoroutine(IntroSeq());
    }


    IEnumerator IntroSeq()
    {
        
        Managers_YGU.Resource.LoadAsync<TextAsset>("Intro1",load);
        yield return new WaitForSeconds(_introWait);
        var routine = StartCoroutine(interp.CoStartCutScene(null));

        yield return routine;

        routine = StartCoroutine(StartIntro());

        yield return routine;

        interp.InitInterpreter(true);

        Managers_YGU.Resource.LoadAsync<TextAsset>("Intro2",load);
        yield return new WaitForSeconds(2);
        routine = StartCoroutine(interp.CoStartCutScene(() => SceneManager.LoadScene(_sceneName)));
    }
//Managers_YGU.Resource.LoadAsync<TextAsset>("Intro2",loadOuttro);
    IEnumerator StartIntro()
    {
        // 1. 초기 설정 (플레이어 컨트롤 비활성화 및 회전)
        _cont.enabled = false;
        _player.transform.rotation = Quaternion.Euler(0, -180, 0);
        
        

        // 2. 애니메이션 실행 및 이동 시작
        _cont.anim.SetFloat("Speed", 2);

        StartCoroutine(Fade(1,0,false));
        StartCoroutine(MoveTransform(_mainCamera, _camMoveDist, _camMoveTime,_player.transform));

        // 플레이어 이동과 카메라 이동을 동시에 실행
        var r1 = StartCoroutine(MoveTransform(_player.transform, _playerMoveDist, _playerMoveTime,null));
        yield return new WaitForSeconds(_fadeDelay);
        var r2 = StartCoroutine(Fade(0,1,true));

        yield return r1;
        yield return r2;
        // 3. 이동 완료 후 설정
        _cont.anim.SetFloat("Speed", 0);
    }

    // 이동을 담당하는 공통 루틴
    IEnumerator MoveTransform(Transform target, float dist, float time,Transform lookTar)
    {
        if (target == null) yield break;


        float start = target.position.z;
        float targetPos = start + dist;
        float elapsed = 0f;

        while (elapsed < time)
        {
            if(lookTar is not null)
            {
                target.transform.LookAt(lookTar);
                target.transform.Rotate(new Vector3(Mathf.Clamp(target.transform.eulerAngles.x - _rotation,_clampMin,_clampMax),0,0));
            }
            
            var pos = target.position;
            pos.z = Mathf.Lerp(start, targetPos, elapsed / time);
            target.position = pos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
    }

    IEnumerator Fade(float start, float end,bool load)
    {
        float timer = 0f;
        Color tempColor = _fadeImage.color;
        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            tempColor.a = Mathf.Lerp(start, end, timer / _fadeDuration);
            _fadeImage.color = tempColor;
            yield return null;
        }
        _fadeImage.color = tempColor;
        // if(load)
        // {
        //     SceneManager.LoadScene(_sceneName);
        //     _cont.enabled = true;
        // }
        
    }
}

/*


        

*/