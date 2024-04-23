using System;
using UnityEngine;

namespace Reuse.Grid
{
    public class TestInfinityGrid : MonoBehaviour
    {
        public void Start()
        {
            var grid1 = new InfinityGrid<char>(3, 3, '0');

            //grid1.PrintGrid();
            grid1.InsertSquareByMiddlePoint(1, 1, 3, 3, '1');
            grid1.PrintGrid();
            
            Debug.Log("\n \n \n \n \n \n \n \n \n \n \n");
            
            grid1.Resize(5, 5, '0', 2, 1);
            grid1.PrintGrid();

        }
        

        public bool CheckIfThereIsSomething(char value)
        {
            return value <= '0';
        }
    }
}