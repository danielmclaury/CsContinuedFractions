using System;
using System.Numerics;

public class Rational
{

  public BigInteger Numerator { get; private set; }
  public BigInteger Denominator { get; private set; }

  private static BigInteger m_gcd(BigInteger a, BigInteger b)
  {
    // Looks like we don't have BigInteger.GreatestCommonDenominator in mono
    // so just implement the Euclidean algorithm here.

     if(a < 0) { a = - a; }
     if(b < 0) { b = - b; }

     while(b > 0)
     {
       BigInteger t = b;

       b = a % b;
       a = t;
     }

     return a;
  }

  public Rational(BigInteger t_numerator, BigInteger t_denominator)
  {
    // Normalize to lowest terms with positive denominator.

    if(t_denominator == 0)
    {
      throw new DivideByZeroException();
    }
    else if(t_numerator == 0)
    {
      Numerator = 0;
      Denominator = 1;
    }
    else
    {
      BigInteger gcd = m_gcd(t_numerator, t_denominator);

      int sign = t_denominator > 0 ? 1 : -1;

      Numerator = sign * t_numerator / gcd;
      Denominator = sign * t_denominator / gcd;
    }
  }

  public static implicit operator Rational(BigInteger t_integer)
  {
    return new Rational(t_integer, 1);
  }

  public static implicit operator Rational(int t_integer)
  {
    return new Rational(t_integer, 1);
  }

  public static Rational Pow(Rational q, int n)
  {
    if(n >= 0)
    {
      return new Rational(BigInteger.Pow(q.Numerator, n), BigInteger.Pow(q.Denominator, n));
    }
    else
    {
      return 1 / Pow(q, -n);
    }
  }

  public static explicit operator double(Rational t_rational)
  {
    int exponent 
      = (int)(BigInteger.Log(t_rational.Numerator, 2)
        - BigInteger.Log(t_rational.Denominator, 2) + 52);

    Rational q = t_rational * Rational.Pow(2, exponent);

    double result = (double)BigInteger.Divide(q.Numerator, q.Denominator) * Math.Pow(2, -exponent);

    return result;
  }

  public static Rational operator -(Rational a)
  {
    return new Rational(-a.Numerator, a.Denominator);
  }

  public static Rational operator +(Rational a, Rational b)
  {
    return new Rational(
      a.Denominator * b.Numerator + b.Denominator * a.Numerator,
      a.Denominator * b.Denominator);
  }

  public static Rational operator -(Rational a, Rational b)
  {
    return a + (-b);
  }

  public static Rational operator *(Rational a, Rational b)
  {
    return new Rational(
      a.Numerator * b.Numerator,
      a.Denominator * b.Denominator); 
  }

  public static Rational operator /(Rational a, Rational b)
  {
    return new Rational(
      a.Numerator * b.Denominator,
      a.Denominator * b.Numerator);
  }

  public static bool operator ==(Rational a, Rational b)
  {
    // Handle the null == null case, and some others as a bonus.

    if(object.ReferenceEquals(a, b)) { return true; }

    // These are normalized so that each rational has a unique representation

    return (a.Numerator == b.Numerator) && (a.Denominator == b.Denominator);
  }

  public static bool operator !=(Rational a, Rational b)
  {
    return !(a == b);
  }

  public static bool operator <(Rational a, Rational b)
  {
    // These are normalized so that the denominators are positive.

   return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
  }

  public static bool operator <=(Rational a, Rational b)
  {
    return (a < b) || (a == b);
  }

  public static bool operator >(Rational a, Rational b)
  {
    return !(a <= b);
  }

  public static bool operator >=(Rational a, Rational b)
  {
    return !(a < b);
  }

  public override bool Equals(object other)
  {
    if(other is Rational)
    {
      return (this == (Rational)other);
    }
    else
    {
      return false;
    }
  }

  public override int GetHashCode()
  {
    return 
      7 * Numerator.GetHashCode()
        + Denominator.GetHashCode();
  }

  public override string ToString()
  {
    if(Denominator == 1)
    {
      return Numerator.ToString();
    }
    else
    {
      return Numerator.ToString() + " / " + Denominator.ToString();
    }
  }
}
