using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 기상정보
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string data = "";
        public static string data2 = "";

        private void button1_Click(object sender, EventArgs e)
        {
            string strRead = "http://www.weather.go.kr/weather/forecast/mid-term-rss3.jsp?stnId=108";
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(strRead);

            XmlNodeList Nodes = XmlDoc.SelectNodes("//channel/item/description/body/location[2]");
            foreach (XmlNode Node in Nodes)
            {
                XmlNode city = Node.SelectSingleNode("city");
                richTextBox1.Text += city.InnerText.ToString() + Environment.NewLine;
            }

            XmlNodeList Nodes2 = XmlDoc.SelectNodes("//channel/item/description/body/location[2]/data");
            foreach (XmlNode Node in Nodes2)
            {
                XmlNode tmEf = Node.SelectSingleNode("tmEf");
                XmlNode wf = Node.SelectSingleNode("wf");
                XmlNode tmn= Node.SelectSingleNode("tmn");
                XmlNode tmx = Node.SelectSingleNode("tmx");
                data += tmEf.InnerText + wf.InnerText + "최저기온" + tmn.InnerText + "도 / " + "최고기온" + tmx.InnerText + "도" + Environment.NewLine;
            }
            richTextBox1.Text += data.ToString();


            //브리핑
            XmlNodeList Nodes3 = XmlDoc.SelectNodes("//channel/item/description/header");
            foreach (XmlNode Node in Nodes3)
            {
                XmlNode wf = Node.SelectSingleNode("wf");
                data2 += Environment.NewLine + wf.InnerText;
            }

            data2 = data2.Replace("<br />", "");
            data2 = data2.Replace("*", "");
            richTextBox1.Text += data2.ToString() + Environment.NewLine + Environment.NewLine;

            //오늘 날씨
            string strRead2 = "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=2824561100";
            XmlDocument XmlDoc2 = new XmlDocument();
            XmlDoc2.Load(strRead2);

            XmlNodeList Nodes4 = XmlDoc2.SelectNodes("//channel");
            foreach (XmlNode Node in Nodes4)
            {
                XmlNode pubDate = Node.SelectSingleNode("pubDate");
                richTextBox1.Text += "측정시간 : "+ pubDate.InnerText.ToString() + Environment.NewLine;
            }

            int count = 0;
            XmlNodeList Nodes5 = XmlDoc2.SelectNodes("//channel/item/description/body/data");
            foreach (XmlNode Node in Nodes5)
            {
                if (count < 3)
                {
                    XmlNode hour = Node.SelectSingleNode("hour");
                    XmlNode temp = Node.SelectSingleNode("temp");
                    XmlNode wfKor = Node.SelectSingleNode("wfKor");
                    richTextBox1.Text += hour.InnerText + "시 " + temp.InnerText + "도 / " + "상태 : " + wfKor.InnerText + Environment.NewLine;
                }
                else
                {
                    break;
                }

                count++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
