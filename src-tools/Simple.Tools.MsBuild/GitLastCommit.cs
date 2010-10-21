using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using GitSharp;

namespace Simple.Tools.MsBuild
{
    public class GitLastCommit : Task
    {
        [Required]
        public string Path { get; set; }

        [Output]
        public ITaskItem CommitHash { get; set; }

        public override bool Execute()
        {
            string hash = "private";

            var path = Repository.FindRepository(Path);
            if (Repository.IsValid(path))
            {
                var repo = new Repository(path);
                hash = repo.CurrentBranch.CurrentCommit.Hash;
            }

            CommitHash = new TaskItem(hash);

            return true;
        }
    }
}
