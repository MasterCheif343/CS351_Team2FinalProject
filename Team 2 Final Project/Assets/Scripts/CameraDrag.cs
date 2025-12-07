using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;
    private float zoomSpeed = 10f;
    private float minZoom = 3f;
    private float maxZoom = 20f;
    private float targetZoom;
    public float zoomStep = 1f;
    public float zoomSmoothSpeed = 8f;

    [Header("Camera Movement Limits")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    private void Awake()
    {
        _mainCamera = Camera.main;
        targetZoom = _mainCamera.orthographicSize;

    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _origin = GetMousePosition;
            _isDragging = true;
        }
        else if (ctx.canceled)
        {
            _isDragging = false;
        }
    }
    public void OnZoom(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        float scrollValue = ctx.ReadValue<Vector2>().y;

        // makes so it zooms in steps
        if (scrollValue > 0)
            targetZoom -= zoomStep;
        else if (scrollValue < 0)
            targetZoom += zoomStep;

        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
    }

    private void LateUpdate()
    {
        // for camera drag
        if (_isDragging)
        {
            _difference = GetMousePosition - transform.position;
            transform.position = _origin - _difference;
        }

        // Smoother zoom
        _mainCamera.orthographicSize =
    Mathf.Lerp(_mainCamera.orthographicSize, targetZoom, zoomSmoothSpeed * Time.deltaTime);

        // stops camera movement past a certain point
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());
}
