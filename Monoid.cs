using System;
using System.Diagnostics.Contracts;
using Semigroup;
using static Semigroup.Semigroup;
using List;

namespace Monoid {

  // A monoid is a semigroup with a unit.
  // Instances should satisfy the following laws (and the semigroup laws):
  //   Multiply(x, zero) == x
  //   Multiply(zero, x) == x
  interface Monoid<A> where A : Semigroup<A> {
    A zero { [Pure] get; } // Should be a static constant, but I can't do that in c#
  }


  static class Monoid {
    // This is wrong! The problem is that there is no way to
    // find out what zero is without having an instance, and
    // I cannot create one
    public static A MConcat<A>(List<A> l) where A : Monoid<A>, Semigroup<A> {
      // return SConcat(A.zero, l);       // Not a static constant
      // return SConcat(new A().zero, l); // Does not work
      // return SConcat(l.head.zero, l);  // Head could be null
      return SConcat(l.head, l);          // Wrong but compiles, crashes if head is null
    }
  }


  // ============== Instances: =================

  // Boolean and
  class All : Monoid<All>, Semigroup<All> {
    public All zero { get => new All(true); }
    public readonly bool all;
    public All (bool x) { all = x; }
    public All Multiply(All x) => new All(all && x.all);
  }

  // Boolean or
  class Any : Monoid<Any>, Semigroup<Any> {
    public Any zero { get => new Any(false); }
    public readonly bool all;
    public Any (bool x) { all = x; }
    public Any Multiply(Any x) => new Any(all && x.all);
  }

  // Works on any kind of number, but c#
  class Sum : Monoid<Sum>, Semigroup<Sum> {
    public Sum zero { get => new Sum(0); }
    public readonly int sum;
    public Sum (int x) { sum = x; }
    public Sum Multiply(Sum x) => new Sum(sum + x.sum);
  }

  // ditto
  class Product : Monoid<Product>, Semigroup<Product> {
    public Product zero { get => new Product(1); }
    public readonly int product;
    public Product (int x) { product = x; }
    public Product Multiply(Product x) => new Product(product * x.product);
  }

}
