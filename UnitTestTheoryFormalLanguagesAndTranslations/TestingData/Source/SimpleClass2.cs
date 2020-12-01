using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestTheoryFormalLanguagesAndTranslations.TestingData.Source
{
    class SimpleClass2
    {
        public int a { get; private set; }
        public int b { get; set; }
        public string c { get; private set; }
        public SimpleClass2()
        {
            a = b = 0;
            c = "";
        }

        private void MethodA()
        {
            a = b * 2;
        }

        public void MethodB()
        {
            MethodA();
            c = (a + b).ToString();
        }

        public void MethodC(string s)
        {
            c = s;
        }
    }
}
