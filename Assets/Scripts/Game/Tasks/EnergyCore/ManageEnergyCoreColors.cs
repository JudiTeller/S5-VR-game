using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.EnergyCore
{
    public class ManageEnergyCoreColors : MonoBehaviour
    {
        // stores the gameObjects of the cores
        [SerializeField] private GameObject energyCoreRed;
        [SerializeField] private GameObject energyCoreBlue;
        [SerializeField] private GameObject energyCoreGreen;
        [SerializeField] private GameObject energyCoreYellow;
        [SerializeField] private GameObject energyCoreMagenta;
        [SerializeField] private GameObject energyCoreCyan;
        [SerializeField] private GameObject energyCoreEmpty;
        
        // stores the gameObjects of the cells
        [SerializeField] private GameObject energyCell1;
        [SerializeField] private GameObject energyCell2;
        [SerializeField] private GameObject energyCell3;
        [SerializeField] private GameObject energyCell4;
        [SerializeField] private GameObject energyCell5;
        [SerializeField] private GameObject energyCell6;
        
        /// <summary>
        /// sets the colors of the energy cores and
        /// sets the color of the energy cells to a random one of the list
        /// </summary>
        private void Start()
        {
            //sets the color of the cores
            SetColor(energyCoreRed, Color.red);
            SetColor(energyCoreBlue, Color.blue);
            SetColor(energyCoreYellow, Color.yellow);
            SetColor(energyCoreGreen, Color.green);
            SetColor(energyCoreMagenta, Color.magenta);
            SetColor(energyCoreCyan, Color.cyan);
            
            SetColor(energyCoreEmpty, Color.white);
            
            // adds all used colors to a list
            var colors = new List<Color>
            {
                Color.red,
                Color.green,
                Color.blue,
                Color.yellow,
                Color.cyan,
                Color.magenta,
            };

            //sets the color of the cells
            SetColor(energyCell1, SelectRandomColor(colors));
            SetColor(energyCell2, SelectRandomColor(colors));
            SetColor(energyCell3, SelectRandomColor(colors));
            SetColor(energyCell4, SelectRandomColor(colors));
            SetColor(energyCell5, SelectRandomColor(colors));
            SetColor(energyCell6, SelectRandomColor(colors));
        }
        
        /// <summary>
        /// sets the color of an object
        /// </summary>
        /// <param name="target"></param>
        /// <param name="newColor"></param>
        private static void SetColor(GameObject target, Color newColor)
        {
            var material = target.GetComponent<Renderer>().material;
            material.color = newColor;
        }

        /// <summary>
        /// returns a random color of the used list
        /// </summary>
        /// <param name="possibleColors"></param>
        /// <returns></returns>
        private static Color SelectRandomColor(IList<Color> possibleColors)
        {
            var randomIndex = Random.Range(0, possibleColors.Count);

            var selectedColor = possibleColors[randomIndex];
            
            possibleColors.RemoveAt(randomIndex);

            return selectedColor;
        }

        /// <summary>
        /// returns the color of the gameObject
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Color GetColor(GameObject target)
        {
            return target.GetComponent<Renderer>().material.color;
        }
    }
}