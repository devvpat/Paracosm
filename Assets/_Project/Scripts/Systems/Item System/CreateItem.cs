using UnityEngine;

namespace ACoolTeam
{
    [CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Items/Create Item")]
    public class CreateItem : ItemObject
    {
        private void Awake()
        {
            if (CoolDown == 0)
            {
                CoolDown = 0.6f;
            }
        }
    }
}
