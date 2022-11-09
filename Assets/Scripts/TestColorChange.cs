using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColorChange : MonoBehaviour
{
    [SerializeField] private Material m_Material;

    private void Start()
    {
        m_Material.color = Color.black;
    }
}
