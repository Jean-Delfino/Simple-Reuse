using System;
using System.Collections.Generic;
using System.Linq;
using Reuse.Collections;
using UnityEngine;

namespace Reuse.Grid
{
    public class InfinityGrid<T>
    { 
        private T[,] _values;
        
        public (int x, int y) GetGridSize()
        {
            if (_values == null) throw new NullReferenceException();
            
            return (_values.GetLength(1), _values.GetLength(0));
        }
        
        // Constructor to initialize the structure with dimensions and default values
        public InfinityGrid(int sizeX, int sizeY, T defaultValue)
        {
            _values = new T[sizeY, sizeX];
            
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    _values[y, x] = defaultValue;
                }
            }
        }

        public T GetValue(int x, int y)
        {
            if (CheckIfOutOfBounds(x, y, _values)) throw new IndexOutOfRangeException();

            return _values[y, x];
        }

        public T GetValue(int x, int y, T defaultValue)
        {
            if (CheckIfOutOfBounds(x, y, _values)) return defaultValue;

            return _values[y, x];
        }
        
        public void SetValue(T[,] values, int x, int y, T value)
        {
            if(CheckIfOutOfBounds(x, y, values)) throw new IndexOutOfRangeException();
            
            values[y, x] = value;
        }
        
        public void SetValue(int x, int y, T value)
        {
            if(CheckIfOutOfBounds(x, y, _values)) throw new IndexOutOfRangeException();
            
            _values[y, x] = value;
        }

        public bool CheckIfOutOfBoundY(T[,] values, int y)
        {
            return y < 0 || y >= values.GetLength(0);
        }
        
        public bool CheckIfOutOfBoundsX(T[,] values, int x)
        {
            return x < 0 || x >= values.GetLength(1); 
        }

        public bool CheckIfOutOfBounds(int x, int y, T[,] values)
        {
            return CheckIfOutOfBoundY(values, y) || CheckIfOutOfBoundsX(values, x);
        }

        public bool CheckIfInsideBounds(int x, int y, T[,] values)
        {
            return CheckIfOutOfBounds(x, y, values);
        }

        public void Resize(int newSizeX, int newSizeY, T defaultValue, int shiftX = 0, int shiftY = 0)
        {
            var oldSizeY = _values.GetLength(0);
            var oldSizeX = _values.GetLength(1);
            
            var newValues = new T[newSizeY, newSizeX];
            
            var shifts = CalculateShift(oldSizeY, oldSizeX, newSizeX, newSizeY, shiftX, shiftY);

            CornerFill(newValues, defaultValue, oldSizeY, oldSizeX, newSizeX, newSizeY, shifts);
            
            int x, y , i = 0, j = 0;

            for (x = shifts.x, i = 0; i < Mathf.Min(oldSizeX, newSizeX) && x < newSizeX; x++, i++)
            {
                for (y = shifts.y, j = 0; j < Mathf.Min(oldSizeY, newSizeY) && y < newSizeY; y++, j++)
                {
                    SetValue(newValues, x, y, _values[j, i]);
                }
            }

            _values = newValues;
        }

        public void SetAll(T value)
        {
            for (int i = 0; i < _values.GetLength(1); i++)
            {
                for (int j = 0; j < _values.GetLength(0); j++)
                {
                    SetValue(i, j, value);
                }
            }
        }

        private void CornerFill(T[,] array,T value, int oldSizeY, int oldSizeX, int newSizeX, int newSizeY, (int x, int y) shifts)
        {
            //X
            FillWith(array, value, 0, 0, shifts.x, newSizeY);
            FillWith(array, value, newSizeX - (newSizeX - oldSizeX - shifts.x), 0, newSizeX, newSizeY);
            //Y
            FillWith(array, value, 0, 0, newSizeX, shifts.y);
            FillWith(array, value, 0, newSizeY - (newSizeY - oldSizeY - shifts.y), newSizeX, newSizeY);
        }
        
        public void FillWith(T[,] array,T value, int startingX, int startingY, int endingX, int endingY)
        {
            for (int x = startingX; x < endingX; x++)
            {
                for (int y = startingY; y < endingY; y++)
                {
                    SetValue(array, x, y, value);
                }
            }
        }
        
        private (int x, int y) CalculateShift(int oldSizeY, int oldSizeX, int newSizeX, int newSizeY, int shiftX, int shiftY)
        {
            if (shiftY < 0 || shiftY > newSizeY - oldSizeY)
            {
                shiftY = 0;
            }

            if (shiftX < 0 || shiftX > newSizeX - oldSizeX)
            {
                shiftX = 0;
            }

            return (shiftX, shiftY);
        }

        public void PrintGrid()
        {
            for (int i = 0; i < _values.GetLength(1); i++)
            {
                for (int j = 0; j < _values.GetLength(0); j++)
                {
                    Debug.Log($"{i} {j} = {_values[i,j]}");
                }
            }
        }

        public void PrintGridOnePrint()
        {
            string printString = "";

            for (int i = 0; i < _values.GetLength(1); i++)
            {
                for (int j = 0; j < _values.GetLength(0); j++)
                {
                    printString += $"({i},{j}) {_values[i, j]} ";
                }

                printString += "\n";
            }

            Debug.Log(printString);
        }

        public void InsertSquareByMiddlePoint(int middlePointX, int middlePointY, int width, int height, T value)
        {
            InsertSquareByEndingPoint(
                middlePointX - (width / 2), 
                middlePointX + (width / 2) - Convert.ToInt16((width) % 2 == 0),
                middlePointY - (height / 2),
                middlePointY + (height / 2) - Convert.ToInt16((height) % 2 == 0), 
                value);
        }

        public void InsertSquareByEndingPoint(int startingPointX, int endingPointX, int startingPointY,
            int endingPointY, T value)
        {
            for (int i = startingPointX; i <= endingPointX; i++)
            {
                for (int j = startingPointY; j <= endingPointY; j++)
                {
                    SetValue(i, j, value);
                }
            }
        }
        
        public bool CheckSquareValuesMiddlePoint(int middlePointX, int middlePointY, int width, int height, T defaultValue, Func<T, bool> compareMethod)
        {
            return CheckSquareValuesEndingPoint(
                middlePointX - (width / 2),
                middlePointX + (width / 2) - Convert.ToInt16((width) % 2 == 0),
                middlePointY - (height / 2),
                middlePointY + (height / 2) - Convert.ToInt16((height) % 2 == 0), 
                defaultValue, 
                compareMethod);
        }
        
        public bool CheckSquareValuesEndingPoint(int startingPointX, int endingPointX, int startingPointY,
            int endingPointY, T defaultValue, Func<T, bool> compareMethod)
        {
            for (int i = startingPointX; i <= endingPointX; i++)
            {
                for (int j = startingPointY; j <= endingPointY; j++)
                {
                    if (!compareMethod.Invoke(GetValue(i, j, defaultValue))) return false;
                }
            }

            return true;
        }
        
        public (int x, int y) CheckHowOutOfBoundsIs(int x, int y)
        {
            return (CheckHowOutOfBoundsIsByAxis(x, 0), CheckHowOutOfBoundsIsByAxis(y, 1));
        }

        private int CheckHowOutOfBoundsIsByAxis(int value, int axis)
        {
            if (value < 0) return value * -1;
            var len = _values.GetLength(axis);
            if (value > len - 1) return value - len + 1;

            return 0;
        }

        public (int negativeShiftX, int positiveShiftX, int negativeShiftY, int positiveShiftY) CalculateAllOutOfOutsOffSquare(int middlePointX, int middlePointY, int width, int height)
        {
            (int negativeShiftX, int positiveShiftX, int negativeShiftY, int positiveShiftY) bounds;

            bounds.negativeShiftX = CheckHowOutOfBoundsIsByAxis(middlePointX - (width / 2), 1);
            bounds.positiveShiftX = CheckHowOutOfBoundsIsByAxis(middlePointX + (width / 2) - Convert.ToInt16((width) % 2 == 0), 1);
            bounds.negativeShiftY = CheckHowOutOfBoundsIsByAxis(middlePointY - (height / 2), 0);
            bounds.positiveShiftY = CheckHowOutOfBoundsIsByAxis(middlePointY + (height / 2) - Convert.ToInt16((height) % 2 == 0), 0);

            return bounds;
        }

        public (int x, int y)[] FindValidPointsSidesOnly(int x, int y)
        {
            var points = new (int x, int y)[4];

            points[0] = (x + 1, y);
            points[1] = (x - 1, y);
            points[2] = (x, y + 1);
            points[3] = (x, y - 1);
            
            return points;
        }

        public (int x, int y)[] FindValidPointsWithDiagonals(int x, int y)
        {
            var points = new (int x, int y)[8];
            var count = 0;
            
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    points[count] = (x + i, y + j);
                    count++;
                }
            }

            return points;
        }

        public (int minSize, (int x, int y)[] points) SimpleAStarTravel(int xDestination, 
            int yDestination, 
            int xStart, 
            int yStart,
            Func<int, int, (int x, int y)[]> pointExplorationWay, 
            Func<int, int, T[,], bool> isPointValid,
            Func<int, int, int, int, bool> isValidEndingPoint,
            Func<int, int, int, int, float> distanceMethod,
            bool leavesAfterFinding = true
            )
        {
            //Again, I wanted a PriorityQueue, but this will work
            SortedTupleBag<float, List<(int x, int y)>> travelPoint = new()
            {
                { distanceMethod.Invoke(xStart, yStart, xDestination, yDestination), new () { (xStart, yStart) } }
            };

            HashSet<(int x, int y)> travelledPoint = new();

            var minDistance = int.MaxValue;
            var possibleToFind = false;
            (int x, int y)[] points = null;

            while (travelPoint.Count > 0)
            {
                var firstElement = travelPoint.ElementAt(0);
                var actualPointList = firstElement.Item2;
                var actualPoint = actualPointList[^1];
                travelPoint.Remove(firstElement);
                
                if(actualPointList.Count > minDistance) continue;

                if (isValidEndingPoint.Invoke(actualPoint.x, actualPoint.y, xDestination, yDestination))
                {
                    if (actualPointList.Count < minDistance)
                    {
                        possibleToFind = true;
                        minDistance = actualPointList.Count;
                        points = actualPointList.ToArray();
                        
                        if(leavesAfterFinding) break;
                    }
                }

                foreach (var point in pointExplorationWay.Invoke(actualPoint.x, actualPoint.y))
                {
                    if (isPointValid.Invoke(point.x, point.y, _values) || travelledPoint.Contains((point.x, point.y))) continue;

                    travelPoint.Add(distanceMethod.Invoke(point.x, point.y, xDestination, yDestination), 
                        new List<(int x, int y)>(actualPointList) { (point.x, point.y) });
                }

                travelledPoint.Add(actualPoint);
            }

            return possibleToFind ? (minDistance, points) : (-1, null);
        }
    }
}