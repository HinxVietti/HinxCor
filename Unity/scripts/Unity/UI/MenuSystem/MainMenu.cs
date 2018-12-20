using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MenuNode<MenuNodeN1>
{
    private UnityEngine.UI.Image image;
    [SerializeField] Color colorOnHover = new Color(0.6f, 0.8f, 0.9f, 1);

    protected override void Start()
    {
        this.image = GetComponent<UnityEngine.UI.Image>();
        this.OnMouseEnter = OnEnter;
        this.OnMouseExit = OnExit;
    }

    private void OnExit()
    {
        image.color = Color.white;
    }

    private void OnEnter()
    {
        image.color = colorOnHover;
    }
}

