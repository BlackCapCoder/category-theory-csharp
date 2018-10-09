using System;

namespace Ord {

  // Down gives the opposite ordering
  class Down<A> : IComparable<Down<A>> where A : IComparable<A> {
    public readonly A down;
    public Down (A x) { down = x; }
    public int CompareTo (Down<A> x) => -down.CompareTo(x.down);
  }

}
