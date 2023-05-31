using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Teigha.Geometry;

namespace Dwg2Json.Util
{
    public class JsonPolyline
    {
        public string name;
        public string area;
        public string description;
        public string wind;
        public string relLoc;
        public string relDir;
        public string relDis;
        public string isSafe;
        public Point3d[] coordinates;
        private int num = 0;
        public JsonPolyline() { }
        public JsonPolyline(List<Point3d> pointlist)
        {
            if (pointlist != null)
            {
                num = pointlist.Count;
                coordinates = new Point3d[num];
                for (int i = 0; i < num; i++)
                {
                    coordinates[i] = pointlist[i];
                }
            }   
        }
         public override string ToString()
         {
            string diff = "\"";
            string ent = "\r\n";
            string tab = "\t";
            string str = ent+ "{"+tab+ diff + "name" + diff + ":" + diff + this.name + diff + ","+ent;
            str += tab + diff + "area" + diff + ":" + diff + this.area + diff + "," + ent;
            str += tab +diff + "description" + diff + ":" + diff + this.description + diff + "," + ent;
            str += tab + diff +"wind"+diff+":"+diff+this.wind+diff + "," + ent;
            str += tab + diff + "relLoc" + diff + ":" + diff + this.relLoc + diff + "," + ent;
            str += tab + diff + "relDir" + diff + ":" + diff + this.relDir + diff + "," + ent;
            str += tab + diff + "relDis" + diff + ":" + diff + this.relDis + diff + "," + ent;
            str += tab + diff + "isSafe" + diff + ":" + diff + this.isSafe + diff + "," + ent;
            str += tab + diff + "coordinates" + diff + ":"+"[" + ent;

            for (int i = 0; i < coordinates.Length; i++)
         {
             
             if (i == (coordinates.Length - 1))
             {
                    str += tab + tab + "[" + coordinates[i].X + "," + coordinates[i].Z + "," + coordinates[i].Y + "]" + ent;
              }
                else
                {
                    str += tab + tab + "[" + coordinates[i].X + "," + coordinates[i].Z + "," + coordinates[i].Y + "],"+ent;
                }
         }
         return str + "]}" + ent;
        }
        /// <summary>
        /// 重载Equals方法
        /// </summary>
        /// <param name="jP"></param>
        /// <returns></returns>
        public bool Equals(JsonPolyline jP)
        {
            if(jP == null||this ==null)
                return false;
            
            for (int i = 0; i < this.coordinates.Length; i++)
            {
                if(this.coordinates[i].X!= jP.coordinates[i].X|| this.coordinates[i].Y != jP.coordinates[i].Y|| this.coordinates[i].Z != jP.coordinates[i].Z)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断某个二维点包不包含在折线内
        /// </summary>
        /// <param name="point2D"></param>
        /// <returns></returns>
        public bool Contain(Point2d point2D)
        {
            if (point2D != null && this != null)
            {
                foreach(var tmp in this.coordinates)
                {
                    if(tmp.X == point2D.X && tmp.Y == point2D.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public double Distance(Point point)
        {
            double distance = -1;
            if (point != null&& this != null)
            {
                var ar3d = this.coordinates;
                distance = Maths.GetDistance(point, new Point((int)ar3d[0].X, (int)ar3d[0].Y));
                for (int i =0;i<ar3d.Length-1;i++)
                {
                    Point s = new Point((int)ar3d[i].X, (int)ar3d[i].Y);
                    Point e = new Point((int)ar3d[i+1].X, (int)ar3d[i+1].Y);
                    if(distance> Maths.GetDis(point, s, e))
                    {
                        distance = Maths.GetDis(point, s, e);
                    }
                }
            }
            return distance;
        }
    }
}
