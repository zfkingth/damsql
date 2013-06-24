using System;
using System.Collections;

namespace hammergo.caculator
{
	
	/// <summary>
	/// �������͵�ö�ٽṹ,��ʶ��:Identifier,������,FunName,
	/// ������:+,-,*,/,������(,leftp,������),rightp
	/// </summary>
	public enum WordType
	{
		/// <summary>
		/// +
		/// </summary>
		Plus=0,

		/// <summary>
		/// -
		/// </summary>
		Minus=1,

		/// <summary>
		/// *
		/// </summary>
		Mul=2,

		/// <summary>
		/// /
		/// </summary>
		Div=3,


		/// <summary>
		/// ^
		/// </summary>
		Power=4,

		/// <summary>
		/// (
		/// </summary>
		Leftp=5,




		
		
		/// <summary>
		/// )
		/// </summary>
		Rightp,

		/// <summary>
		/// ����
		/// </summary>
		Number,

		
		/// <summary>
		/// ��ʶ��
		/// </summary>
		
		Identifier,
		/// <summary>
		/// ������
		/// </summary>
		FunName,

		
	
	
	
	
		/// <summary>
		/// ����ı�ʶ��������
		/// </summary>
		IdentifierWithDot,
		/// <summary>
		/// �ո�
		/// </summary>

		Blank,
		/// <summary>
		/// ����
		/// </summary>
		Comma


	}


	
	
/// <summary>
/// ɨ����
/// </summary>
	internal abstract class AbstractScan
	{
		/// <summary>
		/// ���嵥�ʵ���󳤶�
		/// </summary>
		public  const int MaxSize=128;
		
		protected  char ch;//������¶������ַ�
		protected  char [] strToken=new char[MaxSize];//��ŵ��ʷ��ŵ��ַ���

		protected  string calcStr;//���ʽ�ַ���

		protected  LinkList list=new LinkList();//��ŵ�����Ϣ��˫������

		

		protected  int index=0;//����ַ�������

		protected  static int stateCount=20;//DFA����״̬�ĸ���,״̬ת���������
		protected  static  int colCount=15;//״̬ת���������

		protected  static int [,] states;//״̬ת����

		

		protected  int state=0;//NFA�ĵ�ǰ״̬

		protected int strTokenIndex=0;//����strToken���±�

	

		protected AbstractScan()
		{
			
		}
	
//		/// <summary>
//		/// ����һ��Scan��
//		/// </summary>
//		public Scan()
//		{
//			setStates();
//		}
//		

		/// <summary>
		/// ��ɨ���ַ����л�ȡһ���ַ���������ɨ���ַ���βʱ����'\0'
		/// </summary>
		/// <returns></returns>
		protected char getChar()
		{
			if(index==calcStr.Length)
				return '\0';//���ʽɨ�����
			else
			{
				return calcStr[index++];
			}

		}

		/// <summary>
		/// ������Ҫɨ��ı��ʽ
		/// </summary>
		/// <param name="calcString"></param>
		protected void setString(string calcString)
		{
			calcStr=calcString;
			index=0;
			state=0;
			
			clearToken();
		}


		/// <summary>
		///�ж��ַ��Ƿ���a-z,A-Z�е�һ���ַ�
		/// </summary>
		/// <param name="achar"></param>
		/// <returns></returns>
		protected  bool isLetter(char achar)
		{
			if((achar>='a'&&achar<='z')||(achar>='A'&&achar<='Z'))
				return true;
			else return false;
		}

		/// <summary>
		/// �ж��ַ��Ƿ���һ������
		/// </summary>
		/// <param name="achar"></param>
		/// <returns></returns>
		protected bool isDigit(char achar)
		{
			if(achar>='0'&&achar<='9')
				return true;
			else return false;
		}


		/// <summary>
		/// ���ַ����뵽strToken������
		/// </summary>
		/// <param name="c"></param>
		protected  void addToToken(char c)
		{
			if(strTokenIndex<strToken.Length)
				strToken[strTokenIndex++]=c;
			else
				throw new Exception(string.Format("����Խ�磬�������ʵ���󳤶�{0}",MaxSize));
		}

