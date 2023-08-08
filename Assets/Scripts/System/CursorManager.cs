using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursorTexture;
    public Texture2D cursorClickedTexture;

    private Vector2 cursorHotspot;

    private CursorController _cursorController;
    
    [Header("Sound Configure")]
    public AudioClip buttonVoiceEffect;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        _cursorController = new CursorController();
        ChangeCursor(defaultCursorTexture);
        Cursor.lockState = CursorLockMode.Confined;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        _cursorController.Mouse.Click.started += _ => StartCursorClicked();
        _cursorController.Mouse.Click.performed += _ => EndCursorClicked();
    }

    private void EndCursorClicked()
    {
        ChangeCursor(defaultCursorTexture);
    }

    private void StartCursorClicked()
    {
        audioSource.PlayOneShot(buttonVoiceEffect);
        ChangeCursor(cursorClickedTexture);
        
    }

    private void OnEnable()
    {
        _cursorController.Enable();
    }

    private void OnDisable()
    {
        _cursorController.Disable();
    }


    private void ChangeCursor(Texture2D texture2D)
    {
        cursorHotspot = new Vector2(texture2D.width / 2, texture2D.height / 2);
        Cursor.SetCursor(texture2D, cursorHotspot, CursorMode.Auto);
    }
}