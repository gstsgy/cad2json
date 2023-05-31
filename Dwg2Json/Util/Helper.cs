using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Teigha.Runtime;

namespace Dwg2Json.Util
{
    class Helper
    {
        private static List<JsonLine> listLine = new List<JsonLine>();
        private static List<JsonPolyline> listPolyline = new List<JsonPolyline>();
        private static FieldInfo[] p;
        static double maxZ, minZ;
        static double imxZ, imiZ;
        static int count;
        static Helper()
        {
            p = new JsonPolyline().GetType().GetFields();
        }
        private Helper() { }
        public static void Llinit()
        {
            listLine.Clear();
            maxZ = minZ = 0;
        }
        public static void Lpinit()
        {
            listPolyline.Clear();
        }
        public static void LladdLine(JsonLine jsonLine)
        {
            listLine.Add(jsonLine);
        }
        public static void LlChange(int i)
        {
            listLine[i].enable = true;
        }
        public static List<JsonLine> GetListLine()
        {
            return listLine;
        }
        public static void LpaddLine(JsonPolyline jsonPolyline)
        {
            JsonPolyline j = new JsonPolyline();
            for (int i = 0; i < p.Length; i++)
            {
                string name = p[i].Name;
                if (name != "coordinates")
                {
                    p[i].SetValue(j, p[i].GetValue(jsonPolyline));
                }
            }
            j.coordinates = new Point3d[jsonPolyline.coordinates.Length];
            for (int i = 0; i < jsonPolyline.coordinates.Length; i++)
            {
                j.coordinates[i] = new Point3d(jsonPolyline.coordinates[i].X, jsonPolyline.coordinates[i].Y, jsonPolyline.coordinates[i].Z);
            }
            listPolyline.Add(j);
        }
        public static List<JsonPolyline> GetlistPolyline()
        {
            List<JsonPolyline> l = new List<JsonPolyline>();
            foreach (var tmp in listPolyline)
            {
                JsonPolyline j = new JsonPolyline();
                for (int i = 0; i < p.Length; i++)
                {
                    string name = p[i].Name;
                    if (name != "coordinates")
                    {
                        p[i].SetValue(j, p[i].GetValue(tmp));
                    }
                }
                j.coordinates = new Point3d[tmp.coordinates.Length];
                for (int i = 0; i < tmp.coordinates.Length; i++)
                {
                    j.coordinates[i] = new Point3d(tmp.coordinates[i].X, tmp.coordinates[i].Y, tmp.coordinates[i].Z);
                }
                l.Add(j);
            }
            return l;
        }

