using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices; 
using System.Runtime.CompilerServices;


namespace hammergo.caculator
{
	/// <summary>
	/// CalcFunction ��ժҪ˵����
	/// </summary>
	/// 

	public interface ICalcFunction
	{
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		double compute(string s);
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="s"></param>
		/// <param name="slist"></param>
		/// <returns></returns>
		double compute(string s,MyList slist);

	}


	/// <summary>
	/// 
	/// </summary>
	[ClassInterface(ClassInterfaceType.None)] 
	public sealed class CalcFunction:ICalcFunction
	{
		

		Calculator calculator=new Calculator();

		Stack stack=new Stack();

		AbstractScan scan=null;
		Type type=typeof(CalcFunction);

		/// <summary>
        /// �Ƿ�ʹ�ñ��ʽǰ׺,usePrefixΪtrue,����������������ǰ��Ҫ����! ��!kk23.e4 ��!kk-kk.e4
		/// </summary>
        /// <param name="usePrefix"></param>
        public CalcFunction(bool usePrefix)
		{
            scan = ScanFactory.createScan(usePrefix);
		}

        /// <summary>
        /// ������������������Ĭ�Ϸ�ʽ����ʹ��!ǰ׺
        /// </summary>
        public CalcFunction()
        {
            scan = ScanFactory.createScan(false);
        }

/// <summary>
/// ����������ı��ʽ��ֵ
/// </summary>
/// <param name="s"></param>
/// <returns></returns>
	
		public double compute(string s)
		{
			if(s.Trim().Length==0)
			{
				throw new Exception("���ʽΪ��");
			}
			LinkList list=scan.parse(s);

//			int [,]priorities=Calculator.priorities;
//			for(int i=0;i<6;i++)
//			{
//				for(int j=0;j<6;j++)
//				{
//					Console.Write(priorities[i,j].ToString());
//				}
//				Console.WriteLine();
//			}

			return calculator.compute(calcFuns(list));
		}

		/// <summary>
		/// �õ�������ʽ�����б�������
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public ArrayList getVaribles(string s)
		{
			if(s.Trim().Length==0)
			{
				throw new Exception("���ʽΪ��");
			}
			ArrayList array=new ArrayList(8);
			

			LinkList list=scan.parse(s);

			LinkNode node=list.First;
			while(node!=null)
			{
				if(node.getWord().wordType==WordType.Identifier||node.getWord().wordType==WordType.IdentifierWithDot)
				{
					array.Add(node.getWord().valueString);
				}

				node=node.Next;
			}

			return array;
		}

		/// <summary>
		/// ��������ı��ʽ��ֵ
		/// </summary>
		/// <param name="s">��ʽ�ַ���</param>
		/// <param name="slist">�����б�</param>
		/// <returns></returns>
		public double compute(string s,MyList slist)
		{
			if(s.Trim().Length==0)
			{
				throw new Exception("���ʽΪ��");
			}
			LinkList list=scan.parse(s);
			list.replaceVariable(slist);

			return calculator.compute(calcFuns(list));
		}

	
	