		/// <summary>
		/// ������strToken���������
		/// </summary>
		protected  void clearToken()
		{
			for(int i=0;i<strToken.Length;i++)
				strToken[i]='\0';
			strTokenIndex=0;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		protected  abstract int getCharType(char c);
//		{
//			switch(c)
//			{
//				case 'E':return 1;
//				case 'e':return 1;
//				case ' ':return 4;
//				case ',':return 5;
//				case '.':return 6;
//				case '+':return 7;
//				case '-':return 8;
//				case '*':return 9;
//				case '/':return 10;
//				
//				case '(':return 11;
//				case ')':return 12;
//				case '^':return 13;
//				case '!':return 14;
//
//			}
//
//			if(isLetter(c))return 2;
//			if(isDigit(c))return 3;
//
//
//			//û��ƥ�������
//			return -1;
//		}


	

		/// <summary>
		/// �����ַ���
		/// </summary>
		/// <returns></returns>
		public LinkList parse(string str)
		{
			setString(str.Trim());
			list.removeAll();
			ch=this.getChar();
			while(ch!='\0')
			{
				int typeCode=getCharType(ch);//����ַ�ch��������
				if(typeCode==-1)
				throw new Exception("���ʽ�в��ܳ����ַ�"+ch);

				int newstate=states[state,typeCode];
				if(newstate==-2)//û����ƥ���״̬
				{
					if(states[state,0]==-1)//stateΪ�ս�״̬
					{
						//һ�����ʵ�ɨ�������
						addToList(state);

						//����״̬
						state=0;//����ɨ����һ������
						clearToken();

						continue;//ֱ�ӽ�����һ��ѭ��������getChar();


					}
					else  //������ȷ��ƥ�䣬���ִ���
					{
						throw new Exception("���ʽ����");
					}
				}
				else //��ƥ���״̬
				{
					state=newstate;
					if(state!=0)//��ֹ�����Ե��ַ����뵽strToken��
					addToToken(ch);//���ַ��ӵ�������

				}
				ch=getChar();
			}
			
			if(states[state,0]==-1)//�ж����һ�����ʵ�״̬
			{
				addToList(state);
				
			}
			else
				throw new Exception("���ʽ�е�ĩβ����");


			list.modify();
			return list;
		}

		/// <summary>
		/// ����ɨ����ɺ󣬽����ʼ��뵽list��
		/// </summary>
		/// <param name="state"></param>

		private void addToList(int state)
		{
			Word w;//Ҫ���뵽����ĵ���
			switch(state)
			{
				case 1:w=new Word(WordType.Identifier,makeString(strToken)); break;
				case 3:w=new Word(WordType.IdentifierWithDot,makeString(strToken)); break;//����ı�ʶ��
				case 4:w=new Word(WordType.Plus,makeString(strToken)); break;
				case 5:w=new Word(WordType.Minus,makeString(strToken)); break;
				case 6:w=new Word(WordType.Number,makeString(strToken)); break;
				case 8: w=new Word(WordType.Number,makeString(strToken)); break;
				case 11:w=new Word(WordType.Number,makeString(strToken)); break;
				case 12:w=new Word(WordType.Mul,makeString(strToken)); break;
				case 13:w=new Word(WordType.Div,makeString(strToken)); break;
				case 14:w=new Word(WordType.Leftp,makeString(strToken)); break;
				case 15:w=new Word(WordType.Rightp,makeString(strToken)); break;
				case 17:w=new Word(WordType.Comma,makeString(strToken));break;
				case 18:w=new Word(WordType.Power,makeString(strToken));break;
				default:
					throw new Exception("����״̬�������");

			}

			
			
			
			list.add(new LinkNode(w));

			
		}
		

		/// <summary>
		/// �����ַ���
		/// </summary>
		/// <param name="chars"></param>
		/// <returns></returns>
		protected  string makeString(char [] chars)
		{
			int index=0;
			for(;index<chars.Length;index++)
			{
				if(chars[index]=='\0')
				break;
			}

			return new string(chars,0,index);

			

		}



		
	}


}
