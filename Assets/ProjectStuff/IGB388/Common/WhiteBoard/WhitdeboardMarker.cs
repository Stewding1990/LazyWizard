using UnityEngine;
using OculusSampleFramework;
using OVR;
using System.Linq;

public class WhitdeboardMarker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;
    private RaycastHit _touch;
    private WhiteBoard _whiteBoard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;
    private bool isSpraying = false;

    private float timer = 0;
    private float timerCutoff = 2.3f;

    private OVRInput.Controller m_controller = OVRInput.Controller.None;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        _tipHeight = tip.localScale.y * 2;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (OVRInput.GetConnectedControllers() == OVRInput.Controller.None)
        {
            return;
        }

        if (m_controller == OVRInput.Controller.None)
        {
            if (OVRInput.GetConnectedControllers() != OVRInput.Controller.None)
            {
                m_controller = OVRInput.GetConnectedControllers();
            }
        }

        if (isSpraying)
        {
            Draw();
        }

    }

    private void Draw()
    {
        if (Physics.Raycast(tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("WhiteBoard"))
            {
                if (_whiteBoard == null)
                {
                    _whiteBoard = _touch.transform.GetComponent<WhiteBoard>();
                }
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteBoard.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _whiteBoard.textureSize.y - (penSize / 2));

                if (y < 0 || y > _whiteBoard.textureSize.y || x < 0 || x > _whiteBoard.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _whiteBoard.texture.SetPixels(x, y, penSize, penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteBoard.texture.SetPixels(lerpX, lerpY, penSize, penSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;

                    _whiteBoard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }
        _whiteBoard = null;
        _touchedLastFrame = false;
    }

    private void CheckController()
    {
        bool primaryTriggerDown = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller);
        if (primaryTriggerDown)
        {
            isSpraying = true;
        }
        else
        {
            isSpraying = false;
        }
    }
}
