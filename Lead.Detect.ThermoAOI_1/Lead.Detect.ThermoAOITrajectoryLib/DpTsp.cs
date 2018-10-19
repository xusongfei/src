using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOITrajectoryLib
{
    public class DpTsp : ITsp
    {
        private List<PosXYZ> pos;

        private double[,] Graph;

        public DpTsp(PosGraph graph)
        {
            Graph = graph.Graph;
            pos = graph.pos;
        }



        private int N; //点的个数 
        private int col;

        private double[,] D;
        private float[,] F;
        private int[,] M;

        private void dp()
        {
            //遍历并填表
            for (var i = 1; i < col - 1; i++)//最后一列不在循环里计算
                for (var j = 1; j < N; j++)
                {
                    if (((int)Math.Pow(2, j - 1) & i) == 0)//结点j不在i表示的集合中
                    {
                        var min = 65535d;
                        for (var k = 1; k < N; k++)
                            if (((int)Math.Pow(2, k - 1) & i) > 0)//非零表示结点k在集合中
                            {
                                var temp = D[j, k] + F[k, i - (int)Math.Pow(2, k - 1)];
                                if (temp < min)
                                {
                                    min = temp;
                                    F[j, i] = (float)min;//保存阶段最优值
                                    M[j, i] = k;//保存最优决策
                                }
                            }

                    }
                }

            {
                //最后一列，即总最优值的计算
                var min = 65535d;
                for (var k = 1; k < N; k++)
                {
                    //b-1的二进制全1，表示集合{1,2,3,4,5}，从中去掉k结点即将k对应的二进制位置0
                    var temp = D[0, k] + F[k, col - 1 - (int)Math.Pow(2, k - 1)];
                    if (temp < min)
                    {
                        min = temp;
                        F[0, col - 1] = (float)min;//总最优解
                        M[0, col - 1] = k;
                    }
                }

                Console.WriteLine("最短路径长度：" + F[0, col - 1].ToString("F3"));

                MinLength = F[0, col - 1];

            }


            {

                Order = new int[N + 1];
                //回溯查表M输出最短路径(编号0~n-1)
                Console.WriteLine("最短路径(编号0—n-1)：" + "0");

                var k = 0;
                var next = 0;
                for (var i = col - 1; i > 0;)//i的二进制是5个1，表示集合{1,2,3,4,5}
                {
                    next = (int)M[next, i];//下一步去往哪个结点
                    i = i - (int)Math.Pow(2, next - 1);//从i中去掉j结点
                    Console.WriteLine(next);
                    Order[k++] = next;
                }
            }
        }

        public double MinLength { get; private set; }
        public int[] Order { get; private set; }


        public double RunTcp()
        {
            N = pos.Count;
            D = Graph;
            col = (int)Math.Pow(2, N - 1);

            F = new float[N, col];
            M = new int[N, col];
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    F[j, i] = -1;
                    M[j, i] = -1;
                }
            }

            //给F的第0列赋初值
            for (var i = 0; i < N; i++)
                F[i, 0] = (float)D[i, 0];



            dp();


            return 0;
        }
    }
}