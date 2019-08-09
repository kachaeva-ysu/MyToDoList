﻿using MasterSlaveSync.Conflict;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Synchronizer
    {
        public SyncOptions SyncOptions { get; private set; } = new SyncOptions();

        private IDirectoryInfo master;
        private IDirectoryInfo slave;

        private IFileSystem _fileSystem;

        public Synchronizer(string masterPath, string slavePath) :
            this(masterPath, slavePath, new FileSystem()) { }

        public Synchronizer(string masterPath, string slavePath, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;

            master = _fileSystem.DirectoryInfo.FromDirectoryName(masterPath);
            slave = _fileSystem.DirectoryInfo.FromDirectoryName(slavePath);
        }

        public List<IConflict> CollectConflicts()
        {
            var collector = new ConflictsCollector();
            return collector.CollectConflicts(master, slave);
        }

        public void SyncDirectories(List<IConflict> conflicts)
        {
            var syncProcessor = new SyncProcessor(SyncOptions);
            syncProcessor.Synchronize(conflicts);
        }

    }
}
