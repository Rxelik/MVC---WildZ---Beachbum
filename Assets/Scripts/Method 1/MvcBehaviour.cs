using UnityEngine;
using System;
public class MvcBehaviour : MonoBehaviour
{
    private Application app;

    protected Application App
    {
        get { return app ?? (app = FindObjectOfType<Application>()); }
    }
}