using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace RoslynAnalyzer
{
    /*
     * here is some code for you 
     * and for cheking it works
     */
     // some simple code in comments
    interface IInter1
    {

    }

    interface IInter2
    {

        int X { get; set; }
    }

    interface IInter3
    {

    }

    class Code
    {

        static void parse()
        {
            int a = 4;
        }
	void c()
        {
            try
            {
                int a = 3;
                a = a + 2;
            }catch (Exception ex)
            {
                var s = ex;
                int a = 4;
            }
        }
	int v(int r, int e)
        {
            int x;
            Random rand = new Random();
            if (rand.Next() > 11)
            {
                int a = 4;
                string er = "asdfsdfaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadfsdfsdfsdfsdfadfwerwerwerwerdfsdfsgsdfadgewafdsafdsgaegafasdfasdfasdfwegwgasgasdfasdgwgasdf";
                string er2 = "asdfsdfaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadfsdfsdfsdfsdfadfwerwerwerwerdfsdfsgsdfadgewafdsafdsgaegafasdfasdfasdfwegwgasgasdfasdgwgasdf";
                return 2;
            } else
            {
                return 1;
            }
            try
            {
                int a = 3;
                a = a + 2;
            }
            catch (Exception ex)
            {
            }
        }
        void x(int x, bool flag)
        {

        }

        void TestValueMethod()
        {
            int x = 10;
            Debug.Assert(x > 20);
            Debug.Assert(x % 2 == 2);
        }

        int y()
        {
            int x;
            int a = 100;
            if (a % 10 == 0)
            {
                if (a % 3 == 0)
                {
                    if (a % 5 == 0)
                    {
                        int setv = 122;
                    }
                }
            }
            Random rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    for (int k = 0; k < 40; k++)
                    {

                    }
                }
            }
            if (rand.Next() > 11)
            {
                return 2;
            } else
            {
                return 1;
            }

        }
    }
}
