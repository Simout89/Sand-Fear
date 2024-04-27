using UnityEngine;

interface ICollectable
{
    void Collect();
    void Put();

    public Vector3 PlayerScale { get; set; }
    public Vector3 PlayerPos { get; set; }
    public Vector3 PlayerRottation { get; set; }

    public Vector3 ShelfPos { get; set; }
    public Vector3 ShelfScale { get; set; }
}