        /// <summary>
        /// List<JsonLine>运算
        /// </summary>
        /// <param name="list">原来的List<JsonLine></param>
        /// <param name="x">偏移量</param>
        /// <param name="y">偏移量</param>
        /// <returns>新的List<JsonLine></returns>
        public static List<JsonLine> ComputeLl(List<JsonLine> list, double x, double y)
        {
            List<JsonLine> l = new List<JsonLine>();
            foreach (var tmp in list)
            {
                JsonLine j = new JsonLine();
                j.color = tmp.color;
                j.enable = tmp.enable;
                j.x1 = tmp.x1 - x;
                j.y1 = tmp.y1 - y;
                j.z1 = tmp.z1;
                j.z2 = tmp.z2;
                j.x2 = tmp.x2 - x;
                j.y2 = tmp.y2 - y;
                l.Add(j);
            }
            return l;
        }
        public static List<JsonPolyline> ComputeLl(List<JsonPolyline> list, double x, double y)
        {
            List<JsonPolyline> l = new List<JsonPolyline>();
            if (list == null)
            {
                return l;
            }
            foreach (var tmp in list)
            {
                JsonPolyline j = new JsonPolyline();
                for (int i = 0; i < p.Length; i++)
                {
                    string name = p[i].Name;
                    if (name != "coordinates")
                    {
                        p[i].SetValue(j, p[i].GetValue(tmp));
                    }
                }
                j.coordinates = new Point3d[tmp.coordinates.Length];
                for (int i = 0; i < tmp.coordinates.Length; i++)
                {
                    j.coordinates[i] = new Point3d(tmp.coordinates[i].X - x, tmp.coordinates[i].Y - y, tmp.coordinates[i].Z);
                }
                l.Add(j);
            }
            return l;
        }
        /// <summary>
        /// List<JsonLine>缩放
        /// </summary>
        /// <param name="list">原List<JsonLine></param>
        /// <param name="mul">缩放量</param>
        /// <returns>缩放后的List<JsonLine></returns>
        public static List<JsonPolyline> Sf(List<JsonPolyline> list, int mul)
        {
            List<JsonPolyline> l = new List<JsonPolyline>();
            if (list == null)
            {
                return l;
            }
            if (mul > 0)
            {
                foreach (var tmp in list)
                {
                    JsonPolyline j = new JsonPolyline();
                    for (int i = 0; i < p.Length; i++)
                    {
                        string name = p[i].Name;
                        if (name != "coordinates")
                        {
                            p[i].SetValue(j, p[i].GetValue(tmp));
                        }
                    }
                    j.coordinates = new Point3d[tmp.coordinates.Length];
                    for (int i = 0; i < tmp.coordinates.Length; i++)
                    {
                        j.coordinates[i] = new Point3d(tmp.coordinates[i].X / mul, tmp.coordinates[i].Y / mul, tmp.coordinates[i].Z);
                    }
                    l.Add(j);
                }
            }
            else
            {
                int tmpmul = Math.Abs(mul - 1);
                foreach (var tmp in list)
                {
                    JsonPolyline j = new JsonPolyline();
                    for (int i = 0; i < p.Length; i++)
                    {
                        string name = p[i].Name;
                        if (name != "coordinates")
                        {
                            p[i].SetValue(j, p[i].GetValue(tmp));
                        }
                    }
                    j.coordinates = new Point3d[tmp.coordinates.Length];
                    for (int i = 0; i < tmp.coordinates.Length; i++)
                    {
                        j.coordinates[i] = new Point3d(tmp.coordinates[i].X * tmpmul, tmp.coordinates[i].Y * tmpmul, tmp.coordinates[i].Z);
                    }
                    l.Add(j);
                }
            }
            return l;
        }
        public static List<JsonLine> Sf(List<JsonLine> list, int mul, CheckedListBox listBox)
        {
            List<int> index = new List<int>();
            if (listBox.Items.Count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listBox.GetItemChecked(i))
                    {
                        index.Add(i);
                    }
                }
            }
            List<JsonLine> l = new List<JsonLine>();
            if (mul > 0)
            {
                foreach (var tmp in list)
                {
                    bool have = false;
                    foreach (int num in index)
                    {
                        if (tmp.z1 > (imiZ + (num - 1) * 100) && tmp.z1 < (imiZ + num * 100))
                        {
                            have = true;
                        }
                    }
                    if (!have)
                    {
                        continue;
                    }
                    JsonLine j = new JsonLine();
                    j.enable = tmp.enable;
                    j.color = tmp.color;
                    j.x1 = tmp.x1 / mul;
                    j.y1 = tmp.y1 / mul;
                    j.x2 = tmp.x2 / mul;
                    j.y2 = tmp.y2 / mul;
                    j.z1 = tmp.z1;
                    j.z2 = tmp.z2;
                    l.Add(j);
                }
            }
            else
            {
                int tmpmul = Math.Abs(mul - 1);
                foreach (var tmp in list)
                {
                    bool have = false;
                    foreach (int num in index)
                    {
                        if (tmp.z1 > (imiZ + (num - 1) * 100) && tmp.z1 < (imiZ + num * 100))
                        {
                            have = true;
                        }
                    }
                    if (!have)
                    {
                        continue;
                    }
                    JsonLine j = new JsonLine();
                    j.enable = tmp.enable;
                    j.color = tmp.color;
                    j.x1 = tmp.x1 * tmpmul;
                    j.y1 = tmp.y1 * tmpmul;
                    j.x2 = tmp.x2 * tmpmul;
                    j.y2 = tmp.y2 * tmpmul;
                    j.z1 = tmp.z1;
                    j.z2 = tmp.z2;
                    l.Add(j);
                }
            }
            return l;
        }
        /// <summary>
        /// 返回原坐标
        /// </summary>
        /// <param name="point"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point3d Goreturn(Point3d point, int mul, double x, double y)
        {

            Point3d point3;
            point3 = new Point3d(point.X + x, point.Y + y, point.Z);
            if (mul > 0)
            {
                point3 = new Point3d(point3.X * mul, point3.Y * mul, point.Z);
            }
            else
            {
                int tmp = Math.Abs(mul - 1);
                point3 = new Point3d(point3.X / tmp, point3.Y / tmp, point.Z);
            }

            return point3;
        }
        /// <summary>
        /// 返回原坐标
        /// </summary>
        /// <param name="point"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<Point3d> GoCom(List<Point3d> point, double x, double y)
        {
            List<Point3d> l = new List<Point3d>();
            if (point.Count == 0)
            {
                return l;
            }
            foreach (var tmp in point)
            {
                Point3d point3 = new Point3d(tmp.X - x, tmp.Y - y, tmp.Z);
                l.Add(point3);
            }
            return l;
        }
        /// <summary>
        /// list转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="l"></param>
        /// <returns></returns>
        public static string List2Json<T>(List<T> l)
        {
            string str = "[";
            for (int i = 0; i < l.Count; i++)
            {
                if (i == l.Count - 1)
                {
                    str += l[i].ToString();
                }
                else
                {
                    str += l[i].ToString() + ",";
                }
            }
            return str += "]";
        }
        /// <summary>
        /// 查找离某点最近的巷道
        /// </summary>
        /// <param name="point2D"></param>
        /// <returns></returns>
        public static JsonPolyline GetPolylineByPoint(Point point2D)
        {
            JsonPolyline jp; int count = 0;
            if (listPolyline.Count > 0 && point2D != null)
            {
                double distance = listPolyline[0].Distance(point2D);
                for (int j = 0; j < listPolyline.Count; j++)
                {
                    if (distance > listPolyline[j].Distance(point2D))
                    {
                        distance = listPolyline[j].Distance(point2D);
                        count = j;
                    }
                }
                if (distance < 50)
                {
                    jp = new JsonPolyline();
                    for (int i = 0; i < p.Length; i++)
                    {
                        string name = p[i].Name;
                        if (name != "coordinates")
                        {
                            p[i].SetValue(jp, p[i].GetValue(listPolyline[count]));
                        }
                    }
                    jp.coordinates = new Point3d[listPolyline[count].coordinates.Length];
                    for (int i = 0; i < listPolyline[count].coordinates.Length; i++)
                    {
                        jp.coordinates[i] = new Point3d(listPolyline[count].coordinates[i].X, listPolyline[count].coordinates[i].Y, listPolyline[count].coordinates[i].Z);
                    }
                    listPolyline.RemoveAt(count);
                    return jp;
                }
            }
            return null;
        }
        public static void Json2List(string fname, int num, CheckedListBox listBox)
        {
            string tunnelsFilePath = Path.Combine(Directory.GetCurrentDirectory(), fname);
            string tunnelsJson = FileUtil.ReadFile(tunnelsFilePath, Encoding.UTF8);
            JArray tunnelsJArr = (JArray)JsonConvert.DeserializeObject(tunnelsJson);
            //JsonWriter JW =  
            if (tunnelsJArr == null || tunnelsJArr.Count == 0)
                return;
            if (num == 1)
            {
                foreach (var tmp in tunnelsJArr)
                {
                    //先解析出coordinates
                    string points = tmp["coordinates"].ToString();
                    points = points.Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                    string[] sArray = points.Split(',');
                    JsonPolyline j = new JsonPolyline();
                    j.coordinates = new Point3d[sArray.Length / 3];
                    for (int i = 0; i < sArray.Length; i = i + 3)
                    {
                        j.coordinates[i / 3] = new Point3d(double.Parse(sArray[i]), double.Parse(sArray[i + 2]), double.Parse(sArray[i + 1]));
                    }
                    // string vvv = tmp["desription"].ToString();
                    //利用反射取出其他值
                    FieldInfo[] jp = j.GetType().GetFields();
                    for (int i = 0; i < jp.Length; i++)
                    {
                        if (jp[i].Name != "coordinates")
                        {
                            jp[i].SetValue(j, tmp[jp[i].Name].ToString());
                        }
                    }
                    LpaddLine(j);
                }
            }
            else
            {
                maxZ = minZ = (double)tunnelsJArr[0]["z1"];
                foreach (var tmp in tunnelsJArr)
                {
                    JsonLine j = new JsonLine();
                    FieldInfo[] jp = j.GetType().GetFields();
                    for (int i = 0; i < jp.Length; i++)
                    {
                        if (jp[i].Name != "enable" && jp[i].Name != "color" && jp[i].Name != "layer")
                        {
                            if ((double)tmp["z1"] > maxZ)
                            {
                                maxZ = (double)tmp["z1"];
                            }
                            if ((double)tmp["z2"] > maxZ)
                            {
                                maxZ = (double)tmp["z2"];
                            }
                            if ((double)tmp["z1"] < minZ)
                            {
                                minZ = (double)tmp["z1"];
                            }
                            if ((double)tmp["z2"] < minZ)
                            {
                                minZ = (double)tmp["z2"];
                            }
                            jp[i].SetValue(j, ((double)tmp[jp[i].Name]));
                        }
                        else if (jp[i].Name == "enable")
                        {
                            jp[i].SetValue(j, (tmp[jp[i].Name].ToString() == "1" ? true : false));
                        }
                        else
                        {
                            jp[i].SetValue(j, tmp[jp[i].Name].ToString());
                        }
                    }
                    LladdLine(j);
                    HeightList(listBox);
                }
            }
        }
        /// <summary>
        /// 读取CAD文件
        /// </summary>
        /// <param name="fname"></param>
        public static void ReadCAD(string fname, CheckedListBox listBox)
        {

            using (Services ser = new Services())
            {
                Database db = new Database(false, false);
                db.ReadDwgFile(fname, System.IO.FileShare.Read, false, null);
                using (var trans = db.TransactionManager.StartTransaction())
                {
                    BlockTableRecord btrec = (BlockTableRecord)trans.GetObject(db.CurrentSpaceId, OpenMode.ForRead);
                    string type1 = "";
                    maxZ = -9999;
                    minZ = 9999;
                    foreach (ObjectId objid in btrec)
                    {
                        // if(objid.)
                        Entity ent;
                        try { ent = trans.GetObject(objid, OpenMode.ForRead) as Entity; }
                        catch (Teigha.Runtime.Exception ee)
                        {
                            continue;
                        }
                        //if(ent.Layer.)
                        string layer = ent.Layer;
                        string elem = ent.GetType().Name;
                        type1 += ent.Color.ToString() + ",";
                        if (elem.Equals("Line"))
                        {



                            Line line = (Line)ent;
                            if (line.StartPoint.Z > maxZ)
                            {
                                maxZ = line.StartPoint.Z;
                            }
                            if (line.EndPoint.Z > maxZ)
                            {
                                maxZ = line.EndPoint.Z;
                            }
                            if (line.StartPoint.Z < minZ)
                            {
                                minZ = line.StartPoint.Z;
                            }
                            if (line.EndPoint.Z < minZ)
                            {
                                minZ = line.EndPoint.Z;
                            }
                            LladdLine(new JsonLine(line));

                            //&& layer.Equals("图层")                           
                        }
                    }
                }
                db.Dispose();
                HeightList(listBox);
            }
        }

        public static object SurPoint(Point point, List<JsonLine> ll)
        {
            Point3d p = new Point3d();
            double cx = point.X - ll[0].x1;
            double cy = point.Y - ll[0].y1;
            double cl = Math.Sqrt(cx * cx + cy * cy);
            foreach (var tmp in ll)
            {
                double dlx = point.X - tmp.x1;
                double dly = point.Y - tmp.y1;
                double dis = Math.Sqrt(dlx * dlx + dly * dly);
                if (cl > dis)
                {
                    cl = dis;
                    p = new Point3d(tmp.x1, tmp.y1, tmp.z1);
                }
                dlx = point.X - tmp.x2;
                dly = point.Y - tmp.y2;
                dis = Math.Sqrt(dlx * dlx + dly * dly);
                if (cl > dis)
                {
                    cl = dis;
                    p = new Point3d(tmp.x2, tmp.y2, tmp.z2);
                }

            }
            if (cl > 20)
                return null;
            return p;
        }

        public static void HeightList(CheckedListBox listBox)
        {           
            if (maxZ - minZ == 0 || minZ > maxZ|| listBox==null)
                return;
            imxZ = ((int)maxZ / 100) * 100;
            imiZ = ((int)minZ / 100) * 100;
            count = ((int)(imxZ - imiZ) / 100) + 2;
            listBox.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    listBox.Items.Add(minZ + "～" + (imiZ));
                }
                else if (i == count - 1)
                {
                    listBox.Items.Add(imxZ + "～" + (maxZ));
                }
                else
                {

                    listBox.Items.Add((imiZ + (i - 1) * 100) + "～" + (imiZ + i * 100));
                }
                listBox.SetItemChecked(i, true);
            }


        }
    }
}
