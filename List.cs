using System;

namespace List {

  class List<T> {
    public T head;
    public List<T> tail;

    public List (T head) {
      this.head = head;
      this.tail = null;
    }
    private List (T head, List<T> tail) {
      this.head = head;
      this.tail = tail;
    }

    public List<T> Cons (T val) {
      return new List<T>(val, this);
    }
  }

}
