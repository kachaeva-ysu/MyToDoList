﻿namespace ToDoListProject
{
    public interface IWriterReader
    {
        public void Write(string message);
        public string Read();
    }
}
