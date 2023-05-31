/*
 * 由SharpDevelop创建。
 * 用户： guyue
 * 日期: 2018/8/4
 * 时间: 14:53
 * 
 */
using System;
using System.Drawing;

namespace Dwg2Json.Util
{
	/// <summary>
	/// Description of Maths.
	/// </summary>
	public static class Maths
	{
		/// <summary>
		/// 求两点之间的距离
		/// </summary>
		/// <param name="p1">点1</param>
		/// <param name="p2">点2</param>
		/// <returns>距离</returns>
		public static double GetDistance(Point p1, Point p2)
		{
			return Math.Sqrt((p2.X-p1.X)*(p2.X-p1.X)+(p2.Y-p1.Y)*(p2.Y-p1.Y));			
		}
		/// <summary>
		/// 求三角形某个内角的余弦
		/// </summary>
		/// <param name="a">邻边的长度</param>
		/// <param name="b">邻边的长度</param>
		/// <param name="c">对边的长度</param>
		/// <returns>返回cos值</returns>
		public static double GetCos(double a,double b,double c)
		{
			if(a+b>c&&b+c>a&&a+c>b){
				return (a*a+b*b-c*c)/(2*a*c);
			}
			return 1;
		}
		/// <summary>
		/// 点到直线的距离
		/// </summary>
		/// <param name="p">点p</param>
		/// <param name="s">直线所在点s</param>
		/// <param name="e">直线所在点e</param>
		/// <returns>距离</returns>
		public static double GetDisH(Point p, Point s,Point e)
		{
			double A =e.Y-s.Y;
			double B =s.X - e.X;
			double C = s.Y*(e.X-s.X)-s.X*(e.Y-s.Y);
			double dis = Math.Abs(p.X*A+B*p.Y+C)/Math.Sqrt(A*A+B*B);
            return dis;
		}
		/// <summary>
		/// 点到线段的距离
		/// </summary>
		/// <param name="p">点</param>
		/// <param name="s">线段起始点</param>
		/// <param name="e">线段终点</param>
		/// <returns>距离</returns>
		public static double GetDis(Point p, Point s,Point e){
			double dis =-1;
			double a =GetDistance(p,s),b = GetDistance(p,e),c =GetDistance(s,e);
			double cosB = GetCos(a,c,b);double cosC = GetCos(b,c,a);
			if(cosB>0&&cosB<1&&cosC>0&&cosC<1){
				dis = GetDisH(p,s,e);
			}
			else{
				if(a<b){
					dis = a;
				}
				else{
					dis =b;
				}
			}
			return dis;
		}
	}
}
