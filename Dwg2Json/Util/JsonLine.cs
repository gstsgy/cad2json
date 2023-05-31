using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teigha.DatabaseServices;

namespace Dwg2Json.Util
{
    /// <summary>
    /// JsonLine类，
    /// </summary>
    public class JsonLine
    {

        public Boolean enable;
        public double x1 = 0, y1 = 0, z1 = 0, x2 = 0, y2 = 0, z2 = 0;
        public string color = "";
        public string layer = "";
        public JsonLine() { }
       
        public JsonLine(Line line)
        {
            x1 = line.StartPoint.X;
            y1 = line.StartPoint.Y;
            z1 = line.StartPoint.Z;
            x2 = line.EndPoint.X;
            y2 = line.EndPoint.Y;
            z2 = line.EndPoint.Z;
            int r = line.Color.Red;
            int g = line.Color.Green;
            int b = line.Color.Blue;
            color = string.Format("#{0:x2}{1:x2}{2:x2}", r, g, b);
            layer = line.Layer;
            enable = false;
        }
        public override string  ToString()
        {
            string diff = "\"";
            string json = "{" + diff + "enable" + diff + ":" + (this.enable ? 1 : 0)+",";
            json += diff + "x1" + diff + ":" + this.x1 + ",";
            json += diff + "y1" + diff + ":" + this.y1 + ",";
            json += diff + "z1" + diff + ":" + this.z1 + ",";
            json += diff + "x2" + diff + ":" + this.x2 + ",";
            json += diff + "y2" + diff + ":" + this.y2 + ",";
            json += diff + "z2" + diff + ":" + this.z2 + ",";
            json += diff + "color" + diff + ":" +diff+ this.color+diff + ",";
            json += diff + "layer" + diff + ":" + diff + this.layer + diff+"}";
            return json;
        }
    }
}
