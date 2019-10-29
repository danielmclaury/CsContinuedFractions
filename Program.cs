using System;
using System.Collections.Generic;
using System.Numerics;

public class Program
{
  public static void Main(string[] args)
  {
    IEnumerable<BigInteger> goldenRatioTerms
      = new PeriodicContinuedFractionTerms(
          new List<BigInteger>(),
          new List<BigInteger>(){1});

    ContinuedFraction goldenRatioCF = new ContinuedFraction(goldenRatioTerms);

    Console.WriteLine("Phi = " + goldenRatioCF.ToString());
    Console.WriteLine("Convergents: ");

    for(int i = 0; i < 15; i++)
    {
      Rational convergent = goldenRatioCF.GetConvergent(i);
      Console.WriteLine(convergent + " = " + (double)convergent);
    }
    Console.WriteLine();


    double log2_5 = Math.Log(5) / Math.Log(2);

    IEnumerable<BigInteger> log2_5_terms = new DoubleContinuedFractionTerms(log2_5);

    ContinuedFraction log2_5_CF = new ContinuedFraction(log2_5_terms);

    Console.WriteLine("log_2(5) = " + log2_5_CF.ToString());
    Console.WriteLine("Convergents: ");

    for(int i = 0; i < 15; i ++)
    {
      Rational convergent = log2_5_CF.GetConvergent(i);
      Console.WriteLine(convergent + " = " + (double)convergent);
    }
  }
}
