using System;
using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOITrajectoryLib
{
    public class BackTsp : ITsp
    {
        public BackTsp(PosGraph graph)
        {
            Graph = graph.Graph;
            pos = graph.pos;
        }

        public double[,] Graph;
        public List<PosXYZ> pos;


        public double MinLength { get; private set; }
        public int[] Order { get; private set; }


        private void Backtrack(int t, int[] x)
        {
            //对顶点t进行操作，父结点的解向量是x，
            if (t >= vexnum)
            {
                //解向量的第一个元素应该是初始顶点，如0，最后一个元素也是0
                x[t] = 0; //最后一个节点赋值：0。
                constraint(x, t);
            }
            else
            {
                //所有顶点都解完
                int i, j;
                int[] cx = new int[vexnum + 1];
                for (j = 0; j < t; j++)
                {
                    cx[j] = x[j]; //拷贝父结点
                }

                cx[t] = t;
                if (constraint(cx, t))
                {
                    Backtrack(t + 1, cx); //不交换的情况下进行递归
                }

                //不断递归调用【Backtrack】，进行DFS
                for (i = 1; i < t; i++)
                {
                    cx = new int[vexnum + 1];
                    for (j = 0; j < t; j++) cx[j] = x[j]; //拷贝父结点
                    cx[t] = t;
                    swap(cx, i, t);
                    if (constraint(cx, t))
                    {
                        Backtrack(t + 1, cx); //交换的情况下进行递归
                    }
                }
            }
        }


        private int vexnum = 0; //顶点数目
        private double bestCost = 0;
        private int[] bestX; //最优解向量
        private bool isTraverseDeep;

        bool constraint(int[] x, int len)
        {
            //对解进行约束
            double cost = 0;
            int i;
            int pre = x[0];
            for (i = 1; i <= len; i++)
            {
                var dist = Graph[pre, x[i]];
                if (dist <= 0)
                {
                    return false; //不连通，则为否。约束（constraint）函数
                }
                cost += dist;
                pre = x[i];
            }

            if (isTraverseDeep)
            {
                //如果已经进行了最底部的遍历，则对这个当前花费进行判别。界限（bound）函数
                if (cost < bestCost)
                {
                    //比最优解要小
                    if (len == vexnum)
                    {
                        //已经遍历完
                        bestCost = cost;
                        bestX = x; //设置最优解向量
                    }
                    return true;
                }
                else return false;
            }
            else if (len == vexnum)
            {
                //首次遍历到底部
                bestCost = cost;
                bestX = x; //设置最优解向量
                isTraverseDeep = true;
                return true;
            }
            return true;
        }


        private void swap(int[] nums, int a, int b)
        {
            int tmp = nums[a];
            nums[a] = nums[b];
            nums[b] = tmp;
        }


        public double RunTcp()
        {
            vexnum = pos.Count;
            isTraverseDeep = false;

            Backtrack(1, new[] { 0 });

            Console.WriteLine("path:");
            for (int i = 0; i < pos.Count; i++)
            {
                Console.WriteLine(bestX[i]);
            }

            Console.WriteLine("path length:");
            Console.WriteLine(bestCost);
            return 0;
        }
    }
}