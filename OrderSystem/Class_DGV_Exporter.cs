using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OrderSystem
{
    /// <summary>
    /// 该类用于实现对DataGridView的导出Excel和画图表的操作
    /// </summary>
    public class DGV_Exporter
    {
        /// <summary>
        /// 将DataGridView内容导出为Excel表格
        /// </summary>
        /// <param name="dataGridView1">需导出的Excel</param>
        /// <param name="sheetName">sheetName</param>
        public static void exportToExcel(DataGridView dataGridView1, string sheetName)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = @"Excel|*.xls|WPS_Excel|*.xlsx|所有文件|*.*";
            saveFileDialog1.InitialDirectory = @"D:";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            dataGridView1.AllowUserToAddRows = false;
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow rowHead = sheet.CreateRow(0);

            //填写表头
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                rowHead.CreateCell(i, CellType.String).SetCellValue(dataGridView1.Columns[i].HeaderText.ToString());
            }

            //填写内容
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);

                //填入数据
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    row.CreateCell(j, CellType.String).SetCellValue(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
            }

            using (FileStream stream = File.OpenWrite(saveFileDialog1.FileName))
            {
                workbook.Write(stream);
                stream.Close();
            }
            MessageBox.Show("导出数据成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GC.Collect();
        }


        /// <summary>
        /// 用于画菜品汇总的饼状图
        /// </summary>
        /// <param name="dataTable">图表的数据</param>
        /// <param name="chart1">chart控件</param>
        /// <param name="chartTitle">图表标题</param>
        public static void drawChart_Pie(DataTable dataTable, Chart chart1, string chartTitle)
        {
            //清楚原有的图表
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add(chartTitle);

            Series series = new Series();
            // 设置饼图属性
            series["PieLabelStyle"] = "Disabled"; // 禁用饼图标签
            series["PieLineColor"] = "Black";
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = false;

            //
            foreach (DataRow row in dataTable.Rows)
            {
                string dishName = row[0].ToString(); 
                int totalPrice = int.Parse(row[2].ToString()); 
                series.Points.AddXY(dishName, totalPrice);
            }

            chart1.Series.Add(series);

        }


        /// <summary>
        /// 用于画柱状图（员工销售额、每日销售额）
        /// </summary>
        /// <param name="dataTable">图表数据</param>
        /// <param name="chart1">chart控件</param>
        /// <param name="chartTitle">图表标题</param>
        public static void drawChart_Bar(DataTable dataTable, Chart chart1, string chartTitle)
        {
            //清楚原有图表
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add(chartTitle);

            Series series = new Series("销售额");

            foreach (DataRow row in dataTable.Rows)
            {
                string dishName = row[0].ToString(); 
                int totalPrice = int.Parse(row[2].ToString()); 
                series.Points.AddXY(dishName, totalPrice);
            }

            chart1.Series.Add(series);

        }
    }
}
