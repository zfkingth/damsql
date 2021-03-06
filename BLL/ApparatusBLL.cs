 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: BLL class .
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年2月2日 20:06:22    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Collections.Generic;
using hammergo.Model;
using hammergo.DALFactory;
using hammergo.IDAL;
using System.Collections;


namespace hammergo.BLL
{
	/// <summary>
	/// 业务逻辑类的摘要说明。
	/// </summary>
    
    public class  ApparatusBLL: ApparatusBLLBase
    {

        /// <summary>
        /// 验证测点的计算表达式，出错就抛出异常
        /// </summary>
        /// <param name="appCalcName"></param>
        /// <param name="consList"></param>
        /// <param name="mesList"></param>
        /// <param name="calList"></param>
        public void ValidateParams(string appCalcName, IList<ConstantParam> consList, IList<MessureParam> mesList, IList<CalculateParam> calList)
        {
            List<string> nameList = new List<string>(20);
            List<string> symbolList = new List<string>(20);



            checkParamNames(consList, mesList, calList, nameList, symbolList);

            hammergo.caculator.MyList list = new hammergo.caculator.MyList(10);

            int num = 1;//填充一些数据，测试计算的表达式
            foreach (string s in symbolList)
            {
                list.add(s, num++);
            }

            //简单的判断公式的依赖关系，只能精确到仪器,不能精确的量（如n01cf14.e的依赖关系，过于复杂）


            //生成新的图
            ALGraph.MyGraph graph = new ALGraph.MyGraph();
            hammergo.caculator.CalcFunction calc = new hammergo.caculator.CalcFunction();
            hammergo.caculator.MyList calcNameList = new hammergo.caculator.MyList(5);



            foreach (CalculateParam cp in calList)
            {
                string formula = cp.CalculateExpress;
                string symbol = cp.ParamSymbol;

                if (formula == null)
                {
                    throw new Exception(string.Format("计算参数 {0} 的计算公式不能为空", cp.ParamName));
                }

                ArrayList vars = calc.getVaribles(formula);

                intitalVars(appCalcName, vars, graph, calcNameList, symbol);
            }


            ArrayList toplist = graph.topSort();
            if (toplist.Count != graph.Vexnum)
            {
                throw new Exception("公式存在循环依赖");
            }

            ArrayList storeTopList = toplist;

            // 刚才是检查仪器内的循环依赖,现在和所有的仪器一起检查


            graph = new ALGraph.MyGraph();

            for (int i = 0; i < calcNameList.Length; i++)
            {
                graph.addArcNode(new ALGraph.ArcNode(), calcNameList.getKey(i), appCalcName);//全部使用计算名称
                constructGraph(appCalcName, graph);//递归加入其子结点
            }

            toplist = graph.topSort();
            if (toplist.Count != graph.Vexnum)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder(128);
                foreach (ALGraph.VNode node in graph.vertices)
                {
                    sb.Append(node.data).Append("  ");

                }
                throw new Exception(sb.ToString() + " 仪器公式存在循环依赖");
            }


            toplist = storeTopList;

            //图不存在回路

            for (int i = 0; i < calcNameList.Length; i++)
            {


                string calcName = calcNameList.getKey(i);

                Apparatus refApp = dal.GetModelBy_CalculateName(calcName);

                if (refApp == null)
                {
                    throw new Exception(string.Format("计算名称为{0}的仪器不存在!", calcName));
                }

                ConstantParamBLL consBLL = new ConstantParamBLL();
                MessureParamBLL mesBLL = new MessureParamBLL();
                CalculateParamBLL calcBLL = new CalculateParamBLL();


                ArrayList paList = new ArrayList(12);
                paList.AddRange(consBLL.GetListByappName(refApp.AppName));
                paList.AddRange(mesBLL.GetListByappName(refApp.AppName));
                paList.AddRange(calcBLL.GetListByappName(refApp.AppName));

                for (int j = 0; j < paList.Count; j++)
                {
                    IParamInterface pi = paList[j] as IParamInterface;

                    list.add(calcName + "." + pi.ParamSymbol, j + 1);
                }



            }


