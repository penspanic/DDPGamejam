using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public static class MathUtil
{
    public static readonly Vector3 InvalidVector3 = new Vector3(float.NaN, float.NaN, float.NaN);

    // http://answers.unity3d.com/questions/167622/vector3tostring.html
    public static string ToPreciseString(this Vector2 vec)
    {
        return vec.ToString("G4");
    }

    public static string ToPreciseString(this Vector3 vec)
    {
        return vec.ToString("G4");
    }

    public static string ToDebugString(this Vector2 vec)
    {
        return vec.ToString("F4");
    }

    public static string ToDebugString(this Vector3 vec)
    {
        return vec.ToString("F4");
    }

    public static float DistanceWith(this Vector3 lhs, Vector3 rhs)
    {
        return (lhs - rhs).magnitude;
    }

    public static float DistanceWith(this Vector2 lhs, Vector2 rhs)
    {
        return (lhs - rhs).magnitude;
    }

    private static Regex floatRegex = new Regex(@"(-?[a-zA-Z0-9]*\.?[-a-zA-Z0-9]*)");
    public static Vector3 Vector3FromString(string vectorString)
    {
        // 개선이 매우 필요함

        MatchCollection matches = floatRegex.Matches(vectorString);
        Match[] realMatched = (from Match match in matches
                               where match.Value != string.Empty
                               select match).ToArray();

        if (realMatched.Count() != 3)
        {
            Debug.LogError("Vector3 parse failed, " + vectorString);
            return Vector3.zero;
        }

        return new Vector3(float.Parse(realMatched[0].Value), float.Parse(realMatched[1].Value), float.Parse(realMatched[2].Value));
    }

    public static Vector2 Vector2FromString(string vectorString)
    {
        // 개선이 매우 필요함

        MatchCollection matches = floatRegex.Matches(vectorString);
        Match[] realMatched = (from Match match in matches
                               where match.Value != string.Empty
                               select match).ToArray();

        if (realMatched.Count() != 2)
        {
            Debug.LogError("Vector3 parse failed, " + vectorString);
            return Vector2.zero;
        }

        return new Vector2(float.Parse(realMatched[0].Value), float.Parse(realMatched[1].Value));
    }
}
