using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestTheoryFormalLanguagesAndTranslations.TestingData.Source
{
    public class SimpeClass
    {
        public int value { get; private set; }
        public SimpeClass()
        {
            value = 0;
        }

        public string MethodA(int a, int b, int c)
        {
            value = a + b * c;
            return a + " + " + b + " = " + c;
        }
    }
}
