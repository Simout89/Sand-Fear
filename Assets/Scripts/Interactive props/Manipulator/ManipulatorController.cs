using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class ManipulatorController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Buttons TriangularButtonUp;
    [SerializeField] private Buttons TriangularButtonDown;
    [SerializeField] private Buttons TriangularButtonRight;
    [SerializeField] private Buttons TriangularButtonLeft;
    [SerializeField] private Buttons GripTriangularButtonUp;
    [SerializeField] private Buttons GripTriangularButtonDown;
    [Header("Trigger")]
    [SerializeField] private GripTrigger DownTrigger;
    [SerializeField] private GripTrigger BackTrigger;
    [SerializeField] private GripTrigger LeftTrigger;
    [SerializeField] private GripTrigger RightTrigger;
    [SerializeField] private GripTrigger ForwardTrigger;
    [Header("Parts")]
    [SerializeField] private Transform Arrow;
    [SerializeField] private Transform Ñarriage;
    [SerializeField] private Transform Grip;
    [SerializeField] private Transform Rope;
    [Header("Options")]
    [SerializeField] private float ArrowMaxRotate = 45;
    [SerializeField] private float ArrowSpeed = 1f;
    [SerializeField] private float CarriageDistance = 1;
    [SerializeField] private float CarriageSpeed = 1f;
    [SerializeField] private float GripDistance = 1;
    [SerializeField] private float GripSpeed = 1f;


    private float ArrowAngleValue;
    private float CarriageValue;
    private float GripValue;

    private Vector3 CarriageStartPos;
    private Vector3 GripStartPos;
    private Vector3 RopeStartScale;
    private Vector3 RopeStartPos;



    private void Awake()
    {
        CarriageStartPos = Ñarriage.localPosition;
        GripStartPos = Grip.localPosition;
        RopeStartScale = Rope.localScale;
        RopeStartPos = Rope.localPosition;
    }
    private void Update()
    {
        ArrowButtons();
        GripButtons();

        Arrow.localRotation = Quaternion.Euler(0, Mathf.Clamp(ArrowAngleValue * ArrowMaxRotate, -ArrowMaxRotate, ArrowMaxRotate), 0);
        Ñarriage.localPosition = CarriageStartPos + new Vector3(Mathf.Clamp(CarriageValue * CarriageDistance, 0, CarriageDistance), 0, 0);
        Grip.localPosition = GripStartPos - new Vector3(0, 0, Mathf.Clamp(GripValue * GripDistance, 0, GripDistance));

        Rope.localScale = RopeStartScale + new Vector3(0, 0, Mathf.Clamp(GripValue * GripDistance, 0, GripDistance));
        Rope.localPosition = RopeStartPos - new Vector3(0, 0, Mathf.Clamp(GripValue * GripDistance, 0, GripDistance)) / 2;
    }
    private void GripButtons()
    {
        if (GripTriangularButtonDown.Hold && !DownTrigger.Trigger)
            GripValue = Mathf.Clamp(GripValue + GripSpeed * Time.deltaTime, 0, 1);
        if (GripTriangularButtonUp.Hold)
            GripValue = Mathf.Clamp(GripValue - GripSpeed * Time.deltaTime, 0, 1);
    }

    private void ArrowButtons()
    {
        if (TriangularButtonRight.Hold && !RightTrigger.Trigger)
            ArrowAngleValue = Mathf.Clamp(ArrowAngleValue + ArrowSpeed * Time.deltaTime, -1, 1);
        if (TriangularButtonLeft.Hold && !LeftTrigger.Trigger)
            ArrowAngleValue = Mathf.Clamp(ArrowAngleValue - ArrowSpeed * Time.deltaTime, -1, 1);

        if (TriangularButtonUp.Hold && !ForwardTrigger.Trigger)
            CarriageValue = Mathf.Clamp(CarriageValue + CarriageSpeed * Time.deltaTime, 0, 1);
        if (TriangularButtonDown.Hold && !BackTrigger.Trigger)
            CarriageValue = Mathf.Clamp(CarriageValue - CarriageSpeed * Time.deltaTime, 0, 1);
    }
}
