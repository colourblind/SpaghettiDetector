using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaghettiDetector.Test
{
    class TestClassA
    {
        int foo { get; set; }
        string bar { get; set; }
        TestClassAA a { get; set; }
    }

    class TestClassAA
    {
        int foo { get; set; }
        TestClassAAA a { get; set; }
        TestClassAAB b { get; set; }
    }

    class TestClassAAA
    {
        int foo { get; set; }
        SpaghettiDetector s { get; set; }
    }

    class TestClassAAB
    {
        int foo { get; set; }
    }
}
