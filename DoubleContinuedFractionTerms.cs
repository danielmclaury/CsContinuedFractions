using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

class DoubleContinuedFractionTerms : IEnumerable<BigInteger>
{
  private double m_remainder;

  public DoubleContinuedFractionTerms(double t_double)
  {
    m_remainder = t_double;
  }

  public IEnumerator<BigInteger> GetEnumerator()
  {
    while(m_remainder != 0)
    {
      int term = (int)m_remainder;

      yield return term;

      if(term == m_remainder)
      {
        yield break;
      }
      else
      {
        m_remainder = 1 / (m_remainder - term);
      }
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}
