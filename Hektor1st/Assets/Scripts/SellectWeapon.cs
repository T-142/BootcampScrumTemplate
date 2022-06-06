using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class SellectWeapon : MonoBehaviour
    {
        public int selectedWeapon,selectedcolor = 1;
        public Material gun;

        private void Start()
        {
            SelectWeapon();
        }
        private void Update()
        {
            
            int lastSelectedWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                {
                    selectedWeapon = 0;
                }
                else
                {
                    selectedWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                if (selectedWeapon <= 0)
                {
                    selectedWeapon = transform.childCount-1;
                }
                else
                {
                    selectedWeapon--;
                }
            }
            if (lastSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
        }
        void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                    gun.color = new Color32(255, 110, 110, 255);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                    gun.color = new Color32(255, 255, 255, 255);
                }
                i++;
            }
        }
    }
        
}