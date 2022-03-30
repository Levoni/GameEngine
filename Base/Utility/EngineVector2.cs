using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   [Serializable]
   public class EngineVector2
   {
      public float X { get; set; }
      public float Y { get; set; }

      #region Constructors
      public EngineVector2()
      {
         X = 0;
         Y = 0;
      }

      public EngineVector2(float X, float Y)
      {
         this.X = X;
         this.Y = Y;
      }
      #endregion

      #region Custom Operators
      public static EngineVector2 operator *(EngineVector2 edgeOne, EngineVector2 EdgeTwo)
      {
         return new EngineVector2 { X = edgeOne.X * EdgeTwo.X, Y = edgeOne.Y * EdgeTwo.Y };
      }
      public static EngineVector2 operator *(EngineVector2 edge, int scaler)
      {
         return new EngineVector2 { X = edge.X * scaler, Y = edge.Y * scaler };
      }
      public static EngineVector2 operator *(int scaler, EngineVector2 edge)
      {
         return new EngineVector2 { X = edge.X * scaler, Y = edge.Y * scaler };
      }
      public static EngineVector2 operator *(EngineVector2 edge, float scaler)
      {
         return new EngineVector2 { X = edge.X * scaler, Y = edge.Y * scaler };
      }
      public static EngineVector2 operator /(EngineVector2 edge, float scaler)
      {
         return new EngineVector2 { X = edge.X / scaler, Y = edge.Y / scaler };
      }

      public static EngineVector2 operator +(EngineVector2 edgeOne, EngineVector2 edgeTwo)
      {
         return new EngineVector2 { X = edgeOne.X + edgeTwo.X, Y = edgeOne.Y + edgeTwo.Y };
      }

      public static EngineVector2 operator -(EngineVector2 edgeOne, EngineVector2 edgeTwo)
      {
         return new EngineVector2 { X = edgeOne.X - edgeTwo.X, Y = edgeOne.Y - edgeTwo.Y };
      }
      #endregion

      #region Static Functions
      public static float dotProduct(EngineVector2 v1, EngineVector2 v2)
      {
         return v1.X * v2.X + v1.Y * v2.Y;
      }
      public static EngineVector2 GetProjection(EngineVector2 edgeToProject, EngineVector2 edgeToProjectOnto)
      {
         return edgeToProjectOnto * (dotProduct(edgeToProject, edgeToProjectOnto) / (edgeToProjectOnto.ToMagnitudeSquared()));
      }
      public static EngineVector2 Max()
      {
         return new EngineVector2 { X = float.MaxValue, Y = float.MaxValue };
      }
      public static EngineVector2 Min()
      {
         return new EngineVector2 { X = float.MinValue, Y = float.MinValue };
      }
      public static EngineVector2 RotateAroundPoint(EngineVector2 vectorToRotate, EngineVector2 pointToRotateAround, float degreeOfRotation)
      {
         EngineVector2 temp = vectorToRotate - pointToRotateAround;
         temp.RotateVector(degreeOfRotation);
         temp += pointToRotateAround;
         return temp;
      }
      #endregion

      #region Functions
      public EngineVector2 GetOrthoginal()
      {
         return new EngineVector2 { X = -Y, Y = X };
      }

      public float ToMagnitudeSquared()
      {
         return X * X + Y * Y;
      }

      public bool Equals(EngineVector2 obj)
      {
         return X == obj.X && Y == obj.Y;
      }

      public EngineVector2 ToUnitVector()
      {
         EngineVector2 unitVector = new EngineVector2();
         if(X != 0 ||  Y != 0)
         {
            float magnitude = X * X + Y * Y;
            magnitude = (float)Math.Sqrt(magnitude);
            magnitude = 1 / magnitude;
            unitVector.X = (X * magnitude);
            unitVector.Y = (Y * magnitude);
         }
         return unitVector;
      }

      public void RotateVectorAroundPoint(EngineVector2 pointToRotateAround, float degreeOfRotation)
      {
         X -= pointToRotateAround.X;
         Y -= pointToRotateAround.Y;
         RotateVector(degreeOfRotation);
         X += pointToRotateAround.X;
         Y += pointToRotateAround.Y;
      }

      public void RotateVector(float degrees)
      {
         double Radians = (degrees * Math.PI) / 180f;
         float X2 = (float) ( (X * Math.Cos(Radians)) - (Y * Math.Sin(Radians)) );
         float Y2 = (float) ( (X * Math.Sin(Radians)) + (Y * Math.Cos(Radians)) );
         X = X2;
         Y = Y2;
      }

      //Angle is 0 degrees when facing right
      public double GetDegreeRotation()
      {
         double angleInRadians = Math.Atan2(Y, X);
         //Note: *-1 is due to positive going counterclockwise in math
         double angleInDegrees = ((angleInRadians * 180) / Math.PI);
         //angleInDegrees *= -1;
         ////Note: -90 is due to sprites not being rotated when facing down not to the right like math
         angleInDegrees += 90;


         return angleInDegrees;
      }

      public double GetRadianRotation()
      {
         return Math.Atan2(Y, X);
      }
      #endregion
   }
}
