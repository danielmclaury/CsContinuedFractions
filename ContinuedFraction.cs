using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class ContinuedFraction
{
  private bool m_consumed = false;

  private IEnumerator<BigInteger> m_enumerator;

  private List<BigInteger> m_terms = new List<BigInteger>();

  private List<Rational> m_convergents = new List<Rational>();

  private void m_fetchNextTerm()
  {
    if(m_consumed) { return; }

    // Get the term and cache it in m_terms

    if(m_enumerator.MoveNext())
    {
      m_terms.Add(m_enumerator.Current);
    }
    else
    {
      m_consumed = true;
      return;
    }

    // Calculate the corresponding convergent

    int n = m_terms.Count;

    if(n == 1)
    {
      m_convergents.Add(m_terms[0]);
    }
    else if(n == 2)
    {
      m_convergents.Add(m_terms[0] + (Rational)1/m_terms[1]);
    }
    else
    {
      m_convergents.Add(new Rational(
        m_terms[n-1] * m_convergents[n-2].Numerator 
          + m_convergents[n-3].Numerator,
        m_terms[n-1] * m_convergents[n-2].Denominator
          + m_convergents[n-3].Denominator));
    }
  }

  public ContinuedFraction(IEnumerable<BigInteger> t_terms)
  {
    m_enumerator = t_terms.GetEnumerator();

    while(! m_consumed && m_terms.Count < 10)
    {
      m_fetchNextTerm();
    }
  }

  public Rational GetConvergent(int t_n)
  {
    while(! m_consumed && t_n > m_terms.Count - 1)
    {
      m_fetchNextTerm();
    }

    return m_convergents[t_n];
  }

  public override string ToString()
  {
    return "[ " + m_terms[0] + "; " 
           + string.Join(", ", m_terms.Skip(1)) 
           + (m_consumed ? "" : ", ..." ) + " ]";
  }
}
