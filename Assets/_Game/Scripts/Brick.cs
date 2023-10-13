using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]private BrickColor color;

    public BrickColor Color { get => color; set => color = value; }
}
