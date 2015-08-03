﻿using System.Collections.Generic;
using Mp3Handler;

namespace Tests
{
    class TestFileHandler:IFileHandler
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Dictionary<FrameType,string> Tags { get; set; } 

        public TestFileHandler(string filename)
        {
            FileName = filename;
            Tags = new Dictionary<FrameType, string>();
        }
        
        public Dictionary<FrameType, string> GetTags()
        {
            return Tags;
        }

        public void SetTags(Dictionary<FrameType, string> tags)
        {
            foreach (var tag in tags)
            {
                if (Tags.ContainsKey(tag.Key))
                {
                    Tags[tag.Key] = tag.Value;
                }
                else
                    Tags.Add(tag.Key,tag.Value);
            }
        }

        public void Rename(string newName)
        {
            FileName = newName;
        }
    }
}
