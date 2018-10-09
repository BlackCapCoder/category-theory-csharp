using System;

namespace Functor {

  // Haskell:
  // class functor f where
  //   map :: (a -> b) -> f a -> f b

  // Java approximation:
  // interface Functor<T,F extends Functor<?,?>> {
  //   <R> F map(Function<T,R> f);
  // }

  class A<T> {}

  // C#
  interface Functor<A> {
    F map<B,F> (Func<A, B> f) where F : Functor<B>;
  }

  // The language is really starting to fall apart here.
  // As far as I know, there is no way to enforce that
  // map must return a functor of the same type.
  // Java is a hack- but at least you can do it!

  // Functor laws:
  //   fmap id == id
  //   fmap (f . g) == fmap f . fmap g

  // class Box<A> : Functor<A> {
  //   public A x;
  //   public Box (A a) { x = a; }
  //
  //   public Box<B> map<B,F> (Func<A, B> f) => new Box(f.Invoke(x));
  // }

}

