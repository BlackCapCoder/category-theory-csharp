using System;
using Ord;
using List;
using Functor;

namespace Semigroup {

  // Represents something that can be composed.
  // Instances should satisfy the following laws:
  //   Multiply(x, Multiply(y, z)) == Multiply(Multiply(x, y), z)
  interface Semigroup<A> {
    A Multiply (A x);
  }


  static class Semigroup {
    public static A SConcat<A> (A x, List<A> l) where A : Semigroup<A> {
      if (l != null && l.tail != null) return x.Multiply(SConcat(l.head, l.tail));
      return x;
    }
  }


  // ============= Instances ===============

  // x.Multiply(y) == x
  class First<A> : Semigroup<First<A>> {
    public readonly A first;
    public First (A x) { this.first = x; }
    public First<A> Multiply(First<A> _) { return this; }

    // public First<R> map<R>(Func<A, R> f) => new First(f.apply(first));
    // public int map<R>(Func<A, R> f) => 3;
  }

  // Smallest of comparable things
  class Min<A> : Semigroup<Min<A>> where A : IComparable<A> {
    public readonly A min;
    public Min (A x) { this.min = x; }
    public Min<A> Multiply(Min<A> x) => min.CompareTo(x.min) > 0? x: this;
  }

  // Opposite: x.multiply(y) == Dual(y).multiply(Dual(x));
  class Dual<A> : Semigroup<Dual<A>> where A : Semigroup<A> {
    public readonly A dual;
    public Dual (A x) { this.dual = x; }
    public Dual<A> Multiply(Dual<A> x) => new Dual<A>(x.dual.Multiply(dual));
  }

  // Last<A> = Dual<First<A>>
  class Last<A> : Semigroup<Last<A>> {
    private Dual<First<A>> del;
    public Last (A x) { this.del = new Dual<First<A>>(new First<A>(x)); }
    public A last { get => del.dual.first; }
    public Last<A> Multiply(Last<A> x) => new Last<A>(del.Multiply(x.del).dual.first);
  }

  // Max<A> = Min<Down<A>>
  class Max<A> : Semigroup<Max<A>> where A : IComparable<A> {
    private Min<Down<A>> del;
    public Max (A x) { del = new Min<Down<A>>(new Down<A>(x)); }
    public A max { get => del.min.down; }
    public Max<A> Multiply(Max<A> x) => new Max<A>(del.Multiply(x.del).min.down);
  }

}
