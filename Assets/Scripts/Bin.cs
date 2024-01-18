using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {
    [SerializeField] List<ItemTag> acceptedItemTypes;

    public bool CanAcceptItem(BeltItem itemToCompare) { return acceptedItemTypes.Contains(itemToCompare.Type); }
}
