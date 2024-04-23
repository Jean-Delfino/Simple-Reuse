using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilMathOperations
    {
        public static List<Vector2> CreateBezierOfTwoPoint(Vector3 referencePoint, Vector2 curveStartPoint, Vector2 targetPoint, Vector2 curveEndPoint, int resolution)
        {
            Vector2 magnitudePoint = referencePoint;
            var points = new List<Vector2>();
            
            for (int j = 0; j < resolution; j++)
            {
                float t = j / (resolution - 1f);
                Vector2 a = Vector2.Lerp(curveStartPoint, targetPoint, t);
                Vector2 b = Vector2.Lerp(targetPoint, curveEndPoint, t);
                Vector2 p = Vector2.Lerp(a, b, t);

                if ((p - magnitudePoint).sqrMagnitude > 0.001f)
                {
                    points.Add(p);
                    magnitudePoint = p;
                }
            }

            return points;
        }
        
        public static float CalculateManhattanDistance(Vector2 a, Vector2 b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        public static float CalculateManhattanDistanceInGrid(int xStart, int yStart, int xDestination, int yDestination)
        {
            return CalculateManhattanDistance(new Vector2(xStart, yStart), new Vector2(xDestination, yDestination));
        }

        public static float CalculateHypotenuse(float a, float b)
        {
            return Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
        }
        
        public static float CalculateEuclideanDistance(Vector2 a, Vector2 b)
        {
            return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
        }

        public static float CalculateAtan2BasedOnConfinedDimension(Vector2 position, Vector2 dimension)
        {
            return Mathf.Atan2((position.y / (dimension.y / 2))  - 1, 
                (position.x / (dimension.x / 2)) - 1);
        }

        public static float RoundToClosestLimit(float number, float min, float max)
        {
            return number - min < max - number ? min : max;
        }
        
        public static bool IsPointInsideSquare(float xSquare, float ySquare, float width, float height, float xPoint, float yPoint)
        {
            float halfWidth = width /(float) 2 ;
            float halfHeight = height /(float) 2;
            
            bool insideX = Mathf.Abs(xPoint - xSquare) <= halfWidth;
            bool insideY = Mathf.Abs(yPoint - ySquare) <= halfHeight;

            return insideX && insideY;
        }

        public static void ApplySpringForceLaw(ref Vector3 startObjectPosition, ref Vector3 endObjectPosition, float springConstant, float maxExpansionSpring, float maxTries, float closeEnough = 0.01f)
        {
            for (int i = 0; i < maxTries; i++)
            {
                Vector3 displacement;
                int multiplier = 1;
                if (startObjectPosition.y < endObjectPosition.y)
                {
                    displacement = endObjectPosition - startObjectPosition;
                }
                else
                {
                    displacement = startObjectPosition - endObjectPosition;
                    multiplier = -1;
                }
                
                var distance = Vector3.Distance(startObjectPosition, endObjectPosition);
                var force = (-springConstant * (maxExpansionSpring - distance) / maxExpansionSpring) * displacement.normalized;
                
                if(force.magnitude < closeEnough) return;
                
                startObjectPosition += force * multiplier;
                endObjectPosition -= force * multiplier;
            }
        }

        public static double CalculateCoulombsForce(double ke, float q1, float q2, float distance)
        {
            return ke * ((Mathf.Abs(q1) * Mathf.Abs(q2)) / Mathf.Pow(distance, 2));
        }
        
        public static double CalculateCoulombsForce(double ke, float q1, float q2, Vector3 q1Point, Vector3 q2Point)
        {
            return CalculateCoulombsForce(ke, q1, q2, Vector3.Distance(q1Point, q2Point));
        }
    }
}