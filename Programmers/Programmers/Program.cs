using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Programmers;

class Program
{
    private static readonly List<Runner> Runners = new ();
    
    static void Main(string[] args)
    {
        Runners.Add(new NumberRunner());
        Runners.Add(new GraphRunner());
        
        foreach (var runner in Runners)
        {
            runner.Solution();
        }
    }
}

