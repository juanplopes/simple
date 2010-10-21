using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitSharp;

namespace Simple.Tools.MsBuild.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"d:\projects\simple";

            string ret = "private";

            path = Repository.FindRepository(path);
            if (Repository.IsValid(path))
            {
                var repo = new Repository(path);
                ret = repo.CurrentBranch.CurrentCommit.Hash;
            }
        }
    }
}
