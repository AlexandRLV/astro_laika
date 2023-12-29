using UnityEngine;

namespace Imports.SpaceShooterTemplate.Scripts
{
    /// <summary>
    /// This script attaches to ‘Background’ object, and would move it up if the object went down below the viewport border. 
    /// This script is used for creating the effect of infinite movement. 
    /// </summary>

    public class RepeatingBackground : MonoBehaviour
    {
        [Tooltip("vertical size of the sprite in the world space. Attach box collider2D to get the exact size")]
        public float verticalSize;
    
        private void Update()
        {
            if (transform.position.y < -verticalSize) //if sprite goes down below the viewport move the object up above the viewport
            {
                RepositionBackground();
            }
        }

        void RepositionBackground() 
        {
            var groundOffSet = new Vector3(0, verticalSize * 2f, 0f);
            transform.position += groundOffSet;
        }
    }
}
