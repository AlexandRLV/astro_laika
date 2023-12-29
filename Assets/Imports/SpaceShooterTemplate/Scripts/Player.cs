using UnityEngine;

namespace Imports.SpaceShooterTemplate.Scripts
{
    public class Player : MonoBehaviour
    {
        public GameObject destructionFX;

        public static Player instance; 

        private void Awake()
        {
            if (instance == null) 
                instance = this;
        }

        //method for damage proceccing by 'Player'
        public void GetDamage(int damage)   
        {
            Destruction();
        }    

        //'Player's' destruction procedure
        void Destruction()
        {
            Instantiate(destructionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
