            foreach (CalculateParam cp in calList)
            {
                string formula = cp.CalculateExpress;
                calc.compute(formula, list);

                string symbol = cp.ParamSymbol;

                int index = toplist.IndexOf(symbol);
                if (index < 0)
                {
                    //此符号不在拓扑图中，没有公式依赖于它
                    index = toplist.Count;

                }

                cp.CalculateOrder = (byte)index;

            }



        }



        public void constructGraph(string calculateName, ALGraph.MyGraph graph)
        {
            hammergo.caculator.MyList list = getChildApp(calculateName);

            for (int i = 0; i < list.Length; i++)
            {
                string calcSn = list.getKey(i);
                graph.addArcNode(new ALGraph.ArcNode(), calculateName, calcSn);


                constructGraph(calcSn, graph);
            }

        }


        /// <summary>
        ///  获取仪器的子仪器数据
        /// </summary>
        /// <param name="测点编号"></param>
        /// <returns></returns>
        public hammergo.caculator.MyList getChildApp(string calculateName)
        {
            hammergo.caculator.MyList list = new hammergo.caculator.MyList(5);


            CalculateParamBLL calcBLL = new CalculateParamBLL();
            List<string> calcNameList = calcBLL.getChildAppCalcName(calculateName);

            foreach (string name in calcNameList)
            {
                list.add(name, 0);
            }


            return list;



        }




        /// <summary>
        /// 构造树型结点，并查找计算变量引用的测点
        /// </summary>
        /// <param name="appCalcName">计算名称</param>
        /// <param name="vars">计算公式中需要的计算变量</param>
        /// <param name="graph">拓扑树</param>
        /// <param name="idList">引用测点计算的计算名称列表</param>
        /// <param name="symbol">当前计算量的符号</param>
        private void intitalVars(string appCalcName, ArrayList vars, ALGraph.MyGraph graph, hammergo.caculator.MyList idList, string symbol)
        {



            for (int i = 0; i < vars.Count; i++)
            {
                string vs = (string)vars[i];
                int pos = vs.IndexOf('.');
                if (pos != -1)//带点的参数
                {
                    string otherID = vs.Substring(0, pos);
                    if (otherID == appCalcName)//计算名称
                    {
                        throw new Exception("公式中的变量不能包含本仪器的二级变量");
                    }
                    else
                    {
                        idList.add(otherID.ToUpper(), 0);
                    }
                }
                else
                {
                    //					//不带点的参数

                    //添加弧

                    graph.addArcNode(new ALGraph.ArcNode(), vs, symbol);



                }
            }

        }





        /// <summary>
        /// 检查参数的名称是否效,如果无效抛出异常
        /// </summary>
        /// <param name="consList">常量参数列表</param>
        /// <param name="mesList">测量参数列表</param>
        /// <param name="calList">计算参数列表</param>
        /// <param name="nameList">参数名称列表</param>
        /// <param name="symbolList">参数符号列表</param>
        private void checkParamNames(IList<ConstantParam> consList, IList<MessureParam> mesList, IList<CalculateParam> calList,
                                     List<string> nameList, List<string> symbolList)
        {
            System.Collections.IList[] array = new System.Collections.IList[] { consList as IList, mesList as IList, calList as IList };

            foreach (System.Collections.IList list in array)
            {


                foreach (IParamInterface pi in list)
                {
                    if (IsValidName(pi.ParamSymbol) == false)
                    {
                        throw new Exception("参数符号只能以26个字母开头,其后接字母和数字");
                    }

                    if (nameList.Contains(pi.ParamName))
                    {
                        throw new Exception(string.Format("参数名称:{0} 重复", pi.ParamName));
                    }

                    if (symbolList.Contains(pi.ParamSymbol))
                    {
                        throw new Exception(string.Format("参数符号:{0} 重复", pi.ParamSymbol));
                    }

                    nameList.Add(pi.ParamName);
                    symbolList.Add(pi.ParamSymbol);


                }
            }




        }


        public bool IsValidName(string strIn)
        {
            // Return true if strIn is in valid name format.
            return System.Text.RegularExpressions.Regex.IsMatch(strIn, @"^([a-zA-Z]+)([0-9a-zA-Z]*)$");
        }
		
    }
}



