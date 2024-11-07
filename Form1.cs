using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangle
{
    public partial class Form1 : Form
    {
        Button btn;
        TextBox txtA, txtB, txtC;
        ListView listView1;
        Label labelA, labelB, labelC;

        PictureBox pictureBox;
        public Form1()
        {
            this.Height = 500;
            this.Width = 800;
            this.Text = "Töötamine kolmnurgaga";
            //Button - btn
            btn = new Button();
            btn.Text = "Käivitamine";
            btn.Font = new Font("Arial", 18, FontStyle.Italic);
            btn.AutoSize = true;
            btn.Height = 80;
            btn.Width = 50;
            btn.FlatAppearance.BorderSize = 6;
            btn.FlatAppearance.BorderColor = Color.LightSeaGreen;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Location = new Point(450, 20);
            btn.BackColor = Color.Khaki;
            Controls.Add(btn);
            btn.Click += Btn_Click;

            //TextBox - txtA
            txtA = new TextBox();
            txtA.Location = new Point(160, 350);
            txtA.Font = new Font("Arial", 13);
            txtA.Width = 100;
            txtA.Height = 80;
            Controls.Add(txtA);

            //Label - labelA
            labelA = new Label();
            labelA.Text = "Külg A";
            labelA.Font = new Font("Arial", 13, FontStyle.Bold | FontStyle.Underline);
            labelA.ForeColor = Color.RoyalBlue;
            labelA.AutoSize = true;
            labelA.Location = new Point(90, 350);
            Controls.Add(labelA);

            //TextBox - txtB
            txtB = new TextBox();
            txtB.Location = new Point(160, 380);
            txtB.Font = new Font("Arial", 13);
            txtB.Width = 100;
            txtB.Height = 80;
            Controls.Add(txtB);

            //Label - labelB
            labelB = new Label();
            labelB.Text = "Külg B";
            labelB.Font = new Font("Arial", 13, FontStyle.Bold | FontStyle.Underline);
            labelB.ForeColor = Color.RoyalBlue;
            labelB.AutoSize = true;
            labelB.Location = new Point(90, 380);
            Controls.Add(labelB);

            //TextBox - txtC
            txtC = new TextBox();
            txtC.Location = new Point(160, 410);
            txtC.Font = new Font("Arial", 13);
            txtC.Width = 100;
            txtC.Height = 80;
            Controls.Add(txtC);

            //Label - labelC
            labelC = new Label();
            labelC.Text = "Külg C";
            labelC.Font = new Font("Arial", 13, FontStyle.Bold | FontStyle.Underline);
            labelC.ForeColor = Color.RoyalBlue;
            labelC.AutoSize = true;
            labelC.Location = new Point(90, 410);
            Controls.Add(labelC);

            //listView1
            listView1 = new ListView();
            listView1.Location = new Point(10, 10);
            listView1.Font = new Font("Arial", 10);
            listView1.Width = 360;
            listView1.Height = 250;
            listView1.View = View.Details;
            listView1.Columns.Add("Väli", 165);
            listView1.Columns.Add("Väärtused", 190);
            listView1.BackColor = Color.Silver;
            listView1.ForeColor = Color.DarkBlue;
            Controls.Add(listView1);

            //pictureBox
            pictureBox = new PictureBox();
            pictureBox.Location = new Point(450, 150);
            pictureBox.Size = new Size(300, 300);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            double a, b, c;
            a = Convert.ToDouble(txtA.Text);
            b = Convert.ToDouble(txtB.Text);
            c = Convert.ToDouble(txtC.Text);

            Triangle triangle = new Triangle(a, b, c);

            listView1.Items.Clear();
            listView1.Items.Add("Külg A");
            listView1.Items.Add("Külg B");
            listView1.Items.Add("Külg C");
            listView1.Items.Add("Периметр");
            listView1.Items.Add("Piirkond");
            listView1.Items.Add("Kõrgus küljele A");
            listView1.Items.Add("Kõrgus küljele B");
            listView1.Items.Add("Kõrgus küljele C");
            listView1.Items.Add("Kas see on olemas?");
            listView1.Items.Add("Täpsustaja");
            listView1.Items[0].SubItems.Add(triangle.outputA());
            listView1.Items[1].SubItems.Add(triangle.outputB());
            listView1.Items[2].SubItems.Add(triangle.outputC());
            listView1.Items[3].SubItems.Add(Convert.ToString(triangle.Perimeter()));
            listView1.Items[4].SubItems.Add(Convert.ToString(triangle.Surface()));
            listView1.Items[5].SubItems.Add(Convert.ToString(triangle.HeightA()));
            listView1.Items[6].SubItems.Add(Convert.ToString(triangle.HeightB()));
            listView1.Items[7].SubItems.Add(Convert.ToString(triangle.HeightC()));
            if (triangle.ExistTriange)
            {
                listView1.Items[8].SubItems.Add("On olemas");
            }
            else
            {
                listView1.Items[8].SubItems.Add("Ei ole olemas");
            }
            //Метод, позволяющий определить тип треугольника и определив тип,
            //меняющий отображаемую картинку на соответствующую.
            //Название типа треугольника отобразите в значении спецификатора.
            if (triangle.ExistTriange)
            {
                if (triangle.TriangleType == "Võrdkülgne")
                {
                    listView1.Items[9].SubItems.Add("Võrdkülgne");
                    pictureBox.Image = Image.FromFile(@"..\..\Vordkulgne.png");
                }
                else if (triangle.TriangleType == "Võrdhaarne")
                {
                    listView1.Items[9].SubItems.Add("Võrdhaarne");
                    pictureBox.Image = Image.FromFile(@"..\..\Vordhaarne.png");
                }
                else if (triangle.TriangleType == "Skaleeni kolmnurk")
                {
                    listView1.Items[9].SubItems.Add("Skaleeni kolmnurk");
                    pictureBox.Image = Image.FromFile(@"..\..\Skaleeni kolmnurk.jpg");
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            else
            {
                pictureBox.Image = null;
            }

            pictureBox.Invalidate(); // Обновляем PictureBox, чтобы отобразить новую картинку

            // Добавьте еще один метод на кнопку Запуск. Для перехода с первой формы на вторую.
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}