using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HET
{
    public partial class Hetman : Form
    {
        public Hetman()
        {
            
            InitializeComponent();
            textBoxDuzeNadruki.Text = "0";
            textBoxMaleNadruki.Text = "0";
            textBoxRabat.Text = "0";

        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
          
        
            int stiches;
            float hafty;
            float glowice;
            int czas;
            int minutes;

            try
            {
                if (textBoxHafty.Text == "" || textBoxStiches.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Żadne pole nie może być puste!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (!float.TryParse(textBoxHafty.Text, out hafty) || !int.TryParse(textBoxStiches.Text, out stiches))
                {
                    MessageBox.Show("Pola muszą zawierać liczby!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch
            {
                
            }

      
            stiches = int.Parse(textBoxStiches.Text);
            hafty = float.Parse(textBoxHafty.Text);
            glowice = float.Parse(comboBox1.Text);

           

            czas = (stiches / 500);

            if (czas <= 15)
            {
                czas = 15;
            }
            float rzuty;
            rzuty = (hafty / glowice);

            



            double rzutyrounding = Math.Ceiling(rzuty);
            
            czas = (int) (czas *rzutyrounding);

            int hours = czas / 60;
            minutes = czas - (hours*60);
            labelHours.Text = czas.ToString() + " minut/y " + "( " + hours.ToString() + "h " + minutes.ToString() + "m )";
            labelrzuty.Text = rzutyrounding.ToString();

            
        }

        //Obsługa zamówień


        
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
           
        }

      
        //Zmienne globalne
        double kosztyNadrukuRabat;
        double kosztyNadrukuBezRabat;
        int maleNadruki;
        int duzeNadruki;
        int sumaNadrukow;
        double rabat;
        
        private void KosztyNadruku()
        {



            rabat = 0;
            duzeNadruki = int.Parse(textBoxDuzeNadruki.Text);
            maleNadruki = int.Parse(textBoxMaleNadruki.Text);
            //Liczy sumę nadruków
            sumaNadrukow = maleNadruki + duzeNadruki;
            rabat = (double.Parse(textBoxRabat.Text)/100);

            textBox4.Text = rabat.ToString();

            int indexComboBox = comboBoxTechnikaNadruku.SelectedIndex;
            
            //Liczy koszt nadruku zależnie od wybranej techniki z comboboxa
            if (sumaNadrukow <= 50)
            {
                if (indexComboBox == 5 || indexComboBox == 6 || indexComboBox == 7 || indexComboBox == 8)
                {
                    MessageBox.Show("Metoda sitodruku przy takim nakładzie jest mało opłacalna.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                 kosztyNadrukuRabat = (double.Parse(listView1_49.Items[indexComboBox].Text) * sumaNadrukow) * (1.0 - rabat);
                kosztyNadrukuBezRabat = (double.Parse(listView1_49.Items[indexComboBox].Text)) * sumaNadrukow;
            }
            //problem w else if
            else if (sumaNadrukow > 50 && sumaNadrukow <= 99)
            {
                 kosztyNadrukuRabat = (double.Parse(listView51_99.Items[indexComboBox].Text) * sumaNadrukow) * (1.0 - rabat);
                kosztyNadrukuBezRabat = double.Parse(listView51_99.Items[indexComboBox].Text) * sumaNadrukow;
            }
            else if (sumaNadrukow >= 100 && sumaNadrukow <= 499)
            {
                  kosztyNadrukuRabat = (double.Parse(listView100_499.Items[indexComboBox].Text) * sumaNadrukow) * (1.0 - rabat);
                kosztyNadrukuBezRabat = (double.Parse(listView100_499.Items[indexComboBox].Text)) * sumaNadrukow;
            }
            kosztyNadrukuRabat = Math.Round(kosztyNadrukuRabat, 2 ,MidpointRounding.AwayFromZero);
           
            
            //Udzielony rabat w PLN
            double udzielonyRabatPLN = kosztyNadrukuBezRabat - kosztyNadrukuRabat;
            
            udzielonyRabatPLN = Math.Round(udzielonyRabatPLN, 2, MidpointRounding.AwayFromZero);

            labelKosztyNadruku.Text = kosztyNadrukuRabat.ToString() + " zł, " + " udzielono " + rabat*100 + "% rabatu - " + udzielonyRabatPLN + "zł";
          
        }





        private void button1_Click(object sender, EventArgs e)
        {
            //Walidacja pustego pola
            if (textBoxMaleNadruki.Text == "" || textBoxDuzeNadruki.Text == "" || textBoxRabat.Text == "")
            {
                MessageBox.Show("Pole nie może być puste", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            KosztyNadruku();
        }

        private void Flexy()
        {
            
            if (comboBoxTechnikaNadruku.SelectedIndex == 0)
            {
             //   KosztyNadruku = double.Parse(listView1000_2500.Items[)
            }
            
        }

        private void informacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Prędkość obrotu głowicy został ustalony na 500 obrotów na minutę.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
