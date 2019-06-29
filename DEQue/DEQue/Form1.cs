using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace DEQue
{
    public partial class Form1 : Form
    {
        Deq d,d1,d2;
        Graphics g;

        class Deq
        {
            public struct Train
            {
                public int number;
                public string content;
                public Train(int num, string con)
                {
                    number = num;
                    content = con;
                }
            }
            public Train [] array;

            public Deq()
            {
                array = new Train[4] { new Train(14, "corn"), new Train(2, "coal"), new Train(8, "sand"), new Train(25, "seed")};
            }
            public Deq(int k)
            {
                array = new Train[k];
            }
            public void Draw(Form1 form, PictureBox pictureBox, Graphics g, bool second)
            {
                if (!second) pictureBox.Refresh();
                int start, center = pictureBox.Width/2;
                start = center - (array.Length / 2)*120;
                foreach (Train tr in array)
                {
                    if (!second)
                    {
                        DrawTrain(tr.number, tr.content, start, 40, g, form);
                    }
                    else DrawTrain(tr.number, tr.content, start,450, g, form);
                    start += 120;
                }
            }
            public void DrawTrain(int num, string con, int start, int y, Graphics g, Form1 form)
            {
                Pen myPen = new Pen(Color.Black, 3);
                Point[] point = { new Point(start, y), new Point(start + 100, y), new Point(start + 100, y+60), new Point(start, y+60) };
                g.FillPolygon(Brushes.MediumOrchid, point);
                g.DrawPolygon(Pens.Black, point);
                g.FillEllipse(Brushes.DimGray, start + 10, y+50, 30, 30);
                g.FillEllipse(Brushes.DimGray, start + 60, y+50, 30, 30);
                g.DrawEllipse(Pens.Black, start + 10, y+50, 30, 30);
                g.DrawEllipse(Pens.Black, start + 60, y+50, 30, 30);
                g.DrawLine(myPen, start, y+45, start - 10, y+45);
                g.DrawLine(myPen, start + 100, y+45, start + 110, y + 45);
                g.FillEllipse(Brushes.Black, start - 15, y+40, 9, 9);
                g.DrawString(con, new Font("Garamond", 12F, FontStyle.Bold, GraphicsUnit.Point, 204),Brushes.Black,start+25,y+30);
                g.DrawString("" +num, new Font("Garamond", 12F, FontStyle.Bold, GraphicsUnit.Point, 204), Brushes.Black, start + 10, y+10);
                g.DrawRectangle(Pens.Black, start + 10, y +10,20,20);

            }
            public int Count
            {
                get
                {
                    return array.Length;
                }
            }
            public bool Empty
            {
                get
                {
                    return array.Length > 0;
                }
            }
            public void PushBack(Train item)
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = item;
            }
            public void PushFront(Train item)
            {
                Array.Resize(ref array, array.Length + 1);
                for (int i = array.Length - 1; i > 0; i--)
                    array[i] = array[i - 1];
                array[0] = item;
            }
        }

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e) // создать список
        {
            d = new Deq();
            d.Draw(this, pictureBox1,g,false);
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button14.Enabled = true;
            button13.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = true;
            button9.Enabled = true;
            button7.Enabled = true;
            button6.Enabled = true;
            button8.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e) //количество узлов
        {
            
            MessageBox.Show($"Количество узлов в деке : {d.array.Length}.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button14_Click(object sender, EventArgs e) //удалить список
        {
            Array.Resize(ref d.array,0);
            d.Draw(this, pictureBox1,g, false);
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button14.Enabled = false;
            button13.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = false;
            button9.Enabled = false;
            button7.Enabled = false;
            button6.Enabled = false;
            button8.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) //вставить в начало
        {
            try
            {
                string con = textBox5.Text;
                int num = Convert.ToInt32(textBox7.Text);
                Deq.Train item = new Deq.Train(num,con);
                d.PushFront(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Номер должен быть целым числом!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            d.Draw(this, pictureBox1, g, false);
        }

        private void button4_Click(object sender, EventArgs e) //вставить в конец
        {
            try
            {
                string con = textBox4.Text;
                int num = Convert.ToInt32(textBox8.Text);
                Deq.Train item = new Deq.Train(num, con);
                d.PushBack(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Номер должен быть целым числом!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            d.Draw(this, pictureBox1, g, false);
        }

        private void button8_Click(object sender, EventArgs e) //Сортировка
        {
            d.array =  d.array.OrderBy(i => i.number).ToArray();
            d.Draw(this, pictureBox1, g, false);
        }

        private void button7_Click(object sender, EventArgs e) //найти элемент
        {
            int num = Convert.ToInt32(textBox3.Text);
            string con = null;
            for (int i = 0; i < d.array.Length; i++)
            {
                if (d.array[i].number == num)
                    con = d.array[i].content;
            }

            if (con != null)
            MessageBox.Show($"Элемент с ключем {num}: {con}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show($"Элемент с ключем {num} не найден.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button9_Click(object sender, EventArgs e) //вставить после k
        {
            string con = textBox6.Text;
            int k = Convert.ToInt32(textBox1.Text);
            int num = Convert.ToInt32(textBox9.Text);
            int key = 0;
            Array.Resize(ref d.array, d.array.Length + 1);
            for (int i = 0; i < d.array.Length; i++)
            {
                if (d.array[i].number == k)
                    key = i + 1;
            }
            if (key != 0)
            {
                for (int i = d.array.Length - 1; i > key; i--)
                    d.array[i] = d.array[i - 1];
                d.array[key] = new Deq.Train(num,con);
            }
            else MessageBox.Show($"Элемент с ключем {k} не найден.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            d.Draw(this, pictureBox1, g, false);
        }

        private void button11_Click(object sender, EventArgs e) // скопировать
        {
            d2 = new Deq(d.array.Length);
            d.array.CopyTo(d2.array,0);
            d.Draw(this, pictureBox1, g, false);
            d2.Draw(this, pictureBox1, g, true);
        }

        private void button10_Click(object sender, EventArgs e) //извлечь элемент
        {
            int num = Convert.ToInt32(textBox2.Text);
            int key = -1;
            for (int i = 0; i < d.array.Length; i++)
            {
                if (d.array[i].number == num)
                    key = i;
            }
            if (key != -1)
            {
                for (int i = key; i < d.array.Length-1; i++)
                    d.array[i] = d.array[i + 1];
                Array.Resize(ref d.array, d.array.Length - 1);
            }
            else MessageBox.Show($"Элемент с ключем {num} не найден.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            d.Draw(this, pictureBox1, g, false);
        }

        private void button12_Click(object sender, EventArgs e) //разбить на 2 
        {
            int size = d.array.Length/2;
            d1 = new Deq(size);
            for (int i = 0; i < size; i++)
            {
                d1.array[i] = d.array[i];
            }
            d2 = new Deq(d.array.Length - size);
            for (int i = 0; i < d.array.Length-size; i++)
            {
                d2.array[i] = d.array[i+size];
            }
            d = d1;
            d.Draw(this, pictureBox1, g, false);
            d2.Draw(this, pictureBox1, g, true);
        }

        private void button13_Click(object sender, EventArgs e) //объединить в 1
        {
            try
            {
                d1 = new Deq(d.array.Length + d2.array.Length);
                d1.array = d.array.Concat(d2.array).ToArray();
                d = d1;
                d2 = null;
                d.Draw(this, pictureBox1, g, false);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при объединении! Нужно задать второй список", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
