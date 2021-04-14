using UnityEngine;

public class CCameraManager : CSingleton<CCameraManager>
{
    [SerializeField] private Camera ins_Cam;

    private Vector3 _dragOrigin;
    private EmCameraType _eCameraType;


    #region [code] Camera of Values

    // const values.
    private const float _fZoomMax = 45.0f;
    private const float _fZoomMin = 20.0f;

    private const float _fZoomSpeed = 10.0f;
    private const float _fMoveSpeed = 30.0f;

    private float _fDist = 0.0f;

    private float _fCamPosX = 0.0f;
    private float _fCamPosY = 0.0f;
    private float _fCamPosZ = 0.0f;

    private float _fCamRotX = 0.0f;
    private float _fCamRotY = 0.0f;
    private float _fCamRotZ = 0.0f;


    private bool _bBattleCamera = false;


    #endregion


    public void SetCameraView(EmCameraType eCameraType)
    {
        this._eCameraType = eCameraType;
        this._bBattleCamera = false;

        // 초기화.
        this._fCamPosX = 0.0f;
        this._fCamPosY = 0.0f;
        this._fCamPosZ = 0.0f;

        this._fCamRotX = 0.0f;
        this._fCamRotY = 0.0f;
        this._fCamRotZ = 0.0f;


        ins_Cam.fieldOfView = 15.0f;

        switch (_eCameraType)
        {
            case EmCameraType.Start:

                _fCamPosX = 4.59f;
                _fCamPosY = 12.2f;
                _fCamPosZ = -5.47f;

                _fCamRotX = 36.16f;
                _fCamRotY = -33.698f;

                break;

            case EmCameraType.Lobby:
                ins_Cam.clearFlags = CameraClearFlags.SolidColor;

                _fCamPosX =0.0f;
                _fCamPosY = 0.0f;
                _fCamPosZ = 0.0f;

                _fCamRotX = 0.0f;
                _fCamRotY = 0.0f;

                break;

            case EmCameraType.Battle:
                ins_Cam.clearFlags = CameraClearFlags.Skybox;

                SetBattleView();
                _bBattleCamera = true;

                break;
        }

        ins_Cam.transform.localPosition = new Vector3(_fCamPosX, _fCamPosY, _fCamPosZ);
        ins_Cam.transform.rotation = Quaternion.Euler(_fCamRotX, _fCamRotY, _fCamRotZ);
    }


    private void SetBattleView()
    {
        string strScene = CSceneManager.Inst.GetCurSceneName().ToString();

        ins_Cam.fieldOfView = 45.0f;

        switch (strScene)
        {
            case CDataManager.m_strOne:
                _fCamPosX = 8.7f;
                _fCamPosY = 95.0f;
                _fCamPosZ = -100.0f;

                _fCamRotX = 55.0f;


                break;

            case CDataManager.m_strTwo:
                _fCamPosX = -22.0f;
                _fCamPosY = 110.0f;
                _fCamPosZ = -108.0f;

                _fCamRotX = 55.0f;
                break;
        }
    }


    private void FixedUpdate()
    {
        if (_bBattleCamera)
        {
            BattleZoom();
            BattleMove();
        }

    }


    private void BattleZoom()
    {
        _fDist = Input.GetAxis("Mouse ScrollWheel") * -1 * _fZoomSpeed;

        if (_fDist != 0)
        {
            ins_Cam.fieldOfView += _fDist;
        }

        if (ins_Cam.fieldOfView <= _fZoomMin)
        {
            ins_Cam.fieldOfView = _fZoomMin;
        }
        if (ins_Cam.fieldOfView >= _fZoomMax)
        {
            ins_Cam.fieldOfView = _fZoomMax;
        }

    }


    private float _fpanBorderThickness = 400.0f;
    private void BattleMove()
    {
        if (CUIManager.Inst.m_cUITowerBattle == null)
        {
            if (Input.GetMouseButtonDown(0))
                _dragOrigin = ins_Cam.ScreenToViewportPoint(Input.mousePosition);

            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.y >= Screen.height - _fpanBorderThickness)
                {
                    transform.Translate(Vector3.forward * _fMoveSpeed * Time.deltaTime, Space.World);
                }
                if (Input.mousePosition.y <= _fpanBorderThickness)
                {
                    transform.Translate(Vector3.back * _fMoveSpeed * Time.deltaTime, Space.World);
                }
                if (Input.mousePosition.x >= Screen.width - _fpanBorderThickness)
                {
                    transform.Translate(Vector3.right * _fMoveSpeed * Time.deltaTime, Space.World);
                }
                if (Input.mousePosition.x <= _fpanBorderThickness)
                {
                    transform.Translate(Vector3.left * _fMoveSpeed * Time.deltaTime, Space.World);
                }

                Vector3 VecPos = transform.position;

                transform.position = VecPos;
            }

        }

    }



}
