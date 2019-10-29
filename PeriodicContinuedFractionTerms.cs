using System.Collections;
using System.Collections.Generic;
using System.Numerics;

class PeriodicContinuedFractionTerms : IEnumerable<BigInteger>
{
  private List<BigInteger> m_initialSegment;
  private List<BigInteger> m_repeatingSegment;

  public PeriodicContinuedFractionTerms(
    IEnumerable<BigInteger> t_initialSegment,
    IEnumerable<BigInteger> t_repeatingSegment)
  {
    m_initialSegment = new List<BigInteger>(t_initialSegment);
    m_repeatingSegment = new List<BigInteger>(t_repeatingSegment);
  }

  public IEnumerator<BigInteger> GetEnumerator()
  {
    foreach(BigInteger term in m_initialSegment)
    {
      yield return term;
    }

    while(true)
    {
      foreach(BigInteger term in m_repeatingSegment)
      {
        yield return term;
      }      
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}
