using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector4 = UnityEngine.Vector4;

public class MaterialController : MonoBehaviour
{
    public Material Material;
    public Vector2 Pos;
    public float Scale;
    public float Angle;

    private Vector2 _smoothPos;
    private float _smoothScale;
    private float _smoothAngle;

    void FixedUpdate()
    {
        UpdateShader();
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Scale *= 0.99f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Scale *= 1.01f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Angle -= 0.01f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Angle += 0.01f;
        }

        Vector2 dir = new Vector2(0.01f * Scale, 0);
        float s = Mathf.Sin(Angle);
        float c = Mathf.Cos(Angle);
        dir = new Vector2(dir.x * c, dir.x * s);

        if (Input.GetKey(KeyCode.A))
        {
            Pos -= dir;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Pos += dir;
        }

        dir = new Vector2(-dir.y, dir.x);
        if (Input.GetKey(KeyCode.S))
        {
            Pos -= dir;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Pos += dir;
        }
    }

    private void UpdateShader()
    {
        _smoothPos = Vector2.Lerp(_smoothPos, Pos, 0.03f);
        _smoothScale = Mathf.Lerp(_smoothScale, Scale, .03f);
        _smoothAngle = Mathf.Lerp(_smoothAngle, Angle, .03f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float scaleX = _smoothScale;
        float scaleY = _smoothScale;

        if (aspectRatio > 1.0f)
        {
            scaleY /= aspectRatio;
        }
        else
        {
            scaleX *= aspectRatio;
        }

        Material.SetVector("_Area", new Vector4(_smoothPos.x, _smoothPos.y, scaleX, scaleY));
        Material.SetFloat("_Angle", _smoothAngle);
    }
}
