using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a single flower with nectar
/// </summary>
public class Flower : MonoBehaviour
{
    [Tooltip("The color when the flower is full")]
    public Color fullFlowercolor = new Color(1f, 0f, .3f);

    [Tooltip("The color when the flower is empty")]
    public Color emptyFlowercolor = new Color(.5f, 0f, 1f);

    /// <summary>
    /// The trigger collider representing the nectar
    /// </summary>
    [HideInInspector]
    public Collider nectarCollider;

    // The solid collider representing the flower petals
    private Collider flowerCollider;

    // The flower's material
    private Material flowerMaterial;

    /// <summary>
    /// A vector pointing straight out of the flower
    /// </summary>
    public Vector3 FlowerUpVector
    {
        get
        {
            return flowerCollider.transform.up;
        }
    }

    /// <summary>
    /// The center position of the nectar collider
    /// </summary>
    public Vector3 FlowerCenterPosition
    {
        get
        {
            return flowerCollider.transform.position;
        }
    }

    /// <summary>
    /// The amount of nectar remaining in the flower
    /// </summary>
    public float NectarAmount { get; private set; }

    /// <summary>
    /// Whether the flower has any nectar remaining
    /// </summary>
    public bool HasNectar
    {
        get
        {
            return NectarAmount > 0f;
        }
    }


    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount">The amount of nectar to remove</param>
    /// <returns>The actual amount successfully removed</returns>
    public float Feed(float amount)
    {
        // Track how much nectar was successfully taken (cannot take more than is available)
        float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);

        // Substract the nectar
        NectarAmount -= amount;

        if (NectarAmount <= 0f)
        {
            // No nectar remaining
            NectarAmount = 0f;

            // Disable the flower and nectar colliders
            flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            // Change the flower color to indicate that it is empty
            flowerMaterial.SetColor("_BaseColor", emptyFlowercolor);
        }

        // Return the amount of nectar that was taken
        return nectarTaken;
    }
}