		/// <summary>
		/// ������׺���ʽ�е����к���
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		private LinkList calcFuns(LinkList list)
		{
			//������������

			
			ArrayList paramsList=new ArrayList(5);
			LinkNode funNode=null,insertNode=null,nextTempNode=null;
			for(LinkNode node=list.First;node!=null;node=nextTempNode)
			{
				nextTempNode=node.Next;//node����ѭ������ÿ�μ����ﶼҪ��)ɾ����������ǰ����node����һ������ֵ
				


				if(node.getWord().wordType==WordType.Leftp)
					stack.Push(node);
				else if(node.getWord().wordType==WordType.Rightp)
				{
					if(stack.Count<1)
						throw new Exception("���Ų�ƥ�䣬ȱ������");
					else if(((LinkNode)stack.Peek()).Previous==null||((LinkNode)stack.Peek()).Previous.getWord().wordType!=WordType.FunName)
					{
						//����ֻ��һ�����ʽ����������һ������
						//����������ǰ�治�Ǻ�������������ǰ��û��Ԫ��
						stack.Pop();
						continue;

					}
					else 
					{

						paramsList.Clear();
						
						
						LinkNode start=(LinkNode)stack.Pop();
						funNode=start.Previous;
						insertNode=node.Next;
						list.removeBetween(start,node);
						LinkList innerList=new LinkList(start,node);//����������

						//��һ���������������û���κβ���
						if(innerList.Count==2)//�ж��������Ƿ�Ϊ��
							
						{
							//���㺯����ֵ
							
							double result=Function(funNode.getWord().valueString,paramsList);
							

							list.remove(funNode);
							list.insert(insertNode,new LinkNode(new Word(WordType.Number,result.ToString())));

							continue;
						}
						
						else
						{

							innerList.remove(start);//ɾ������
							innerList.remove(node);
							
							LinkNode comma=null;
							comma=innerList.searchComma();

							//�ڶ��������������ֻ��һ���������������ʽ��
							if(comma==null)//û�ж���
							{
								
								paramsList.Add(calculator.compute(innerList));

								//���ݲ������㺯����ֵ
								double result=Function(funNode.getWord().valueString,paramsList);

								list.remove(funNode);
								list.insert(insertNode,new LinkNode(new Word(WordType.Number,result.ToString())));


								continue;

							}
							else��/////������������������ж���������ʽ
							{


								
								while(comma!=null)
								{
									LinkNode newStart=innerList.First;
									innerList.removeBetween(newStart,comma);
									LinkList tempList=new LinkList(newStart,comma);
									
									if(tempList.Count<2)
									{
										throw new Exception("����ǰ��������");
									}
									tempList.remove(tempList.End);//ɾ������


									paramsList.Add(calculator.compute(tempList));

									comma=innerList.searchComma();
																
								}

								if(innerList.Count==0)
								{
									throw new Exception("�����Ķ��ź���������");
								}
								else
								{
									
									paramsList.Add(calculator.compute(innerList));
									//�ö���������㺯��

									double result=Function(funNode.getWord().valueString,paramsList);
									
									list.remove(funNode);
									list.insert(insertNode,new LinkNode(new Word(WordType.Number,result.ToString())));


									
								}
							}

						}

					}
				}

				


				

			}


			return list;

		}


		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="name"></param>
		/// <param name="array"></param>
		/// <returns></returns>
        

		private double Function(string name,ArrayList array)
		{


			string fname="f_"+name.ToLower();
			
			
			MethodInfo info=type.GetMethod(fname);

			if(info==null)
				throw new Exception(name+"������û��ʵ��");

			

			return (double)info.Invoke(this,new object[]{array});
		}

		/// <summary>
		/// max���㺯��
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("MAX(number1,number2,...)","����һ��ֵ�е����ֵ��",
			 "Number1, number2, ...    ��Ҫ�����ҳ����ֵ�����ֲ�����")]
		public double f_max(ArrayList array)
		{
			if(array.Count==0)
				throw new Exception("max�����Ĳ�������");

			double r=(double)array[0];

			for(int i=1;i<array.Count;i++)
			{
				double temp=(double)array[i];
				if(temp>r)
					r=temp;
			}

		
			return r;

		
		}


