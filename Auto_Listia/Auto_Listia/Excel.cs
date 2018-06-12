using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Excel = Microsoft.Office.Interop.Excel;
using _Aplication = Microsoft.Office.Interop.Excel.Application;
using _Wookbook = Microsoft.Office.Interop.Excel.Workbook;
using _Wooksheet = Microsoft.Office.Interop.Excel.Worksheet;
using System.IO;
using System.Diagnostics;

namespace Auto_Listia
{
    class Excel
    {
        string path = "";
        _Aplication excel = new _Excel.Application();
        _Wookbook wb;
        _Wooksheet ws;

        public static bool WaitForFileInUse(string filename, TimeSpan timeToWait)
        {
            bool ready = false;
            while (!ready)
                try
                {
                    File.Open(filename, FileMode.Open, FileAccess.Write, FileShare.None).Dispose();
                    ready = true;
                }
                catch (IOException)
                {
                    if (timeToWait.TotalMilliseconds <= 0)
                        break;
                    int wait = (int)Math.Min(timeToWait.TotalMilliseconds, 1000.0);
                    timeToWait -= TimeSpan.FromMilliseconds(wait);
                    System.Threading.Thread.Sleep(wait); // sleep one second
                }

            return ready;
        }

        public Excel(string path, int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = excel.Worksheets[Sheet];
        }

        public string Readcell(int i,int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2.ToString();
            else
                return "";

        }
        public void WriteToCell(int i, int j, string s)
        {
            i++;
            j++;
            ws.Cells[i, j] = s;
        }
        public void Save()
        {
            wb.Save();
        }
        public void SaveAs(string xpath)
        {
            wb.SaveAs(xpath);
        }

        public void Close()
        {
            wb.Close();
            excel.Quit();
        }

        public bool IsCellEmpty(int i,int j)
        {
            if (Readcell(i,j)!="")
                return false;
            else return true;
        }

        public int EmptyCellatColsLocation(int j)
        {
            int i = 0;
            while(!IsCellEmpty(i,j))
            {
                i++;
            }
            return i;
        }

        public void addListToCell(int i,int j_start,List<string> arr)
        {
            foreach(string s in arr)
            {
                //ws.Cells[i, j_start] = s;
                WriteToCell(i, j_start, s);
                j_start++;
            }
        }
    }
}
