﻿namespace MyStack
{
    class Node<T>
    {
        public T Value;
        public Node<T> Previous;

        public Node(T value)
        {
            Value = value;
            Previous = null;
        }
    }
}