		/// <summary>
		/// ����PI��ֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[Desp("PI( )","�������� 3.14159265358979������ѧ���� pi��",
			 "")]
		public double f_pi(ArrayList array)
		{
			if(array.Count!=0)
				throw new Exception("PI�����Ĳ�������");

			return Math.PI;


		}

		/// <summary>
		/// sin���� ����Ϊ������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("SIN(number)","���ظ����Ƕȵ�����ֵ��",
			 "Number    Ϊ��Ҫ�����ҵĽǶȣ��Ի��ȱ�ʾ��")]
		public double f_sin(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("sin�����Ĳ�������");

			return Math.Sin((double)array[0]);

		}

		/// <summary>
		/// ʵ��cos
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("COS(number)","���ظ����Ƕȵ�����ֵ��",
			 "Number Ϊ��Ҫ�����ҵĽǶȣ��Ի��ȱ�ʾ��")]
		public double f_cos(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("cos�����Ĳ�������");

			return Math.Cos((double)array[0]);

		}
		


		/// <summary>
		/// ��ͺ���
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>


		[Desp("SUM(number1,number2, ...)","����ĳһ��Ԫ����������������֮�͡�",
			 "Number1, number2, ...    Ϊ��Ҫ��͵Ĳ�����")]
		public double f_sum(ArrayList array)
		{
			if(array.Count==0)
				throw new Exception("sum�����Ĳ�������");

			double sum=0;
			for(int i=0;i<array.Count;i++)
			{
				sum+=(double)array[i];
				

			}

			return sum;

		}



		/// <summary>
		/// ��ƽ��ֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("AVERAGE(number1,number2,...)","���ز�����ƽ��ֵ������ƽ��ֵ����",
			 "Number1, number2, ...    Ϊ��Ҫ����ƽ��ֵ�Ĳ�����")]
		public double f_average(ArrayList array)
		{
			if(array.Count==0)
				throw new Exception("average�����Ĳ�������");

			double average=0;
			for(int i=0;i<array.Count;i++)
			{
				average+=(double)array[i];
				

			}

			return average/array.Count;

		}

		/// <summary>
		/// ����Сֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("MIN(number1,number2,...)","����һ��ֵ�е���Сֵ��",
			 "Number1, number2,...    ��Ҫ�����ҳ���Сֵ�����ֲ�����")]

		public double f_min(ArrayList array)
		{
			if(array.Count==0)
				throw new Exception("min�����Ĳ�������");

			double r=(double)array[0];

			for(int i=1;i<array.Count;i++)
			{
				double temp=(double)array[i];
				if(temp<r)
					r=temp;
			}

		
			return r;

		
		}
		

		/// <summary>
		/// ������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("TAN(number)","���ظ����Ƕȵ�����ֵ��",
			 "Number    ΪҪ�����еĽǶȣ��Ի��ȱ�ʾ��")]
	

		public double f_tan(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("tan�����Ĳ�������");

			return Math.Tan((double)array[0]);

		}

		/// <summary>
		/// �����ֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>


		[Desp("ABS(number)","�������ֵľ���ֵ������ֵû�з��š�","Number   ��Ҫ���������ֵ��ʵ����")]

		public double f_abs(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("abs�����Ĳ�������");

			double r=(double)array[0];

			if(r>=0)
				return r;
			else return -r;

		}


		
		/// <summary>
		/// ������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("ACOS(number)","�������ֵķ�����ֵ��������ֵ�ǽǶȣ���������ֵΪ���֡����صĽǶ�ֵ�Ի��ȱ�ʾ����Χ�� 0 �� pi��"
			 ,"Number    �Ƕȵ�����ֵ��������� -1 �� 1 ֮�䡣")]

		public double f_acos(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("acos�����Ĳ�������");

			return Math.Acos((double)array[0]);

		}


		/// <summary>
		/// ������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[Desp("ASIN(number)","���ز����ķ�����ֵ��������ֵΪһ���Ƕȣ��ýǶȵ�����ֵ�����ڴ˺����� number ���������صĽǶ�ֵ���Ի��ȱ�ʾ����ΧΪ -pi/2 �� pi/2��",
			 "Number    �Ƕȵ�����ֵ��������� -1 �� 1 ֮�䡣")]

		public double f_asin(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("asin�����Ĳ�������");

			return Math.Asin((double)array[0]);

		}



		/// <summary>
		/// ������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("ATAN (number)","���ط�����ֵ��������ֵΪ�Ƕȣ�������ֵ������ number ����ֵ�����صĽǶ�ֵ���Ի��ȱ�ʾ����ΧΪ -pi/2 �� pi/2��",
			 "Number    �Ƕȵ�����ֵ��")]
		public double f_atan(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("atan�����Ĳ�������");

			return Math.Atan((double)array[0]);

		}

		/// <summary>
		/// ����ָ����X�ἰY������ֵ�ģ����ط�����ֵ��
		/// ����ֵ��-Pi��Pi֮�䣬������-Pi
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("ATAN2(x_num,y_num)","���ظ����� X �� Y ����ֵ�ķ�����ֵ�������еĽǶ�ֵ���� X ����ͨ��ԭ��͸�������� (x_num, y_num) ��ֱ��֮��ļнǡ�����Ի��ȱ�ʾ������ -pi �� pi ֮�䣨������ -pi����",
			 "X_num    ��� X ���ꡣY_num    ��� Y ���ꡣ")]
		public double f_atan2(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("atan2�����Ĳ�������");

			return Math.Atan2((double)array[1],(double)array[0]);

		}

		/// <summary>
		/// ���ط�˫������ֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>


		[Desp("ACOSH(number)","���� number �����ķ�˫������ֵ������������ڻ���� 1����˫������ֵ��˫�����Ҽ�Ϊ�ú����Ĳ�������� ACOSH(COSH(number)) ���� number��",
			 "Number    ���ڵ��� 1 ��ʵ����")]
		public double f_cosh(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("cosh�����Ĳ�������");

			return Math.Cosh((double)array[0]);

		}


		/// <summary>
		/// �׳�
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("FACT(number)","�������Ľ׳ˣ�һ�����Ľ׳˵��� 1*2*3*...* ������",
			 "Number    Ҫ������׳˵ķǸ������������� Number �������������βȡ����")]
		public double f_fact(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("fact�����Ĳ�������");

			int fact=(int)(double)array[0],result=1;

			if(fact<0)
				throw new Exception("�׳˺���fack�Ĳ��������Ǹ���");

			if(fact==0)return 1;

			while(fact!=0)
			{
				result*=fact--;
			}


			return result;


			
		}


		/// <summary>
		/// ����������ȡ��
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("INT(number)","�������������뵽��ӽ���������",
			 "Number    ��Ҫ������������ȡ����ʵ����")]

		public double f_int(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("int�����Ĳ�������");

			return (int)(double)array[0];
		}


		/// <summary>
		/// ����Ȼ����
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>


		[Desp("LN(number)","����һ��������Ȼ��������Ȼ�����Գ����� e (2.71828182845904) Ϊ�ס�",
			 "Number    �����ڼ�������Ȼ��������ʵ����")]
		public double f_ln(ArrayList array)
		{

			if(array.Count!=1)
				throw new Exception("ln�����Ĳ�������");

			double r=(double)array[0];

			if(r<=0)
			{
				throw new Exception("ln�����Ĳ������������");
			}


			return Math.Log(r,Math.E);
		}




		/// <summary>
		/// ���س���e
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		
		[Desp("E()","���س���E��",
			 "")]
		public double f_e(ArrayList array)
		{
			if(array.Count!=0)
				throw new Exception("e()�����Ĳ�����������Ϊ0");

			return Math.E;
		}



		/// <summary>
		/// ���ݵ����������ֵĶ���
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("LOG(number,base)","����ָ���ĵ���������һ�����Ķ�����",
			 "Number    Ϊ���ڼ����������ʵ����Base    Ϊ�����ĵ��������ʡ�Ե������ٶ���ֵΪ10��")]
		public double f_log(ArrayList array)
		{

			if(array.Count!=2)
				throw new Exception("log�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1];

			if(r1<=0||r2<=0)
				throw new Exception("log�����Ĳ����������0");

			return Math.Log(r1,r2);

		}

		/// <summary>
		/// �������ȡ����
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("MOD(number,divisor)","�������������������������������������ͬ��",
			 "Number    Ϊ��������Divisor    Ϊ������")]
		public double f_mod(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("mod�����Ĳ�������Ϊ����");

			int r1=(int)(double)array[0],r2=(int)(double)array[1];

			return r1%r2;
		}

		/// <summary>
		/// �������ĳ���
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[Desp("POWER(number,power)","���ظ������ֵĳ��ݡ�",
			 "Number    ����������Ϊ����ʵ����Power    ָ������������ָ�����ݳ˷���")]
		public double f_power(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("power�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1];

			return Math.Pow(r1,r2);
		}




		/// <summary>
		/// ��˫������ֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		
		[Desp("SINH(number)","����ĳһ���ֵ�˫������ֵ��",
			 "Number    Ϊ����ʵ����")]
		public double f_sinh(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("sinh�����Ĳ�������Ϊһ��");

			double r1=(double)array[0];

			return Math.Sinh(r1);

		}

		/// <summary>
		/// ��˫������ֵ
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[Desp("TANH(number)","����ĳһ���ֵ�˫�����С�",
			 "Number    Ϊ����ʵ����")]
		public double f_tanh(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("tanh�����Ĳ�������Ϊһ��");

			double r1=(double)array[0];
			

			return Math.Tanh(r1);

		}



		/// <summary>
		/// ��ָ����λ������ֵ������������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[Desp("ROUND(number,num_digits)","����ĳ�����ְ�ָ��λ��ȡ��������֡�",
			 "Number    ��Ҫ����������������֡�Num_digits    ָ����λ��������λ�������������롣")]
		public double f_round(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("round�����Ĳ�������Ϊ����");

			double r1=(double)array[0];
			int r2=(int)(double)array[1];

			return Math.Round(r1,r2);
		}


		/// <summary>
		/// ���п�������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		
		[Desp("SQRT(number)","������ƽ������",
			 "Number    Ҫ����ƽ����������")]
		public double f_sqrt(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("sqrt�����Ĳ�������Ϊһ��");

			double r1=(double)array[0];
			if(r1<0)throw new Exception("sqrt�Ĳ������������");

			return Math.Sqrt(r1);
		}


	

		/// <summary>
		/// e��������
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		[Desp("exp(number)","������Ȼ����e��number�η���",
			 "Number    ��ָ����")]
		public double f_exp(ArrayList array)
		{
			if(array.Count!=1)
				throw new Exception("exp�����Ĳ�������Ϊһ��");
			double r1=(double)array[0];
			

			return Math.Pow(Math.E,r1);



		}

		/// <summary>
		/// ���number1����number2 ����1,���򷵻�0
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("big(number1,number2)","���number1>number2 ����1,���򷵻�0",
			 "number1,number2 Ҫ�Ƚϵ�������")]
		public double f_big(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("big�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1];

			return r1>r2?1:0;
		}

		
		/// <summary>
		/// ���number1���ڵ���number2 ����1,���򷵻�0
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("bigeq(number1,number2)","���number1>number2 ����1,���򷵻�0",
			 "number1,number2 Ҫ�Ƚϵ�������")]
		public double f_bigeq(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("bigeq�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1];

			return r1>=r2?1:0;
		}

		/// <summary>
		/// ���number1С��number2 ����1,���򷵻�0
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("sml(number1,number2)","���number1<number2 ����1,���򷵻�0",
			 "number1,number2 Ҫ�Ƚϵ�������")]
		public double f_sml(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("sml�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1];

			return r1<r2?1:0;
		}

		/// <summary>
		/// ���number1С�ڵ���number2 ����1,���򷵻�0
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("smleq(number1,number2)","���number1<number2 ����1,���򷵻�0",
			 "number1,number2 Ҫ�Ƚϵ�������")]
		public double f_smleq(ArrayList array)
		{
			if(array.Count!=2)
				throw new Exception("smleq�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1];

			return r1<=r2?1:0;
		}
		/// <summary>
		/// ���number1����number2��number3֮�� ����1,���򷵻�0
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("bet(number1,number2)","���number1����number2��number3֮�� ����1,���򷵻�0",
			 "number1,number2,number3 Ҫ�Ƚϵ�������")]
		public double f_bet(ArrayList array)
		{
			if(array.Count!=3)
				throw new Exception("bet�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1],r3=(double)array[2];

			double t=0.0;
			if(r2>r3)
			{
				t=r2;
				r2=r3;
				r3=t;
			}

			if(r1>r2&&r1<r3)return 1;
			else return 0;


		}

		/// <summary>
		/// ���number1����number2��number3֮��,�������� ����1,���򷵻�0
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>

		[Desp("beteq(number1,number2,number3)","���number1����number2��number3֮�� ����1,���򷵻�0",
			 "number1,number2,number3 Ҫ�Ƚϵ�������")]
		public double f_beteq(ArrayList array)
		{
			if(array.Count!=3)
				throw new Exception("beteq�����Ĳ�������Ϊ����");

			double r1=(double)array[0],r2=(double)array[1],r3=(double)array[2];

			double t=0.0;
			if(r2>r3)
			{
				t=r2;
				r2=r3;
				r3=t;
			}

			if(r1>=r2&&r1<=r3)return 1;
			else return 0;


		}




		}
}
