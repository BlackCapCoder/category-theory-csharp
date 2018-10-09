using System;
using Semigroup;

namespace ctsharp
{
  class Program
  {
    static void Main(string[] args)
    {
      Min<int> x = new Min<int>(2);
      Min<int> y = new Min<int>(3);
      Min<int> z = new Min<int>(1);
      Console.WriteLine(x.Multiply(y).Multiply(z).min);
    }
  }
}
