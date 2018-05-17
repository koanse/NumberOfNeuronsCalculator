using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NComb
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dgvN.Rows.Add(new object[] { "Количество нейронов в коре головного мозга", 10000000000 });
            dgvN.Rows.Add(new object[] { "Количество нейронов, взаимодействующих с корой головного мозга", 4000000000 });
            dgvN.Rows.Add(new object[] { "Доля нервных волокон в молекулярном слое", 0.35 });
            dgvN.Rows.Add(new object[] { "Доля нервных волокон в наружнем зернистом слое", 0.05 });
            dgvN.Rows.Add(new object[] { "Доля нервных волокон в пирамидальном слое", 0.05 });
            dgvN.Rows.Add(new object[] { "Доля нервных волокон во внешнем зернистом слое", 0.25 });
            dgvN.Rows.Add(new object[] { "Доля нервных волокон в ганглионарном слое", 0.25 });
            dgvN.Rows.Add(new object[] { "Доля нервных волокон в слое полиморфных клеток", 0.15 });
            dgvN.Rows.Add(new object[] { "Количество дендритов у мелких пирамидальных клеток", 20 });
            dgvN.Rows.Add(new object[] { "Количество дендритов у средних пирамидальных клеток", 30 });
            dgvN.Rows.Add(new object[] { "Количество дендритов у крупных пирамидальных клеток", 30 });
            dgvN.Rows.Add(new object[] { "Количество дендритов у возбуждающих клеток", 15 });
            dgvN.Rows.Add(new object[] { "Количество дендритов у тормозящих клеток", 15 });
            dgvN.Rows.Add(new object[] { "Количество дендритов у клеток Беца", 20 });
            dgvL.Rows.Add(new object[] { "Молекулярный", 1, 0, 0, 1, 10, 0 });
            dgvL.Rows.Add(new object[] { "Наружний зернистый", 10, 0, 0, 6, 5, 0 });
            dgvL.Rows.Add(new object[] { "Пирамидальный", 0, 11, 0, 0, 4, 0 });
            dgvL.Rows.Add(new object[] { "Внутренний зернистый", 0, 1, 0, 15, 0, 0 });
            dgvL.Rows.Add(new object[] { "Ганглионарный", 0, 0, 16, 1, 4, 7 });
            dgvL.Rows.Add(new object[] { "Полиморфных клеток", 12, 0, 0, 0, 7, 0 });
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void calcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                double N = double.Parse(dgvN.Rows[0].Cells[1].Value.ToString());
                double N0 = double.Parse(dgvN.Rows[1].Cells[1].Value.ToString());
                double[] PV = new double[6];
                for (int i = 0; i < 6; i++)
                    PV[i] = double.Parse(dgvN.Rows[2 + i].Cells[1].Value.ToString());
                double[] d = new double[6];
                for (int i = 0; i < 6; i++)
                    d[i] = double.Parse(dgvN.Rows[8 + i].Cells[1].Value.ToString());
                double[,] NL = new double[6, 6];
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 6; j++)
                        NL[i, j] = double.Parse(dgvL.Rows[i].Cells[j + 1].Value.ToString());
                double[,] mK;
                double K = Comb.LogK(N, N0, NL, PV, d, out mK);
                string s = "Распределение логарифмов комбинаций нервных клеток по типам и слоям";
                s += "<table border = 1><tr><td><td>Мелкие пирамидальные<td>Средние пирамидальные<td>Крупные пирамидальные<td>Возбуждающие<td>Тормозящие<td>Клетки Беца";
                string[] arrL = new string[] { "Молекулярный", "Наружний зернистый", "Пирамидальный",
                    "Внутренний зернистый", "Ганглиарный", "Полиморфных клеток" };
                for (int i = 0; i < 6; i++)
                {
                    s += "<tr><td>" + arrL[i];
                    for (int j = 0; j < 6; j++)
                        s += string.Format("<td>{0:F3}", mK[i, j]);
                }
                s += "</table>";
                s += "Общее количество комбинаций нервных клеток в коре головного мозга:<br>";
                s += string.Format("K = 2<sup>{0:F3}</sup>", K);
                wb.DocumentText = s;
                tabPage2.Select();
            }
            catch
            {
                MessageBox.Show("Ошибка в исходных данных");
            }
        }
    }
}